using System;
using Domain.Common;

namespace Domain.Models
{
	public class Group:BaseEntity
	{
		public string Name { get; set; }
		public string TeacherName { get; set; }
		public string Room { get; set; }

	}
}

