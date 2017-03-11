using GameTimer;
using HadoukInput;
using MenuBuddy;
using Microsoft.Xna.Framework;

namespace FlockBuddyFlockingDemo
{
	public class DisplayScreen : Screen, IGameScreen
	{
		GameClock FlockTimer { get; set; }
		FlocksCollection Flocks { get; set; }

		public DisplayScreen(FlocksCollection flocks)
		{
			CoveredByOtherScreens = false;
			CoverOtherScreens = false;

			Flocks = flocks;
			FlockTimer = new GameClock();
		}

		public override void Update(GameTime gameTime, bool otherWindowHasFocus, bool covered)
		{
			base.Update(gameTime, otherWindowHasFocus, covered);

			FlockTimer.Update(gameTime);
			Flocks.Update(FlockTimer);
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			ScreenManager.SpriteBatchBegin();
			Flocks.Draw();
			ScreenManager.SpriteBatchEnd();
		}

		public void HandleInput(InputState input)
		{
		}
	}
}
