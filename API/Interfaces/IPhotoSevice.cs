using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace API.Interfaces
{
	public interface IPhotoSevice
	{
		Task<ImageUploadResult> AddPhotoAsync(IFormFile formFile);
		Task<DeletionResult> DeletePhotoAsync(string publicId);
	}
}