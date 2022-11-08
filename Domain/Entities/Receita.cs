using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("receitas")]
    public class Receita : EntidadeBase
    {
        [Column("nome")]
        public string Nome { get; set; }

        [Column("imagem")]
        public string Imagem { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("habilitado")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool Habilitado { get; set; }

        [Column("empresa_id")]
        public Guid EmpresaId { get; set; }

        public Empresa Empresa { get; set; }

        [Column("valor")]
        public decimal Valor { get; set; }

        public List<ReceitaIngrediente> ReceitasIngredientes { get; set; }
    }
}
