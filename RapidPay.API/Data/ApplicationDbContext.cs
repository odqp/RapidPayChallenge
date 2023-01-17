using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RapidPay.API.Models;

namespace RapidPay.API.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Card> Cards { get; set; }
		public DbSet<Payment> Payments { get; set; }
	}
}
