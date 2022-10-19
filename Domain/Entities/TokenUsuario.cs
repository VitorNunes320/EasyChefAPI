using CrossCutting.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("tokens_usuarios")]
    public class TokenUsuario
    {
        public TokenUsuario()
        {
            Id = Guid.NewGuid();
            CriadoEm = DataUtils.GetDateTimeBrasil();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("usuario_id")]
        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("habilitado")]
        public bool Habilitado { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("expira_em")]
        public DateTime ExpiraEm { get; set; }

    }
}
