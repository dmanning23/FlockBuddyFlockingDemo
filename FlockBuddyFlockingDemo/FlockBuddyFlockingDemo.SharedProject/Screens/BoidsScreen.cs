using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlockBuddyFlockingDemo
{
	/// <summary>
	/// This 
	/// </summary>
	public class BoidsScreen : BaseTab
	{
		readonly FlockManager _flock;
		ILabel _numBoids;

		public BoidsScreen(FlockManager flock) : base("BoidsScreen")
		{
			_flock = flock;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			//Add the header
			AddHeader(_flock);

			//add the Num Boids control
			AddNumBoids();

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
				_flock.RemoveBoid();
				_numBoids.Text = _flock.Flock.Boids.Count.ToString();
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
				_flock.AddBoid();
				_numBoids.Text = _flock.Flock.Boids.Count.ToString();
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
			_numBoids = new Label(_flock.Flock.Boids.Count.ToString(), FontSize.Small)
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
	}
}
