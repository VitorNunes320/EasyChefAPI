using CrossCutting.Utils;

namespace Domain.Exceptions
{
    public class PerfilNaoExisteException : Exception
    {
        public PerfilNaoExisteException() : base(Mensagens.ErroPerfilNaoExiste)
        {

        }
    }
}
