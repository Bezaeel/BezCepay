using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BezCepay.Data.IRepositories;
using BezCepay.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BezCepay.Data.Repositories
{
    public class PaymentRepository<TModel> : Repository<Payment>, IPaymentRepository
    {
        AppDbContext context;
        public PaymentRepository(AppDbContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public override async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await context.Payments.Include(c => c.Order).ToListAsync();
        }

        public async Task<Payment> GetAsync(Expression<Func<Payment, bool>> predicate)
        {
            return await context.Payments.Include(c => c.Order).Where(predicate).FirstOrDefaultAsync();
        }

    }
}