using CellSpacePartitionLib;
using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlockBuddyWidgets
{
	/// <summary>
	/// This is the screen that displays all the widgets for adjusting flocks
	/// </summary>
	public class FlocksScreen : BaseTab
	{
		List<FlockManager> Flocks { get; set; }
		IButton _addFlock;

		public FlocksScreen(List<FlockManager> flocks) : base("FlocksScreen")
		{
			Flocks = flocks;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			//add a button for each flock in the system
			foreach (var flock in Flocks)
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
			var flockManager = new FlockManager(new Flock());
			Flocks.Add(flockManager);

			flockManager.Flock.CellSpace = new CellSpacePartition<IMover>(Vector2.Zero, 80, 16, 32);

			var flockControl = new FlockControl(flockManager, Flocks, this);

			//add a button control for it
			ToolStack.InsertItemBefore(flockControl, _addFlock);
		}

		public void RemoveFlock(FlockManager flock)
		{
			//remove from the flock collection
			Flocks.Remove(flock);

			foreach (var flockManager in Flocks)
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
