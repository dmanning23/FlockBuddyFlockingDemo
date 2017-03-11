using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System;

namespace FlockBuddyFlockingDemo
{
	public class WallsDropdown : Dropdown<DefaultWalls>
	{
		FlockManager _flock;

		public WallsDropdown(FlockManager flock)
		{
			_flock = flock;
			foreach (DefaultWalls wall in Enum.GetValues(typeof(DefaultWalls)))
			{
				AddWall(wall);
			}
			SelectedItem = _flock.Walls;
		}

		private void AddWall(DefaultWalls wall)
		{
			//add this dude
			var dropitem = new DropdownItem<DefaultWalls>(wall, this)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Size = new Vector2(350f, 48f)
			};

			var label = new Label(wall.ToString(), FontSize.Small)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center
			};

			dropitem.AddItem(label);
			AddDropdownItem(dropitem);

			dropitem.OnClick += (obj, e) =>
			{
				_flock.AddDefaultWalls(wall, Resolution.ScreenArea);
			};
		}
    }
}
