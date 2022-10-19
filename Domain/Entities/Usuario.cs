using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("usuarios")]
    public class Usuario : EntidadeBase
    {
        public Usuario() : base() { }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        public string Senha { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("foto")]
        public string? Foto { get; set; }

        public ICollection<PerfilUsuario> PerfisUsuarios { get; set; }
    }
}
