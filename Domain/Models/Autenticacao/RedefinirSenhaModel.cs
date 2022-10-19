using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Autenticacao
{
    /// <summary>
    /// Dados da redefinição de senha do usuário
    /// </summary>
    public class RedefinirSenhaModel
    {
        /// <summary>
        /// Token de redefinição de senha
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Nova senha do usuário
        /// </summary>
        [Required]
        public string NovaSenha { get; set; }
    }
}
