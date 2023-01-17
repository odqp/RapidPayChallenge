using Microsoft.AspNetCore.Identity;

namespace RapidPay.API.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}
