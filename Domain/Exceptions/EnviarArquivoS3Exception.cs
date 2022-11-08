using CrossCutting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EnviarArquivoS3Exception : Exception
    {
        public EnviarArquivoS3Exception() : base(Mensagens.ErroEnviarArquivoS3)
        {

        }
    }
}
