using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Data
{
	public class Seed
	{
		public static async Task SeedUsers(DataContext context)
		{
			if (await context.AppUsers.AnyAsync()) return;

			var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
			var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
			foreach (var user in users)
			{
				using var hmac = new HMACSHA512();
				user.UserName = user.UserName.ToLower();
				user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123"));
				user.PasswordSalt = hmac.Key;

				context.AppUsers.Add(user);
			}

			await context.SaveChangesAsync();
		}
	}
}
