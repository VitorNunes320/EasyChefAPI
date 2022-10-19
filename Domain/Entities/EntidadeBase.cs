using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EntidadeBase
    {
        public EntidadeBase()
        {
			Id = Guid.NewGuid();
		}

		[Key]
		[Column("id")]
		public Guid Id { get; set; }

		[Column("criado_em")]
		public DateTime CriadoEm { get; set; }

		[Column("atualizado_em")]
		public DateTime? AtualizadoEm { get; set; }

		[Column("usuario_criou")]
		public string? UsuarioCriou { get; set; }

		[Column("usuario_atualizou")]
		public string? UsuarioAtualizou { get; set; }
	}
}
