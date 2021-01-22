using FlockBuddy;
using GameTimer;
using HadoukInput;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrimitiveBuddy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlockBuddyWidgets
{
	/// <summary>
	/// This is the playing field for the boids to run aroud in
	/// </summary>
	public class DisplayScreen : WidgetScreen, IGameScreen
	{
		protected GameClock FlockTimer { get; set; }
		protected List<FlockManager> Flocks { get; set; }
		protected Primitive Primitive;

		protected bool DrawDebugCells { get; set; }

		protected bool DrawDebugBoids { get; set; }

		protected object _lock = new object();

		public DisplayScreen(List<FlockManager> flocks) : base("DisplayScreen")
		{
			CoveredByOtherScreens = false;
			CoverOtherScreens = false;

			DrawDebugCells = true;
			DrawDebugBoids = true;

			Flocks = flocks;
			FlockTimer = new GameClock();
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			Primitive = new Primitive(ScreenManager.Game.GraphicsDevice, ScreenManager.SpriteBatch);
		}

		public override void Update(GameTime gameTime, bool otherWindowHasFocus, bool covered)
		{
			base.Update(gameTime, otherWindowHasFocus, covered);

			FlockTimer.Update(gameTime);

			lock (_lock)
			{
				foreach (var flock in Flocks)
				{
					flock.Flock.Update(FlockTimer);
				}
			}
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			if (DrawDebugCells || DrawDebugBoids)
			{
				SpriteBatchBegin(BlendState.NonPremultiplied);

				DrawCellSpace();
				DrawBoids();

				ScreenManager.SpriteBatchEnd();
			}
		}

		public void DrawBoids()
		{
			if (DrawDebugBoids)
			{
				foreach (var flock in Flocks)
				{
					flock.Flock.Draw(Primitive, flock.DebugColor);
				}
			}
		}

		public void DrawCellSpace()
		{
			if (DrawDebugCells)
			{
				//draw just the first cellspace so we can see the grid
				for (var i = 0; (i < Flocks.Count) && (i < 1); i++)
				{
					Flocks[i].Flock.DrawCells(Primitive);
				}
			}
		}

		public virtual void SpriteBatchBegin(Matrix matrix, BlendState blendState, SpriteSortMode sortMode = SpriteSortMode.Deferred)
		{
			ScreenManager.SpriteBatchBegin(blendState, sortMode);
		}

		public virtual void SpriteBatchBegin(BlendState blendState, SpriteSortMode sortMode = SpriteSortMode.Deferred)
		{
			ScreenManager.SpriteBatchBegin(blendState, sortMode);
		}

		public void HandleInput(IInputState input)
		{
		}
	}
}
