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

		ILabel _numBoids;

		public FlockScreen(FlockManager flock, FlocksCollection flocks) : base("FlockScreen")
		{
			Flocks = flocks;
			Flock = flock;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			AddHeader(Flock);

			//add the Num Boids control
			AddNumBoids();

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


		private void AddNumBoids()
		{
			var flockButtons = new StackLayout(StackAlignment.Left)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};

			int sizeDelta = 360;

			//add a subtract button
			var removeButton = new RelativeLayoutButton()
			{
				Size = new Vector2(32f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			removeButton.AddItem(new Label("-", FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
			});
			removeButton.OnClick += (obj, e) =>
			{
				Flock.RemoveBoid();
				_numBoids.Text = Flock.Flock.Boids.Count.ToString();
			};
			flockButtons.AddItem(removeButton);
			sizeDelta -= removeButton.Rect.Width;

			//add a shim
			var shim = new Shim()
			{
				Size = new Vector2(16f, 16f)
			};
			flockButtons.AddItem(shim);
			sizeDelta -= shim.Rect.Width;

			//add the "add" button
			var addButton = new RelativeLayoutButton()
			{
				Size = new Vector2(32f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			addButton.AddItem(new Label("+", FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
			});
			addButton.OnClick += (obj, e) =>
			{
				Flock.AddBoid();
				_numBoids.Text = Flock.Flock.Boids.Count.ToString();
			};
			flockButtons.AddItem(addButton);
			sizeDelta -= addButton.Rect.Width;

			//add another shim
			shim = new Shim()
			{
				Size = new Vector2(16f, 16f)
			};
			flockButtons.InsertItemBefore(shim, addButton);
			sizeDelta -= shim.Rect.Width;

			//add a label with the number of boids
			var relLayout = new RelativeLayout()
			{
				Size = new Vector2(sizeDelta, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = false
			};
			_numBoids = new Label(Flock.Flock.Boids.Count.ToString(), FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			};
			relLayout.AddItem(_numBoids);
			flockButtons.InsertItemBefore(relLayout, shim);
			sizeDelta -= relLayout.Rect.Width;

			ToolStack.AddItem(flockButtons);
			AddShim();
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
