using System;
using System.ComponentModel.DataAnnotations;

namespace CentraliaDevTools.Models
{
	public class AdminMessage
	{
		[Key]
		public int AdminMessageID { get; set; }
		public string MessageContent { get; set; }
	}
}

