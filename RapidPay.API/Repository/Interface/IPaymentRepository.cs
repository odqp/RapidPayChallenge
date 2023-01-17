using RapidPay.API.Models;

namespace RapidPay.API.Repository.Interface
{
	public interface IPaymentRepository : IRepository<Payment>
	{
		Task<Payment> UpdateAsync(Payment entity);
	}
}
