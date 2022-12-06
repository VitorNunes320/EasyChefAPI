using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("mesas")]
    public class Mesa : EntidadeBase
    {
        public Mesa() : base() { }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("codigo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }

        [Column("habilitado")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool Habilitado { get; set; }

        [Column("lugares")]
        public int Lugares { get; set; }

        [Column("ocupada")]
        public bool Ocupada { get; set; }

        [Column("empresa_id")]
        public Guid EmpresaId { get; set; }

        public Empresa Empresa { get; set; }
    }
}
