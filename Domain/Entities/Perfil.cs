using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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
