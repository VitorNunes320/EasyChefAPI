using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ingredientes")]
    public class Ingrediente : EntidadeBase
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("imagem")]
        public string Imagem { get; set; }

        [Column("unidade_medida_id")]
        public Guid UnidadeMedidaId { get; set; }

        public UnidadeMedida UnidadeMedida { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("valor")]
        public decimal Valor { get; set; }

        [Column("habilitado")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool Habilitado { get; set; }

        [Column("quantidade")]
        public int Quantidade { get; set; }

        [Column("empresa_id")]
        public Guid EmpresaId { get; set; }

        public Empresa Empresa { get; set; }

        [Column("estoque_minimo")]
        public decimal EstoqueMinimo { get; set; }

        [Column("estoque_maximo")]
        public decimal EstoqueMaximo { get; set; }
    }

}