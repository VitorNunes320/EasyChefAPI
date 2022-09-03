using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CriarUsuarioModel
    {
        public CriarUsuarioModel(string nome, string email, string senha, List<Guid> perfis)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Perfis = perfis;
        }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Email utilizado para realizar o login
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha utilizada para realizar o login
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Perfis do usuário
        /// </summary>
        public List<Guid> Perfis { get; set; }
    }
}
