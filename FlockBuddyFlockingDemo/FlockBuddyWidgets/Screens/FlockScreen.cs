using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using RandomExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Vector2Extensions;

namespace FlockBuddyWidgets
{
	public class FlockScreen : BaseTab
	{
		List<FlockManager> Flocks { get; set; }
		FlockManager Flock { get; set; }

		ILabel _numBoids;

		Random _random = new Random();

		public FlockScreen(FlockManager flock, List<FlockManager> flocks) : base("FlockScreen")
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

			//add the groups button
			AddFlockGroupsButton();

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
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			removeButton.AddItem(new Label("-", Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
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
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			addButton.AddItem(new Label("+", Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
			});
			addButton.OnClick += (obj, e) =>
			{
				Flock.AddBoid(_random.NextVector2(0f, 1280f, 0f, 720f),
					_random.NextVector2(-1f, 1f, -1f, 1f).Normalized(),
					_random.NextFloat(Flock.BoidWalkSpeed, Flock.BoidMaxSpeed));
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
			_numBoids = new Label(Flock.Flock.Boids.Count.ToString(), Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			};
			relLayout.AddItem(_numBoids);
			flockButtons.InsertItemBefore(relLayout, shim);
			sizeDelta -= relLayout.Rect.Width;

			ToolStack.AddItem(flockButtons);
			AddShim();
		}

		protected void AddFlockGroupsButton()
		{
			//add the "Add Behavior" button
			var button = CreateButton("Manage Flock Groups");
			button.OnClick += (obj, e) =>
			{
				//create the popup box
				ScreenManager.AddScreen(new FlockGroupsMessageBox(Flock, Flocks));
			};
		}
	}
}
