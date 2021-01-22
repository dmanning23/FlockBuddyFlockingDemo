using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System;
using System.Threading.Tasks;

namespace FlockBuddyWidgets
{
	public class AddBehaviorMessageBox : MessageBoxScreen
	{
		readonly FlockManager _flock;
		Dropdown<EBehaviorType> _dropdown;
		readonly BehaviorsScreen _behaviorScreen;

		public AddBehaviorMessageBox(FlockManager flock, BehaviorsScreen behaviorScreen) : base("Select a behavior to add:", "")
		{
			_flock = flock;
			OnSelect += OnAddBehavior;
			_behaviorScreen = behaviorScreen;
		}


		protected override async Task AddAdditionalControls()
		{
			await base.AddAdditionalControls();

			//add a shim between the text and the buttons
			ControlStack.AddItem(new Shim() { Size = new Vector2(0, 16f) });

			//create the dropdown
			_dropdown = new Dropdown<EBehaviorType>(this)
			{
				Size = new Vector2(768f, 32f),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				HasBackground = true
			};

			//populate the dropdown
			foreach (EBehaviorType behavior in Enum.GetValues(typeof(EBehaviorType)))
			{
				if (!_flock.HasBehavior(behavior))
				{
					var dropitem = new DropdownItem<EBehaviorType>(behavior, _dropdown)
					{
						Vertical = VerticalAlignment.Center,
						Horizontal = HorizontalAlignment.Center,
						Size = new Vector2(768f, 32f)
					};

					var label = new Label(behavior.ToString(), Content, FontSize.Small)
					{
						Vertical = VerticalAlignment.Center,
						Horizontal = HorizontalAlignment.Center,
					};

					dropitem.AddItem(label);
					_dropdown.AddDropdownItem(dropitem);
				}
			}

			//add the dropdown to the controlstack
			ControlStack.AddItem(_dropdown);
		}

		void OnAddBehavior(object obj, EventArgs e)
		{
			var selectedBehavior = _dropdown.SelectedItem;
			_behaviorScreen.AddBehavior(selectedBehavior);
		}
	}
}
