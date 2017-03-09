using Heeelp.Core.Command.Expertise;
using Heeelp.Core.Domain;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Text;
using Heeelp.Core.Common;
using System.Web.Script.Serialization;
using System.Web.Http.Cors;
using Heeelp.Core.Logging;
using Heeelp.Core.Storage;
using System.Web.Http.Description;

namespace Heeelp.Core.WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ExpertiseController : ApiController
    {
        private readonly ICommandBus bus;
        private readonly IExpertiseDao _ExpertiseDao;
        private readonly IUserDao _UserDao;
        private IStorage _storage;
        private string _containerName;
        private readonly IFileTempDao _FileTemp;
        private long fileId;
        private List<int> _listFileTemp;
        private List<FIleServer> fs;

        //public ValuesController() : base()
        //{
        //    //Mapper.CreateMap<OrderSeat, AssignSeat>();

        //    this.bus = new CommandBus(new MessageSender(null, "", ""),new  JsonTextSerializer());
        //}



        public ExpertiseController(ICommandBus bus, IFileTempDao fileTemp, IExpertiseDao _expertiseDao, IUserDao _userDao) : base()
        {

            this.bus = bus;
            this._ExpertiseDao = _expertiseDao;
            this._FileTemp = fileTemp;
            this._listFileTemp = new List<int>();
            this._UserDao = _userDao;
            _storage = new Storage.StorageClient(CustomConfiguration.Storage);
            _containerName = CustomConfiguration.ContainerName;
        }


        /// <summary>
        /// Send Expertise
        /// </summary>
        /// <param name="jsonData">json Expertise</param>
        /// <remarks>Send Expertise</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(Expertise))]
        [HttpPost]
        public async Task<HttpResponseMessage> PostFile(string jsonData)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = serializer.Deserialize<dynamic>(jsonData);

            try
            {

                string Name = json["Name"];
                string Description = json["Description"];
                string ExpertiseFather = json["ExpertiseFather"];
                int? expertiseFatherId = null;
                if (!string.IsNullOrEmpty(ExpertiseFather))
                    expertiseFatherId = int.Parse(ExpertiseFather);
                string ApprovalStatusId = json["ApprovalStatusId"];
                string CreatedBy = json["CreatedBy"];
                string ApprovedBy = json["ApprovedBy"];


                //recebo as imagens se tiver
                var provider = new MultipartMemoryStreamProvider();
                if (Request.Content.IsMimeMultipartContent())
                {

                    try
                    {
                        await Request.Content.ReadAsMultipartAsync(provider);
                        fs = new List<FIleServer>();
                        int count = 0;


                        foreach (var f in provider.Contents)
                        {
                            var fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");

                            string mimeType = new MimeType().GetMimeType(fileName);

                            var stream = (HttpContext.Current.Request.Files[count]).InputStream;

                            byte[] bytesInStream = new byte[stream.Length];
                            stream.Read(bytesInStream, 0, bytesInStream.Length);

                            Domain.ReadModel.FileTemp ft = new Domain.ReadModel.FileTemp();
                            ft.FileIntegrationCode = Guid.NewGuid();
                            ft.FilePath = _storage.UploadFile(_containerName, ft.FileIntegrationCode.ToString(), mimeType, bytesInStream);
                            ft.OriginalName = f.Headers.ContentDisposition.FileName.Replace("\"", "");
                            try
                            {
                                using (Image img = Image.FromStream(stream: stream,
                               useEmbeddedColorManagement: false,
                               validateImageData: false))
                                {
                                    ft.Width = img.PhysicalDimension.Width.ToString();
                                    ft.Height = img.PhysicalDimension.Height.ToString();
                                }
                                count++;
                            }
                            catch (Exception ex)
                            {
                                LogManager.Error("Erro ao recuperar dimensoes da imagen", ex);
                            }


                            //salvo na tablea temporaria


                            _listFileTemp.Add(_FileTemp.SaveFileTemp(ft));


                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.Info("Expertise sem Imagem");

                    }


                }


                var user = this._UserDao.Get(new Domain.ReadModel.User { UserId = int.Parse(CreatedBy) });



                var expertise = new AddExpertiseCommand()
                {
                    Name = Name,
                    DefaultDescription = Description,
                    ApprovedDate = DateTime.UtcNow,
                    CreatedDateUTC = DateTime.UtcNow,
                    ExpertiseFatherId = expertiseFatherId,
                    ApprovalStatusId = byte.Parse(ApprovalStatusId),
                    CreatedBy = int.Parse(CreatedBy),
                    ApprovedBy = int.Parse(ApprovedBy),
                    Active = true,
                    listFileTemp = _listFileTemp,
                    PersonId = user.PersonId
                };

                this.bus.Send(expertise);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                LogManager.Error("Erro ao cadastrar Expertise: ", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [HttpGet]
        [Route("api/Expertise/ListExpertises/")]
        public HttpResponseMessage ListExpertises()
        {
            IEnumerable<ExpertiseListDTO> expertiseList = _ExpertiseDao.ListExpertises();

            return Request.CreateResponse(HttpStatusCode.OK, expertiseList);
        }

        [HttpPost]
        [HttpGet]
        [Route("api/Expertise/ListMainExpertises/")]
        public HttpResponseMessage ListMainExpertises()
        {
            IEnumerable<ExpertiseListDTO> expertiseList = _ExpertiseDao.ListMainExpertises();

            return Request.CreateResponse(HttpStatusCode.OK, expertiseList);
        }

        [HttpPost]
        [HttpGet]
        [Route("api/Expertise/ListSubExpertises/{ExpertiseFatherId}")]
        public HttpResponseMessage ListSubExpertises(int ExpertiseFatherId)
        {
            IEnumerable<ExpertiseListDTO> expertiseList = _ExpertiseDao.ListSubExpertises(ExpertiseFatherId);

            return Request.CreateResponse(HttpStatusCode.OK, expertiseList);
        }

        [HttpPost]
        [HttpGet]
        [Route("api/Expertise/GetExpertiseWithImages/{ExpertiseId}")]
        public HttpResponseMessage GetExpertiseWithImages(int ExpertiseId)
        {
            IEnumerable<ExpertiseListDTO> expertiseList = _ExpertiseDao.ListSubExpertises(ExpertiseId);

            return Request.CreateResponse(HttpStatusCode.OK, expertiseList);
        }

        [HttpGet]
        [HttpPost]
        [Route("api/Expertise/GetExpertise/{id}")]
        public HttpResponseMessage GetExpertise(int id)
        {
            ExpertisePhotoDTO expertise = _ExpertiseDao.GetExpetisePhoto(id);

            if (expertise != null && expertise.ImageListId.Count > 0)
            {
                var _client = new HttpClient();
                _client.BaseAddress = new Uri(CustomConfiguration.WebApiFileServer);
                HttpResponseMessage response = _client.PostAsJsonAsync("/api/FileServer/GetFileList", expertise.ImageListId).Result;

                if (response.IsSuccessStatusCode)
                {
                    dynamic res = response.Content.ReadAsAsync<dynamic>().Result;
                    expertise.fileUrl = new List<FIleUrlDTO>();
                    foreach (var item in res)
                    {
                        expertise.fileUrl.Add(new FIleUrlDTO() { FileId = item.FileId, URL = item.URL });
                    }
                }
                else
                {
                    LogManager.Error("Erro GetExpertise" + response.Content.ToString());
                }



            }

            return Request.CreateResponse(HttpStatusCode.OK, expertise);

        }

        [HttpGet]
        [HttpPost]
        [Route("api/Expertise/GetExpertiseClassified/{id}")]
        public HttpResponseMessage GetExpertiseClassified(int id)
        {
            ExpertiseDTO expertise = _ExpertiseDao.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, expertise);
        }
    }
}
