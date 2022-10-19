using CrossCutting.Utils;

namespace Domain.Exceptions
{
    public class EmailUtilizadoException : Exception
    {
        public EmailUtilizadoException() : base(Mensagens.ErroEmailUtilizado)
        {

        }
    }
}
