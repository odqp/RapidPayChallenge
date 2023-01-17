using System.ComponentModel.DataAnnotations;

namespace RapidPay.API.Models
{
	public class Card
	{
		[Key]
		[Required]
		[MaxLength(15)]
		[MinLength(15)]
		[RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
		public string Number { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }
		public ICollection<Payment> Payments { get; set; }
	}
}
