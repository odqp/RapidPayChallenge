using RapidPay.API.Data;
using RapidPay.API.Models;
using RapidPay.API.Repository.Interface;

namespace RapidPay.API.Repository
{
	public class PaymentRepository : Repository<Payment>, IPaymentRepository
	{
		private readonly ApplicationDbContext _db;
		public PaymentRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Payment> UpdateAsync(Payment entity)
		{
			entity.UpdatedDate = DateTime.Now;
			_db.Payments.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
