using Heeelp.Core.Command.ExternalModules;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Domain.ReadModel.DTO.ExternalModules.Promotion;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Logging;
using Heeelp.Core.Storage;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace Heeelp.Core.WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/promotion")]
    public class PromotionController : BaseController
    {
        private readonly ICommandBus bus;
        private readonly IPersonDao _PersonDao;
        private long fileId;
        private readonly IPersonAddressDao _PersonAddressDao;

        private readonly IFileTempDao _FileTemp;
        private List<int> _listFileTemp;
        private List<FIleServer> fs;
        private IStorage _storage;
        private string _containerName;


        public PromotionController(ICommandBus bus, IFileTempDao fileTemp, IPersonDao _PersonDao) : base()
        {

            this.bus = bus;
            this._PersonDao = _PersonDao;
            this._listFileTemp = new List<int>();
            this._FileTemp = fileTemp;
            _storage = new Storage.StorageClient(CustomConfiguration.Storage);
            _containerName = CustomConfiguration.ContainerName;
        }


        #region Create Promotions


        /// <summary>
        /// Adiciona Nova Discount Promocao
        /// </summary>
        /// <param name="promotion">promotion</param>
        /// <remarks>Add nova Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(PromotionDiscountInputDTO))]
        [HttpPost]
        [Authorize]
        [Route("AddDiscountPromotion")]
        public HttpResponseMessage AddDiscountPromotion(PromotionDiscountInputDTO promotion)
        {
            Guid promotionIntegrationCode = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                try
                {
                    //Criacao de promocao sera uma operacao critica. Imaginemos o Facilitador na rua usando o APP. Tem de ser tolerante a falha e baseado no processamento de comando

                    Claims claims = new Claims().Values();
                    promotion.UserSystemId = claims.userSystemId;
                    promotion.PotencialDemand = _PersonDao.GetAmountCostumersNearby(promotion.PersonIntegrationCode, promotion.ExpertiseId);

                    AddDiscountPromotionCommand command = new AddDiscountPromotionCommand();
                    command.PromotionMethodPaymentId = promotion.PromotionMethodPaymentId;
                    command.PromotionIntegrationCode = promotion.PromotionIntegrationCode == Guid.Empty ? promotionIntegrationCode : promotion.PromotionIntegrationCode;
                    command.PersonIntegrationCode = promotion.PersonIntegrationCode;
                    command.PersonId = promotion.PersonId;
                    command.ExpertiseId = promotion.ExpertiseId;
                    command.Title = promotion.Title;
                    command.ShortDescription = promotion.ShortDescription;
                    command.StartDateUTC = promotion.StartDateUTC;
                    command.ValidUntilUTC = promotion.ValidUntilUTC;
                    command.RequiredTimeForActivation = (short)promotion.RequiredTimeForActivation;
                    command.PromotionBillingModelId = promotion.PromotionBillingModelId;
                    command.PromotionRecurrenceId = promotion.PromotionRecurrenceId;
                    command.DiscountePercentege = promotion.DiscountePercentege;
                    command.PromotionPaymentTypeId = promotion.PromotionPaymentTypeId;
                    command.NormalPrice = promotion.NormalPrice;
                    command.PromotionalPrice = promotion.PromotionalPrice;
                    command.NumberOfAvailableCoupons = promotion.NumberOfAvailableCoupons;
                    command.CurrencyId = promotion.CurrencyId;
                    command.NeighbourhoodId = promotion.NeighbourhoodId;
                    command.PotencialDemand = promotion.PotencialDemand;
                    command.UserSessionId = promotion.UserSessionId;
                    command.ShortDescription = promotion.ShortDescription;
                    command.UserSystemId = promotion.UserSystemId;
                    command.PromotionMethodPaymentId = promotion.PromotionMethodPaymentId;

                    bus.Send(command);

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }


            }


            return Request.CreateResponse(HttpStatusCode.OK, promotionIntegrationCode);
        }

        /// <summary>
        /// Adiciona Nova Award Promocao
        /// </summary>
        /// <param name="promotion">promotion</param>
        /// <remarks>Add nova Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(PromotionAwardInputDTO))]
        [HttpPost]
        [Authorize]
        [Route("AddAwardPromotion")]
        public HttpResponseMessage AddAwardPromotion(PromotionAwardInputDTO promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Claims claims = new Claims().Values();

                    AddAwardPromotionCommand command = new AddAwardPromotionCommand();
                    command.PromotionIntegrationCode = promotion.PromotionIntegrationCode;
                    command.PersonIntegrationCode = promotion.PersonIntegrationCode;
                    command.ExpertiseId = promotion.ExpertiseId;
                    command.Title = promotion.Title;
                    command.ShortDescription = promotion.ShortDescription;
                    command.StartDateUTC = promotion.StartDateUTC;
                    command.ValidUntilUTC = promotion.ValidUntilUTC;
                    command.RequiredTimeForActivation = (short)promotion.RequiredTimeForActivation;
                    command.PriceInHeeelps = promotion.PriceInHeeelps;
                    command.NumberOfAvailableCoupons = promotion.NumberOfAvailableCoupons;
                    command.NeighbourhoodId = promotion.NeighbourhoodId;
                    command.UserSessionId = promotion.UserSessionId;
                    command.ShortDescription = promotion.ShortDescription;
                    command.UserSystemId = promotion.UserSystemId;
                    bus.Send(command);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Adiciona Nova Gift Promocao
        /// </summary>
        /// <param name="promotion">promotion</param>
        /// <remarks>Add nova Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(PromotionGiftInputDTO))]
        [HttpPost]
        [Authorize]
        [Route("AddGiftPromotion")]
        public HttpResponseMessage AddGiftPromotion(PromotionGiftInputDTO promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AddGiftPromotionCommand command = new AddGiftPromotionCommand();
                    command.PromotionIntegrationCode = promotion.PromotionIntegrationCode;
                    command.PersonIntegrationCode = promotion.PersonIntegrationCode;
                    command.ExpertiseId = promotion.ExpertiseId;
                    command.Title = promotion.Title;
                    command.ShortDescription = promotion.ShortDescription;
                    command.StartDateUTC = promotion.StartDateUTC;
                    command.ValidUntilUTC = promotion.ValidUntilUTC;
                    command.RequiredTimeForActivation = promotion.RequiredTimeForActivation;
                    command.PromotionBillingModelId = promotion.PromotionBillingModelId;
                    command.PromotionPaymentTypeId = promotion.PromotionPaymentTypeId;
                    command.NumberOfAvailableCoupons = promotion.NumberOfAvailableCoupons;
                    command.NeighbourhoodId = promotion.NeighbourhoodId;
                    command.PotencialDemand = promotion.PotencialDemand;
                    command.UserSession = promotion.UserSession;
                    command.IssueDateUTC = promotion.IssueDateUTC;
                    command.UserSystemId = promotion.UserSystemId;

                    bus.Send(command);

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion


        #region Query Promotion

        /// <summary>
        /// Lista Promocoes
        /// </summary>
        /// <param name="personIntegrationCode">personIntegrationCode</param>
        /// <remarks>Lista Promocoes</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(Guid))]
        [HttpGet]
        [Authorize]
        [Route("ListPromotions/{personIntegrationCode}")]
        public HttpResponseMessage ListPromotions(Guid personIntegrationCode)
        {
            try
            {
                var _clientPromotion = new HttpClient();
                _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                var resultTask = _clientPromotion.GetAsync("api/Promotion/ListPromotions?PersonIntegrationCode=" + personIntegrationCode).Result;
                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("GetPromotionList Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                }

                var promotionList = resultTask.Content.ReadAsAsync<IEnumerable<PromotionListDto>>().Result;
                return Request.CreateResponse(HttpStatusCode.OK, promotionList);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
                //throw ex;
            }
        }


        /// <summary>
        /// Busca Promocao
        /// </summary>
        /// <param name="promotionId">promotionId</param>
        /// <remarks>Busca Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(Guid))]
        [HttpGet]
        [Authorize]
        [Route("GetPromotion/{promotionId}")]
        public HttpResponseMessage GetPromotion(Guid promotionId)
        {
            try
            {
                var _clientPromotion = new HttpClient();
                _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                var resultTask = _clientPromotion.GetAsync("api/Promotion/GetPromotion?promotionId=" + promotionId).Result;
                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("GetPromotion Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                }

                var promotion = resultTask.Content.ReadAsAsync<PromotionDto>();
                return Request.CreateResponse(HttpStatusCode.OK, promotion);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Busca PromotionTypeList
        /// </summary>
        /// <remarks>Busca PromotionTypeList</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("GetPromotionTypeList")]
        public HttpResponseMessage GetPromotionTypeList()
        {
            try
            {
                List<EnumList> list = GeneralEnumerators.GetList<GeneralEnumerators.enumPromotionType>();
                return Request.CreateResponse(HttpStatusCode.OK, list);

                //var _clientPromotion = new HttpClient();
                //_clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                //var resultTask = _clientPromotion.GetAsync("api/SystemValues/GetPromotionTypeList").Result;
                //if (!resultTask.IsSuccessStatusCode)
                //{
                //    LogManager.Error("GetPromotionTypeList Handler: Erro ao requisitar web.api promotion:  status: " + resultTask.StatusCode);
                //    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                //}

                //var promotionType = resultTask.Content.ReadAsAsync<List<PromotionTypeOutputDTO>>();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca PromotionBillingModelList
        /// </summary>
        /// <remarks>Busca PromotionBillingModelList</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("GetPromotionBillingModelList")]
        public HttpResponseMessage GetPromotionBillingModelList()
        {
            try
            {
                List<EnumList> list = GeneralEnumerators.GetList<GeneralEnumerators.enumPromotionBillingModel>();
                return Request.CreateResponse(HttpStatusCode.OK, list);

                //var _clientPromotion = new HttpClient();
                //_clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                //var resultTask = _clientPromotion.GetAsync("api/SystemValues/GetPromotionBillingModelList").Result;
                //if (!resultTask.IsSuccessStatusCode)
                //{
                //    LogManager.Error("GetPromotionBillingModelList Handler: Erro ao requisitar web.api promotion:  status: " + resultTask.StatusCode);
                //    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                //}

                //var promotionBillingModel = resultTask.Content.ReadAsAsync<List<PromotionBillingModelOutputDTO>>();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Busca PromotionMethodPayment
        /// </summary>
        /// <remarks>Busca PromotionMethodPayment</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("GetPromotionMethodPaymentList")]
        public HttpResponseMessage GetPromotionMethodPaymentList()
        {
            try
            {
                List<EnumList> list = GeneralEnumerators.GetList<GeneralEnumerators.enumPromotionMethodPayment>();
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Busca PromotionRecurrenceList
        /// </summary>
        /// <remarks>Busca PromotionRecurrenceList</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("GetPromotionRecurrenceList")]
        public HttpResponseMessage GetPromotionRecurrenceList()
        {
            try
            {
                List<EnumList> list = GeneralEnumerators.GetList<GeneralEnumerators.enumPromotionRecurrence>();
                return Request.CreateResponse(HttpStatusCode.OK, list);
                //var _clientPromotion = new HttpClient();
                //_clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                //var resultTask = _clientPromotion.GetAsync("api/SystemValues/GetPromotionRecurrenceList").Result;
                //if (!resultTask.IsSuccessStatusCode)
                //{
                //    LogManager.Error("GetPromotionRecurrenceList Handler: Erro ao requisitar web.api promotion:  status: " + resultTask.StatusCode);
                //    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                //}

                //var promotionRecurrenceOutput = resultTask.Content.ReadAsAsync<List<PromotionRecurrenceOutputDTO>>();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca PromotionPaymentTypeList
        /// </summary>
        /// <remarks>Busca PromotionPaymentTypeList</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("GetPromotionPaymentTypeList")]
        public HttpResponseMessage GetPromotionPaymentTypeList()
        {
            try
            {
                List<EnumList> list = GeneralEnumerators.GetList<GeneralEnumerators.enumPromotionPaymentType>();
                return Request.CreateResponse(HttpStatusCode.OK, list);

                //var _clientPromotion = new HttpClient();
                //_clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                //var resultTask = _clientPromotion.GetAsync("api/SystemValues/GetPromotionPaymentTypeList").Result;
                //if (!resultTask.IsSuccessStatusCode)
                //{
                //    LogManager.Error("GetPromotionPaymentTypeList Handler: Erro ao requisitar web.api promotion:  status: " + resultTask.StatusCode);
                //    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                //}

                //var promotionPaymentType = resultTask.Content.ReadAsAsync<List<PromotionPaymentTypeOutputDTO>>();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Busca Cupom
        /// </summary>
        /// <remarks>Busca detalhe meu cupom</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("GetMyCoupon/{id}")]
        public HttpResponseMessage GetMyCoupon(int id)
        {
            try
            {
                var _clientPromotion = new HttpClient();
                _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                var resultTask = _clientPromotion.GetAsync("/api/coupon/GetMyCoupon/" + id).Result;
                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("GetPromotionPaymentTypeList Handler: Erro ao requisitar web.api promotion:  status: " + resultTask.StatusCode);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }

                var coupon = resultTask.Content.ReadAsAsync<MyCouponsDTO>();
                return Request.CreateResponse(HttpStatusCode.OK, coupon);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca Meus Cupons
        /// </summary>
        /// <remarks>Busca meus cupons</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize]
        [Route("GetMyCoupons")]
        public HttpResponseMessage GetMyCoupons()
        {
            try
            {
                Claims claims = new Claims().Values();
                var _clientPromotion = new HttpClient();
                _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                var resultTask = _clientPromotion.GetAsync("/api/coupon/GetMyCoupons/?userId=" + claims.userSystemId).Result;
                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("GetPromotionPaymentTypeList Handler: Erro ao requisitar web.api promotion:  status: " + resultTask.StatusCode);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }

                var coupon = resultTask.Content.ReadAsAsync<List<MyCouponsDTO>>();
                return Request.CreateResponse(HttpStatusCode.OK, coupon);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion


        #region Edit Promotion

        /// <summary>
        /// Edita Discount Promocao
        /// </summary>
        /// <param name="promotion">promotion</param>
        /// <remarks>Edita Discount Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(EditPromotionDiscountInputDTO))]
        [HttpPost]
        [Authorize]
        [Route("EditPromotionDiscountInfo")]
        public HttpResponseMessage EditPromotionDiscountInfo(EditPromotionDiscountInputDTO promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _clientPromotion = new HttpClient();
                    _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                    var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/EditPromotionDiscountInfo", promotion).Result;
                    if (!resultTask.IsSuccessStatusCode)
                    {
                        LogManager.Error("EditPromotionDiscountInfo Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                    }



                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Edita Award Promocao
        /// </summary>
        /// <param name="promotion">promotion</param>
        /// <remarks>Edita Award Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(EditPromotionAwardInputDTO))]
        [HttpPost]
        [Authorize]
        [Route("EditPromotionAwardInfo")]
        public HttpResponseMessage EditPromotionAwardInfo(EditPromotionAwardInputDTO promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var _clientPromotion = new HttpClient();
                    _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                    var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/EditPromotionAwardInfo", promotion).Result;
                    if (!resultTask.IsSuccessStatusCode)
                    {
                        LogManager.Error("EditPromotionAwardInfo Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                    }




                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Edita Gift Promocao
        /// </summary>
        /// <param name="promotion">promotion</param>
        /// <remarks>Edita Gift Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(EditPromotionGiftInputDTO))]
        [HttpPost]
        [Authorize]
        [Route("EditPromotionGiftInfo")]
        public HttpResponseMessage EditPromotionGiftInfo(EditPromotionGiftInputDTO promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var _clientPromotion = new HttpClient();
                    _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                    var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/EditPromotionGiftInfo", promotion).Result;
                    if (!resultTask.IsSuccessStatusCode)
                    {
                        LogManager.Error("EditPromotionGiftInfo Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Edita detalhes Promocao
        /// </summary>
        /// <param name="promotion">promotion</param>
        /// <remarks>Edita detalhes Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(PromotionDetailsInputDTO))]
        [HttpPost]
        [Authorize]
        [Route("EditPromotionDetails")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage EditPromotionDetails(PromotionDetailsInputDTO promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Claims claims = new Claims().Values();
                    var _clientPromotion = new HttpClient();
                    _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                    promotion.UserSystemId = claims.userSystemId;
                    var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/EditPromotionDetails", promotion).Result;
                    if (!resultTask.IsSuccessStatusCode)
                    {
                        LogManager.Error("EditPromotionDetails Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { });
        }

        /// <summary>
        /// Edita Imagem Promocao
        /// </summary>
        /// <remarks>Edita Imagem Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [Authorize]
        [Route("EditPromotionPhoto")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<HttpResponseMessage> EditPromotionPhoto()
        {
            PromotionPhotoInputDTO promotionPhoto = new PromotionPhotoInputDTO();
            var provider = new MultipartMemoryStreamProvider();
            Claims claims = new Claims().Values();
            NameValueCollection parameters = HttpContext.Current.Request.Params;
            try
            {
                if (Request.Content.IsMimeMultipartContent())
                {
                    try
                    {
                        await Request.Content.ReadAsMultipartAsync(provider);
                        fs = new List<FIleServer>();
                        int count = 0;
                        foreach (var file in HttpContext.Current.Request.Files)
                        {
                            var f = provider.Contents[count];
                            string fileName;
                            if (f.Headers.ContentDisposition == null || f.Headers.ContentDisposition.FileName == null)
                                fileName = parameters["FileName"].ToString() + ".jpeg";
                            else
                                fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");

                            string mimeType = new MimeType().GetMimeType(fileName);
                            var stream = (HttpContext.Current.Request.Files[count]).InputStream;
                            byte[] bytesInStream = new byte[stream.Length];
                            stream.Read(bytesInStream, 0, bytesInStream.Length);

                            Domain.ReadModel.FileTemp ft = new Domain.ReadModel.FileTemp();
                            ft.FileIntegrationCode = Guid.NewGuid();
                            ft.FilePath = _storage.UploadFile(_containerName, ft.FileIntegrationCode.ToString(), mimeType, bytesInStream);
                            ft.OriginalName = fileName;
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
                            int fileId = _FileTemp.SaveFileTemp(ft);
                            _listFileTemp.Add(fileId);

                            promotionPhoto.PromotionIntegrationCode = Guid.Parse(parameters["IntegrationCode"].ToString());
                            promotionPhoto.Active = true;
                            promotionPhoto.FileId = fileId;
                            promotionPhoto.IsDefault = true;
                            promotionPhoto.PathImage = ft.FilePath;
                            promotionPhoto.ShowOrder = (byte)count;
                            promotionPhoto.UserSystemId = claims.userSystemId;

                            var _clientPromotion = new HttpClient();
                            _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                            var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/EditPromotionPhoto", promotionPhoto).Result;
                            if (!resultTask.IsSuccessStatusCode)
                            {
                                LogManager.Error("EditPromotionPhoto Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        LogManager.Error(string.Format("PromotionProspectAdd image Error:{0}", ex));

                    }

                    return Request.CreateResponse(HttpStatusCode.OK, new { promotionPhoto.PathImage });
                }
            }
            catch (System.Exception ex)
            {
                LogManager.Error(string.Format("ValidateUser Error:{0} : {1}", ex, ex.StackTrace));
                LogManager.Error(string.Format("ValidateUser Error Parametros recebidos: Alert = {0}, normalPrice =   {1} , discountPercent = {2} ,ExpertiseId =  {3} ,Title =  {4} ,ShortDescription = {5} ,FullDescription = {6} ,StartDateUTC = {7} ,ValidUntilUTC = {8} ,RequiredTimeForActivation = {9},PromotionTypeId = {10} ,PromotionBillingModelId = {11} ,PromotionRecurrenceId = {12} ,PromotionPaymentTypeId = {13} ,CurrencyId = {14} ,ExpertisePromotionCostReferenceId = {15},NumberOfAvailableCoupons = {16} ,PersonIntegrationID = {17}", parameters["Alert"], parameters["NormalPrice"], parameters["DiscountePercentege"], parameters["ExpertiseId"], parameters["Title"], parameters["ShortDescription"], parameters["FullDescription"], parameters["StartDateUTC"], parameters["ValidUntilUTC"], parameters["RequiredTimeForActivation"], parameters["PromotionTypeId"], parameters["PromotionBillingModelId"], parameters["PromotionRecurrenceId"], parameters["PromotionPaymentTypeId"], parameters["CurrencyId"], parameters["ExpertisePromotionCostReferenceId"], parameters["NumberOfAvailableCoupons"], parameters["PersonIntegrationID"].Replace(",", "")));


                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { });

        }


        /// <summary>
        /// Aprovar Promocao
        /// </summary>
        /// <remarks>Aprovar Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [Authorize]
        [Route("ApprovePromotion")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage ApprovePromotion(ApprovePromotionInputDTO promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Claims claims = new Claims().Values();
                    var _clientPromotion = new HttpClient();
                    _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                    promotion.UserSystemId = claims.userSystemId;

                    var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/ApprovedPromotion", promotion).Result;
                    if (!resultTask.IsSuccessStatusCode)
                    {
                        LogManager.Error("EditPromotionDetails Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { });

        }

        /// <summary>
        /// Rejeitar Promocao
        /// </summary>
        /// <remarks>Rejeitar Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [Authorize]
        [Route("RefusePromotion")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage RefusePromotion(RefusedPromotionInputDTO promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Claims claims = new Claims().Values();
                    var _clientPromotion = new HttpClient();
                    _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                    promotion.UserSystemId = claims.userSystemId;

                    var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/RefusedPromotion", promotion).Result;
                    if (!resultTask.IsSuccessStatusCode)
                    {
                        LogManager.Error("EditPromotionDetails Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { });

        }


        #endregion

        #region Upload    
        /// <summary>
        /// Upload Imagem Promocao
        /// </summary>
        /// <remarks>Upload Imagem Promocao</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost]
        [Authorize]
        [Route("UploadPromotionPhoto")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<HttpResponseMessage> UploadPromotionPhoto()
        {

            if (ModelState.IsValid)
            {
                var provider = new MultipartMemoryStreamProvider();
                Claims claims = new Claims().Values();
                NameValueCollection parameters = HttpContext.Current.Request.Params;
                try
                {
                    //LogManager.Info(string.Concat(" PromotionProspectAdd ismimemultipart: ", Request.Content.IsMimeMultipartContent()));
                    if (Request.Content.IsMimeMultipartContent())
                    {

                        try
                        {
                            await Request.Content.ReadAsMultipartAsync(provider);
                            fs = new List<FIleServer>();
                            int count = 0;
                            //LogManager.Info(string.Concat("PromotionProspectAdd ismimemultipart: ", HttpContext.Current.Request.Files.Count));
                            string pathStorage = string.Empty;
                            foreach (var file in HttpContext.Current.Request.Files)
                            {
                                var f = provider.Contents[count];
                                string fileName;
                                if (f.Headers.ContentDisposition == null || f.Headers.ContentDisposition.FileName == null)
                                    fileName = parameters["FileName"].ToString() + ".jpeg";
                                else
                                    fileName = f.Headers.ContentDisposition.FileName.ToString().Replace("\"", "");

                                string mimeType = new MimeType().GetMimeType(fileName);
                                var stream = (HttpContext.Current.Request.Files[count]).InputStream;
                                byte[] bytesInStream = new byte[stream.Length];
                                stream.Read(bytesInStream, 0, bytesInStream.Length);

                                Domain.ReadModel.FileTemp ft = new Domain.ReadModel.FileTemp();
                                ft.FileIntegrationCode = Guid.NewGuid();
                                pathStorage = _storage.UploadFile(_containerName, ft.FileIntegrationCode.ToString(), mimeType, bytesInStream);
                                ft.FilePath = pathStorage;
                                ft.OriginalName = fileName;
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
                                int fileId = _FileTemp.SaveFileTemp(ft);
                                _listFileTemp.Add(fileId);
                                PromotionPhotoInputDTO promotionPhoto = new PromotionPhotoInputDTO();
                                promotionPhoto.PromotionIntegrationCode = Guid.Parse(parameters["IntegrationCode"].ToString());
                                promotionPhoto.Active = true;
                                promotionPhoto.FileId = fileId;
                                promotionPhoto.IsDefault = (count == 1);
                                promotionPhoto.PathImage = ft.FilePath;
                                promotionPhoto.ShowOrder = (byte)count;
                                promotionPhoto.UserSystemId = claims.userSystemId;
                                var _clientPromotion = new HttpClient();
                                _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                                var resultTask = _clientPromotion.PostAsJsonAsync("api/Promotion/UploadPromotionPhoto", promotionPhoto).Result;
                                if (!resultTask.IsSuccessStatusCode)
                                {
                                    LogManager.Error("UploadPromotionPhoto Handler: Erro ao enviar web.api promotion:  status: " + resultTask.StatusCode);
                                }


                            }
                            return Request.CreateResponse(HttpStatusCode.OK, pathStorage);
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(string.Format("PromotionProspectAdd image Error:{0}", ex));

                        }
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { });

        }
        #endregion


        #region Coupon

        /// <summary>
        /// Emite um cupom de desconto
        /// </summary>
        /// <param name="Coupon">Coupon</param>
        /// <remarks>Emite novo Cupom de Desconto</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(PromotionDiscountInputDTO))]
        [HttpPost]
        [Authorize]
        [Route("IssueCoupon")]
        public HttpResponseMessage IssueCoupon(IssueCouponInputDto coupon)
        {
            try
            {
                Claims claims = new Claims().Values();

                coupon.PersonCustomerId = claims.personId;
                coupon.UserCustomerId = claims.userSystemId;
                //todo: o valor da usersession deveria estar presente nas claims
                //coupon.UserSessionIssue = 

                var _clientPromotion = new HttpClient();
                _clientPromotion.BaseAddress = new Uri(CustomConfiguration.WebApiPromotion);
                _clientPromotion.DefaultRequestHeaders.Add("Authorization", "Bearer " + coupon.Token);
                var resultTask = _clientPromotion.PostAsJsonAsync("api/Coupon/IssueCoupon", coupon).Result;

                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("Failure issuing a new coupon.  Status: " + resultTask.StatusCode);
                }
                else
                {
                    var couponResponse = resultTask.Content.ReadAsAsync<CouponIssuedOutputDTO>().Result;
                    if (couponResponse.IsError != true)
                        return Request.CreateResponse(HttpStatusCode.OK, couponResponse);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, couponResponse.ResultMessage);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { });
        }


        /// <summary>
        /// Ativa um cupom de desconto
        /// </summary>
        /// <param name="Coupon">Coupon</param>
        /// <remarks>Ativa um Cupom de Desconto</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(HttpStatusCode))]
        [HttpPost]
        [Authorize]
        [Route("TransactCoupon")]
        public HttpResponseMessage TransactCoupon(TransactCouponDto coupon)
        {
            //transformar esse processo em um comando para dar opcao de tolerancia a falha
            try
            {
                Claims claims = new Claims().Values();
                coupon.UserId = claims.userSystemId;
                coupon.PersonIntegrationCode = claims.personIntegrationCode;
                //todo: o valor da usersession deveria estar presente nas claims
                //coupon.UserSessionIssue = 

                TransactCouponCommand command = new TransactCouponCommand()
                {
                    CouponIntegrationCode = coupon.CouponIntegrationCode,
                    UserSessionTrade = coupon.UserSessionTrade,
                    PersonIntegrationCode = claims.personIntegrationCode,
                    UserId = claims.userSystemId,
                    QRCode = coupon.QRCode
                };
                this.bus.Send(command);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, new { });
        }


        /// <summary>
        /// Cancela um cupom de desconto
        /// </summary>
        /// <param name="Coupon">Coupon</param>
        /// <remarks>Cancela um Cupom de Desconto</remarks>
        /// <returns></returns>
        /// <response code="200"></response>
        [ResponseType(typeof(HttpStatusCode))]
        [HttpPost]
        [Authorize]
        [Route("CancelCoupon")]
        public HttpResponseMessage CancelCoupon(CancelCouponDto coupon)
        {
            //transformar esse processo em um comando para dar opcao de tolerancia a falha
            Claims claims = new Claims().Values();
            CancelCouponCommand command = new CancelCouponCommand()
            {
                CouponIntegrationCode = coupon.CouponIntegrationCode,
                UserSessionTrade = coupon.UserSessionTrade,
                PersonIntegrationCode = claims.personIntegrationCode,
                UserId = claims.userSystemId
            };
            this.bus.Send(command);

            return Request.CreateResponse(HttpStatusCode.OK, new { });
        }

        #endregion

        #region Delete

        [HttpPost]
        [Authorize]
        [Route("DeletedPromotion")]
        public HttpResponseMessage DeletedPromotion(PromotionDeletedDTO promotion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Claims claims = new Claims().Values();
                    promotion.UserSystemId = claims.userSystemId;
                    DeletedPromotionCommand command = new DeletedPromotionCommand();
                    command.PromotionIntegrationCode = promotion.PromotionIntegrationCode;
                    command.UserSystemId = promotion.UserSystemId;
                    command.UserSessionId = promotion.UserSessionId;
                    bus.Send(command);
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, new { });
        }
        #endregion


    }
}
