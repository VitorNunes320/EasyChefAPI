﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("empresas")]
    public class Empresa : EntidadeBase
    {
        public Empresa() : base() { }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("usuario_id")]
        public Guid UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        [Column("habilitado")]
        public bool Habilitado { get; set; }
    }
}