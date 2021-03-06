﻿using System;

namespace Heeelp.Core.Command.Location
{
    public class AddCityCommand : CommandBase
    {
        public AddCityCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public int CityId { get; set; }

        public string Name { get; set; }

        public int StateRegionId { get; set; }

        public string Coordinates { get; set; }

        public string PostCode { get; set; }

        public bool Active { get; set; }

        public DateTime InsertedDate { get; set; }

        public string PhoneCode { get; set; }
    }
}
