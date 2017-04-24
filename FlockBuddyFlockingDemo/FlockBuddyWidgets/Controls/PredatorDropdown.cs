using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FlockBuddyWidgets
{
	class PredatorDropdown : FlockDropdown
	{
		public PredatorDropdown(FlockManager flock, List<FlockManager> flocks) : base(flock, flocks)
		{
			SelectedItem = flock.Flock.Predators;
		}

		protected override DropdownItem<IFlock> AddFlock(IFlock flock, Color color)
		{
			var flockWidget = base.AddFlock(flock, color);

			flockWidget.OnClick += (obj, e) =>
			{
				_flock.Flock.Predators = flock;
			};

			return flockWidget;
		}
	}
}
