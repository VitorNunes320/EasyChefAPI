namespace Domain.Models.Pedido
{
    public class MesaModel
    {

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Codigo { get; set; }

        public bool Ocupada { get; set; }
    }
}
