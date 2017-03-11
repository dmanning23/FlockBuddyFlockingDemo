using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;

namespace FlockBuddyFlockingDemo
{
	class PredatorDropdown : FlockDropdown
	{
		public PredatorDropdown(FlockManager flock, FlocksCollection flocks) : base(flock, flocks)
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
