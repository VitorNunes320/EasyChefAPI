using Data.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        private readonly DbSet<Pedido> _pedidos;

        public PedidoRepository(AppDbContext easyChefContext) : base(easyChefContext)
        {
            _pedidos = easyChefContext.Pedidos;
        }

    }
}
