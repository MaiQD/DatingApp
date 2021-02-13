using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly DataContext _dataContext;
		private readonly ITokenService _tokenService;
		private readonly IUserRepository _userRepository;

		public AccountController(DataContext dataContext,
			ITokenService tokenService,
			IUserRepository userRepository)
		{
			_dataContext = dataContext;
			_tokenService = tokenService;
			_userRepository = userRepository;
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			if (await IsExistsUser(registerDto.UserName)) return BadRequest("Username is taken");
			using var hmac = new HMACSHA512();

			var user = new AppUser
			{
				UserName = registerDto.UserName.ToLower(),
				PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
				PasswordSalt = hmac.Key
			};
			_dataContext.Add(user);
			await _dataContext.SaveChangesAsync();
			return new UserDto { Username = user.UserName, Token = _tokenService.CreateToken(user) };
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _userRepository.GetUserByUsernameAsync(loginDto.UserName.ToLower());
			if (user == null)
				return Unauthorized("Invalid username");
			using var hmac = new HMACSHA512(user.PasswordSalt);
			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
			for (int i = 0; i < user.PasswordHash.Length; i++)
				if (computedHash[i] != user.PasswordHash[i])
					return Unauthorized("Invalid password");
			return new UserDto { 
				Username = user.UserName, 
				Token = _tokenService.CreateToken(user), 
				PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url 
			};
		}

		private async Task<bool> IsExistsUser(string userName)
		{
			return await _dataContext.AppUsers.AnyAsync(p => p.UserName == userName.ToLower());
		}
	}
}