using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPay.API.Models
{
	public class Payment
	{
		[Key]
		public Guid Id { get; set; }		
		[Required]
		[MaxLength(15)]
		[MinLength(15)]
		[RegularExpression("^[0-9]*$", ErrorMessage = "Only numbers are allowed")]
		[ForeignKey("Card")]
		public string CardNumber { get; set; }
		public double Amount { get; set; }
		public double Fee { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime UpdatedDate { get; set; }

	}
}
