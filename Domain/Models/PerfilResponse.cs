using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    /// <summary>
    /// Dados do perfil do usuário
    /// </summary>
    public class PerfilResponse
    {
        public PerfilResponse(Guid id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        /// <summary>
        /// Código de identificação do perfil
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Descrição do perfil
        /// </summary>
        public string Descricao { get; set; }
    }
}
