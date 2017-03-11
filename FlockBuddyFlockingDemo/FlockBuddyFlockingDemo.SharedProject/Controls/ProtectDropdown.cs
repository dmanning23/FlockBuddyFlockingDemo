﻿using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;

namespace FlockBuddyFlockingDemo
{
	class ProtectDropdown : FlockDropdown
	{
		public ProtectDropdown(FlockManager flock, FlocksCollection flocks) : base(flock, flocks)
		{
			SelectedItem = flock.Flock.Vips;
		}

		protected override DropdownItem<IFlock> AddFlock(IFlock flock, Color color)
		{
			var flockWidget = base.AddFlock(flock, color);

			flockWidget.OnClick += (obj, e) =>
			{
				_flock.Flock.Vips = flock;
			};

			return flockWidget;
		}
	}
}
