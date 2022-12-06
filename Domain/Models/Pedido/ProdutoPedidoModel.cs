namespace Domain.Models.Pedido
{
    public class ProdutoPedidoModel
    {
        public Guid Id { get; set; }

        public string? Imagem { get; set; }

        public string? Nome { get; set; }

        public string? NomeCliente { get; set; }

        public decimal? Valor { get; set; }

        public decimal Quantidade { get; set; }
    }
}
