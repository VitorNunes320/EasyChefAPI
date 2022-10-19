using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Autenticacao
{
    /// <summary>
    /// Tokens de acesso
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Access Token
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Refresh Token'
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}
