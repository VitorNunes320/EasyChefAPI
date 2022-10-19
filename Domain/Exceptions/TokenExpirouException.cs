using CrossCutting.Utils;

namespace Domain.Exceptions
{
    public class TokenExpirouException : Exception
    {
        public TokenExpirouException() : base(Mensagens.FalhaTokenExpirou)
        {

        }
    }
}
