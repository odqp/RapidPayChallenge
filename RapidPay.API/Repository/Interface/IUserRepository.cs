using RapidPay.API.Models.Dto.Authorization;

namespace RapidPay.API.Repository.Interface
{
    public interface IUserRepository
	{
		bool IsUniqueUser(string username);
		Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
		Task<UserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
	}
}
