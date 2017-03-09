namespace Heeelp.Core.Domain
{
    using Common;
    using Infrastructure.Database;
    using Infrastructure.Messaging;
    using Process.Event.User;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("User")]
    public partial class User : IAggregateRoot, IEventPublisher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserFile = new HashSet<UserFile>();
            UserHistory = new HashSet<UserHistory>();
        }

        public User(Guid integrationCode, string name, string email,   string secundaryEmail, string smartPhoneNumber, int personId, bool isDefaultUser, byte userProfileId, byte authenticationModeId, DateTime creationDateUTC, bool active, string enrollmentIP, int? createdBy, bool? isPerpetual, string loginPassword, byte? languageId, byte userStatusId, short serverInstanceId)
        {

            this.IntegrationCode = integrationCode;
            this.Name = name;
            this.Email = email;
            this.SecundaryEmail = secundaryEmail;
            this.SmartPhoneNumber = smartPhoneNumber;
            this.PersonId = personId;
            this.IsDefaultUser = isDefaultUser;
            this.UserProfileId = userProfileId;
            this.UserStatusId = userStatusId;
            this.AuthenticationModeId = authenticationModeId;
            this.LanguageId = languageId;
            this.CreationDateUTC = creationDateUTC;
            this.Active = active;
            this.EnrollmentIP = enrollmentIP;
            this.CreatedBy = createdBy;
            this.ServerInstanceId = serverInstanceId;
            this.IsPerpetual = isPerpetual;
            this.LoginPassword = loginPassword;
            UserFile = new HashSet<UserFile>();
            UserHistory = new HashSet<UserHistory>();

        }

        public User(Guid integrationCode, string name, string email, string secundaryEmail, string smartPhoneNumber, int personId, bool isDefaultUser, byte userProfileId, byte userStatusId, byte authenticationModeId, byte? languageId, DateTime creationDateUTC, DateTime? validationDateUTC, bool active, string enrollmentIP, string validationIP, int? createdBy, short serverInstanceId, bool? isPerpetual, string loginPassword)
        {

            this.IntegrationCode = integrationCode;
            this.Name = name;
            this.Email = email;
            this.SecundaryEmail = secundaryEmail;
            this.SmartPhoneNumber = smartPhoneNumber;
            this.PersonId = personId;
            this.IsDefaultUser = isDefaultUser;
            this.UserProfileId = userProfileId;
            this.UserStatusId = userStatusId;
            this.AuthenticationModeId = authenticationModeId;
            this.LanguageId = languageId;
            this.CreationDateUTC = creationDateUTC;
            this.ValidationDateUTC = validationDateUTC;
            this.Active = active;
            this.EnrollmentIP = enrollmentIP;
            this.ValidationIP = validationIP;
            this.CreatedBy = createdBy;
            this.ServerInstanceId = serverInstanceId;
            this.IsPerpetual = isPerpetual;
            this.LoginPassword = loginPassword;
            UserFile = new HashSet<UserFile>();
            UserHistory = new HashSet<UserHistory>();

        }


        public User(int userId, string secundaryEmail, string smartPhoneNumber, string loginPassword)
        {
            this.UserId = userId;
            this.SecundaryEmail = secundaryEmail;
            this.SmartPhoneNumber = smartPhoneNumber;
            this.LoginPassword = loginPassword;
            UserFile = new HashSet<UserFile>();
            UserHistory = new HashSet<UserHistory>();

        }

        public List<IEvent> events = new List<IEvent>();
        public IEnumerable<IEvent> Events { get { return this.events; } }

        public void AddEvent(IEvent @event)
        {
            this.events.Add(@event);
        }



        [NotMapped]
        public Guid Id { get; set; }

        public void CompleteSuccessFile(FIleServer fs)
        {
            this.AddEvent(new UserCreatedFIleEvent
            {
                SourceId = this.Id,
                UserId = this.UserId,
                FilePath = fs.FilePath,
                Width = fs.Width,
                Height = fs.Height,
                OriginalName = fs.OriginalName,
                FileTempId = fs.FileTempId,
                Description = "Usuário do Heeelp",
                FriendlyName = fs.FriendlyName,
                FileUtilizationId = fs.FileUtilizationId,
                Alt = fs.Alt,
                FileIntegrationCode = fs.FileIntegrationCode,
                Name = fs.Name,
                FileOriginTypeId = (int)GeneralEnumerators.EnumModules.Core_User,
                PersonId = fs.PersonId,
                UploadedBy = fs.UploadedBy
            });
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public Guid IntegrationCode { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string SecundaryEmail { get; set; }

        public string SmartPhoneNumber { get; set; }

        public int PersonId { get; set; }

        public bool IsDefaultUser { get; set; }

        public byte UserProfileId { get; set; }

        public byte UserStatusId { get; set; }

        public byte AuthenticationModeId { get; set; }

        public byte? LanguageId { get; set; }

        public DateTime CreationDateUTC { get; set; }

        public DateTime? ValidationDateUTC { get; set; }

        public short? FormFillTime { get; set; }
        
        public string ValidationToken { get; set; }

        public bool? SecurityCheckNecessary { get; set; }

        public short? ValidationAttempts { get; set; }

        public short? LoginFailAttempts { get; set; }

        public DateTime? LastLoginFailUTC { get; set; }

        public bool Active { get; set; }

        public string LoginPassword { get; set; }

        public string EnrollmentIP { get; set; }

        public string ValidationIP { get; set; }

        public int? CreatedBy { get; set; }

        public short ServerInstanceId { get; set; }

        public bool? IsPerpetual { get; set; }

        public string UrlImagemLogo { get; set; }


        [NotMapped]
        public virtual Language Language { get; set; }
       
        public virtual Person Person { get; set; }

        public virtual ServerInstance ServerInstance { get; set; }

        [NotMapped]
        public virtual User CreatedByUser { get; set; }

        [NotMapped]
        public virtual UserProfile UserProfile { get; set; }

        [NotMapped]
        public virtual UserStatus UserStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserFile> UserFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserHistory> UserHistory { get; set; }

        [NotMapped]
        public virtual UserPreference UserPreference { get; set; }

    }
}
