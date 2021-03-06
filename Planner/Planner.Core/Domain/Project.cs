﻿using System;
using System.Collections.Generic;

namespace Planner.Core.Domain
{
	public class Project
	{
		public int ID { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public DateTime UpdatedAt { get; private set; }

        public List<Item> Items { get; set; }

		public Project(string name, string description)
		{
            this.Name = name;
            this.Description = description;
			//this.ID = new Guid();
			this.CreatedAt = DateTime.Now;
		}

        public void SetDescription(string description)
		{
			//If needed we can add here validation for the field
			this.Description = description;
            SetUpdate();

        }

        public void SetName(string name)
		{
			this.Name = name;
            SetUpdate();

        }

		private void SetUpdate()
		{
			this.UpdatedAt = DateTime.Now;
		}
	}
}
