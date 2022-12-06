using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("empresas")]
    public class Empresa : EntidadeBase
    {
        public Empresa() : base() { }

        [Column("nome")]
        public string Nome { get; set; }

        public List<Usuario> Usuarios { get; set; }

        [Column("habilitado")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool Habilitado { get; set; }
    }
}
