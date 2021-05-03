using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helper;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
	public class LikesController : BaseApiController
	{
		private readonly IUserRepository _userRepository;
		private readonly ILikesRepository _likesRepository;

		public LikesController(IUserRepository userRepository,
			ILikesRepository likesRepository)
		{
			_userRepository = userRepository;
			_likesRepository = likesRepository;
		}
		[HttpPost("{username}")]
		public async Task<ActionResult> AddLike(string username)
		{
			var currentUserId = User.GetUserId();
			var likedUser = await _userRepository.GetUserByUsernameAsync(username);
			var currentUser = await _likesRepository.GetUserWithLike(currentUserId);

			if (likedUser == null)
				return NotFound();
			if (username == currentUser.UserName)
				return BadRequest("You cannot like yourself");
			var userLike = await _likesRepository.GetUserLike(currentUserId, likedUser.Id);
			if (userLike != null)
				return BadRequest("You already like this user");

			userLike = new UserLike
			{
				LikedUserId = likedUser.Id,
				SourceUserId = currentUser.Id
			};
			currentUser.LikedUsers.Add(userLike);
			if (await _userRepository.SaveAllAsync())
				return Ok();
			return BadRequest("Failed to like this user");

		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLike([FromQuery]LikesParams likesParams)
		{
			likesParams.UserId = User.GetUserId();
			var users =  await _likesRepository.GetUserLikes(likesParams);

			Response.AddPaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

			return Ok(users);
		}
	}
}
