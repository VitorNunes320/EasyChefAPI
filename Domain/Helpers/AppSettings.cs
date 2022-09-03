using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }

        public string JWTSecret { get; set; }
    }
}
