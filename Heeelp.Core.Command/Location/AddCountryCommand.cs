using System;

namespace Heeelp.Core.Command.Location
{
    public class AddCountryCommand : CommandBase
    {
        public AddCountryCommand()
        {
            this.Id = Guid.NewGuid();
        }
        public byte CountryId { get; set; }

        public string Name { get; set; }
      
        public string Code { get; set; }

        public string PhoneCode { get; set; }

        public byte LanguageId { get; set; }

        public byte CurrencyId { get; set; }
    }
}
