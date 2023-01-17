using RapidPay.API.Models;

namespace RapidPay.API.Repository.Interface
{
	public interface ICardRepository : IRepository<Card>
	{
		Task<Card> UpdateAsync(Card entity);
	}
}
