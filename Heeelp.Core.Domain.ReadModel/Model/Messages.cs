namespace Heeelp.Core.Domain.ReadModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MessageLog.Messages")]
    public partial class Messages
    {
        public Guid Id { get; set; }

        public string Kind { get; set; }

        public string SourceId { get; set; }

        public string AssemblyName { get; set; }

        public string Namespace { get; set; }

        public string FullName { get; set; }

        public string TypeName { get; set; }

        public string SourceType { get; set; }

        public string CreationDate { get; set; }

        public string Payload { get; set; }
    }
}
