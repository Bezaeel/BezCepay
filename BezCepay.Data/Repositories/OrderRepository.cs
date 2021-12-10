using System.Collections.Generic;
using System.Threading.Tasks;
using BezCepay.Data.IRepositories;
using BezCepay.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BezCepay.Data.Repositories
{
    public class OrderRepository<TModel> : Repository<Order>, IOrderRepository
    {
        AppDbContext context;
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }
        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await context.Orders.Include(c => c.Payment).ToListAsync();
        }
    }
}