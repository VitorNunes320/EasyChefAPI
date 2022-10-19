using CrossCutting.Utils;

namespace Domain.Exceptions
{
    public class TokenUtilizadoException : Exception
    {
        public TokenUtilizadoException() : base(Mensagens.FalhaTokenUtilizado)
        {

        }
    }
}
