using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helper;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
	public class LikesRepository : ILikesRepository
	{
		private readonly DataContext _context;

		public LikesRepository(DataContext context)
		{
			_context = context;
		}
		public async Task<UserLike> GetUserLike(int sourceUserId, int likeUserId)
		{
			return await _context.Likes.FindAsync(sourceUserId, likeUserId);
		}

		public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams)
		{
			var users = _context.AppUsers.OrderBy(u => u.UserName).AsQueryable();
			var likes = _context.Likes.AsQueryable();

			//những user mà user hiện tại đã like
			if (likesParams.Predicate == "liked")
			{
				likes = likes.Where(like => like.SourceUserId == likesParams.UserId);
				users = likes.Select(like => like.LikedUser);
			}
			//những user đã like user hiện tại
			if (likesParams.Predicate == "likedBy")
			{
				likes = likes.Where(like => like.LikedUserId == likesParams.UserId);
				users = likes.Select(like => like.SourceUser);
			}
			var likedUser = users.Select(user => new LikeDto
			{
				UserName = user.UserName,
				KnownAs = user.KnownAs,
				Age = user.DateOfBirth.CalculateAge(),
				PhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain).Url,
				City = user.City,
				Id = user.Id
			});
			return await PagedList<LikeDto>.CreateAsync(likedUser,likesParams.PageNumber,likesParams.PageSize);
		}

		public async Task<AppUser> GetUserWithLike(int userId)
		{
			return await _context.AppUsers
				.Include(x => x.LikedUsers).
				FirstOrDefaultAsync(x => x.Id == userId);
		}
	}
}
