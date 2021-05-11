﻿using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Data
{
	public static class Seed
	{
		public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			if (await userManager.Users.AnyAsync()) return;

			var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
			var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
			if (users == null )
				return;

			var roles = new List<AppRole>
			{
				new AppRole{Name="Member"},
				new AppRole{Name="Admin"},
				new AppRole{Name="Moderator"},
			};

			foreach (var role in roles)
			{
				var res = await roleManager.CreateAsync(role);
			}
				

			foreach (var user in users)
			{
				user.UserName = user.UserName.ToLower();
				await userManager.CreateAsync(user, "a123");
				await userManager.AddToRoleAsync(user, "Member");
			}

			var admin = new AppUser
			{
				UserName = "admin"
			};
			await userManager.CreateAsync(admin, "a123");
			await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator" });
		}
	}
}