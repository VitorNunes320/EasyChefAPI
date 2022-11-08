using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("unidade_medidas")]
    public class UnidadeMedida : EntidadeBase
    {

        [Column("id")]
        public Guid Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("habilitado")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool Habilitado { get; set; }
    }
}