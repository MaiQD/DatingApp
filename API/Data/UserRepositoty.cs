﻿using API.DTOs;
using API.Entities;
using API.Helper;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
	public class UserRepositoty : IUserRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public UserRepositoty(DataContext context,IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
		{
			// AsNoTracking bảo EF ngừng tracking entities, khi entities chỉ dùng để đọc
			var query = _context.Users.AsQueryable();

			query = query.Where(u => u.UserName != userParams.CurrentUsername);
			query = query.Where(u => u.Gender == userParams.Gender);

			var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
			var maxDob = DateTime.Today.AddYears(-userParams.MinAge);
			query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob);

			query = userParams.OrderBy switch
			{
				"created" => query.OrderByDescending(p => p.Created),
				_ => query.OrderByDescending(p => p.LastActive)
			};

			return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
		}

		public async Task<MemberDto> GetMemberUsernameAsync(string username)
		{
			return await _context
				.Users
				.Where(x => x.UserName == username)
				.ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();
		}

		public async Task<AppUser> GetUserByIdAsync(int id)
		{
			return await _context.Users.FindAsync(id);
		}

		public async Task<AppUser> GetUserByUsernameAsync(string username)
		{
			return await _context.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username);
		}

		public async Task<string> GetUserGender(string username)
		{
			return await _context.Users.Where(p => p.UserName == username).Select(p => p.Gender).FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<AppUser>> GetUsersAsync()
		{
			return await _context.Users.Include(p=>p.Photos).ToListAsync();
		}

		public void Update(AppUser user)
		{
			_context.Entry(user).State = EntityState.Modified;
		}
	}
}
