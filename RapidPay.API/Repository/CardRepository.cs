using RapidPay.API.Data;
using RapidPay.API.Models;
using RapidPay.API.Repository.Interface;

namespace RapidPay.API.Repository
{
	public class CardRepository : Repository<Card>, ICardRepository
	{
		private readonly ApplicationDbContext _db;
		public CardRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Card> UpdateAsync(Card entity)
		{
			entity.UpdatedDate = DateTime.Now;
			_db.Cards.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
