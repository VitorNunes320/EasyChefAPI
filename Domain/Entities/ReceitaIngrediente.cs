using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("receitas_ingredientes")]
    public class ReceitaIngrediente
    {
        public ReceitaIngrediente()
        {
            Id = Guid.NewGuid();
        }

        [Column("id")]
        public Guid Id { get; set; }

        [Column("receita_id")]
        public Guid ReceitaId { get; set; }

        public Receita Receita { get; set; }

        [Column("ingrediente_id")]
        public Guid IngredienteId { get; set; }

        public Ingrediente Ingrediente { get; set; }

        [Column("quantidade")]
        public decimal Quantidade { get; set; }

        [Column("unidade_medida_id")]
        public Guid UnidadeMedidaId { get; set; }

        public UnidadeMedida UnidadeMedida { get; set; }
    }
}
