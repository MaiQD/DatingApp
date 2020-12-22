﻿using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly DataContext _dataContext;

		public AccountController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		[HttpPost("register")]
		public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
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
			return user;
		}
		[HttpPost("login")]
		public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
		{
			var user = await _dataContext.AppUsers.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName.ToLower());
			if (user == null)
				return Unauthorized("Invalid username");
			using var hmac = new HMACSHA512(user.PasswordSalt);
			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
			for (int i = 0; i < user.PasswordHash.Length; i++)
				if (computedHash[i] != user.PasswordHash[i])
					return Unauthorized("Invalid password");
			return user;
		}
		private async Task<bool> IsExistsUser(string userName)
		{
			return await _dataContext.AppUsers.AnyAsync(p => p.UserName == userName.ToLower());
		}
	}
}
