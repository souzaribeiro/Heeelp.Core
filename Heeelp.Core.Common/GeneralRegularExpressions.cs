using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Common
{
    public  class GeneralRegularExpressions
    {

        private static string regexValidatePhoneNumber = @"^\([1-9]{2}\) [2-9][0-9]{3,4}\-[0-9]{4}$";
        private static string regexValidateIP = "^((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\\.){3}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})$";
        private static string regexValidateURL = "^((http[s]?|ftp):\\/)?\\/?([^:\\/\\s]+)((\\/\\w+)*\\/)([\\w\\-\\.]+[^#?\\s]+)(.*)?(#[\\w\\-]+)?$";
        private static string regexValidateDate = "^ ([1-9]|0[1-9]|[1,2][0-9]|3[0,1])/([1-9]|1[0,1,2])/\\d{4}$";

        // A read-only static property:
        public static string RegexValidatePhoneNumber
        {
            get { return regexValidatePhoneNumber; }
        }

        public static string RegexValidateIP
        {
            get { return regexValidateIP; }
        }

        public static string RegexValidateURL
        {
            get { return regexValidateURL; }
        }

        public static string RegexValidateDate
        {
            get { return regexValidateDate; }
        }

    }

}
