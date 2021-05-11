using API.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
	public class AppUser:IdentityUser<int>
	{
		public DateTime DateOfBirth { get; set; }
		public string KnownAs { get; set; }
		public DateTime Created { get; set; } = DateTime.Now;
		public DateTime LastActive { get; set; } = DateTime.Now;
		public string Gender { get; set; }
		public string Introduction { get; set; }
		public string LookingFor { get; set; }
		public string Interests { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public virtual ICollection<Photo> Photos { get; set; }
		public virtual ICollection<UserLike> LikedByUsers { get; set; }
		public virtual ICollection<UserLike> LikedUsers { get; set; }
		public virtual ICollection<Message> MessageSent { get; set; }
		public virtual ICollection<Message> MessageReceived { get; set; }
		public ICollection<AppUserRole> UserRoles { get; set; }
	}
}
