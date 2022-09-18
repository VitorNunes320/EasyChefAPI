using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
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
