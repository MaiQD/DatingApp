using API.Data;
using API.Helper;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration _config)
		{
			services.Configure<CloudinarySettings>(_config.GetSection("CloudingSettings"));
			//tạo dependency injection cho Token Service
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IPhotoSevice, PhotoService>();
			services.AddScoped<ILikesRepository, LikesRepository>();
			services.AddScoped<LogUserActivity>();
			services.AddScoped<IUserRepository, UserRepositoty>();
			services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
			services.AddDbContext<DataContext>(options =>
			{
				options.UseSqlite(_config.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
			});
			return services;
		}
	}
}
