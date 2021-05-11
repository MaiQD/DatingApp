using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class DataContext : IdentityDbContext<AppUser,AppRole,int, 
		IdentityUserClaim<int>,AppUserRole,IdentityUserLogin<int>,
		IdentityRoleClaim<int>,IdentityUserToken<int>>
	{
		public DataContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<UserLike> Likes { get; set; }
		public DbSet<Message> Messages { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<AppUser>()
				.HasMany(u => u.UserRoles)
				.WithOne(r => r.User)
				.HasForeignKey(u => u.UserId)
				.IsRequired();

			builder.Entity<AppRole>()
				.HasMany(u => u.UserRoles)
				.WithOne(r => r.Role)
				.HasForeignKey(u => u.RoleId)
				.IsRequired();

			builder.Entity<UserLike>()
				.HasKey(k => new { k.SourceUserId, k.LikedUserId });

			//những user appuser đã like
			builder.Entity<UserLike>()
				.HasOne(s => s.SourceUser)
				.WithMany(l => l.LikedUsers)
				.HasForeignKey(s => s.SourceUserId)
				.OnDelete(DeleteBehavior.Cascade);

			//những user đã like appuser
			builder.Entity<UserLike>()
				.HasOne(s => s.LikedUser)
				.WithMany(l => l.LikedByUsers)
				.HasForeignKey(s => s.LikedUserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Message>()
				.HasOne(u => u.Recipient)
				.WithMany(m => m.MessageReceived)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Message>()
				.HasOne(u => u.Sender)
				.WithMany(m => m.MessageSent)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}