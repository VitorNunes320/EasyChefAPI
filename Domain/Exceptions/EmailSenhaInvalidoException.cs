using CrossCutting.Utils;

namespace Domain.Exceptions
{
    public class EmailSenhaInvalidoException : Exception
    {
        public EmailSenhaInvalidoException() : base(Mensagens.ErroEmailSenha)
        {

        }
    }
}
