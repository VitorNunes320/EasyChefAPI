using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    /// <summary>
    /// Dados do login do usuário
    /// </summary>
    public class LoginModel
    {
        public LoginModel(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        /// <summary>
        /// Email utilizado para realizar o login
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Senha utilizada para realizar o login
        /// </summary>
        [Required]
        public string Senha { get; set; }
    }
}
