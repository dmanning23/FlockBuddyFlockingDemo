using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlockBuddyWidgets
{
	/// <summary>
	/// This is a window that displays all the flocks (except the target flock) with checkboxes for None, Predator, Prey, Vip
	/// The user can select one option for each flock.
	/// When the user hits OK all the selected flocks will be added to the correct flock group of the target.
	/// </summary>
	public class FlockGroupsMessageBox : MessageBoxScreen
	{
		#region Properties

		FlockManager Target { get; set; }

		List<FlockManager> Flocks { get; set; }

		List<FlockGroupControl> FlockControls { get; set; }

		#endregion //Properties

		#region Methods

		public FlockGroupsMessageBox(FlockManager target, List<FlockManager> flocks) : base("Select the group for each flock")
		{
			Target = target;
			Flocks = flocks.Where(x => x != target).ToList();
			FlockControls = new List<FlockGroupControl>();

			OnSelect += OnAddGroups;
		}

		protected override async Task AddAdditionalControls()
		{
			await base.AddAdditionalControls();

			//add a shim between the text and the buttons
			ControlStack.AddItem(new Shim() { Size = new Vector2(0, 16f) });

			//Add the headers to the page
			ControlStack.AddItem(new FlockGroupControl(null, null, true, this));
			ControlStack.AddItem(new Shim() { Size = new Vector2(0, 8f) });

			//populate the stack layout
			foreach (var flock in Flocks)
			{
				var groupControl = new FlockGroupControl(Target, flock, false, this);
				ControlStack.AddItem(groupControl);
				FlockControls.Add(groupControl);
			}
		}

		void OnAddGroups(object obj, EventArgs e)
		{
			foreach (var groupControl in FlockControls)
			{
				groupControl.OnAddGroup();
			}
		}

		#endregion //Methods
	}
}
