using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Heeelp.Core.Common.GeneralEnumerators;

namespace Heeelp.Core.Common
{
    public static class Constants
    {
        public static List<int> DefaultManagerAccessProfile()
        {
            return new List<int>() {
                (int)EnumProfileClaims.GerenciadoPrestadorServico,
                (int)EnumProfileClaims.GestorCoWorking,
                (int)EnumProfileClaims.GestorPrestadorServico,
                (int)EnumProfileClaims.GestorRH   ,
                (int)EnumProfileClaims.AdminHeeelp
            };
        }

        public static List<EnumProfileClaims> DefaultManagerAccessProfileClaims()
        {
            return new List<EnumProfileClaims>() {
               EnumProfileClaims.GerenciadoPrestadorServico,
               EnumProfileClaims.GestorCoWorking,
               EnumProfileClaims.GestorPrestadorServico,
               EnumProfileClaims.GestorRH   ,
               EnumProfileClaims.AdminHeeelp
            };
        }

        public static bool IsManager(List<EnumProfileClaims> profileList)
        {

            List<EnumProfileClaims> managerProfiles = Constants.DefaultManagerAccessProfileClaims();         

            foreach (var profileClaim in profileList)
            {
                if (managerProfiles.Contains(profileClaim))
                    return true;      // in case of manager
            }
            return false;
        }

    }
}
