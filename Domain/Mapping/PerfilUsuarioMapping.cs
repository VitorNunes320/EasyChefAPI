using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Mapping
{
    public class PerfilUsuarioMapping : IEntityTypeConfiguration<PerfilUsuario>
    {
        public void Configure(EntityTypeBuilder<PerfilUsuario> builder)
        {
            builder.HasOne<Usuario>(x => x.Usuario)
                .WithMany(x => x.PerfisUsuarios)
                .HasForeignKey(x => x.UsuarioId);

            builder.HasOne<Perfil>(x => x.Perfil);

            builder.HasKey(x => new { x.UsuarioId, x.PerfilId });
        }
    }
}
