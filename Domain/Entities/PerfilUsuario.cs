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
    [Table("perfis_usuarios")]
    public class PerfilUsuario
    {
        public PerfilUsuario(Guid usuarioId, Guid perfilId, string usuarioCriou)
        {
            UsuarioId = usuarioId;
            PerfilId = perfilId;
            UsuarioCriou = usuarioCriou;
            CriadoEm = DataUtils.GetDateTimeBrasil();
        }

        [Column("usuario_id")]
        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        [Column("perfil_id")]
        public Guid PerfilId { get; set; }

        public Perfil Perfil { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }

        [Column("atualizado_em")]
        public DateTime? AtualizadoEm { get; set; }

        [Column("usuario_criou")]
        public string? UsuarioCriou { get; set; }

        [Column("usuario_atualizou")]
        public string? UsuarioAtualizou { get; set; }
	}
}
