using CrossCutting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TokenNaoExisteException : Exception
    {
        public TokenNaoExisteException() : base(Mensagens.FalhaTokenNaoExiste)
        {

        }
    }
}
