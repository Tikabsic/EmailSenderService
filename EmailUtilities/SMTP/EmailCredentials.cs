using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailUtilities.SMTP
{
    internal static class EmailCredentials
    {
        internal static string EMailAddress = "";
        internal static string EMailPassword = "";
        internal static string Host = "";
        internal static int Port;
        internal static bool EnableSsl;
    }
}
