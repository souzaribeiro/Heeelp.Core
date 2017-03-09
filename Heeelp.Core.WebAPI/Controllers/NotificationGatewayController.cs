using System;
using System.Net.Http;
//using System.Web.Mvc;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.DTO;
using Heeelp.Core.Command.ExternalModules;
using Heeelp.Core.Infrastructure.Messaging;
using System.Net;
using Heeelp.Core.Logging;
using Heeelp.Core.Domain.ReadModel.DTO.ExternalModules.Notification;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Heeelp.Core.WebAPI.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/notification")]
    public class NotificationGatewayController : BaseController
    {

        private readonly ICommandBus bus;

        public NotificationGatewayController(ICommandBus bus) : base()
        {
            this.bus = bus;
        }

        // GET: Notification
        
        [HttpPost]
        [Authorize]
        public HttpResponseMessage SendNotification(SendNotificationInputDTO notification)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Claims claims = new Claims().Values();

                    SendExternalNotificationCommand command = new SendExternalNotificationCommand()
                    {
                        PersonFromId = notification.PersonFromId,
                        PersonToId = notification.PersonToId,
                        MessageCodeType = notification.MessageCodeType,
                        Title = notification.Title,
                        Body = notification.Body
                    };
                    this.bus.Send(command);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                LogManager.Error("Erro ao registrar solicitação de envio de notificação", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }


        // POST: Notification
        [HttpPost]
        [Authorize]
        [Route("ListNotifications")]
        public HttpResponseMessage ListNotifications(ListNotificationsInputDTO notification)
        {
            try
            {
                var _clientNotification = new HttpClient();
                _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
                //todo arrumar o token
                _clientNotification.DefaultRequestHeaders.Add("Authorization", "Bearer " + notification.Token);
                var resultTask = _clientNotification.GetAsync("/api/Communication/ListNotifications").Result;

                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("Erro na solicitação de lista de notificação " + resultTask.StatusCode);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, resultTask.StatusCode.ToString());
                }
                else
                {
                    var notifications = resultTask.Content.ReadAsAsync<PersonCommunicationListDTO>().Result;

                    return Request.CreateResponse(HttpStatusCode.OK, notifications);

                }
            }
            catch (Exception ex)
            {
                LogManager.Error("Erro na solicitação de lista de notificação", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }
        // POST: Notification
        [HttpPost]
        [Authorize]
        [Route("SetAllNotificationsViewed")]
        public HttpResponseMessage SetAllNotificationsViewed(SetAllNotificationsViewedInputDTO notification)
        {
            try
            {
                var _clientNotification = new HttpClient();
                _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
                //todo arrumar o token
                _clientNotification.DefaultRequestHeaders.Add("Authorization", "Bearer " + notification.Token);
                var resultTask = _clientNotification.GetAsync("/api/Communication/SetAllNotificationsViewed").Result;

                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("Erro na solicitação de lista de notificação " + resultTask.StatusCode);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, resultTask.StatusCode.ToString());
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                LogManager.Error("Erro na solicitação de lista de notificação", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }

        // POST: Notification
        [HttpPost]
        [Authorize]
        [Route("GetLastestNotifications")]
        public HttpResponseMessage GetLastestNotifications(GetLastestNotificationsInputDTO notification)
        {
            try
            {
                var _clientNotification = new HttpClient();
                _clientNotification.BaseAddress = new Uri(CustomConfiguration.WebApiNotification);
                //todo arrumar o token
                _clientNotification.DefaultRequestHeaders.Add("Authorization", "Bearer " + notification.Token);
                var resultTask = _clientNotification.GetAsync("/api/Communication/GetLastestNotifications").Result;

                if (!resultTask.IsSuccessStatusCode)
                {
                    LogManager.Error("Erro na solicitação de lista de notificação " + resultTask.StatusCode);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, resultTask.StatusCode.ToString());
                }
                else
                {
                    var notifications = resultTask.Content.ReadAsAsync<PersonCommunicationListDTO>().Result;
                    return Request.CreateResponse(HttpStatusCode.OK, notifications);
                }
            }
            catch (Exception ex)
            {
                LogManager.Error("Erro na solicitação de lista de notificação", ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }

        }
    }
}