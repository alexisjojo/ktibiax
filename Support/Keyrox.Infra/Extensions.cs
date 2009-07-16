using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Keyrox.Infra {
    public static class Extensions {

        /// <summary>
        /// Gets the build date.
        /// </summary>
        /// <param name="asm">The asm.</param>
        /// <returns></returns>
        public static DateTime GetBuildDate(this AssemblyName asm) {
            return new System.DateTime(2000, 1, 1).AddDays(asm.Version.Build);
        }

        /// <summary>
        /// Fills the left spaces.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string FillLeftSpaces(this Version value, int length) {
            string strvalue = value.ToString();
            string strleftz = "";
            for (int i = strvalue.Length; i < length; i++) {
                strleftz += " ";
            }
            return strvalue + strleftz;
        }

    }
}
