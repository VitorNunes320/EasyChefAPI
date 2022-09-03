using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ResponseBase
    {
        public ResponseBase() { }

        public ResponseBase(ResponseStatus status, string mensagem)
        {
            Status = status;
            Mensagem = mensagem;
        }

        public ResponseStatus Status { get; set; }

        public string Mensagem { get; set; }
    }
}
