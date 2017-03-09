using Heeelp.Core.Command.Person;
using Heeelp.Core.Common;
using Heeelp.Core.Domain.ReadModel.Interfaces;
using Heeelp.Core.Infrastructure.Database;
using Heeelp.Core.Infrastructure.Messaging;
using Heeelp.Core.Infrastructure.Messaging.Handling;
using System;
using System.IO;
using System.Net.Http;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json;

namespace Heeelp.Core.ProcessManager.CommandHandlers.Person
{
    public class PersonAddressCommandHandler :
        ICommandHandler<AddPersonAddressCommand>,
        ICommandHandler<UpdatePersonAddressCompleteRegistrationCommand>
    {
        private readonly ICommandBus bus;
        private Func<IDataContext<Domain.PersonAddress>> contextFactory;

        private IFileTempDao _FileTemp;
        private readonly IPersonDao _PersonDao;
        private DbGeography point;
        private dynamic geocode;

        public PersonAddressCommandHandler(Func<IDataContext<Domain.PersonAddress>> contextFactory, IPersonDao personDao, IFileTempDao contextFactoryFileTmp)
        {
            this.contextFactory = contextFactory;
            this._FileTemp = contextFactoryFileTmp;
            _PersonDao = personDao;
        }


        public void Handle(AddPersonAddressCommand command)
        {
            var repository = this.contextFactory();


            var person = _PersonDao.GetByPersonIntegrationId(command.PersonIntegrationId);

            var pointString = string.Format("POINT{0}", command.Coordinates.Replace(",", ""));

            var point = DbGeography.PointFromText(pointString, 4326);



            var personAddress = new Domain.PersonAddress(command.PersonAddressId, person.PersonId,
                command.AddressTypeId, command.StartDateUTC, command.StreetName, command.Number,
                command.NeighbourhoodId, command.Neighbourhood, command.Country, command.State, command.City,
                command.PostCode, point, command.ContactPhoneNumber, command.ServerInstanceId,
                command.CreatedBy, command.ContactEMail, command.Active);
            repository.Save(personAddress);
        }

        public void Handle(UpdatePersonAddressCompleteRegistrationCommand command)
        {

            var repository = this.contextFactory();


            var person = _PersonDao.GetByPersonIntegrationId(command.PersonIntegrationId);

            var _client = new HttpClient();
            _client.BaseAddress = new Uri("http://maps.google.com");
            string uri = "/maps/api/geocode/json?address=" + command.StreetName + " " + command.Number + " " + command.City + " " + command.State + " " + command.Country;
            HttpResponseMessage response = _client.GetAsync(uri).Result;

            if (response.IsSuccessStatusCode)
            {
                var ret = response.Content.ReadAsStringAsync().Result;
                geocode = JsonConvert.DeserializeObject<dynamic>(ret);
                var pointString = string.Format("POINT({0} {1})", geocode.results[0].geometry.location.lat, geocode.results[0].geometry.location.lng);
                point = DbGeography.PointFromText(pointString.Replace(",", "."), 4326);
                command.Neighbourhood = geocode.results[0].address_components[2].long_name;
                command.PostCode = geocode.results[0].address_components[7].long_name;
                command.StreetName = command.StreetName + ", " + command.Number + " - " + command.Neighbourhood + ", " + command.City + " - " + geocode.results[0].address_components[5].short_name + ", " + command.PostCode + ", " + command.Country;
            }


            var personAddress = new Domain.PersonAddress(
                command.PersonAddressId,
                person.PersonId,
                1,
                DateTime.UtcNow,
                command.StreetName,
                command.Number,
                command.Neighbourhood,
                command.Country,
                command.State,
                command.City,
                command.PostCode,
                point,
                command.ContactPhoneNumber,
                command.ServerInstanceId,
                command.CreatedBy,
                command.ContactEMail,
                true);

            repository.Save(personAddress);

        }


    }
}
