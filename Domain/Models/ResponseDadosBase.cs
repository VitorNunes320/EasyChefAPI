using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ResponseDadosBase<T> : ResponseBase
    {
        public ResponseDadosBase() { }

        public ResponseDadosBase(ResponseStatus status, string mensagem, T dados)
        {
            Status = status;
            Mensagem = mensagem;
            Dados = dados;
        }

        public T Dados { get; set; }
    }
}
