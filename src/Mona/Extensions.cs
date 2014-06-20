using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mona
{
    /// <summary>
    /// Helpers/Extensions for common types
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Completes a string.Format to interpolate the arguments into the format in the current culture
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Interpolate(this string format, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, format, args: args);
        }
    }
}
