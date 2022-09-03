using CrossCutting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public Usuario()
        {
			Id = Guid.NewGuid();
			CriadoEm = DataUtils.GetDateTimeBrasil();
		}

		public Guid Id { get; set; }

		public string Email { get; set; }

		public string Senha { get; set; }

		public string Nome { get; set; }

		public string? Foto { get; set; }

		public DateTime CriadoEm { get; set; }

		public DateTime? AtualizadoEm { get; set; }

		public string? UsuarioCriou { get; set; }

		public string? UsuarioAtualizou { get; set; }

        public ICollection<PerfilUsuario> PerfisUsuarios { get; set; }
    }
}
