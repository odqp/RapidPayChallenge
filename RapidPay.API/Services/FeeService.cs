using RapidPay.API.Services.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RapidPay.API.Services
{
	public class FeeService : IFeeService
	{
		private DateTime _date;
		private double _lastFee = 1;
		public FeeService()
		{
			_lastFee = 1.1;
			_date = DateTime.Now;
		}

		private double getDecimalFee() {
			Random rnd = new Random();
			var randomDouble = rnd.NextDouble();
			var ramdomFee = randomDouble + rnd.Next(0, 1);

			ramdomFee = ramdomFee != 0 ? ramdomFee : 0.00001;

			return ramdomFee;
		}

		private void assignNewValues(double fee, DateTime dateLimitDate)
		{
			_date = dateLimitDate;
			_lastFee = fee;
		}
		public double CalculateFee()
		{
			double fee = _lastFee;
			var currentDate = DateTime.Now;
			var dateLimitDate = _date.AddHours(1);

			if (currentDate > dateLimitDate)
			{
				var newDecimalFee = getDecimalFee();
				fee *= newDecimalFee;
				//
				assignNewValues(fee, dateLimitDate);
			}

			return fee;
		}
	}
}
