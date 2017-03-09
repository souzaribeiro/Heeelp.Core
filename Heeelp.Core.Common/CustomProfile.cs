using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Heeelp.Core.Common.GeneralEnumerators;

namespace Heeelp.Core.Common
{
    public static class CustomProfile
    {
        public static List<int> ListProfiles(int userProfileId, List<byte> rulesListId)
        {
            List<int> ret = new List<int>();
            foreach (var personProfileId in rulesListId)
            {

                if (userProfileId == (int)EnumUserProfile.SemAcesso || personProfileId == (int)EnumPersonProfile.Colaborador)// Not Alowed
                    ret.Add((int)EnumProfileClaims.Colaborador);
                if (userProfileId == (int)EnumUserProfile.Administrador && personProfileId == (int)EnumPersonProfile.EmpresaAssociadaClubedeBeneficios) // Administrador && Empresa Associada ao Clube de Beneficios 
                    ret.Add((int)EnumProfileClaims.GestorRH);
                if (userProfileId == (int)EnumUserProfile.Administrador && personProfileId == (int)EnumPersonProfile.PrestadorServiços)// Administrador && Prestador de Serviços 
                    ret.Add((int)EnumProfileClaims.GestorPrestadorServico);
                if (userProfileId == (int)EnumUserProfile.Gerenciado && personProfileId == (int)EnumPersonProfile.PrestadorServiços)// Gerenciado && Prestador de Serviços
                    ret.Add((int)EnumProfileClaims.GerenciadoPrestadorServico);
                if (userProfileId == (int)EnumUserProfile.Administrador && personProfileId == (int)EnumPersonProfile.EmpresaCoWorking)// Administrador && Empresa de CoWorking
                    ret.Add((int)EnumProfileClaims.GestorCoWorking);
                if (userProfileId == (int)EnumUserProfile.Administrador && personProfileId == (int)EnumPersonProfile.AdminstradorSistema)// Administrador && Prestador de Serviços 
                    ret.Add((int)EnumProfileClaims.AdminHeeelp);

            }
            return ret;
        }
        public static List<EnumProfileClaims> ListProfilesClaims(int userProfileId, List<byte> rulesListId)
        {
            List<EnumProfileClaims> ret = new List<EnumProfileClaims>();
            foreach (var personProfileId in rulesListId)
            {

                if (userProfileId == (int)EnumUserProfile.SemAcesso || personProfileId == (int)EnumPersonProfile.Colaborador)// Not Alowed
                    ret.Add(EnumProfileClaims.Colaborador);
                if (userProfileId == (int)EnumUserProfile.Administrador && personProfileId == (int)EnumPersonProfile.EmpresaAssociadaClubedeBeneficios) // Administrador && Empresa Associada ao Clube de Beneficios 
                    ret.Add(EnumProfileClaims.GestorRH);
                if (userProfileId == (int)EnumUserProfile.Administrador && personProfileId == (int)EnumPersonProfile.PrestadorServiços)// Administrador && Prestador de Serviços 
                    ret.Add(EnumProfileClaims.GestorPrestadorServico);
                if (userProfileId == (int)EnumUserProfile.Gerenciado && personProfileId == (int)EnumPersonProfile.PrestadorServiços)// Gerenciado && Prestador de Serviços
                    ret.Add(EnumProfileClaims.GerenciadoPrestadorServico);
                if (userProfileId == (int)EnumUserProfile.Administrador && personProfileId == (int)EnumPersonProfile.EmpresaCoWorking)// Administrador && Empresa de CoWorking
                    ret.Add(EnumProfileClaims.GestorCoWorking);
                if (userProfileId == (int)EnumUserProfile.Administrador && personProfileId == (int)EnumPersonProfile.AdminstradorSistema)// Administrador && Prestador de Serviços 
                    ret.Add(EnumProfileClaims.AdminHeeelp);

            }
            return ret;
        }
    }
}
