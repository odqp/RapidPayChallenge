using Microsoft.EntityFrameworkCore;
using RapidPay.API.Data;
using RapidPay.API.Repository.Interface;
using System.Linq.Expressions;
using System.Linq;

namespace RapidPay.API.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbSet = _db.Set<T>();
		}

		public async Task CreateAsync(T entity)
		{
			await dbSet.AddAsync(entity);
			await SaveAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			
			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (includeProperties != null)
			{
				foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}
			return await query.FirstOrDefaultAsync();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}


	}
}
