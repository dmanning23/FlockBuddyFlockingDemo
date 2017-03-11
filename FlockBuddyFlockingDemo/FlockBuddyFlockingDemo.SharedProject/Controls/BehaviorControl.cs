using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlockBuddyFlockingDemo
{
    public class BehaviorControl : StackLayout
	{
		public EBehaviorType Behavior { get; private set; }
		readonly BehaviorsScreen _behaviorScreen;
		public FlockManager Flock { get; private set; }

		public BehaviorControl(FlockManager flock, BehaviorsScreen behaviorScreen, EBehaviorType behavior) : base(StackAlignment.Left)
		{
			Flock = flock;
			_behaviorScreen = behaviorScreen;
			Behavior = behavior;
			Horizontal = HorizontalAlignment.Left;
			Vertical = VerticalAlignment.Top;

			int sizeDelta = 360;

			//add a label with the behavior name
			var button = new RelativeLayout()
			{
				Size = new Vector2(220f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = false,
			};
			button.AddItem(new Label(Behavior.ToString(), FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			});
			AddItem(button);
			sizeDelta -= button.Rect.Width;

			//add a shim
			var shim = new Shim()
			{
				Size = new Vector2(16f, 16f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};
			AddItem(shim);
			sizeDelta -= shim.Rect.Width;

			//add a num edit to change the weight
			var weight = new NumEdit(FontSize.Small)
			{
				Size = new Vector2(64f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = true,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				Number = Flock.GetBehaviorWeight(Behavior),

			};
			AddItem(weight);
			weight.OnNumberEdited += (obj, e) =>
			{
				Flock.SetBehaviorWeight(Behavior, weight.Number);
			};
			sizeDelta -= weight.Rect.Width;

			//add a shim
			shim = new Shim()
			{
				Size = new Vector2(16f, 16f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};
			AddItem(shim);
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
			AddItem(removeButton);
			sizeDelta -= removeButton.Rect.Width;

			//delete this flock and control when this button clicked
			removeButton.OnClick += (obj, e) =>
			{
				_behaviorScreen.RemoveBehavior(Behavior);
			};
		}
	}
}
