using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System;

namespace FlockBuddyFlockingDemo
{
	public class FlockDropdown : Dropdown<IFlock>
	{
		FlocksCollection _flocks;
		protected FlockManager _flock;

		public FlockDropdown(FlockManager flock, FlocksCollection flocks)
		{
			_flocks = flocks;
			_flock = flock;
			foreach (var flockManager in flocks.Flocks)
			{
				if (flockManager != flock)
				{
					AddFlock(flockManager.Flock, flockManager.DebugColor);
				}
			}
		}

		protected virtual DropdownItem<IFlock> AddFlock(IFlock flock, Color color)
		{
			//add this dude
			var dropitem = new DropdownItem<IFlock>(flock, this)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Size = new Vector2(350f, 48f)
			};

			var label = new Label(color.ToString(), FontSize.Small)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				TextColor = color
			};

			dropitem.AddItem(label);
			AddDropdownItem(dropitem);

			return dropitem;
		}
	}
}
