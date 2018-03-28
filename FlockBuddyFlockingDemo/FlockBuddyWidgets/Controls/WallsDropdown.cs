using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;
using System;

namespace FlockBuddyWidgets
{
	public class WallsDropdown : Dropdown<DefaultWalls>
	{
		FlockManager _flock;

		public WallsDropdown(FlockManager flock, IScreen screen) : base(screen)
		{
			_flock = flock;
			foreach (DefaultWalls wall in Enum.GetValues(typeof(DefaultWalls)))
			{
				AddWall(wall, screen);
			}
			SelectedItem = _flock.Walls;
		}

		private void AddWall(DefaultWalls wall, IScreen screen)
		{
			//add this dude
			var dropitem = new DropdownItem<DefaultWalls>(wall, this)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Size = new Vector2(350f, 48f)
			};

			var label = new Label(wall.ToString(), screen.Content, FontSize.Small)
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
