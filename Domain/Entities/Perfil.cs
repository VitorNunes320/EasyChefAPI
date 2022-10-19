using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("perfis")]
    public class Perfil : EntidadeBase
    {
        [Column("tipo_perfil")]
		public TipoPerfil TipoPerfil { get; set; }

        [Column("descricao")]
		public string Descricao { get; set; }
	}
}
