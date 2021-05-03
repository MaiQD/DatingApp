using API.DTOs;
using API.Entities;
using API.Helper;
using System.Threading.Tasks;

namespace API.Interfaces
{
	public interface ILikesRepository
	{
		Task<UserLike> GetUserLike(int sourceUserId, int likeUserId);

		Task<AppUser> GetUserWithLike(int userId);

		Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
	}
}