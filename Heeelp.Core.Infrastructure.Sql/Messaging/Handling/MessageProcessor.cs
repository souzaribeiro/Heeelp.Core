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

namespace Heeelp.Core.Infrastructure.Sql.Messaging.Handling
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Heeelp.Core.Infrastructure.Serialization;
    using Heeelp.Core.Infrastructure.Sql.Messaging;
    using System.Linq;
    using Logging;
    using System.Data.SqlClient;
    using System.Data.Entity.Infrastructure;/// <summary>
                                            /// Provides basic common processing code for components that handle 
                                            /// incoming messages from a receiver.
                                            /// </summary>
    public abstract class MessageProcessor : IProcessor, IDisposable
    {
        private readonly IMessageReceiver receiver;
        private readonly ITextSerializer serializer;
        private readonly object lockObject = new object();
        private bool disposed;
        private bool started = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageProcessor"/> class.
        /// </summary>
        protected MessageProcessor(IMessageReceiver receiver, ITextSerializer serializer)
        {
            this.receiver = receiver;
            this.serializer = serializer;
        }

        /// <summary>
        /// Starts the listener.
        /// </summary>
        public virtual void Start()
        {
            ThrowIfDisposed();
            lock (this.lockObject)
            {
                if (!this.started)
                {
                    this.receiver.MessageReceived += OnMessageReceived;
                    this.receiver.Start();
                    this.started = true;
                }
            }
        }

        /// <summary>
        /// Stops the listener.
        /// </summary>
        public virtual void Stop()
        {
            lock (this.lockObject)
            {
                if (this.started)
                {
                    this.receiver.Stop();
                    this.receiver.MessageReceived -= OnMessageReceived;
                    this.started = false;
                }
            }
        }

        /// <summary>
        /// Disposes the resources used by the processor.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void ProcessMessage(object payload, string correlationId);

        /// <summary>
        /// Disposes the resources used by the processor.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Stop();
                    this.disposed = true;

                    using (this.receiver as IDisposable)
                    {
                        // Dispose receiver if it's disposable.
                    }
                }
            }
        }

        MessageProcessor()
        {
            Dispose(false);
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            Trace.WriteLine(new string('-', 100));

            try
            {
                var body = Deserialize(args.Message.Body);

                TracePayload(body);
                Trace.WriteLine("");

                ProcessMessage(body, args.Message.CorrelationId);

                Trace.WriteLine(new string('-', 100));
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                LogManager.Error("Error Robo:", ex);
                Trace.TraceError("{0}\n\n{1}\n\nValidation errors:{1}\n\n{2}\n\n{3}", ex, Environment.NewLine,
                    ex.EntityValidationErrors.Select(e => string.Join(Environment.NewLine, e.ValidationErrors.Select(v => string.Format("{0} - {1}", v.PropertyName, v.ErrorMessage)))),
                    ex.EntityValidationErrors.Select(y => y.ValidationErrors).First().First().ErrorMessage

                    );
                throw ex;

            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0) // Assume the interesting stuff is in the first error
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 547: // Foreign Key violation
                            throw new InvalidOperationException("Some helpful description", ex);
                            break;
                        case 2601: // Primary key violation
                            throw new InvalidOperationException("Some other helpful description", ex);
                            break;
                        default:
                            throw ex;
                    }
                }

            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                // NOTE: we catch ANY exceptions as this is for local 
                // development/debugging. The Windows Azure implementation 
                // supports retries and dead-lettering, which would 
                // be totally overkill for this alternative debug-only implementation.
                LogManager.Error("Error Robo:", ex);
                Trace.TraceError("An exception happened while processing message through handler/s:\r\n{0}", ex);
                Trace.TraceWarning("Error will be ignored and message receiving will continue.");

                throw ex;
            }
        }

        protected object Deserialize(string serializedPayload)
        {
            using (var reader = new StringReader(serializedPayload))
            {
                return this.serializer.Deserialize(reader);
            }
        }

        protected string Serialize(object payload)
        {
            using (var writer = new StringWriter())
            {
                this.serializer.Serialize(writer, payload);
                return writer.ToString();
            }
        }

        private void ThrowIfDisposed()
        {
            if (this.disposed)
                throw new ObjectDisposedException("MessageProcessor");
        }


        [Conditional("TRACE")]
        private void TracePayload(object payload)
        {
            Trace.WriteLine(this.Serialize(payload));
        }
    }
}
