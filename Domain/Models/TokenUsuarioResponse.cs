using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TokenUsuarioResponse
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string? Foto { get; set; }

        public TokenResponse Tokens { get; set; }
    }
}
