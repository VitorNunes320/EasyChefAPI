using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    /// <summary>
    /// Classe padrão de resposta da API
    /// </summary>
    /// <typeparam name="T">Tipo dos dados</typeparam>
    public class ResponseDadosBase<T> : ResponseBase
    {
        public ResponseDadosBase() { }

        public ResponseDadosBase(ResponseStatus status, string mensagem, T dados)
        {
            Status = status;
            Mensagem = mensagem;
            Dados = dados;
        }

        /// <summary>
        /// Dados da requisição
        /// </summary>
        public T Dados { get; set; }
    }
}
