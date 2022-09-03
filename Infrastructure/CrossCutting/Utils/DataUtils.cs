using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Utils
{
    public class DataUtils
    {
        public static DateTime GetDateTimeBrasil()
        {
            TimeZoneInfo timeZoneInfo;

            try
            {
                // Tenta buscar o timezone do windows
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            }
            catch (Exception)
            {
                // Tenta buscar o timezone do linux
                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo");
            }

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo).ToUniversalTime();
        }
    }
}
