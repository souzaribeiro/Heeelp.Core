namespace Heeelp.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class BaseCEPs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(11)]
        public string cep { get; set; }

        [StringLength(2)]
        public string uf { get; set; }

        [StringLength(150)]
        public string cidade { get; set; }

        [StringLength(150)]
        public string bairro { get; set; }

        [StringLength(150)]
        public string logradouro { get; set; }

        [StringLength(30)]
        public string latitude { get; set; }

        [StringLength(30)]
        public string longitude { get; set; }

        [StringLength(3)]
        public string ibge_cod_uf { get; set; }

        [StringLength(10)]
        public string ibge_cod_cidade { get; set; }

        [StringLength(20)]
        public string area_cidade_km2 { get; set; }

        [StringLength(3)]
        public string ddd { get; set; }
    }
}
