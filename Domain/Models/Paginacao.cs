using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Paginacao<T>
    {
        public int Quantidade { get; set; }

        public T Dados { get; set; }
    }
}
