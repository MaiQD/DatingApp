using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly DataContext _dataContext;

		public UsersController(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		[HttpGet]
		public ActionResult<IEnumerable<AppUser>> GetUsers()
		{
			return _dataContext.AppUsers.ToList();
		}
		[HttpGet("{id}")]
		public ActionResult<AppUser> GetUsers(int id)
		{
			return _dataContext.AppUsers.Find(id);
		}
	}
}
