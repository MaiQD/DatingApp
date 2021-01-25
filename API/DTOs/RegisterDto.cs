using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
	public class RegisterDto
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		[StringLength(10,MinimumLength =2)]
		public string Password { get; set; }
	}
}
