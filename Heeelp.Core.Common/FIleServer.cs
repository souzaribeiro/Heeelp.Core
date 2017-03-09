using Heeelp.Core.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Heeelp.Core.Common
{
    public class FIleServer
    {
        private HttpResponseMessage response;

        public FIleServer()
        {

        }
        public Int64 SendFile(FIleServer fs)
        {
            Int64 ret = 0;
            var _client = new HttpClient();
            dynamic file = new JObject();
            file.PersonId = fs.PersonId;
            file.FileOriginTypeId = fs.FileOriginTypeId;
            file.OriginalName = fs.OriginalName;

            file.FileUtilization = new JObject();
            file.FileUtilization.Name = fs.OriginalName;

            file.FileProperty = new JObject();
            file.FileProperty.Description = fs.Description;
            file.FileProperty.FriendlyName = fs.FriendlyName;
            file.FileProperty.Alt = fs.Alt;
            file.FileProperty.Width = fs.Width;
            file.FileProperty.Height = fs.Height;

            string uri = string.Format("{0}api/FileServer/UploadFile?jsonData={1}", CustomConfiguration.WebApiFileServer, file);

            //HttpContent c

            //using (var content =
            // new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
            //{
            //    content.Add(streamContent);
            //}
            using (var content = new MultipartFormDataContent())
            {

                // StreamContent s = new StreamContent(streamContent);
                //var fileContent = new ByteArrayContent(byteFile);
                var fileContent = new ByteArrayContent(File.ReadAllBytes(fs.FilePath));
                //var fileContent = new MediaTypeHeaderValue(System.Web.MimeMapping.GetMimeMapping(streamContent));
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fs.OriginalName
                };
                //fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                content.Add(fileContent);
                response = _client.PostAsync(uri, content).Result;

            }

            if (response.IsSuccessStatusCode)
            {
                ret = Convert.ToInt64(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                LogManager.Error(string.Format("Send WebApi FileServer Error:{0} file:{1}", response, file));
            }

            return ret;
        }

        public Int64 SendFilePath(FIleServer fs)
        {
            Int64 ret = 0;
            var _clientSendFilePath = new HttpClient();
            _clientSendFilePath.BaseAddress = new Uri(CustomConfiguration.WebApiFileServer);
            string uri = "api/FileServer/SendFilePath";

            var message = new
            {
                PersonId = fs.PersonId,
                FileOriginTypeId = fs.FileOriginTypeId,
                OriginalName = fs.OriginalName,
                FileUtilizationName = fs.OriginalName,
                FilePropertyDescription = fs.Description,
                FilePropertyFriendlyName = fs.FriendlyName,
                FilePropertyAlt = fs.Alt,
                FilePropertyWidth = fs.Width,
                FilePropertyHeight = fs.Height,
                FilePath = fs.FilePath,
                FileIntegrationCode = fs.FileIntegrationCode,
                FileUtilizationId = fs.FileUtilizationId,
                UploadedBy = fs.UploadedBy
            };

            HttpResponseMessage response = _clientSendFilePath.PostAsJsonAsync(uri, message).Result;


            if (response.IsSuccessStatusCode)
            {
                ret = Convert.ToInt64(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                LogManager.Error(string.Format("Send WebApi FileServer Error:{0} file:{1}", response, message.FileIntegrationCode));
                throw new Exception();
            }

            return ret;
        }

        public FIleServer GetFile(long id)
        {
            FIleServer fs = new FIleServer();

            try
            {  //Call Account, create new user
                var _clientAccount = new HttpClient();
                _clientAccount.BaseAddress = new Uri(CustomConfiguration.WebApiFileServer);
                string uri = "api/FileServer/GetFile";
                HttpResponseMessage response = _clientAccount.PostAsJsonAsync(uri, id).Result;

                if (response.IsSuccessStatusCode)
                {
                    fs = response.Content.ReadAsAsync<FIleServer>().Result;
                }

            }
            catch (Exception ex)
            {
                // write Error Log
                throw ex;
            }
            return fs;
        }

        public string FilePath { get; set; }
        public int FileServerId { get; set; }
        public int FileTempId { get; set; }
        public string OriginalName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }
        public string Alt { get; set; }
        public int PersonId { get; set; }
        public int FileOriginTypeId { get; set; }
        public StreamContent streamContent { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public short ShowOrder { get; set; }
        public bool IsDefault { get; set; }
        public byte[] byteFile { get; set; }
        public Guid FileIntegrationCode { get; set; }


        public long FileId { get; set; }
        public string FileOriginDetails { get; set; }
        public short FileTypeId { get; set; }
        public byte FileUtilizationId { get; set; }
        public long FilePropertyId { get; set; }
        public long FileWorkFlowId { get; set; }
        public short Size { get; set; }
        public int? UploadedBy { get; set; }
        public long? UserSessionId { get; set; }
        public string ServerInstanceId { get; set; }
        public string PhysicalPath { get; set; }
        public string TemporaryURL { get; set; }
        public string FinalURL { get; set; }
        public bool Active { get; set; }


    }
}
