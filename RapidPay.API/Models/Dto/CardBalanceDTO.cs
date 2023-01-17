using System.ComponentModel.DataAnnotations;

namespace RapidPay.API.Models.Dto
{
	public class CardBalanceDTO
	{
		public string Number { get; set; }
		public double Balance { get; set; }
	}
}
