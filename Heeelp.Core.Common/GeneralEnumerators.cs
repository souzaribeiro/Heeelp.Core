using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Common
{
    /// <summary>
    /// General Enumerators
    ///  /// </summary>
    public class GeneralEnumerators
    {
        /// <summary>
        /// Modules Enumerators
        /// </summary>
        public enum EnumModules
        {
            /// <summary>
            /// Module Core Expertise
            /// </summary>
            Core_Expertise = 1,
            /// <summary>
            /// Module Core Person
            /// </summary>
            Core_Person = 2,
            /// <summary>
            /// Module Core User
            /// </summary>
            Core_User = 3,
            /// <summary>
            /// Module Marketing
            /// </summary>
            Marketing = 4,
            /// <summary>
            /// Module Promotion
            /// </summary>
            Promotion = 5,
            /// <summary>
            /// Module Notification
            /// </summary>
            Notification = 6,
            /// <summary>
            /// Module FileServer
            /// </summary>
            FileServer = 7
        }
        /// <summary>
        ///Person Type Enum
        /// </summary>
        public enum EnumPersonType
        {
            /// <summary>
            ///Pessoa Fisica
            /// </summary>
            Natural_Person = 1,
            /// <summary>
            /// Pessoa Juridica
            /// </summary>
            Legal_Person = 2

        }
        /// <summary>
        ///PersonAccountStatus
        /// </summary>
        public enum EnumPersonAccountStatus
        {
            /// <summary>
            ///Alterar
            /// </summary>
            Aguardando = 1,
            Ativa = 2,
            Cancelada = 3,
            Suspenso = 4
        }

        public enum EnumDocumentType
        {
            CPF = 1,
            RG = 2,
            CNPJ = 3
        }

        public enum EnumPersonAccountEventType
        {

            Credit = 1,
            Debit = 2
        }
        public enum EnumSecurityCheckStatus
        {
            Ok = 1,
            Suspect = 2,
            Fraud = 3
        }

        public enum EnumClientApplication
        {
            WebBrowser = 1,
            Android = 2,
            IOS = 3
        }
        public enum EnumPersonProfile
        {
            Colaborador = 1,
            PrestadorServiços = 2,
            EmpresaAssociadaClubedeBeneficios = 3,
            AdminstradorSistema = 4,
            EmpresaCoWorking = 5,
            InstituiçãoEnsino = 6,
            Condomínio = 7
        }


        public enum enumPromotionType
        {
            [Description("Desconto")]
            Discount = 1,
            [Description("Recompensa")]
            Award = 2,
            [Description("Presente")]
            Gift = 3
        }

        public enum EnumProfileClaims
        {
            [Description("Colaborador")]
            Colaborador = 1,
            [Description("GestorRH")]
            GestorRH = 2,
            [Description("GestorPrestadorServico")]
            GestorPrestadorServico = 3,
            [Description("GerenciadoPrestadorServico")]
            GerenciadoPrestadorServico = 4,
            [Description("GestorCoWorking")]
            GestorCoWorking = 5,
            [Description("AdminHeeelp")]
            AdminHeeelp = 6

        }

        public enum EnumFileUtiliaztion
        {
            Logo = 1,
            Oferta = 2,
            Album = 3
        }


        public enum EnumTransactionOringinType
        {
            Core_Person = 1
        }


        public enum EnumTransactionStatus
        {
            AguardandoProcessamento = 0,
            EmCurso = 1,
            Aprovada = 2,
            Negada = 3,

        }

        public enum EnumCurrency
        {
            [AttributeInfo("Real", "R$")]
            Real = 1
        }



        public enum EnumTransactionParticipantRole
        {
            Marketplace = 1,
            Prestador = 2,
            Consumidor = 3,
            Anfitriao = 4
        }

        public enum EnumTypeOperation
        {
            NewUser = 1,
            CuponTransaction = 2
        }

        public enum EnumUserDefault
        {
            Heeelp = 1
        }
        public enum ExpertiseTypes
        {
            AllTypes = 1,
            Services = 2,
            Products = 3
        }

        public enum ClassifiedType
        {
            Promotion = 1,
            Classified = 2,
            Sponsored = 3
        }

        public enum EnumUserProfile
        {
            Administrador = 1,
            Gerenciado = 2,
            SemAcesso = 3
        }
        public enum EnumUserStatus
        {
            Ativo = 1,
            LoginBloqueado = 2,
            Cancelado = 3,
            AguardandoAtivacao = 4
        }
        public enum EnumCommandsErrorStatus
        {
            Waiting = 1,
            Success = 2,
            Fail = 3,
            FailTemporary = 4
        }
        public enum EnumEventsErrorStatus
        {
            Waiting = 1,
            Success = 2,
            Fail = 3,
            FailTemporary = 4
        }

        public enum EnumLanguage { Portuguese = 94, English = 2, Spanish = 3 }
        public enum EnumPersonOriginType { CadastroAssistido = 1, PainelAdministrativoHeeelp = 2, IntegracaoAPIExterna = 3, PainelAdministrativoGestor = 4 }
        public enum EnumCountry
        {
            [Description("Brasil-BR")]
            Brazil = 1
        }
        public enum EnumState
        {
            [Description("SP")]
            SP = 25

        }
        public enum EnumCity
        {
            [Description("São Paulo")]
            SaoPaulo = 2432
        }
        public enum EnumPersonStatus
        {
            AguardandoAnálise = 1,
            AguardandoAtivação = 2,
            CadastroAprovado = 3,
            CadastroRejeitado = 4,
            ContaTemporariamenteBloqueada = 5,
            ContaSuspensa = 6,
            ContaEncerrada = 7
        }
        public enum EnumTransactionOringinTypeId
        {
            CorePerson = 1,
            CouponTransaction = 2,
            Denounce = 3,
            Award = 4,
            Ecommerce = 5
        }
        public enum EnumAuthenticationMode { Mode1 = 1 }
        public enum EnumServerInstance { Server1 = 1 }
        public enum EnumRulesStatus { Ativo = 1, Inativo = 2 }

        public enum EnumEmailWelcomeType
        {
            [Description("BoasVindColab")]
            BoasVindColab = 1,
            [Description("BoasVindColbWL")]
            BoasVindColbWL = 2,
            [Description("BoasVindGestCW")]
            BoasVindGestCW = 3,
            [Description("BoasVindGestRH")]
            BoasVindGestRH = 4,
            [Description("BoasVindGesRHWL")]
            BoasVindGesRHWL = 5,
            [Description("BoasVindPresSer")]
            BoasVindPresSer = 6,
            [Description("BoasVindColabWeb")]
            BoasVindColabWeb = 7


        }

        public enum enumValidationErrorCode
        {
            None = 0,
            Required = 800501,
            TooShort = 800502,
            InvalidEmail = 800503,
            InvalidDate = 800504,
            DateOutOfRange = 800505,
            InvalidNumber = 800506,
            NumberOutOfRange = 800507,
            ValueOutOfRange = 800508,
            InvalidPhoneNumber = 800509,
            InvalidIP = 800510,
            InvalidGuid = 800511,
            InvalidURL = 800512
        }


        public enum EnumEmailAcvtiveType
        {
            [Description("AtivarContaCola")]
            AtivarContaCola = 1,
            [Description("AtivContColabWL")]
            AtivContColabWL = 2,
            [Description("AtivContGestCWL")]
            AtivContGestCWL = 3,
            [Description("AtivContGesRHWL")]
            AtivContGesRHWL = 4,
            [Description("AtivContGestRH")]
            AtivContGestRH = 5,
            [Description("AtivContPresSer")]
            AtivContPresSer = 6,
            [Description("AtivContAdmHeeelp")]
            AtivContAdmHeeelp = 7
        }
        public enum EnumEmailForgotPassWord
        {
            [Description("RecuperarSenha")]
            RecuperarSenha = 1,
            [Description("RecupSenhaWL")]
            RecupSenhaWL = 2,
        }
        public enum EnumSkinType
        {
            Default = 1,
            WhiteLabel = 2

        }

        public enum EnumAddressType
        {
            [Description("Comercial")]
            Comercial = 1
        }

        public enum EnumPersonBenefitClub
        {

            [Description("Heeelp")]
            Heeelp = 1,
            [Description("Go Plus")]
            GoPlus = 2

        }

        public enum EnumPromotionApprovalStatus
        {
            [Description("Aguardando Aprovação")]
            AguardandoAprovacao = 1,
            [Description("Aprovado")]
            Aprovado = 2,
            [Description("Rejeitado")]
            Rejeitado = 3
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

        public static List<EnumList> GetList<T>() where T : struct
        {
            var items = new List<EnumList>();
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                items.Add(new EnumList
                {
                    Id = Convert.ToInt32(value),
                    Name = GetEnumDescription((Enum)(object)value),
                    Value = Enum.GetName(typeof(T), value),
                });
            }
            return items;
        }
        #region EnumPromotion

        public enum enumPromotionMethodPayment
        {
            [Description("Valor Definido")]
            ValorDefinido = 1,
            [Description("Valor Calculado")]
            ValorCalculado = 2,
            [Description("Valor Percentual")]
            ValorPercentual = 3
        }
        public enum enumTimeForActivation
        {
            [Description("Imediato")]
            Imediato = 1,
            [Description("15 Minutos")]
            m15 = 2,
            [Description("Meia Hora")]
            m30 = 3,
            [Description("1 Hora")]
            m60 = 4,
            [Description("2 Horas")]
            m120 = 5,
            [Description("4 Horas")]
            m240 = 6,
            [Description("8 Horas")]
            m480 = 7
        }

        public enum enumPromotionBillingModel
        {
            [Description("Valor Por Cupom Transacionado")]
            ValorPorCupomTransacionado = 1,
            [Description("Valor Por Promoção")]
            ValorPorPromocao = 2,
            [Description("Não se Aplica")]
            NaoSeAplica = 3
        }
        public enum enumPromotionPaymentType
        {
            [Description("Pré-Pago")]
            PrePago = 1,
            [Description("Pós-Pago")]
            PosPago = 2
        }
        public enum enumPromotionRecurrence
        {
            [Description("Não Recorrente")]
            NaoRecorrente = 1,
            [Description("Semanal")]
            Semanal = 2,
            [Description("Quinzenal")]
            Quinzenal = 3,
            [Description("Mensal")]
            Mensal = 4,
            [Description("Bimestral")]
            Bimestral = 5,
            [Description("Trimestral")]
            Trimestral = 6,
            [Description("Não se Aplica")]
            NaoSeAplica = 7
        }
        #endregion


    }
    /// <summary>
    //var info = GeneralEnumerators.EnumCurrency.Real.GetAttribute<AttributeInfo>();
    ///Console.WriteLine("Description: {0}\nValue:{1}",info.Description, info.Value);
    /// </summary>
    public static class Extensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : System.Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class AttributeInfo : DescriptionAttribute
    {
        public AttributeInfo(string description, string value)
        {
            this.Description = description;
            this.Value = value;
        }

        public string Description { get; set; }
        public string Value { get; set; }
    }

    public class EnumList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

}
