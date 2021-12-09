using BezCepay.Data.IRepositories;
using BezCepay.Data.Models;

namespace BezCepay.Data.Repositories
{
    public class OrderRepository<TModel> : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}