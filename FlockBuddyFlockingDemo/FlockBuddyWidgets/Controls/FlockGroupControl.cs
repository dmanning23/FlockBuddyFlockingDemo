using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlockBuddyWidgets
{
	public class FlockGroupControl : RelativeLayout
	{
		#region Properties

		FlockManager Target { get; set; }

		FlockManager Flock { get; set; }

		bool IsHeader { get; set; }

		StackLayout Stack { get; set; }

		ICheckbox PredatorCheckbox { get; set; }

		ICheckbox PreyCheckbox { get; set; }

		ICheckbox VipCheckbox { get; set; }

		#endregion //Properties

		#region Methods

		public FlockGroupControl(FlockManager target, FlockManager flock, bool isHeader, IScreen screen)
		{
			Target = target;
			Flock = flock;
			IsHeader = isHeader;

			//set the properties of the widget
			Vertical = VerticalAlignment.Top;
			Horizontal = HorizontalAlignment.Center;

			Stack = new StackLayout(StackAlignment.Left)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
			};

			//add the title
			AddFlockName(screen);

			//add the predator checkbox
			PredatorCheckbox = AddCheckbox(FlockGroup.Predator, screen);

			//add the prey checkbox
			PreyCheckbox = AddCheckbox(FlockGroup.Prey, screen);

			//add the vip checkbox
			VipCheckbox = AddCheckbox(FlockGroup.Vip, screen);

			Size = new Vector2(Stack.Rect.Width, Stack.Rect.Height);
			AddItem(Stack);
		}

		private void AddFlockName(IScreen screen)
		{
			//create the relative layout
			var layout = new RelativeLayout()
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(350f, 32f)
			};

			//add the label if this is the header
			var label = new Label(IsHeader ? "Flock Name" : Flock.Name, screen.Content, FontSize.Small)
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Left,
				TextColor = IsHeader ? Color.White : Flock.DebugColor,
				Highlightable = false,
			};
			layout.AddItem(label);
			Stack.AddItem(layout);
		}

		private ICheckbox AddCheckbox(FlockGroup group, IScreen screen)
		{
			ICheckbox checkbox = null;

			//create the relative layout
			var layout = new RelativeLayout()
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(64f, 32f)
			};

			//add the label if this is the header
			if (IsHeader)
			{
				var label = new Label(group.ToString(), screen.Content, FontSize.Small)
				{
					Vertical = VerticalAlignment.Center,
					Horizontal = HorizontalAlignment.Center,
					Highlightable = false,
				};
				layout.AddItem(label);
			}
			else
			{
				//add the checkbox otherwise
				checkbox = new Checkbox(Target.Flock.IsFlockInGroup(Flock.Flock, group))
				{
					Vertical = VerticalAlignment.Center,
					Horizontal = HorizontalAlignment.Center,
					Size = new Vector2(32f, 32f)
				};

				//setup the onclick of the checkbox
				checkbox.OnClick += (obj, e) =>
				{
					OnCheckboxClicked(group);
				};

				//add to the layout
				layout.AddItem(checkbox);
			}

			//add the layout to the widget
			Stack.AddItem(layout);

			//add a space to the widget
			Stack.AddItem(new Shim()
			{
				Size = new Vector2(16f, 16f),
			});

			//return the checkbox that was created
			return checkbox;
		}

		private void OnCheckboxClicked(FlockGroup group)
		{
			switch (group)
			{
				case FlockGroup.None:
					{
						PredatorCheckbox.IsChecked = false;
						PreyCheckbox.IsChecked = false;
						VipCheckbox.IsChecked = false;
					}
					break;
				case FlockGroup.Predator:
					{
						PreyCheckbox.IsChecked = false;
						VipCheckbox.IsChecked = false;
					}
					break;
				case FlockGroup.Prey:
					{
						PredatorCheckbox.IsChecked = false;
						VipCheckbox.IsChecked = false;
					}
					break;
				case FlockGroup.Vip:
					{
						PredatorCheckbox.IsChecked = false;
						PreyCheckbox.IsChecked = false;
					}
					break;
			}
		}

		public void OnAddGroup()
		{
			if (IsHeader)
			{
				return;
			}

			//first make sure the flock is not added to any groups
			Target.Flock.RemoveFlock(Flock.Flock);

			//add the flock to the correct group
			if (PredatorCheckbox.IsChecked)
			{
				Target.Flock.AddFlockToGroup(Flock.Flock, FlockGroup.Predator);
			}
			else if (PreyCheckbox.IsChecked)
			{
				Target.Flock.AddFlockToGroup(Flock.Flock, FlockGroup.Prey);
			}
			else if (VipCheckbox.IsChecked)
			{
				Target.Flock.AddFlockToGroup(Flock.Flock, FlockGroup.Vip);
			}
		}

		#endregion //Methods
	}
}
