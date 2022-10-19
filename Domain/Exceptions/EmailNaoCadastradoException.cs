using CrossCutting.Utils;

namespace Domain.Exceptions
{
    public class EmailNaoCadastradoException : Exception
    {
        public EmailNaoCadastradoException() : base(Mensagens.ErroEmailNaoCadastrado)
        {

        }
    }
}
