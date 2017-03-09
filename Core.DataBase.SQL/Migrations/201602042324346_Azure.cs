namespace Heeelp.Core.DataBase.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Azure : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressType",
                c => new
                    {
                        AddressTypeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AddressTypeId);
            
            CreateTable(
                "dbo.PersonAddress",
                c => new
                    {
                        PersonAddressId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        AddressTypeId = c.Byte(nullable: false),
                        StartDateUTC = c.DateTime(nullable: false),
                        StreetName = c.String(maxLength: 150, unicode: false),
                        Number = c.Int(),
                        NeighbourhoodId = c.Int(nullable: false),
                        PostCode = c.String(nullable: false, maxLength: 15, unicode: false),
                        Coordinates = c.String(),
                        ContactPhoneNumber = c.String(nullable: false, maxLength: 15, unicode: false),
                        ServerInstanceId = c.Short(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        ContactEMail = c.String(maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonAddressId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .ForeignKey("dbo.User", t => t.CreatedBy)
                .ForeignKey("dbo.Neighbourhood", t => t.NeighbourhoodId)
                .ForeignKey("dbo.AddressType", t => t.AddressTypeId)
                .Index(t => t.PersonId)
                .Index(t => t.AddressTypeId)
                .Index(t => t.NeighbourhoodId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Neighbourhood",
                c => new
                    {
                        NeighbourhoodId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        CityId = c.Int(nullable: false),
                        NeighbourhoodFatherId = c.Int(),
                        Coordinates = c.String(),
                        Active = c.Boolean(nullable: false),
                        InsertedDate = c.DateTime(nullable: false),
                        CityZoneId = c.Int(),
                        PostCode = c.String(maxLength: 15, unicode: false),
                    })
                .PrimaryKey(t => t.NeighbourhoodId)
                .ForeignKey("dbo.CityZone", t => t.CityZoneId)
                .ForeignKey("dbo.Neighbourhood", t => t.NeighbourhoodFatherId)
                .Index(t => t.NeighbourhoodFatherId)
                .Index(t => t.CityZoneId);
            
            CreateTable(
                "dbo.CityZone",
                c => new
                    {
                        CityZoneId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Code = c.String(maxLength: 10, unicode: false),
                        CityId = c.Int(nullable: false),
                        InsertedDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CityZoneId)
                .ForeignKey("dbo.City", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 70, unicode: false),
                        StateRegionId = c.Int(nullable: false),
                        Coordinates = c.String(nullable: false),
                        PostCode = c.String(maxLength: 15, unicode: false),
                        Active = c.Boolean(nullable: false),
                        InsertedDate = c.DateTime(nullable: false),
                        PhoneCode = c.String(maxLength: 2, fixedLength: true),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.StateRegion", t => t.StateRegionId)
                .Index(t => t.StateRegionId);
            
            CreateTable(
                "dbo.StateRegion",
                c => new
                    {
                        StateRegionId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        StateId = c.Int(nullable: false),
                        Coordinates = c.String(),
                        Active = c.Boolean(),
                        InsertedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.StateRegionId)
                .ForeignKey("dbo.State", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        StateId = c.Int(nullable: false),
                        Name = c.String(maxLength: 50, unicode: false),
                        Code = c.String(maxLength: 5, unicode: false),
                        CountryRegionId = c.Int(),
                        Coordinates = c.String(),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.StateId)
                .ForeignKey("dbo.CountryRegion", t => t.CountryRegionId)
                .Index(t => t.CountryRegionId);
            
            CreateTable(
                "dbo.CountryRegion",
                c => new
                    {
                        CountryRegionId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        CountryId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.CountryRegionId)
                .ForeignKey("dbo.Country", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        CountryId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Code = c.String(nullable: false, maxLength: 3, unicode: false),
                        PhoneCode = c.String(nullable: false, maxLength: 2, fixedLength: true, unicode: false),
                        LanguageId = c.Byte(nullable: false),
                        CurrencyId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.CountryId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        LanguageId = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        LanguageCulture = c.String(nullable: false, maxLength: 10, unicode: false),
                        FlagImage = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LanguageId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        IntegrationCode = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 70, unicode: false),
                        FantasyName = c.String(maxLength: 50, unicode: false),
                        NameFromSecurityCheck = c.String(maxLength: 50, unicode: false),
                        SecuritySourceId = c.Int(),
                        IsSafe = c.Boolean(),
                        FriendlyNameURL = c.String(maxLength: 50, unicode: false),
                        PersonOriginTypeId = c.Byte(nullable: false),
                        PersonOriginDetails = c.String(maxLength: 150, unicode: false),
                        CampaignId = c.Long(),
                        CountryId = c.Byte(nullable: false),
                        LanguageId = c.Byte(nullable: false),
                        PersonTypeId = c.Byte(nullable: false),
                        PersonProfileId = c.Byte(nullable: false),
                        PersonStatusId = c.Byte(nullable: false),
                        PersonalWebSite = c.String(maxLength: 150, unicode: false),
                        CurrencyId = c.Byte(nullable: false),
                        CreationDateUTC = c.DateTime(nullable: false),
                        ActivationCode = c.String(maxLength: 50, unicode: false),
                        ActivationDateUTC = c.DateTime(),
                        PhoneNumber = c.String(maxLength: 50, unicode: false),
                        PersonFatherId = c.Int(),
                        InviteId = c.Long(),
                        ServerInstanceId = c.Short(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Country", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Currency", t => t.CurrencyId)
                .ForeignKey("dbo.Person", t => t.PersonFatherId)
                .ForeignKey("dbo.ServerInstance", t => t.ServerInstanceId)
                .ForeignKey("dbo.PersonOriginType", t => t.PersonOriginTypeId)
                .ForeignKey("dbo.PersonProfile", t => t.PersonProfileId)
                .ForeignKey("dbo.PersonStatus", t => t.PersonStatusId)
                .ForeignKey("dbo.PersonType", t => t.PersonTypeId)
                .ForeignKey("dbo.SecuritySource", t => t.SecuritySourceId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.SecuritySourceId)
                .Index(t => t.PersonOriginTypeId)
                .Index(t => t.CountryId)
                .Index(t => t.LanguageId)
                .Index(t => t.PersonTypeId)
                .Index(t => t.PersonProfileId)
                .Index(t => t.PersonStatusId)
                .Index(t => t.CurrencyId)
                .Index(t => t.PersonFatherId)
                .Index(t => t.ServerInstanceId);
            
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        CurrencyId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Symbol = c.String(nullable: false, maxLength: 4, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CurrencyId);
            
            CreateTable(
                "dbo.PersonContract",
                c => new
                    {
                        PersonContractId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        ContractId = c.Short(nullable: false),
                        UserId = c.Int(nullable: false),
                        AgreementDateUTC = c.DateTime(nullable: false),
                        Term = c.String(nullable: false, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonContractId)
                .ForeignKey("dbo.Contract", t => t.ContractId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.Contract",
                c => new
                    {
                        ContractId = c.Short(nullable: false),
                        Term = c.String(unicode: false),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        Active = c.Boolean(),
                        ContractTypeId = c.Byte(),
                        ContractType_ContractTypeId = c.Short(),
                    })
                .PrimaryKey(t => t.ContractId)
                .ForeignKey("dbo.ContractType", t => t.ContractType_ContractTypeId)
                .Index(t => t.ContractType_ContractTypeId);
            
            CreateTable(
                "dbo.ContractType",
                c => new
                    {
                        ContractTypeId = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContractTypeId);
            
            CreateTable(
                "dbo.PersonDocument",
                c => new
                    {
                        PersonDocumentId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        DocumentTypeId = c.Short(nullable: false),
                        Number = c.String(maxLength: 50, unicode: false),
                        Complement = c.String(maxLength: 50, unicode: false),
                        DateIssued = c.DateTime(storeType: "date"),
                        DateValidUntil = c.DateTime(storeType: "date"),
                        InsertedDateUTC = c.DateTime(),
                        FileId = c.Long(nullable: false),
                        Active = c.Boolean(),
                    })
                .PrimaryKey(t => t.PersonDocumentId)
                .ForeignKey("dbo.DocumentType", t => t.DocumentTypeId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.DocumentType",
                c => new
                    {
                        DocumentTypeId = c.Short(nullable: false),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DocumentTypeId);
            
            CreateTable(
                "dbo.PersonExpertise",
                c => new
                    {
                        PersonPageExpertiseId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        ExpertiseId = c.Int(nullable: false),
                        InsertedDateUTC = c.DateTime(nullable: false),
                        InsertedBy = c.Int(nullable: false),
                        ServerInstanceId = c.Short(nullable: false),
                        CustomPhotoFileId = c.Long(),
                        CustomDescription = c.String(maxLength: 150, unicode: false),
                        ExhibitionOrder = c.Byte(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonPageExpertiseId)
                .ForeignKey("dbo.Expertise", t => t.ExpertiseId)
                .ForeignKey("dbo.ServerInstance", t => t.ServerInstanceId)
                .ForeignKey("dbo.User", t => t.InsertedBy)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.ExpertiseId)
                .Index(t => t.InsertedBy)
                .Index(t => t.ServerInstanceId);
            
            CreateTable(
                "dbo.Expertise",
                c => new
                    {
                        ExpertiseId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                        ExpertiseFatherId = c.Int(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDateUTC = c.DateTime(nullable: false),
                        ApprovalStatusId = c.Byte(nullable: false),
                        ApprovedBy = c.Int(nullable: false),
                        ApprovedDate = c.DateTime(nullable: false),
                        DefaultDescription = c.String(nullable: false, maxLength: 150, unicode: false),
                        IsPriceDefinedEditorially = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ExpertiseId)
                .ForeignKey("dbo.ApprovalStatus", t => t.ApprovalStatusId)
                .ForeignKey("dbo.Expertise", t => t.ExpertiseFatherId)
                .ForeignKey("dbo.User", t => t.ApprovedBy)
                .ForeignKey("dbo.User", t => t.CreatedBy)
                .Index(t => t.ExpertiseFatherId)
                .Index(t => t.CreatedBy)
                .Index(t => t.ApprovalStatusId)
                .Index(t => t.ApprovedBy);
            
            CreateTable(
                "dbo.ApprovalStatus",
                c => new
                    {
                        ApprovalStatusId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ApprovalStatusId);
            
            CreateTable(
                "dbo.ExpertiseHistory",
                c => new
                    {
                        ExpertiseHistoryId = c.Int(nullable: false, identity: true),
                        ExpertiseId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                        ExpertiseFatherId = c.Int(),
                        CreatedBy = c.Int(nullable: false),
                        CreatedDateUTC = c.DateTime(nullable: false),
                        ApprovalStatusId = c.Byte(nullable: false),
                        ApprovedBy = c.Int(nullable: false),
                        ApprovedDate = c.DateTime(nullable: false),
                        DefaultIconFileId = c.Long(nullable: false),
                        DefaultPhotoFileId = c.Long(nullable: false),
                        DefaultDescription = c.String(nullable: false, maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.ExpertiseHistoryId)
                .ForeignKey("dbo.Expertise", t => t.ExpertiseId)
                .Index(t => t.ExpertiseId);
            
            CreateTable(
                "dbo.ExpertisePhoto",
                c => new
                    {
                        ExpertisePhotoId = c.Int(nullable: false, identity: true),
                        ExpertiseId = c.Int(nullable: false),
                        FileId = c.Long(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ExpertisePhotoId)
                .ForeignKey("dbo.Expertise", t => t.ExpertiseId)
                .Index(t => t.ExpertiseId);
            
            CreateTable(
                "dbo.PersonInterest",
                c => new
                    {
                        PersonInterestId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        ExpertiseId = c.Int(nullable: false),
                        DisplayOrder = c.Int(),
                        InsertedDateUTC = c.DateTime(nullable: false),
                        InsertedBy = c.Int(),
                        ServerInstanceId = c.Short(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonInterestId)
                .ForeignKey("dbo.ServerInstance", t => t.ServerInstanceId)
                .ForeignKey("dbo.User", t => t.InsertedBy)
                .ForeignKey("dbo.Expertise", t => t.ExpertiseId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.ExpertiseId)
                .Index(t => t.InsertedBy)
                .Index(t => t.ServerInstanceId);
            
            CreateTable(
                "dbo.ServerInstance",
                c => new
                    {
                        ServerInstanceId = c.Short(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ServerInstanceId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        IntegrationCode = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        SecundaryEmail = c.String(maxLength: 50, unicode: false),
                        SmartPhoneNumber = c.String(maxLength: 20, unicode: false),
                        PersonId = c.Int(nullable: false),
                        IsDefaultUser = c.Boolean(nullable: false),
                        UserProfileId = c.Byte(nullable: false),
                        UserStatusId = c.Byte(nullable: false),
                        AuthenticationModeId = c.Int(nullable: false),
                        LanguageId = c.Byte(),
                        CreationDateUTC = c.DateTime(nullable: false),
                        ValidationDateUTC = c.DateTime(),
                        FormFillTime = c.Short(),
                        ValidationToken = c.String(maxLength: 15, unicode: false),
                        SecurityCheckNecessary = c.Boolean(),
                        ValidationAttempts = c.Short(),
                        LoginFailAttempts = c.Short(),
                        LastLoginFailUTC = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        EnrollmentIP = c.String(nullable: false, maxLength: 20, unicode: false),
                        ValidationIP = c.String(maxLength: 20, unicode: false),
                        CreatedBy = c.Int(),
                        ServerInstanceId = c.Short(nullable: false),
                        IsPerpetual = c.Boolean(),
                        UserProfile_UserProfileId = c.Int(nullable: false),
                        UserStatus_UserStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.User", t => t.CreatedBy)
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_UserProfileId)
                .ForeignKey("dbo.UserStatus", t => t.UserStatus_UserStatusId)
                .ForeignKey("dbo.ServerInstance", t => t.ServerInstanceId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.LanguageId)
                .Index(t => t.CreatedBy)
                .Index(t => t.ServerInstanceId)
                .Index(t => t.UserProfile_UserProfileId)
                .Index(t => t.UserStatus_UserStatusId);
            
            CreateTable(
                "dbo.PersonFile",
                c => new
                    {
                        PersonFileId = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        FileId = c.Long(nullable: false),
                        AssociatedDateUTC = c.DateTime(nullable: false),
                        AssocietedBy = c.Int(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonFileId)
                .ForeignKey("dbo.User", t => t.AssocietedBy)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId)
                .Index(t => t.AssocietedBy);
            
            CreateTable(
                "dbo.UserFile",
                c => new
                    {
                        UserFileId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FileId = c.Long(nullable: false),
                        AssociatedDateUTC = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserFileId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserGroupUser",
                c => new
                    {
                        UserGroupUserId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        UserGroupId = c.Int(nullable: false),
                        InsertedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserGroupUserId)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.UserGroupId);
            
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        UserGroupId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserGroupId);
            
            CreateTable(
                "dbo.UserGroupMenu",
                c => new
                    {
                        UserGroupMenuId = c.Int(nullable: false),
                        UserGroupId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                        InsertedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserGroupMenuId)
                .ForeignKey("dbo.Menu", t => t.MenuId)
                .ForeignKey("dbo.UserGroup", t => t.UserGroupId)
                .Index(t => t.UserGroupId)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        MenuId = c.Int(nullable: false),
                        MenuText = c.String(nullable: false, maxLength: 50, unicode: false),
                        PageId = c.Int(nullable: false),
                        MenuAlt = c.String(nullable: false, maxLength: 250, unicode: false),
                        MenuIcon = c.String(nullable: false, maxLength: 250, unicode: false),
                        MenuFatherId = c.Int(),
                    })
                .PrimaryKey(t => t.MenuId)
                .ForeignKey("dbo.Menu", t => t.MenuFatherId)
                .ForeignKey("dbo.Page", t => t.PageId)
                .Index(t => t.PageId)
                .Index(t => t.MenuFatherId);
            
            CreateTable(
                "dbo.Page",
                c => new
                    {
                        PageId = c.Int(nullable: false, identity: true),
                        PageURL = c.String(nullable: false, maxLength: 250, unicode: false),
                        PageTitle = c.String(nullable: false, maxLength: 100, unicode: false),
                        PageDescription = c.String(maxLength: 250, unicode: false),
                        PageTypeId = c.Byte(nullable: false),
                        NumberOfAdvertisingSpaces = c.Short(nullable: false),
                        PageFatherId = c.Int(nullable: false),
                        PageTemplateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PageId)
                .ForeignKey("dbo.Page", t => t.PageFatherId)
                .ForeignKey("dbo.PageType", t => t.PageTypeId)
                .Index(t => t.PageTypeId)
                .Index(t => t.PageFatherId);
            
            CreateTable(
                "dbo.PageType",
                c => new
                    {
                        PageTypeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PageTypeId);
            
            CreateTable(
                "dbo.UserHistory",
                c => new
                    {
                        UserHistoryId = c.Long(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        IntegrationCode = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        SecundaryEmail = c.String(maxLength: 50, unicode: false),
                        SmartPhoneNumber = c.String(maxLength: 20, unicode: false),
                        PersonId = c.Int(nullable: false),
                        IsDefaultUser = c.Boolean(nullable: false),
                        UserProfileId = c.Byte(nullable: false),
                        UserStatusId = c.Byte(nullable: false),
                        AuthenticationModeId = c.Int(nullable: false),
                        LanguageId = c.Byte(),
                        CreationDateUTC = c.DateTime(nullable: false),
                        ValidationDateUTC = c.DateTime(),
                        FormFillTime = c.Short(),
                        ValidationToken = c.String(maxLength: 15, unicode: false),
                        SecurityCheckNecessary = c.Boolean(),
                        ValidationAttempts = c.Short(),
                        LoginFailAttempts = c.Short(),
                        LastLoginFailUTC = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                        EnrollmentIP = c.String(nullable: false, maxLength: 20, unicode: false),
                        ValidationIP = c.String(maxLength: 20, unicode: false),
                        CreatedBy = c.Int(),
                        ServerInstanceId = c.Short(nullable: false),
                        IsPerpetual = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserHistoryId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserModule",
                c => new
                    {
                        UserModuleId = c.Long(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ModuleId = c.Short(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserModuleId)
                .ForeignKey("dbo.Module", t => t.ModuleId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        ModuleId = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleId);
            
            CreateTable(
                "dbo.UserPreference",
                c => new
                    {
                        UserPreferenceId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CouponEstatisticModeId = c.Byte(),
                        CouponPeriodModeId = c.Byte(),
                        SaveRecentQueries = c.Boolean(nullable: false),
                        ShowRecentQueries = c.Byte(),
                        ShowRecentCoupons = c.Boolean(),
                        ShowRecentReviews = c.Boolean(),
                        ShowRecentCheckins = c.Boolean(),
                        ShowPendentActions = c.Boolean(),
                        AcceptReceiveAlertAboutInterests = c.Boolean(nullable: false),
                        AcceptReceiveAlertAboutContent = c.Boolean(nullable: false),
                        AcceptReceiveAlertAboutEvents = c.Boolean(nullable: false),
                        AcceptReceiveWizardSuggestions = c.Boolean(nullable: false),
                        AcceptReceiveNewsletterOffers = c.Boolean(nullable: false),
                        ShowFastProfileBar = c.Boolean(nullable: false),
                        ToggleLeftNavigationMenu = c.Boolean(nullable: false),
                        SelectedSkin = c.Byte(nullable: false),
                        ConfigurationDateUTC = c.DateTime(nullable: false),
                        ShowFriendsActivity = c.Boolean(nullable: false),
                        ShareActivitiesWithFriends = c.Boolean(nullable: false),
                        SearchDistance = c.Byte(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserPreferenceId)
                .ForeignKey("dbo.CouponEstatisticMode", t => t.CouponEstatisticModeId)
                .ForeignKey("dbo.CouponPeriodMode", t => t.CouponPeriodModeId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CouponEstatisticModeId)
                .Index(t => t.CouponPeriodModeId);
            
            CreateTable(
                "dbo.CouponEstatisticMode",
                c => new
                    {
                        CouponEstatisticModeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CouponEstatisticModeId);
            
            CreateTable(
                "dbo.CouponPeriodMode",
                c => new
                    {
                        CouponPeriodModeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CouponPeriodModeId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserProfileId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserProfileId);
            
            CreateTable(
                "dbo.UserStatus",
                c => new
                    {
                        UserStatusId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserStatusId);
            
            CreateTable(
                "dbo.PersonHistoric",
                c => new
                    {
                        PersonHistoricId = c.Long(nullable: false),
                        PersonId = c.Int(nullable: false),
                        IntegrationCode = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 70, unicode: false),
                        FantasyName = c.String(maxLength: 50, unicode: false),
                        NameFromSecurityCheck = c.String(maxLength: 50, unicode: false),
                        SecuritySourceId = c.Int(),
                        IsSafe = c.Boolean(),
                        FriendlyNameURL = c.String(maxLength: 50, unicode: false),
                        PersonOriginTypeId = c.Byte(nullable: false),
                        PersonOriginDetails = c.String(maxLength: 150, unicode: false),
                        CountryId = c.Byte(nullable: false),
                        LanguageId = c.Byte(nullable: false),
                        PersonTypeId = c.Byte(nullable: false),
                        PersonProfileId = c.Byte(nullable: false),
                        PersonStatusId = c.Byte(nullable: false),
                        PersonalWebSite = c.String(maxLength: 150, unicode: false),
                        CurrencyId = c.Byte(nullable: false),
                        CreationDateUTC = c.DateTime(nullable: false),
                        ActivationCode = c.String(maxLength: 50, unicode: false),
                        ActivationDateUTC = c.DateTime(),
                        PhoneNumber = c.String(maxLength: 50, unicode: false),
                        PersonFatherId = c.Int(),
                        InviteId = c.Long(),
                        ServerInstanceId = c.Short(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonHistoricId)
                .ForeignKey("dbo.Person", t => t.PersonId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.PersonOriginType",
                c => new
                    {
                        PersonOriginTypeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonOriginTypeId);
            
            CreateTable(
                "dbo.PersonProfile",
                c => new
                    {
                        PersonProfileId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonProfileId);
            
            CreateTable(
                "dbo.PersonStatus",
                c => new
                    {
                        PersonStatusId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonStatusId);
            
            CreateTable(
                "dbo.PersonType",
                c => new
                    {
                        PersonTypeId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PersonTypeId);
            
            CreateTable(
                "dbo.SecuritySource",
                c => new
                    {
                        SecuritySourceId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.SecuritySourceId);
            
            CreateTable(
                "dbo.AdminUser",
                c => new
                    {
                        AdminUserId = c.Int(nullable: false),
                        User = c.String(maxLength: 10, fixedLength: true),
                    })
                .PrimaryKey(t => t.AdminUserId);
            
            CreateTable(
                "dbo.CommandOrigin",
                c => new
                    {
                        CommandOriginId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CommandOriginId);
            
            CreateTable(
                "dbo.CommandQuee",
                c => new
                    {
                        CommandId = c.Guid(nullable: false),
                        CommandPayload = c.String(nullable: false, unicode: false),
                        CommandStatusId = c.Byte(nullable: false),
                        CreatedDateUTC = c.DateTime(nullable: false),
                        CommandTypeId = c.Short(nullable: false),
                        CommandOriginId = c.Byte(nullable: false),
                        ProcessAttempts = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.CommandId)
                .ForeignKey("dbo.CommandStatus", t => t.CommandStatusId)
                .ForeignKey("dbo.CommandType", t => t.CommandTypeId)
                .ForeignKey("dbo.CommandOrigin", t => t.CommandOriginId)
                .Index(t => t.CommandStatusId)
                .Index(t => t.CommandTypeId)
                .Index(t => t.CommandOriginId);
            
            CreateTable(
                "dbo.CommandStatus",
                c => new
                    {
                        CommandStatusId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CommandStatusId);
            
            CreateTable(
                "dbo.CommandWorkFlow",
                c => new
                    {
                        CommandWorkFlowId = c.Long(nullable: false),
                        CommandId = c.Guid(nullable: false),
                        CommandStatusId = c.Byte(nullable: false),
                        DateStartUTC = c.DateTime(nullable: false),
                        DateEndUTC = c.DateTime(nullable: false),
                        Details = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.CommandWorkFlowId)
                .ForeignKey("dbo.CommandStatus", t => t.CommandStatusId)
                .ForeignKey("dbo.CommandQuee", t => t.CommandId)
                .Index(t => t.CommandId)
                .Index(t => t.CommandStatusId);
            
            CreateTable(
                "dbo.CommandType",
                c => new
                    {
                        CommandTypeId = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Priority = c.Byte(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CommandTypeId);
            
            CreateTable(
                "dbo.EventQuee",
                c => new
                    {
                        EventId = c.Guid(nullable: false),
                        CommandId = c.Guid(nullable: false),
                        EventPayload = c.String(nullable: false, unicode: false),
                        EventStatusId = c.Byte(nullable: false),
                        CreatedDateUTC = c.DateTime(nullable: false),
                        EventTypeId = c.Short(nullable: false),
                        ProcessAttempts = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.EventStatus", t => t.EventStatusId)
                .ForeignKey("dbo.EventType", t => t.EventTypeId)
                .ForeignKey("dbo.CommandQuee", t => t.CommandId)
                .Index(t => t.CommandId)
                .Index(t => t.EventStatusId)
                .Index(t => t.EventTypeId);
            
            CreateTable(
                "dbo.EventStatus",
                c => new
                    {
                        EventStatusId = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventStatusId);
            
            CreateTable(
                "dbo.EventWorkflow",
                c => new
                    {
                        EventWorkflowId = c.Long(nullable: false),
                        EventId = c.Guid(nullable: false),
                        EventStatusId = c.Byte(nullable: false),
                        DateStartUTC = c.DateTime(nullable: false),
                        DateEndUTC = c.DateTime(nullable: false),
                        Details = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.EventWorkflowId)
                .ForeignKey("dbo.EventStatus", t => t.EventStatusId)
                .ForeignKey("dbo.EventQuee", t => t.EventId)
                .Index(t => t.EventId)
                .Index(t => t.EventStatusId);
            
            CreateTable(
                "dbo.EventType",
                c => new
                    {
                        EventTypeId = c.Short(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Priority = c.Byte(nullable: false),
                        Ative = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventTypeId);
            
            CreateTable(
                "dbo.SystemParameter",
                c => new
                    {
                        SystemParameterId = c.Int(nullable: false),
                        ParamName = c.String(maxLength: 50, unicode: false),
                        ParamValue = c.String(maxLength: 250, unicode: false),
                        AdminUserId = c.Int(),
                    })
                .PrimaryKey(t => t.SystemParameterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommandQuee", "CommandOriginId", "dbo.CommandOrigin");
            DropForeignKey("dbo.EventQuee", "CommandId", "dbo.CommandQuee");
            DropForeignKey("dbo.EventWorkflow", "EventId", "dbo.EventQuee");
            DropForeignKey("dbo.EventQuee", "EventTypeId", "dbo.EventType");
            DropForeignKey("dbo.EventWorkflow", "EventStatusId", "dbo.EventStatus");
            DropForeignKey("dbo.EventQuee", "EventStatusId", "dbo.EventStatus");
            DropForeignKey("dbo.CommandWorkFlow", "CommandId", "dbo.CommandQuee");
            DropForeignKey("dbo.CommandQuee", "CommandTypeId", "dbo.CommandType");
            DropForeignKey("dbo.CommandWorkFlow", "CommandStatusId", "dbo.CommandStatus");
            DropForeignKey("dbo.CommandQuee", "CommandStatusId", "dbo.CommandStatus");
            DropForeignKey("dbo.PersonAddress", "AddressTypeId", "dbo.AddressType");
            DropForeignKey("dbo.PersonAddress", "NeighbourhoodId", "dbo.Neighbourhood");
            DropForeignKey("dbo.Neighbourhood", "NeighbourhoodFatherId", "dbo.Neighbourhood");
            DropForeignKey("dbo.Neighbourhood", "CityZoneId", "dbo.CityZone");
            DropForeignKey("dbo.StateRegion", "StateId", "dbo.State");
            DropForeignKey("dbo.State", "CountryRegionId", "dbo.CountryRegion");
            DropForeignKey("dbo.Person", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.User", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Person", "SecuritySourceId", "dbo.SecuritySource");
            DropForeignKey("dbo.Person", "PersonTypeId", "dbo.PersonType");
            DropForeignKey("dbo.Person", "PersonStatusId", "dbo.PersonStatus");
            DropForeignKey("dbo.Person", "PersonProfileId", "dbo.PersonProfile");
            DropForeignKey("dbo.Person", "PersonOriginTypeId", "dbo.PersonOriginType");
            DropForeignKey("dbo.PersonInterest", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PersonHistoric", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PersonFile", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PersonExpertise", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PersonInterest", "ExpertiseId", "dbo.Expertise");
            DropForeignKey("dbo.User", "ServerInstanceId", "dbo.ServerInstance");
            DropForeignKey("dbo.User", "UserStatus_UserStatusId", "dbo.UserStatus");
            DropForeignKey("dbo.User", "UserProfile_UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.UserPreference", "UserId", "dbo.User");
            DropForeignKey("dbo.UserPreference", "CouponPeriodModeId", "dbo.CouponPeriodMode");
            DropForeignKey("dbo.UserPreference", "CouponEstatisticModeId", "dbo.CouponEstatisticMode");
            DropForeignKey("dbo.UserModule", "UserId", "dbo.User");
            DropForeignKey("dbo.UserModule", "ModuleId", "dbo.Module");
            DropForeignKey("dbo.UserHistory", "UserId", "dbo.User");
            DropForeignKey("dbo.UserGroupUser", "UserId", "dbo.User");
            DropForeignKey("dbo.UserGroupUser", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.UserGroupMenu", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.UserGroupMenu", "MenuId", "dbo.Menu");
            DropForeignKey("dbo.Page", "PageTypeId", "dbo.PageType");
            DropForeignKey("dbo.Page", "PageFatherId", "dbo.Page");
            DropForeignKey("dbo.Menu", "PageId", "dbo.Page");
            DropForeignKey("dbo.Menu", "MenuFatherId", "dbo.Menu");
            DropForeignKey("dbo.UserFile", "UserId", "dbo.User");
            DropForeignKey("dbo.User", "CreatedBy", "dbo.User");
            DropForeignKey("dbo.PersonInterest", "InsertedBy", "dbo.User");
            DropForeignKey("dbo.PersonFile", "AssocietedBy", "dbo.User");
            DropForeignKey("dbo.PersonExpertise", "InsertedBy", "dbo.User");
            DropForeignKey("dbo.PersonAddress", "CreatedBy", "dbo.User");
            DropForeignKey("dbo.User", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.Expertise", "CreatedBy", "dbo.User");
            DropForeignKey("dbo.Expertise", "ApprovedBy", "dbo.User");
            DropForeignKey("dbo.PersonInterest", "ServerInstanceId", "dbo.ServerInstance");
            DropForeignKey("dbo.PersonExpertise", "ServerInstanceId", "dbo.ServerInstance");
            DropForeignKey("dbo.Person", "ServerInstanceId", "dbo.ServerInstance");
            DropForeignKey("dbo.PersonExpertise", "ExpertiseId", "dbo.Expertise");
            DropForeignKey("dbo.ExpertisePhoto", "ExpertiseId", "dbo.Expertise");
            DropForeignKey("dbo.ExpertiseHistory", "ExpertiseId", "dbo.Expertise");
            DropForeignKey("dbo.Expertise", "ExpertiseFatherId", "dbo.Expertise");
            DropForeignKey("dbo.Expertise", "ApprovalStatusId", "dbo.ApprovalStatus");
            DropForeignKey("dbo.PersonDocument", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PersonDocument", "DocumentTypeId", "dbo.DocumentType");
            DropForeignKey("dbo.PersonContract", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PersonContract", "ContractId", "dbo.Contract");
            DropForeignKey("dbo.Contract", "ContractType_ContractTypeId", "dbo.ContractType");
            DropForeignKey("dbo.PersonAddress", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Person", "PersonFatherId", "dbo.Person");
            DropForeignKey("dbo.Person", "CurrencyId", "dbo.Currency");
            DropForeignKey("dbo.Person", "CountryId", "dbo.Country");
            DropForeignKey("dbo.Country", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.CountryRegion", "CountryId", "dbo.Country");
            DropForeignKey("dbo.City", "StateRegionId", "dbo.StateRegion");
            DropForeignKey("dbo.CityZone", "CityId", "dbo.City");
            DropIndex("dbo.EventWorkflow", new[] { "EventStatusId" });
            DropIndex("dbo.EventWorkflow", new[] { "EventId" });
            DropIndex("dbo.EventQuee", new[] { "EventTypeId" });
            DropIndex("dbo.EventQuee", new[] { "EventStatusId" });
            DropIndex("dbo.EventQuee", new[] { "CommandId" });
            DropIndex("dbo.CommandWorkFlow", new[] { "CommandStatusId" });
            DropIndex("dbo.CommandWorkFlow", new[] { "CommandId" });
            DropIndex("dbo.CommandQuee", new[] { "CommandOriginId" });
            DropIndex("dbo.CommandQuee", new[] { "CommandTypeId" });
            DropIndex("dbo.CommandQuee", new[] { "CommandStatusId" });
            DropIndex("dbo.PersonHistoric", new[] { "PersonId" });
            DropIndex("dbo.UserPreference", new[] { "CouponPeriodModeId" });
            DropIndex("dbo.UserPreference", new[] { "CouponEstatisticModeId" });
            DropIndex("dbo.UserPreference", new[] { "UserId" });
            DropIndex("dbo.UserModule", new[] { "ModuleId" });
            DropIndex("dbo.UserModule", new[] { "UserId" });
            DropIndex("dbo.UserHistory", new[] { "UserId" });
            DropIndex("dbo.Page", new[] { "PageFatherId" });
            DropIndex("dbo.Page", new[] { "PageTypeId" });
            DropIndex("dbo.Menu", new[] { "MenuFatherId" });
            DropIndex("dbo.Menu", new[] { "PageId" });
            DropIndex("dbo.UserGroupMenu", new[] { "MenuId" });
            DropIndex("dbo.UserGroupMenu", new[] { "UserGroupId" });
            DropIndex("dbo.UserGroupUser", new[] { "UserGroupId" });
            DropIndex("dbo.UserGroupUser", new[] { "UserId" });
            DropIndex("dbo.UserFile", new[] { "UserId" });
            DropIndex("dbo.PersonFile", new[] { "AssocietedBy" });
            DropIndex("dbo.PersonFile", new[] { "PersonId" });
            DropIndex("dbo.User", new[] { "UserStatus_UserStatusId" });
            DropIndex("dbo.User", new[] { "UserProfile_UserProfileId" });
            DropIndex("dbo.User", new[] { "ServerInstanceId" });
            DropIndex("dbo.User", new[] { "CreatedBy" });
            DropIndex("dbo.User", new[] { "LanguageId" });
            DropIndex("dbo.User", new[] { "PersonId" });
            DropIndex("dbo.PersonInterest", new[] { "ServerInstanceId" });
            DropIndex("dbo.PersonInterest", new[] { "InsertedBy" });
            DropIndex("dbo.PersonInterest", new[] { "ExpertiseId" });
            DropIndex("dbo.PersonInterest", new[] { "PersonId" });
            DropIndex("dbo.ExpertisePhoto", new[] { "ExpertiseId" });
            DropIndex("dbo.ExpertiseHistory", new[] { "ExpertiseId" });
            DropIndex("dbo.Expertise", new[] { "ApprovedBy" });
            DropIndex("dbo.Expertise", new[] { "ApprovalStatusId" });
            DropIndex("dbo.Expertise", new[] { "CreatedBy" });
            DropIndex("dbo.Expertise", new[] { "ExpertiseFatherId" });
            DropIndex("dbo.PersonExpertise", new[] { "ServerInstanceId" });
            DropIndex("dbo.PersonExpertise", new[] { "InsertedBy" });
            DropIndex("dbo.PersonExpertise", new[] { "ExpertiseId" });
            DropIndex("dbo.PersonExpertise", new[] { "PersonId" });
            DropIndex("dbo.PersonDocument", new[] { "DocumentTypeId" });
            DropIndex("dbo.PersonDocument", new[] { "PersonId" });
            DropIndex("dbo.Contract", new[] { "ContractType_ContractTypeId" });
            DropIndex("dbo.PersonContract", new[] { "ContractId" });
            DropIndex("dbo.PersonContract", new[] { "PersonId" });
            DropIndex("dbo.Person", new[] { "ServerInstanceId" });
            DropIndex("dbo.Person", new[] { "PersonFatherId" });
            DropIndex("dbo.Person", new[] { "CurrencyId" });
            DropIndex("dbo.Person", new[] { "PersonStatusId" });
            DropIndex("dbo.Person", new[] { "PersonProfileId" });
            DropIndex("dbo.Person", new[] { "PersonTypeId" });
            DropIndex("dbo.Person", new[] { "LanguageId" });
            DropIndex("dbo.Person", new[] { "CountryId" });
            DropIndex("dbo.Person", new[] { "PersonOriginTypeId" });
            DropIndex("dbo.Person", new[] { "SecuritySourceId" });
            DropIndex("dbo.Country", new[] { "LanguageId" });
            DropIndex("dbo.CountryRegion", new[] { "CountryId" });
            DropIndex("dbo.State", new[] { "CountryRegionId" });
            DropIndex("dbo.StateRegion", new[] { "StateId" });
            DropIndex("dbo.City", new[] { "StateRegionId" });
            DropIndex("dbo.CityZone", new[] { "CityId" });
            DropIndex("dbo.Neighbourhood", new[] { "CityZoneId" });
            DropIndex("dbo.Neighbourhood", new[] { "NeighbourhoodFatherId" });
            DropIndex("dbo.PersonAddress", new[] { "CreatedBy" });
            DropIndex("dbo.PersonAddress", new[] { "NeighbourhoodId" });
            DropIndex("dbo.PersonAddress", new[] { "AddressTypeId" });
            DropIndex("dbo.PersonAddress", new[] { "PersonId" });
            DropTable("dbo.SystemParameter");
            DropTable("dbo.EventType");
            DropTable("dbo.EventWorkflow");
            DropTable("dbo.EventStatus");
            DropTable("dbo.EventQuee");
            DropTable("dbo.CommandType");
            DropTable("dbo.CommandWorkFlow");
            DropTable("dbo.CommandStatus");
            DropTable("dbo.CommandQuee");
            DropTable("dbo.CommandOrigin");
            DropTable("dbo.AdminUser");
            DropTable("dbo.SecuritySource");
            DropTable("dbo.PersonType");
            DropTable("dbo.PersonStatus");
            DropTable("dbo.PersonProfile");
            DropTable("dbo.PersonOriginType");
            DropTable("dbo.PersonHistoric");
            DropTable("dbo.UserStatus");
            DropTable("dbo.UserProfile");
            DropTable("dbo.CouponPeriodMode");
            DropTable("dbo.CouponEstatisticMode");
            DropTable("dbo.UserPreference");
            DropTable("dbo.Module");
            DropTable("dbo.UserModule");
            DropTable("dbo.UserHistory");
            DropTable("dbo.PageType");
            DropTable("dbo.Page");
            DropTable("dbo.Menu");
            DropTable("dbo.UserGroupMenu");
            DropTable("dbo.UserGroup");
            DropTable("dbo.UserGroupUser");
            DropTable("dbo.UserFile");
            DropTable("dbo.PersonFile");
            DropTable("dbo.User");
            DropTable("dbo.ServerInstance");
            DropTable("dbo.PersonInterest");
            DropTable("dbo.ExpertisePhoto");
            DropTable("dbo.ExpertiseHistory");
            DropTable("dbo.ApprovalStatus");
            DropTable("dbo.Expertise");
            DropTable("dbo.PersonExpertise");
            DropTable("dbo.DocumentType");
            DropTable("dbo.PersonDocument");
            DropTable("dbo.ContractType");
            DropTable("dbo.Contract");
            DropTable("dbo.PersonContract");
            DropTable("dbo.Currency");
            DropTable("dbo.Person");
            DropTable("dbo.Language");
            DropTable("dbo.Country");
            DropTable("dbo.CountryRegion");
            DropTable("dbo.State");
            DropTable("dbo.StateRegion");
            DropTable("dbo.City");
            DropTable("dbo.CityZone");
            DropTable("dbo.Neighbourhood");
            DropTable("dbo.PersonAddress");
            DropTable("dbo.AddressType");
        }
    }
}
