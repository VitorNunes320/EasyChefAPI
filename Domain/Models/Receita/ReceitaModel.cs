using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Receita
{
    /// <summary>
    /// Dados da receita
    /// </summary>
    public class ReceitaModel
    {
        /// <summary>
        /// Id da receita
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Imagem da receita
        /// </summary>
        public string? Imagem { get; set; }

        /// <summary>
        /// Nome da receita
        /// </summary>
        public string? Nome { get; set; }

        /// <summary>
        /// Descrição da receita
        /// </summary>
        public string? Descricao { get; set; }

        /// <summary>
        /// Valor da receita
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Ingredientes da receita
        /// </summary>
        public List<ReceitaIngredienteModel>? Ingredientes { get; set; }
    }
}
