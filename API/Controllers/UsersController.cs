using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
	public class UsersController : BaseApiController
	{
		private readonly DataContext _dataContext;

		public UsersController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
		{
			return await _dataContext.AppUsers.ToListAsync();
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<AppUser>> GetUsers(int id)
		{
			return await _dataContext.AppUsers.FindAsync(id);
		}
	}
}
