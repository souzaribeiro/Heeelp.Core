namespace Heeelp.Core.DataBase.SQL.ReadModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Domain.ReadModel;
    using System.Data.Entity.Infrastructure;
    public partial class HeeelpReadDataContext : DbContext
    {
        public HeeelpReadDataContext(string connectionString)
            : base(connectionString)
        { }

        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<AdminUser> AdminUser { get; set; }
        public virtual DbSet<ApprovalStatus> ApprovalStatus { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<CityZone> CityZone { get; set; }
        public virtual DbSet<CommandOrigin> CommandOrigin { get; set; }
        public virtual DbSet<CommandQuee> CommandQuee { get; set; }
        public virtual DbSet<CommandStatus> CommandStatus { get; set; }
        public virtual DbSet<CommandType> CommandType { get; set; }
        public virtual DbSet<CommandWorkFlow> CommandWorkFlow { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<ContractType> ContractType { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<CountryRegion> CountryRegion { get; set; }
        public virtual DbSet<CouponEstatisticMode> CouponEstatisticMode { get; set; }
        public virtual DbSet<CouponPeriodMode> CouponPeriodMode { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<EventQuee> EventQuee { get; set; }
        public virtual DbSet<EventStatus> EventStatus { get; set; }
        public virtual DbSet<EventType> EventType { get; set; }
        public virtual DbSet<EventWorkflow> EventWorkflow { get; set; }
        public virtual DbSet<Expertise> Expertise { get; set; }
        public virtual DbSet<ExpertiseHistory> ExpertiseHistory { get; set; }
        public virtual DbSet<ExpertisePhoto> ExpertisePhoto { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Neighbourhood> Neighbourhood { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PageType> PageType { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<PersonRules> PersonRules { get; set; }
        public virtual DbSet<PersonAddress> PersonAddress { get; set; }
        public virtual DbSet<PersonContract> PersonContract { get; set; }
        public virtual DbSet<PersonDocument> PersonDocument { get; set; }
        public virtual DbSet<PersonExpertise> PersonExpertise { get; set; }
        public virtual DbSet<PersonFile> PersonFile { get; set; }
        public virtual DbSet<PersonHistoric> PersonHistoric { get; set; }
        public virtual DbSet<PersonInterest> PersonInterest { get; set; }
        public virtual DbSet<PersonOriginType> PersonOriginType { get; set; }
        public virtual DbSet<PersonProfile> PersonProfile { get; set; }
        public virtual DbSet<PersonStatus> PersonStatus { get; set; }
        public virtual DbSet<PersonType> PersonType { get; set; }
        public virtual DbSet<SecuritySource> SecuritySource { get; set; }
        public virtual DbSet<ServerInstance> ServerInstance { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<StateRegion> StateRegion { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<SystemParameter> SystemParameter { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserFile> UserFile { get; set; }
        public virtual DbSet<UserGroup> UserGroup { get; set; }
        public virtual DbSet<UserGroupMenu> UserGroupMenu { get; set; }
        public virtual DbSet<UserGroupUser> UserGroupUser { get; set; }
        public virtual DbSet<UserHistory> UserHistory { get; set; }
        public virtual DbSet<UserModule> UserModule { get; set; }
        public virtual DbSet<UserPreference> UserPreference { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserStatus> UserStatus { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Commands> Commands { get; set; }
        public virtual DbSet<Events1> Events1 { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<AddressType>()
                .HasMany(e => e.PersonAddress)
                .WithRequired(e => e.AddressType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AdminUser>()
                .Property(e => e.User)
                .IsFixedLength();

            modelBuilder.Entity<ApprovalStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ApprovalStatus>()
                .HasMany(e => e.Expertise)
                .WithRequired(e => e.ApprovalStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .Property(e => e.PostCode)
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .Property(e => e.PhoneCode)
                .IsFixedLength();

            modelBuilder.Entity<City>()
                .HasMany(e => e.CityZone)
                .WithRequired(e => e.City)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CityZone>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CityZone>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<CommandOrigin>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CommandOrigin>()
                .HasMany(e => e.CommandQuee)
                .WithRequired(e => e.CommandOrigin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommandQuee>()
                .Property(e => e.CommandPayload)
                .IsUnicode(false);

            modelBuilder.Entity<CommandQuee>()
                .HasMany(e => e.CommandWorkFlow)
                .WithRequired(e => e.CommandQuee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommandQuee>()
                .HasMany(e => e.EventQuee)
                .WithRequired(e => e.CommandQuee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommandStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CommandStatus>()
                .HasMany(e => e.CommandQuee)
                .WithRequired(e => e.CommandStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommandStatus>()
                .HasMany(e => e.CommandWorkFlow)
                .WithRequired(e => e.CommandStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommandType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CommandType>()
                .HasMany(e => e.CommandQuee)
                .WithRequired(e => e.CommandType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommandWorkFlow>()
                .Property(e => e.Details)
                .IsUnicode(false);

            //modelBuilder.Entity<Contract>()
            //    .Property(e => e.Term)
            //    .IsUnicode(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.PersonContract)
                .WithRequired(e => e.Contract)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ContractType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .Property(e => e.PhoneCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.CountryRegion)
                .WithRequired(e => e.Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CountryRegion>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CouponEstatisticMode>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CouponPeriodMode>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .Property(e => e.Symbol)
                .IsUnicode(false);

            modelBuilder.Entity<Currency>()
                .HasMany(e => e.Person)
                .WithRequired(e => e.Currency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DocumentType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentType>()
                .HasMany(e => e.PersonDocument)
                .WithRequired(e => e.DocumentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventQuee>()
                .Property(e => e.EventPayload)
                .IsUnicode(false);

            modelBuilder.Entity<EventQuee>()
                .HasMany(e => e.EventWorkflow)
                .WithRequired(e => e.EventQuee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EventStatus>()
                .HasMany(e => e.EventQuee)
                .WithRequired(e => e.EventStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventStatus>()
                .HasMany(e => e.EventWorkflow)
                .WithRequired(e => e.EventStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EventType>()
                .HasMany(e => e.EventQuee)
                .WithRequired(e => e.EventType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EventWorkflow>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<Expertise>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Expertise>()
                .Property(e => e.DefaultDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Expertise>()
                .HasMany(e => e.Expertise1)
                .WithOptional(e => e.Expertise2)
                .HasForeignKey(e => e.ExpertiseFatherId);

            modelBuilder.Entity<Expertise>()
                .HasMany(e => e.ExpertiseHistory)
                .WithRequired(e => e.Expertise)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Expertise>()
                .HasMany(e => e.ExpertisePhoto)
                .WithRequired(e => e.Expertise)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Expertise>()
                .HasMany(e => e.PersonExpertise)
                .WithRequired(e => e.Expertise)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Expertise>()
                .HasMany(e => e.PersonInterest)
                .WithRequired(e => e.Expertise)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExpertiseHistory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ExpertiseHistory>()
                .Property(e => e.DefaultDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .Property(e => e.LanguageCulture)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .Property(e => e.FlagImage)
                .IsUnicode(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Country)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Language>()
                .HasMany(e => e.Person)
                .WithRequired(e => e.Language)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.MenuText)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.MenuAlt)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.MenuIcon)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Menu1)
                .WithOptional(e => e.Menu2)
                .HasForeignKey(e => e.MenuFatherId);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.UserGroupMenu)
                .WithRequired(e => e.Menu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.UserModule)
                .WithRequired(e => e.Module)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Neighbourhood>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Neighbourhood>()
                .Property(e => e.PostCode)
                .IsUnicode(false);

            modelBuilder.Entity<Neighbourhood>()
                .HasMany(e => e.Neighbourhood1)
                .WithOptional(e => e.Neighbourhood2)
                .HasForeignKey(e => e.NeighbourhoodFatherId);

            //modelBuilder.Entity<Neighbourhood>()
            //    .HasMany(e => e.PersonAddress)
            //    .WithRequired(e => e.Neighbourhood)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Page>()
                .Property(e => e.PageURL)
                .IsUnicode(false);

            modelBuilder.Entity<Page>()
                .Property(e => e.PageTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Page>()
                .Property(e => e.PageDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Page>()
                .HasMany(e => e.Menu)
                .WithRequired(e => e.Page)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Page>()
                .HasMany(e => e.Page1)
                .WithRequired(e => e.Page2)
                .HasForeignKey(e => e.PageFatherId);

            modelBuilder.Entity<PageType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PageType>()
                .HasMany(e => e.Page)
                .WithRequired(e => e.PageType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.FantasyName)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.NameFromSecurityCheck)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.FriendlyNameURL)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.PersonOriginDetails)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.PersonalWebSite)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.ActivationCode)
                .IsUnicode(false);

            modelBuilder.Entity<Person>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);
                                    //teste checkin
            //modelBuilder.Entity<Person>()
            //    .HasOptional(e => e.PersonFather);
            //    //.HasForeignKey(e => e.PersonFatherId);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonAddress)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonContract)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonDocument)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonExpertise)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonFile)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonHistoric)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.PersonInterest)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.User)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PersonAddress>()
                .Property(e => e.StreetName)
                .IsUnicode(false);

            modelBuilder.Entity<PersonAddress>()
                .Property(e => e.PostCode)
                .IsUnicode(false);

            modelBuilder.Entity<PersonAddress>()
                .Property(e => e.ContactPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PersonAddress>()
                .Property(e => e.ContactEMail)
                .IsUnicode(false);

            //modelBuilder.Entity<PersonContract>()
            //    .Property(e => e.Term)
            //    .IsUnicode(false);

            modelBuilder.Entity<PersonDocument>()
                .Property(e => e.Number)
                .IsUnicode(false);

            modelBuilder.Entity<PersonDocument>()
                .Property(e => e.Complement)
                .IsUnicode(false);

            modelBuilder.Entity<PersonExpertise>()
                .Property(e => e.CustomDescription)
                .IsUnicode(false);

            modelBuilder.Entity<PersonHistoric>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PersonHistoric>()
                .Property(e => e.FantasyName)
                .IsUnicode(false);

            modelBuilder.Entity<PersonHistoric>()
                .Property(e => e.NameFromSecurityCheck)
                .IsUnicode(false);

            modelBuilder.Entity<PersonHistoric>()
                .Property(e => e.FriendlyNameURL)
                .IsUnicode(false);

            modelBuilder.Entity<PersonHistoric>()
                .Property(e => e.PersonOriginDetails)
                .IsUnicode(false);

            modelBuilder.Entity<PersonHistoric>()
                .Property(e => e.PersonalWebSite)
                .IsUnicode(false);

            modelBuilder.Entity<PersonHistoric>()
                .Property(e => e.ActivationCode)
                .IsUnicode(false);

            modelBuilder.Entity<PersonHistoric>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PersonOriginType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PersonOriginType>()
                .HasMany(e => e.Person)
                .WithRequired(e => e.PersonOriginType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PersonProfile>()
                .Property(e => e.Name)
                .IsUnicode(false);

           

            modelBuilder.Entity<PersonStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PersonStatus>()
                .HasMany(e => e.Person)
                .WithRequired(e => e.PersonStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PersonType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PersonType>()
                .HasMany(e => e.Person)
                .WithRequired(e => e.PersonType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecuritySource>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ServerInstance>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ServerInstance>()
                .HasMany(e => e.Person)
                .WithRequired(e => e.ServerInstance)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServerInstance>()
                .HasMany(e => e.PersonExpertise)
                .WithRequired(e => e.ServerInstance)
                .HasForeignKey(e => e.ServerInstanceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServerInstance>()
                .HasMany(e => e.PersonExpertise1)
                .WithRequired(e => e.ServerInstance1)
                .HasForeignKey(e => e.ServerInstanceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServerInstance>()
                .HasMany(e => e.PersonInterest)
                .WithRequired(e => e.ServerInstance)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServerInstance>()
                .HasMany(e => e.User)
                .WithRequired(e => e.ServerInstance)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.StateRegion)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StateRegion>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<StateRegion>()
                .HasMany(e => e.City)
                .WithRequired(e => e.StateRegion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SystemParameter>()
                .Property(e => e.ParamName)
                .IsUnicode(false);

            modelBuilder.Entity<SystemParameter>()
                .Property(e => e.ParamValue)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.SecundaryEmail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.SmartPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ValidationToken)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.EnrollmentIP)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ValidationIP)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Expertise)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.ApprovedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Expertise1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PersonAddress)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PersonExpertise)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.InsertedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PersonFile)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.AssocietedBy);

            modelBuilder.Entity<User>()
                .HasMany(e => e.PersonInterest)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.InsertedBy);

            modelBuilder.Entity<User>()
                .HasMany(e => e.User1)
                .WithOptional(e => e.User2)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserFile)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserGroupUser)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserHistory)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserModule)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserPreference)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserGroup>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .HasMany(e => e.UserGroupMenu)
                .WithRequired(e => e.UserGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserGroup>()
                .HasMany(e => e.UserGroupUser)
                .WithRequired(e => e.UserGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserHistory>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserHistory>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<UserHistory>()
                .Property(e => e.SecundaryEmail)
                .IsUnicode(false);

            modelBuilder.Entity<UserHistory>()
                .Property(e => e.SmartPhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<UserHistory>()
                .Property(e => e.ValidationToken)
                .IsUnicode(false);

            modelBuilder.Entity<UserHistory>()
                .Property(e => e.EnrollmentIP)
                .IsUnicode(false);

            modelBuilder.Entity<UserHistory>()
                .Property(e => e.ValidationIP)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserProfile>()
                .HasMany(e => e.User)
                .WithRequired(e => e.UserProfile)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserStatus>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<UserStatus>()
                .HasMany(e => e.User)
                .WithRequired(e => e.UserStatus)
                .WillCascadeOnDelete(false);
        }
    }
}
