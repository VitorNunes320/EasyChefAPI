using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models.Receita
{
    /// <summary>
    /// Dados do ingrediente
    /// </summary>
    public class IngredienteModel
    {
        /// <summary>
        /// Id do ingrediente
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Imagem do ingrediente
        /// </summary>
        public string? Imagem { get; set; }

        /// <summary>
        /// Nome do ingrediente
        /// </summary>
        public string? Nome { get; set; }

        /// <summary>
        /// Id da unidade de medida do ingrediente
        /// </summary>
        public Guid UnidadeMedidaId { get; set; }

        /// <summary>
        /// Unidade de medida do ingrediente
        /// </summary>
        public string? UnidadeMedida { get; set; }

        /// <summary>
        /// Descrição do ingrediente
        /// </summary>
        public string? Descricao { get; set; }

        /// <summary>
        /// Valor do ingrediente
        /// </summary>
        public decimal Valor { get; set; }
    }
}