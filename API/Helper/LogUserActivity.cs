﻿using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
	public class LogUserActivity : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var resultContext = await next();

			if (!resultContext.HttpContext.User.Identity.IsAuthenticated)
				return;

			var userId = resultContext.HttpContext.User.GetUserId();
			var repo = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
			var user = await repo.UserRepository.GetUserByIdAsync(userId);
			user.LastActive = DateTime.Now;
			await repo.Complete();
		}
	}
}
