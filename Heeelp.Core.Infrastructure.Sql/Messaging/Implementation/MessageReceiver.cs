// ==============================================================================================================
// Microsoft patterns & practices
// CQRS Journey project
// ==============================================================================================================
// ©2012 Microsoft. All rights reserved. Certain content used with permission from contributors
// http://go.microsoft.com/fwlink/p/?LinkID=258575
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.
// ==============================================================================================================

namespace Heeelp.Core.Infrastructure.Sql.Messaging.Implementation
{
    using Logging;
    using System;
    using System.Data;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;
    public class MessageReceiver : IMessageReceiver, IDisposable
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly string name;
        private readonly string readQuery;
        private readonly string deleteQuery;
        private readonly string insertErrorQuery;
        private readonly string insertDescriptionErrorQuery;
        private readonly string selectIntegrationIdQuery;
        private readonly string updateErrorQuery;
        private readonly TimeSpan pollDelay;
        private readonly object lockObject = new object();
        private CancellationTokenSource cancellationSource;

        public MessageReceiver(IDbConnectionFactory connectionFactory, string name, string tableName)
            : this(connectionFactory, name, tableName, TimeSpan.FromMilliseconds(2000))
        {
        }

        public MessageReceiver(IDbConnectionFactory connectionFactory, string name, string tableName, TimeSpan pollDelay)
        {
            this.connectionFactory = connectionFactory;
            this.name = name;
            this.pollDelay = pollDelay;

            this.readQuery =
                string.Format(
                    CultureInfo.InvariantCulture,
                    @"SELECT TOP (1) 
                    {0}.[Id] AS [Id], 
                    {0}.[Body] AS [Body], 
                    {0}.[DeliveryDate] AS [DeliveryDate],
                    {0}.[CorrelationId] AS [CorrelationId]
                    FROM {0} WITH (UPDLOCK, READPAST)
                    WHERE ({0}.[DeliveryDate] IS NULL) OR ({0}.[DeliveryDate] <= @CurrentDate)
                    ORDER BY {0}.[Id] ASC",
                    tableName);
            this.deleteQuery =
                string.Format(
                   CultureInfo.InvariantCulture,
                   "DELETE FROM {0} WHERE Id = @Id",
                   tableName);
            switch (tableName)
            {
                case "SqlBus.Commands":
                    insertErrorQuery = string.Format(
                  CultureInfo.InvariantCulture,
                  "INSERT INTO SqlBus.CommandsError VALUES (@Body,@CurrentDate,@Status,@CorrelationId, @IntegrationId, @Attempts)");
                    selectIntegrationIdQuery = string.Format(
                  CultureInfo.InvariantCulture, "SELECT IntegrationId, Attempts from SqlBus.CommandsError where IntegrationId = @IntegrationId");
                    updateErrorQuery = string.Format(
                  CultureInfo.InvariantCulture,
                  "UPDATE SqlBus.CommandsError  SET Status = @Status  , Attempts =  @Attempts where IntegrationId = @IntegrationId");
                    insertDescriptionErrorQuery = string.Format(
                        CultureInfo.InvariantCulture, "INSERT INTO SqlBus.CommandsErrorDescription VALUES (@IntegrationId, @ErrorDescription, @DateRegister, @ExceptionCode)");
                    break;
                case "SqlBus.Events":
                    insertErrorQuery = string.Format(
                  CultureInfo.InvariantCulture,
                  "INSERT INTO SqlBus.EventsError VALUES (@Body,@CurrentDate,@Status,@CorrelationId, @IntegrationId, @Attempts)");
                    selectIntegrationIdQuery = string.Format(
                  CultureInfo.InvariantCulture, "SELECT IntegrationId, Attempts from SqlBus.EventsError where IntegrationId = @IntegrationId");
                    updateErrorQuery = string.Format(
                  CultureInfo.InvariantCulture,
                  "UPDATE SqlBus.EventsError  SET Status = @Status  , Attempts =  @Attempts where IntegrationId = @IntegrationId");
                    insertDescriptionErrorQuery = string.Format(
                    CultureInfo.InvariantCulture, "INSERT INTO SqlBus.EventsErrorDescription VALUES (@IntegrationId, @ErrorDescription, @DateRegister, @ExceptionCode)");
                    break;
            }

        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived = (sender, args) => { };

        public void Start()
        {
            lock (this.lockObject)
            {
                if (this.cancellationSource == null)
                {
                    this.cancellationSource = new CancellationTokenSource();
                    Task.Factory.StartNew(
                        () => this.ReceiveMessages(this.cancellationSource.Token),
                        this.cancellationSource.Token,
                        TaskCreationOptions.LongRunning,
                        TaskScheduler.Current);
                }
            }
        }

        public void Stop()
        {
            lock (this.lockObject)
            {
                using (this.cancellationSource)
                {
                    if (this.cancellationSource != null)
                    {
                        this.cancellationSource.Cancel();
                        this.cancellationSource = null;
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.Stop();
        }

        MessageReceiver()
        {
            Dispose(false);
        }

        /// <summary>
        /// Receives the messages in an endless loop.
        /// </summary>
        private void ReceiveMessages(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!this.ReceiveMessage())
                {
                    Thread.Sleep(this.pollDelay);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Does not contain user input.")]
        protected bool ReceiveMessage()
        {
            using (var connection = this.connectionFactory.CreateConnection(this.name))
            {
                var currentDate = GetCurrentDate();

                connection.Open();
                using (var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        long messageId = -1;
                        Message message = null;

                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;
                            command.CommandText = this.readQuery;
                            ((SqlCommand)command).Parameters.Add("@CurrentDate", SqlDbType.DateTime).Value = currentDate;

                            using (var reader = command.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    return false;
                                }

                                var body = (string)reader["Body"];
                                var deliveryDateValue = reader["DeliveryDate"];
                                var deliveryDate = deliveryDateValue == DBNull.Value ? (DateTime?)null : new DateTime?((DateTime)deliveryDateValue);
                                var correlationIdValue = reader["CorrelationId"];
                                var correlationId = (string)(correlationIdValue == DBNull.Value ? null : correlationIdValue);

                                message = new Message(body, deliveryDate, correlationId);
                                messageId = (long)reader["Id"];
                            }
                        }

                        try
                        {
                            this.MessageReceived(this, new MessageReceivedEventArgs(message));
                        }
                        catch (Exception ex)
                        {
                            Guid? ID = null;
                            var attempts = 0;
                            var exceptionCode = 0;

                            try
                            {
                                exceptionCode = ((SqlException)ex.InnerException.InnerException).Number;
                            }
                            catch
                            {
                                //Just Trying to convert exception code
                            }

                            try
                            {
                                var serializer = new JavaScriptSerializer();
                                var bodyJson = serializer.Deserialize<dynamic>(message.Body);




                                using (var cnn = this.connectionFactory.CreateConnection(this.name))
                                {
                                    cnn.Open();
                                    using (var cmd = cnn.CreateCommand())
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        cmd.CommandText = this.selectIntegrationIdQuery;
                                        ((SqlCommand)cmd).Parameters.Add("@IntegrationId", SqlDbType.UniqueIdentifier).Value = Guid.Parse(bodyJson["Id"]);


                                        var read = cmd.ExecuteReader();
                                        while (read.Read())
                                        {

                                            ID = Guid.Parse(read["IntegrationId"].ToString());
                                            attempts = int.Parse(read["Attempts"].ToString());
                                        }
                                    }
                                    cnn.Close();
                                }

                                using (var cnn = this.connectionFactory.CreateConnection(this.name))
                                {
                                    cnn.Open();
                                    //consulta

                                    using (var cmd = cnn.CreateCommand())
                                    {
                                        cmd.CommandType = CommandType.Text;
                                        if (ID == null)
                                            cmd.CommandText = this.insertErrorQuery;
                                        else {
                                            cmd.CommandText = this.updateErrorQuery;
                                            attempts++;
                                        }
                                        ((SqlCommand)cmd).Parameters.Add("@Body", SqlDbType.NVarChar).Value = message.Body;
                                        ((SqlCommand)cmd).Parameters.Add("@CurrentDate", SqlDbType.DateTime).Value = currentDate;
                                        ((SqlCommand)cmd).Parameters.Add("@Status", SqlDbType.Int).Value = 1;
                                        ((SqlCommand)cmd).Parameters.Add("@Attempts", SqlDbType.Int).Value = attempts;
                                        ((SqlCommand)cmd).Parameters.Add("@IntegrationId", SqlDbType.UniqueIdentifier).Value = Guid.Parse(bodyJson["Id"]);

                                        if (!string.IsNullOrEmpty(message.CorrelationId))
                                        {
                                            ((SqlCommand)cmd).Parameters.Add("@CorrelationId", SqlDbType.NVarChar).Value = message.CorrelationId;
                                        }
                                        else
                                        {
                                            ((SqlCommand)cmd).Parameters.Add("@CorrelationId", SqlDbType.NVarChar).Value = DBNull.Value;
                                        }

                                        cmd.ExecuteNonQuery();
                                    }


                                    using (var cmd = cnn.CreateCommand())
                                    {
                                        cmd.CommandType = CommandType.Text;
                                            cmd.CommandText = this.insertDescriptionErrorQuery;
                                        
                                        ((SqlCommand)cmd).Parameters.Add("@IntegrationId", SqlDbType.UniqueIdentifier).Value = Guid.Parse(bodyJson["Id"]);
                                        ((SqlCommand)cmd).Parameters.Add("@DateRegister", SqlDbType.DateTime).Value = currentDate;
                                        ((SqlCommand)cmd).Parameters.Add("@ErrorDescription", SqlDbType.NVarChar).Value = ex.ToString();
                                        ((SqlCommand)cmd).Parameters.Add("@ExceptionCode", SqlDbType.BigInt).Value = exceptionCode;
                                       
                                        cmd.ExecuteNonQuery();
                                    }
                                    cnn.Close();
                                }


                            }
                            catch (Exception e)
                            {
                                LogManager.Error(string.Format("Error Save commands or events: {0}", message.Body, e));
                            }
                        }

                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;
                            command.CommandText = this.deleteQuery;
                            ((SqlCommand)command).Parameters.Add("@Id", SqlDbType.BigInt).Value = messageId;

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)                                
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception e)
                        {
                            LogManager.Error(string.Format("Error Rollback commands or events", e));
                        }
                        throw;
                    }
                }
            }


            return true;
        }

        protected virtual DateTime GetCurrentDate()
        {
            return DateTime.UtcNow;
        }
    }
}
