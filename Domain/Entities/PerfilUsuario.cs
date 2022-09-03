using CrossCutting.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PerfilUsuario
    {
        public PerfilUsuario(Guid usuarioId, Guid perfilId, string usuarioCriou)
        {
            UsuarioId = usuarioId;
            PerfilId = perfilId;
            UsuarioCriou = usuarioCriou;
            CriadoEm = DataUtils.GetDateTimeBrasil();
        }

		public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public Guid PerfilId { get; set; }

        public Perfil Perfil { get; set; }

        public DateTime CriadoEm { get; set; }

		public DateTime? AtualizadoEm { get; set; }

		public string? UsuarioCriou { get; set; }

		public string? UsuarioAtualizou { get; set; }
	}
}
