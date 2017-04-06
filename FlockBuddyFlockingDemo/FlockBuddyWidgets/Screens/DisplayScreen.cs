using FlockBuddy;
using GameTimer;
using HadoukInput;
using MenuBuddy;
using Microsoft.Xna.Framework;
using PrimitiveBuddy;
using System.Collections.Generic;

namespace FlockBuddyWidgets
{
	public class DisplayScreen : Screen, IGameScreen
	{
		GameClock FlockTimer { get; set; }
		List<FlockManager> Flocks { get; set; }
		Primitive _prim;

		public DisplayScreen(List<FlockManager> flocks)
		{
			CoveredByOtherScreens = false;
			CoverOtherScreens = false;

			Flocks = flocks;
			FlockTimer = new GameClock();
		}

		public override void LoadContent()
		{
			base.LoadContent();

			_prim = new Primitive(ScreenManager.Game.GraphicsDevice, ScreenManager.SpriteBatch);
		}

		public override void Update(GameTime gameTime, bool otherWindowHasFocus, bool covered)
		{
			base.Update(gameTime, otherWindowHasFocus, covered);

			FlockTimer.Update(gameTime);
			foreach (var flock in Flocks)
			{
				flock.Flock.Update(FlockTimer);
			}
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			ScreenManager.SpriteBatchBegin();

			foreach (var flock in Flocks)
			{
				flock.Flock.Draw(_prim, flock.DebugColor);
			}

			ScreenManager.SpriteBatchEnd();
		}

		public void HandleInput(InputState input)
		{
		}
	}
}
