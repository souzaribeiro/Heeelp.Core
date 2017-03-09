using System;
using System.Configuration;

namespace Heeelp.Core.Common
{
    public static class CustomConfiguration
    {

        public static string HeeelpConnection { get { return ConfigurationManager.ConnectionStrings["HeeelpConnection"].ConnectionString; } }

        public static string WebApiFileServer { get { return ConfigurationManager.AppSettings["WebApiFileServer"]; } }

        public static string WebApiAccount { get { return ConfigurationManager.AppSettings["WebApiAccount"]; } }

        public static string EmailAdmin { get { return ConfigurationManager.AppSettings["EmailAdmin"]; } }

        public static string WebApiIntegration { get { return ConfigurationManager.AppSettings["WebApiIntegration"]; } }

        public static string WebApiContab { get { return ConfigurationManager.AppSettings["WebApiContab"]; } }

        public static string HeeelpClientVersion { get { return ConfigurationManager.AppSettings["HeeelpClientVersion"]; } }

        public static string WebApiNotification { get { return ConfigurationManager.AppSettings["WebApiNotification"]; } }

        public static string UserSessionName { get { return ConfigurationManager.AppSettings["UserSessionName"]; } }

        public static string DefaultPasswordNewUser { get { return ConfigurationManager.AppSettings["DefaultPasswordNewUser"]; } }

        public static string WebApiClassified { get { return ConfigurationManager.AppSettings["WebApiClassified"]; } }

        public static string WebApiCore { get { return ConfigurationManager.AppSettings["WebApiCore"]; } }

        public static string WebApiPromotion { get { return ConfigurationManager.AppSettings["WebApiPromotion"]; } }

        public static string WebApiSocial { get { return ConfigurationManager.AppSettings["WebApiSocial"]; } }

        public static string HeeelpClientWebPortal { get { return ConfigurationManager.AppSettings["HeeelpClientWebPortal"]; } }

        public static int DelayProccess { get { return Convert.ToInt32(ConfigurationManager.AppSettings["DelayProccess"].ToString()); } }
        public static int Attempts { get { return Convert.ToInt32(ConfigurationManager.AppSettings["Attempts"].ToString()); } }
        public static int IdleTimeToProccess { get { return Convert.ToInt32(ConfigurationManager.AppSettings["IdleTimeToProccess"].ToString()); } }

        public static string Storage { get { return ConfigurationManager.ConnectionStrings["Storage"].ConnectionString; } }

        public static string ContainerName { get { return ConfigurationManager.AppSettings["ContainerName"]; } }

        public static string CultureInfo { get { return ConfigurationManager.AppSettings["CultureInfo"]; } }

        public static string WebApiMarketing { get { return ConfigurationManager.AppSettings["WebApiMarketing"]; } }

        public static string PrivacyPolicyUrl { get { return ConfigurationManager.AppSettings["PrivacyPolicyUrl"]; } }

        public static string TermsOfUse { get { return ConfigurationManager.AppSettings["TermsOfUse"]; } }
        
        public static int CookieExpirationDaysAmount { get { return Convert.ToInt32(ConfigurationManager.AppSettings["CookieExpirationDaysAmount"]); } }

        public static string RadiusDistance { get { return ConfigurationManager.AppSettings["RadiusDistance"]; } }

    }
}