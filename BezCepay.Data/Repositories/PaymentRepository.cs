using BezCepay.Data.IRepositories;
using BezCepay.Data.Models;

namespace BezCepay.Data.Repositories
{
    public class PaymentRepository<TModel> : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}