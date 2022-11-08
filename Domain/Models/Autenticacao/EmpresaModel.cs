using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Autenticacao
{
    public class EmpresaModel
    {
        /// <summary>
        /// Id da empresa
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da empresa
        /// </summary>
        public string Nome { get; set; }
    }
}
