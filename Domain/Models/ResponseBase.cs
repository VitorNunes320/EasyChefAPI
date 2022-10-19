using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    /// <summary>
    /// Classe padrão de resposta da API
    /// </summary>
    public class ResponseBase
    {
        public ResponseBase() { }

        public ResponseBase(ResponseStatus status, string mensagem)
        {
            Status = status;
            Mensagem = mensagem;
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
    }
}
