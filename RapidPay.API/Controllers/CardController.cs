using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.API.Models;
using RapidPay.API.Models.Dto;
using RapidPay.API.Repository.Interface;

namespace RapidPay.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardController : ControllerBase
	{
		protected APIResponse _response;
		private readonly ICardRepository _dbCards;
		private readonly IMapper _mapper;

		public CardController(ICardRepository dbCard, IMapper mapper)
		{
			_dbCards = dbCard;
			_mapper = mapper;
			_response = new();
		}

		[Authorize(Roles = "admin")]
		[HttpGet("{number}", Name = "GetBalance")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> Get(string number)
		{
			try
			{
				if (String.IsNullOrWhiteSpace(number))
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(_response);
				}

				var card = await _dbCards.GetAsync(u => u.Number == number, includeProperties: "Payments");

				if (card == null)
				{
					_response.StatusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}

				var cardDTO = _mapper.Map<CardBalanceDTO>(card);
				cardDTO.Balance = card.Payments != null ? card.Payments.Sum(a => a.Amount + a.Fee) : 0;
				//next line is assuming Balance shoulb de negative
				cardDTO.Balance *= -1;
				_response.Result = cardDTO;
				_response.StatusCode = HttpStatusCode.OK;
			}
			catch (Exception ex) //TODO: Improve the exceptions
			{
				_response.IsSuccess = false;
				_response.ErrorMessages
					 = new List<string>() { ex.ToString() };
			}
			return Ok(_response);
		}

		[Authorize(Roles = "admin")]
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<APIResponse>> Post([FromBody] CardCreateDTO createDTO)
		{
			try 
			{
				if (await _dbCards.GetAsync(u => u.Number.ToLower() == createDTO.Number.ToLower()) != null)
				{
					ModelState.AddModelError("CustomError", "Card aready Exist!");
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest(ModelState);
				}

				if (createDTO == null)
				{
					_response.StatusCode = HttpStatusCode.BadRequest;
					return BadRequest();
				}

				var model = _mapper.Map<Card>(createDTO);
				model.CreatedDate= DateTime.UtcNow;
				model.UpdatedDate= DateTime.UtcNow;

				await _dbCards.CreateAsync(model);
				var cardCreated = _mapper.Map<CardCreatedDTO>(model);

				_response.Result = cardCreated;
				_response.StatusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetBalance", new { number = model.Number }, _response);
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
