using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Receita
{
    /// <summary>
    /// Dados dos ingredientes da receita
    /// </summary>
    public class ReceitaIngredienteModel
    {
        /// <summary>
        /// Id da ligação ReceitaIngrediente
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Id do ingrediente
        /// </summary>
        public Guid IngredienteId { get; set; }

        /// <summary>
        /// Nome do ingrediente
        /// </summary>
        public string? Nome { get; set; }

        /// <summary>
        /// Imagem do ingrediente
        /// </summary>
        public string? Imagem { get; set; }

        /// <summary>
        /// Quantidade do ingrediente
        /// </summary>
        public decimal Quantidade { get; set; }

        /// <summary>
        /// Valor do ingrediente
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Id da unidade de medida
        /// </summary>
        public Guid UnidadeMedidaId { get; set; }

        /// <summary>
        /// Nome da unidade de medida
        /// </summary>
        public string? UnidadeMedida { get; set; }
    }
}
