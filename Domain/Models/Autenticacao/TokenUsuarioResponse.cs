namespace Domain.Models.Autenticacao
{
    /// <summary>
    /// Dados do usuário
    /// </summary>
    public class TokenUsuarioResponse
    {
        /// <summary>
        /// Id do usuário
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Foto do usuário
        /// </summary>
        public string? Foto { get; set; }

        /// <summary>
        /// Token do usuário
        /// </summary>
        public TokenResponse Tokens { get; set; }
    }
}
