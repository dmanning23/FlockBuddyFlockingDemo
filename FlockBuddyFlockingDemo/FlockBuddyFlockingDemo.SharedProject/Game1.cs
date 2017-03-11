using FrameRateCounter;
using InputHelper;
using MenuBuddy;

namespace FlockBuddyFlockingDemo
{
	public class Game1 : MouseGame
	{
		FlocksCollection Flocks { get; set; }

		public Game1()
		{
			var debug = new DebugInputComponent(this, ResolutionBuddy.Resolution.TransformationMatrix);
			debug.DrawOrder = 1000;

			//add the fps counter
			var fps = new FpsCounter(this, @"Fonts\ArialBlack14");
			fps.DrawOrder = 100;
			Flocks = new FlocksCollection();
		}

		protected override void LoadContent()
		{
			base.LoadContent();
			Flocks.LoadContent(GraphicsDevice, ScreenManager.SpriteBatch);
		}

		protected override void InitStyles()
		{
			StyleSheet.SmallFontResource = @"Fonts\ArialBlack10";
			base.InitStyles();

			//DefaultStyles.Instance().MainStyle.HasOutline = true;
			//DefaultStyles.Instance().MenuEntryStyle.HasOutline = true;
			//DefaultStyles.Instance().MenuTitleStyle.HasOutline = true;
			//DefaultStyles.Instance().MessageBoxStyle.HasOutline = true;
		}

		public override IScreen[] GetMainMenuScreenStack()
		{
			return new IScreen[] { new DisplayScreen(Flocks), new FlocksScreen(Flocks) };
		}
	}
}