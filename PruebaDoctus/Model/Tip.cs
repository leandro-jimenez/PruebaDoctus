using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PruebaDoctus.Model
{
	public class Tip
	{
		[PrimaryKey]
		public string Id { get; set; }
		public string Date { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

	}
}
