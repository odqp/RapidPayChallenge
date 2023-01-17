using System.ComponentModel.DataAnnotations;

namespace RapidPay.API.Models.Dto
{
	public class PaymentDTO
	{
		[Required]
		[MaxLength(15)]
		[MinLength(15)]
		[RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
		public string CardNumber { get; set; }
		public double Amount { get; set; }
	}
}
