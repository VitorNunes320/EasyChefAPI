using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class EmailSettings
    {
        public string From { get; set; }

        public string Name { get; set; }

        public string SMTP_ID { get; set; }

        public string SMTP_PWD { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string LinkRecuperarSenha { get; set; }

        public string LinkAcessarPlataforma { get; set; }
        public string LinkAlterarSenha { get; set; }
    }
}
