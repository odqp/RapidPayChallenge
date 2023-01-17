using System.Linq.Expressions;

namespace RapidPay.API.Repository.Interface
{
	public interface IRepository<T> where T : class
	{
		Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string? includeProperties = null);
		Task CreateAsync(T entity);
		Task SaveAsync();
	}
}
