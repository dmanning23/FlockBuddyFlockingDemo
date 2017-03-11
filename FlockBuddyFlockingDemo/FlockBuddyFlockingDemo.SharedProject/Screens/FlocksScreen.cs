using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using ResolutionBuddy;

namespace FlockBuddyFlockingDemo
{
	/// <summary>
	/// This is the screen that displays all the flocks
	/// </summary>
	public class FlocksScreen : BaseTab
	{
		FlocksCollection Flocks { get; set; }
		IButton _addFlock;

		public FlocksScreen(FlocksCollection flocks) : base("FlocksScreen")
		{
			Flocks = flocks;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			//add a button for each flock in the system
			foreach (var flock in Flocks.Flocks)
			{
				AddFlockButton(flock);
			}

			//add the "Add Flock" button
			_addFlock = CreateButton("Add Flock");
			_addFlock.OnClick += (obj, e) =>
			{
				AddFlock();
			};

			AddItem(ToolStack);
		}

		private void AddFlockButton(FlockManager flockManager)
		{
			var flockControl = new FlockControl(flockManager, Flocks, this);

			//add a button control for it
			ToolStack.AddItem(flockControl);
		}

		public void AddFlock()
		{
			//create the flock
			var flockManager = new FlockManager(new Flock()) {
				DebugColor = Flocks.GetColor(Flocks.Flocks.Count)
			};
			Flocks.Flocks.Add(flockManager);

			flockManager.Flock.SetWorldSize(new Vector2(Resolution.ScreenArea.Width, Resolution.ScreenArea.Height));

			var flockControl = new FlockControl(flockManager, Flocks, this);

			//add a button control for it
			ToolStack.InsertItemBefore(flockControl, _addFlock);
		}

		public void RemoveFlock(FlockManager flock)
		{
			//remove from the flock collection
			Flocks.Flocks.Remove(flock);

			foreach (var flockManager in Flocks.Flocks)
			{
				flockManager.Flock.RemoveFlock(flock.Flock);
			}

			//remove the widget
			foreach (var item in ToolStack.Items)
			{
				var flockItem = item as FlockControl;
				if (flockItem != null && flockItem.Flock == flock)
				{
					ToolStack.RemoveItem(flockItem);
					return;
				}
			}
		}
	}
}
