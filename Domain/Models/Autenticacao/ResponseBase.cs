using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Autenticacao
{
    /// <summary>
    /// Classe padrão de resposta da API
    /// </summary>
    /// <typeparam name="T">Tipo dos dados</typeparam>
    public class ResponseBase<T>
    {
        public ResponseBase() { }

        public ResponseBase(ResponseStatus status, string mensagem)
        {
            Status = status;
            Mensagem = mensagem;
        }

        public ResponseBase(ResponseStatus status, string mensagem, T dados)
        {
            Status = status;
            Mensagem = mensagem;
            Dados = dados;
        }

        /// <summary>
        /// Status da requisição
        /// </summary>
        [Required]
        public ResponseStatus Status { get; set; }

        /// <summary>
        /// Mensagem da requisição
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Dados da requisição
        /// </summary>
        public T? Dados { get; set; }
    }
}
