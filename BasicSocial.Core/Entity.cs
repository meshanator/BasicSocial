using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSocial.Core
{
	public abstract class Entity : IEntity
	{
		[Key]
		public int Id { get; set; }
	}
}
