using API.Data;
using API.Helper;
using API.Interfaces;
using API.Services;
using API.SingleR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _config)
		{
			services.AddSingleton<PresenceTracker>();

			services.Configure<CloudinarySettings>(_config.GetSection("CloudingSettings"));
			//tạo dependency injection cho Token Service
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IPhotoSevice, PhotoService>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<LogUserActivity>();
			services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlite(_config.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
			});
			return services;
		}
	}
}