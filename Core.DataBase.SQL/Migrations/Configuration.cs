namespace Heeelp.Core.DataBase.SQL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Core.DataBase.SQL;
    using Heeelp.Core.Domain;

    internal sealed class Configuration : DbMigrationsConfiguration<Heeelp.Core.DataBase.SQL.HeeelpDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Core.DataBase.SQL.HeeelpDataContext context)
        {
            //  This method will be called after migrating to the latest version.
            //context.AddressType.AddOrUpdate(x => x.AddressTypeId,
            //    new AddressType() { AddressTypeId = (byte)DomainEnumerators.enumAddressType.Home, Name = "Residencial", Active = true },
            //    new AddressType() { AddressTypeId = (byte)DomainEnumerators.enumAddressType.Business, Name = "Comercial", Active = true },
            //    new AddressType() { AddressTypeId = (byte)DomainEnumerators.enumAddressType.Billing, Name = "Cobran�a", Active = true }
            //);

            //context.ApprovalStatus.AddOrUpdate(x => x.ApprovalStatusId,
            //    new ApprovalStatus() { ApprovalStatusId = (byte)DomainEnumerators.enumApprovalStatus.WaitingAnalysis, Name = "Aguardando An�lise", Active = true },
            //    new ApprovalStatus() { ApprovalStatusId = (byte)DomainEnumerators.enumApprovalStatus.Approved, Name = "Aprovado", Active = true },
            //    new ApprovalStatus() { ApprovalStatusId = (byte)DomainEnumerators.enumApprovalStatus.Rejected, Name = "Rejeitado", Active = true }
            //);

            //context.CommandOrigin.AddOrUpdate(x => x.CommandOriginId,
            //    new CommandOrigin() { CommandOriginId = (byte)DomainEnumerators.enumCommandOrigin.UI, Name = "Interface do Usu�rio", Active = true },
            //    new CommandOrigin() { CommandOriginId = (byte)DomainEnumerators.enumCommandOrigin.EventHandler, Name = "Processamento de Evento", Active = true }
            //);

            //context.CommandStatus.AddOrUpdate(x => x.CommandStatusId,
            //    new CommandStatus() { CommandStatusId = (byte)DomainEnumerators.enumCommandStatus.WaitingProcess, Name = "Aguardando Procesamento", Active = true },
            //    new CommandStatus() { CommandStatusId = (byte)DomainEnumerators.enumCommandStatus.InProcess, Name = "Em Procesamento", Active = true },
            //    new CommandStatus() { CommandStatusId = (byte)DomainEnumerators.enumCommandStatus.Sucess, Name = "Sucesso", Active = true },
            //    new CommandStatus() { CommandStatusId = (byte)DomainEnumerators.enumCommandStatus.TemporaryFailure, Name = "Falha Tempor�ria", Active = true },
            //    new CommandStatus() { CommandStatusId = (byte)DomainEnumerators.enumCommandStatus.DefinitiveFailure, Name = "Falha Definitiva", Active = true }
            //);

            //context.ContractType.AddOrUpdate(x => x.ContractTypeId,
            //    new ContractType() { ContractTypeId = (byte)DomainEnumerators.enumContractType.User, Name = "Usu�rio", Active = true },
            //    new ContractType() { ContractTypeId = (byte)DomainEnumerators.enumContractType.ServiceProvider, Name = "Prestador de Servi�os", Active = true },
            //    new ContractType() { ContractTypeId = (byte)DomainEnumerators.enumContractType.Advertiser, Name = "Anunciante", Active = true }
            //);

            //context.CouponEstatisticMode.AddOrUpdate(x => x.CouponEstatisticModeId,
            //    new CouponEstatisticMode() { CouponEstatisticModeId = (byte)DomainEnumerators.enumCouponEstatisticMode.Simple, Name = "Simples", Active = true },
            //    new CouponEstatisticMode() { CouponEstatisticModeId = (byte)DomainEnumerators.enumCouponEstatisticMode.Detailed, Name = "Detalhado", Active = true }
            //);

            //context.CouponPeriodMode.AddOrUpdate(x => x.CouponPeriodModeId,
            //    new CouponPeriodMode() { CouponPeriodModeId = (byte)DomainEnumerators.enumCouponPeriodMode.Year, Name = "Ano", Active = true },
            //    new CouponPeriodMode() { CouponPeriodModeId = (byte)DomainEnumerators.enumCouponPeriodMode.Month, Name = "M�s", Active = true },
            //    new CouponPeriodMode() { CouponPeriodModeId = (byte)DomainEnumerators.enumCouponPeriodMode.Week, Name = "Semana", Active = true }
            //);

            //context.DocumentType.AddOrUpdate(x => x.DocumentTypeId,
            //    new DocumentType() { DocumentTypeId = (byte)DomainEnumerators.enumDocumentType.CPF, Name = "CPF" },
            //    new DocumentType() { DocumentTypeId = (byte)DomainEnumerators.enumDocumentType.RG, Name = "RG" }
            //);

            //context.EventStatus.AddOrUpdate(x => x.EventStatusId,
            //    new EventStatus() { EventStatusId = (byte)DomainEnumerators.enumEventStatus.WaitingProcess, Name = "Aguardando Processamento", Active = true },
            //    new EventStatus() { EventStatusId = (byte)DomainEnumerators.enumEventStatus.InProcess, Name = "Em Procesamento", Active = true },
            //    new EventStatus() { EventStatusId = (byte)DomainEnumerators.enumEventStatus.Sucess, Name = "Sucesso", Active = true },
            //    new EventStatus() { EventStatusId = (byte)DomainEnumerators.enumEventStatus.TemporaryFailure, Name = "Falha Tempor�ria", Active = true },
            //    new EventStatus() { EventStatusId = (byte)DomainEnumerators.enumEventStatus.DefinitiveFailure, Name = "Falha Definitiva", Active = true }
            //);

            //context.PageType.AddOrUpdate(x => x.PageTypeId,
            //    new PageType() { PageTypeId = (byte)DomainEnumerators.enumPageType.LandingPage, Name = "Landing Page" },
            //    new PageType() { PageTypeId = (byte)DomainEnumerators.enumPageType.FanPage, Name = "Fan Page" },
            //    new PageType() { PageTypeId = (byte)DomainEnumerators.enumPageType.Institutional, Name = "Institucional" },
            //    new PageType() { PageTypeId = (byte)DomainEnumerators.enumPageType.Administrative, Name = "Administrativo" },
            //    new PageType() { PageTypeId = (byte)DomainEnumerators.enumPageType.Management, Name = "Gerenciamento" }
            //);

            //context.PersonOriginType.AddOrUpdate(x => x.PersonOriginTypeId,
            //    new PersonOriginType() { PersonOriginTypeId = (byte)DomainEnumerators.enumPersonOriginType.DirectAccess, Name = "Acesso Direto", Active = true },
            //    new PersonOriginType() { PersonOriginTypeId = (byte)DomainEnumerators.enumPersonOriginType.OrganicSearch, Name = "Mecanismo de Busca Organico", Active = true },
            //    new PersonOriginType() { PersonOriginTypeId = (byte)DomainEnumerators.enumPersonOriginType.MarketingCampaign, Name = "Campanha de Marketing", Active = true }
            //);

            //context.PersonProfile.AddOrUpdate(x => x.PersonProfileId,
            //    new PersonProfile() { PersonProfileId = (byte)DomainEnumerators.enumPersonProfile.User, Name = "Usu�rio", Active = true },
            //    new PersonProfile() { PersonProfileId = (byte)DomainEnumerators.enumPersonProfile.ServiceProvider, Name = "Prestador de Servi�os", Active = true },
            //    new PersonProfile() { PersonProfileId = (byte)DomainEnumerators.enumPersonProfile.Advertiser, Name = "Anunciante", Active = true },
            //    new PersonProfile() { PersonProfileId = (byte)DomainEnumerators.enumPersonProfile.SystemAdministrator, Name = "Adminstrador do Sistema", Active = true }
            //);

            //context.PersonStatus.AddOrUpdate(x => x.PersonStatusId,
            //    new PersonStatus() { PersonStatusId = (byte)DomainEnumerators.enumPersonStatus.WaitingAnalysis, Name = "Aguardando An�lise", Active = true },
            //    new PersonStatus() { PersonStatusId = (byte)DomainEnumerators.enumPersonStatus.WaitingActivation, Name = "Aguardando Ativa��o", Active = true },
            //    new PersonStatus() { PersonStatusId = (byte)DomainEnumerators.enumPersonStatus.EnrollmentApproved, Name = "Cadastro Aprovado", Active = true },
            //    new PersonStatus() { PersonStatusId = (byte)DomainEnumerators.enumPersonStatus.EnrollmentRejected, Name = "Cadastro Rejeitado", Active = true }
            //);

            //context.PersonType.AddOrUpdate(x => x.PersonTypeId,
            //    new PersonType() { PersonTypeId = (byte)DomainEnumerators.enumPersonType.Physic, Name = "Pessoa F�sica", Active = true },
            //    new PersonType() { PersonTypeId = (byte)DomainEnumerators.enumPersonType.Juridic, Name = "Pessoa Jur�dica", Active = true }
            //);

            //context.UserProfile.AddOrUpdate(x => x.UserProfileId,
            //    new UserProfile() { UserProfileId = (byte)DomainEnumerators.enumUserProfile.Admin, Name = "Administrador", Active = true },
            //    new UserProfile() { UserProfileId = (byte)DomainEnumerators.enumUserProfile.Managed, Name = "Gerenciado", Active = true }
            //);

            //context.UserStatus.AddOrUpdate(x => x.UserStatusId,
            //    new UserStatus() { UserStatusId = (byte)DomainEnumerators.enumUserStatus.Active, Name = "Ativo", Active = true },
            //    new UserStatus() { UserStatusId = (byte)DomainEnumerators.enumUserStatus.LoginBlocked, Name = "Login Bloqueado", Active = true },
            //    new UserStatus() { UserStatusId = (byte)DomainEnumerators.enumUserStatus.Canceled, Name = "Cancelado", Active = true }
            //);

        }
    }
}
