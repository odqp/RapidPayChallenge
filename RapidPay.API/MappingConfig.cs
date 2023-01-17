using AutoMapper;
using RapidPay.API.Models;
using RapidPay.API.Models.Dto;
using RapidPay.API.Models.Dto.Authorization;

namespace RapidPay.API
{
    public class MappingConfig : Profile
	{
		public MappingConfig() {
			CreateMap<Card, CardCreateDTO>().ReverseMap();
			CreateMap<Card, CardCreatedDTO>().ReverseMap();
			CreateMap<Card, CardBalanceDTO>().ReverseMap();
			CreateMap<Payment, PaymentDTO>().ReverseMap();
			CreateMap<ApplicationUser, UserDTO>().ReverseMap();
		}
	}
}
