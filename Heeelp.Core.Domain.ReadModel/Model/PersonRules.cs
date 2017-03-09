namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    public partial class PersonRules 
    {
        public PersonRules()
        {

        }
        public int PersonRulesId { get; set; }
        public int PersonId { get; set; }
        public byte PersonProfileId { get; set; }
        public DateTime DateUTC { get; set; }
        public bool Active { get; set; }
        public byte RulesStatusId { get; set; }
    }
}
