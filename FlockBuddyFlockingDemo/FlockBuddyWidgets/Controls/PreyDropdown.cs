using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FlockBuddyWidgets
{
	public class PreyDropdown : FlockDropdown
	{
		public PreyDropdown(FlockManager flock, List<FlockManager> flocks) : base(flock, flocks)
		{
			SelectedItem = flock.Flock.Prey;
		}

		protected override DropdownItem<IFlock> AddFlock(IFlock flock, Color color)
		{
			var flockWidget = base.AddFlock(flock, color);

			flockWidget.OnClick += (obj, e) =>
			{
				_flock.Flock.Prey = flock;
			};

			return flockWidget;
		}
	}
}
