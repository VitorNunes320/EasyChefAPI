using CrossCutting.Utils;

namespace Domain.Exceptions
{
    public class TokenNaoExisteException : Exception
    {
        public TokenNaoExisteException() : base(Mensagens.FalhaTokenNaoExiste)
        {

        }
    }
}
