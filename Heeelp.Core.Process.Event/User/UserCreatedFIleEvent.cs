using Heeelp.Core.Infrastructure.Messaging;
using System;

namespace Heeelp.Core.Process.Event.User
{
    public class UserCreatedFIleEvent : IEvent
    {
        public UserCreatedFIleEvent()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        public int UserId { get; set; }
        public int FileTempId { get; set; }
        public string FilePath { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string OriginalName { get; set; }
        public Guid FileIntegrationCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FriendlyName { get; set; }
        public string Alt { get; set; }
        public int PersonId { get; set; }
        public int FileOriginTypeId { get; set; }
        public byte FileUtilizationId { get; set; }
        public int? UploadedBy { get; set; }
    }
}
