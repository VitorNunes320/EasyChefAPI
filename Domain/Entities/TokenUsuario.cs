using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TokenUsuario
    {
        public TokenUsuario()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public string Token { get; set; }

        public bool Habilitado { get; set; }

        public DateTime CriadoEm { get; set; }

        public DateTime ExpiraEm { get; set; }

    }
}
