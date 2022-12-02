using System;
using System.Collections.Generic;
using Tasks.Types;

namespace Tasks
{
	internal class Task
	{
		public long Identifier { get; set; }

		public string Description { get; set; }

		public Done Done { get; set; }
	}
}
