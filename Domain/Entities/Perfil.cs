using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Perfil
    {
		public Guid Id { get; set; }

		public int TipoPerfil { get; set; }

		public string Descricao { get; set; }

		public DateTime? CriadoEm { get; set; }

		public DateTime? AtualizadoEm { get; set; }

		public string? UsuarioCriou { get; set; }

		public string? UsuarioAtualizou { get; set; }
	}
}
