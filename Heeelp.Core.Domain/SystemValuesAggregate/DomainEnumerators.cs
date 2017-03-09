using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Domain
{
    public class DomainEnumerators
    {

        public enum enumAddressType { Home = 1, Business = 2, Billing = 3 }
        public enum enumApprovalStatus { WaitingAnalysis = 1, Approved = 2, Rejected = 3 }

        public enum enumCommandOrigin { UI = 1, EventHandler = 2 }

        public enum enumCommandStatus { WaitingProcess = 1, InProcess = 2, Sucess = 3, TemporaryFailure = 4, DefinitiveFailure = 5 }

        //a lista sera carregada com base na interacao dos paises contidos na classe CultureInfo. ex.: foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        public enum enumCountry { Brasil = 1, Argentina = 2 }

        public enum enumContractType
        {
            [Description("Politica de Privacidade")]
            PoliticaDePrivacidade = 1,
            [Description("Termos de uso para Colaborador")]
            TermosDeUsoColaborador = 2,
            [Description("Termos de uso para Prestador de Servico")]
            TermosDeUsoPrestadorDeServico = 3,
            [Description("Termos de uso para empresa de Coworking")]
            TermosDeUsoCoworking = 4,
            [Description("Termos de uso para Revendedores")]
            TermosDeUsoRevenda = 5,
            [Description("Termos de uso para Facilitadores")]
            TermosDeUsoFacilitador = 6,
            [Description("Termos de uso para Anunciantes")]
            TermosDeUsoAnunciante = 7,
            [Description("Termos de uso para empresa de RH")]
            TermosDeUsoEmpresaParceiraRH = 8,
            [Description("Termos de uso para Centro Educacional")]
            TermosDeUsoCentroEducacaional = 9,
            [Description("Termos de uso para Condominios")]
            TermosDeUsoCondominio = 10
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public enum enumCouponEstatisticMode { Simple = 1, Detailed = 2 }

        public enum enumCouponPeriodMode { Year = 1, Month = 2, Week = 3 }

        //a lista sera carregada com base na interacao dos paises contidos na classe CultureInfo. ex.: foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))        
        public enum enumCurrency { Real = 1, Dolar = 2 }

        public enum enumDocumentType { CPF = 1, RG = 2 }

        public enum enumEventStatus { WaitingProcess = 1, InProcess = 2, Sucess = 3, TemporaryFailure = 4, DefinitiveFailure = 5 }

        //a lista sera carregada com base na interacao dos paises contidos na classe CultureInfo. ex.: foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))        
        public enum enumLanguage { pt_BR = 1, en_US = 2 }
        public enum enumPageType { LandingPage = 1, FanPage = 2, Institutional = 3, Administrative = 4, Management = 5 }
        public enum enumPersonOriginType { DirectAccess = 1, OrganicSearch = 2, MarketingCampaign = 3 }
        public enum enumPersonStatus { WaitingAnalysis = 1, WaitingActivation = 2, EnrollmentApproved = 3, EnrollmentRejected = 4 }
        public enum enumPersonType { Physic = 1, Juridic = 2 }
        public enum enumPersonProfile { User = 1, ServiceProvider = 2, CompanyPartner = 3, SystemAdministrator = 4, Coworking = 5, EducationalCenter = 6, Condominium = 7 }

        public enum enumPriority { High = 1, Medium = 2, Low = 3 }

        public enum enumSelectedSkin { Basic = 1, Custom = 2 }

        public enum enumShowRecentQueries { Yes = 1, No = 2 }
        public enum enumUserProfile { Admin = 1, Managed = 2 }
        public enum enumUserStatus { Active = 1, LoginBlocked = 2, Canceled = 3 }

    }
}
