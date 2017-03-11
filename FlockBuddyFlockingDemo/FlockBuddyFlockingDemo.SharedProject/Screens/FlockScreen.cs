using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlockBuddyFlockingDemo
{
	public class FlockScreen : BaseTab
	{
		FlocksCollection Flocks { get; set; }
		FlockManager Flock { get; set; }

		public FlockScreen(FlockManager flock, FlocksCollection flocks) : base("FlockScreen")
		{
			Flocks = flocks;
			Flock = flock;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			AddHeader(Flock);

			//add the walls dropdown
			AddWallsDropdown();

			//add the predator dropdown
			AddPredatorDropdown();

			//add the prey dropdown
			AddPreyDropdown();

			//add the vip dropdown
			AddProtectDropdown();

			//add a Behvaiors button
			var behaviorsButton = CreateButton("Behaviors");
			behaviorsButton.OnClick += (obj, e) =>
			{
				ScreenManager.AddScreen(new BehaviorsScreen(Flock));
			};

			//add a Boids button
			var boidsButton = CreateButton("Boids");
			boidsButton.OnClick += (obj, e) =>
			{
				ScreenManager.AddScreen(new BoidsScreen(Flock));
			};

			AddItem(ToolStack);
		}

		protected void AddWallsDropdown()
		{
			ToolStack.AddItem(new Label("Wall Avoidance: ", FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			});
			var walls = new WallsDropdown(Flock)
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			walls.SelectedItem = Flock.Walls;
			ToolStack.AddItem(walls);
			AddShim();
		}

		protected void AddPredatorDropdown()
		{
			ToolStack.AddItem(new Label("Predators", FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			});
			var flockDropdown = new PredatorDropdown(Flock, Flocks)
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			ToolStack.AddItem(flockDropdown);
			AddShim();
		}

		protected void AddPreyDropdown()
		{
			ToolStack.AddItem(new Label("Prey", FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			});
			var flockDropdown = new PreyDropdown(Flock, Flocks)
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			ToolStack.AddItem(flockDropdown);
			AddShim();
		}

		protected void AddProtectDropdown()
		{
			ToolStack.AddItem(new Label("Protect", FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			});
			var flockDropdown = new ProtectDropdown(Flock, Flocks)
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			ToolStack.AddItem(flockDropdown);
			AddShim();
		}
	}
}
