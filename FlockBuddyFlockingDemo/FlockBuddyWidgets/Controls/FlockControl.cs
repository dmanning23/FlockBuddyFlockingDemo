using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FlockBuddyWidgets
{
	public class FlockControl : StackLayout
	{
		FlocksScreen FlocksScreen { get; set; }
		public FlockManager Flock { get; private set; }
		List<FlockManager> _flocks;

		public FlockControl(FlockManager flock, List<FlockManager> flocks, FlocksScreen flocksScreen) : base(StackAlignment.Top)
		{
			Flock = flock;
			_flocks = flocks;
			FlocksScreen = flocksScreen;
			Horizontal = HorizontalAlignment.Left;
			Vertical = VerticalAlignment.Top;

			var flockButtons = new StackLayout(StackAlignment.Left)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};
		
			int sizeDelta = 360;

			//add a button with the flock name
			var button = new RelativeLayoutButton()
			{
				Size = new Vector2(256f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			button.AddItem(new TextEdit(Flock.Name, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				TextColor = Flock.DebugColor
			});
			flockButtons.AddItem(button);
			sizeDelta -= button.Rect.Width;

			//popup a flock window when this button is clicked
			button.OnClick += (obj, e) =>
			{
				flocksScreen.ScreenManager.AddScreen(new FlockScreen(Flock, _flocks));
			};

			//add a shim
			var shim = new Shim()
			{
				Size = new Vector2(16f, 16f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};
			flockButtons.AddItem(shim);
			sizeDelta -= shim.Rect.Width;

			//add a button to delete the flock
			var removeButton = new RelativeLayoutButton()
			{
				Size = new Vector2(sizeDelta, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			removeButton.AddItem(new Label("X", FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight)
			});
			flockButtons.AddItem(removeButton);
			sizeDelta -= removeButton.Rect.Width;

			//delete this flock and control when this button clicked
			removeButton.OnClick += (obj, e) =>
			{
				FlocksScreen.RemoveFlock(Flock);
			};

			AddItem(flockButtons);
			AddItem(shim = new Shim()
			{
				Size = new Vector2(16f, 16f)
			});
		}
	}
}
