using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RapidPay.API.Models;
using RapidPay.API.Models.Dto;
using RapidPay.API.Repository.Interface;
using RapidPay.API.Services.Interface;

namespace RapidPay.API.Controllers
{
	[Route("api/card/")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		protected APIResponse _response;
		private readonly IPaymentRepository _dbPayment;
		private readonly ICardRepository _dbCard;
		private readonly IMapper _mapper;
		private readonly IFeeService _feeService;

		public PaymentController(ICardRepository dbCard, IPaymentRepository dbPayment, IMapper mapper, IFeeService feeService)
		{
			_dbCard = dbCard;
			_dbPayment = dbPayment;
			_mapper = mapper;
			_feeService = feeService;
			_response = new();
		}

		[HttpPost]
		[Route("{number}/payment")]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> Post(string number, [FromBody] PaymentDTO createDTO)
		{
			try {
				if (createDTO.CardNumber != number)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				if (await _dbCard.GetAsync(u => u.Number.ToLower() == createDTO.CardNumber.ToLower()) == null)
				{
					ModelState.AddModelError("CustomError", "Card does not Exist!");
					_response.StatusCode = HttpStatusCode.BadRequest;

					return BadRequest(ModelState);
				}

				if (createDTO == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				var model = _mapper.Map<Payment>(createDTO);
				model.Fee = _feeService.CalculateFee();
				model.CreatedDate = DateTime.UtcNow;
				model.UpdatedDate = DateTime.UtcNow;

				await _dbPayment.CreateAsync(model);

				_response.Result = model;
				_response.StatusCode = HttpStatusCode.Created;
				return Ok(_response);
			}
			catch (Exception ex) //TODO: Improve the exceptions
			{
				_response.IsSuccess = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
				return _response;
			}
		}
	}
}
