using System.ComponentModel.DataAnnotations;

namespace RapidPay.API.Models.Dto
{
	public class CardCreatedDTO
	{
		[Required]
		[MaxLength(15)]
		[MinLength(15)]
		[RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
		public string Number { get; set; }
		public double Balance { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
