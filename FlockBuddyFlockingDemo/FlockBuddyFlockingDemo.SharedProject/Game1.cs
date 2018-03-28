using FlockBuddy;
using FlockBuddyWidgets;
using FrameRateCounter;
using InputHelper;
using MenuBuddy;
using System.Collections.Generic;

namespace FlockBuddyFlockingDemo
{
	public class Game1 : MouseGame
	{
		List<FlockManager> _flocks;

		public Game1()
		{
			var debug = new DebugInputComponent(this, ResolutionBuddy.Resolution.TransformationMatrix);
			debug.DrawOrder = 1000;

			//add the fps counter
			var fps = new FpsCounter(this, @"Content\Fonts\ArialBlack14");
			fps.DrawOrder = 100;

			_flocks = new List<FlockManager>();
		}

		protected override void LoadContent()
		{
			base.LoadContent();
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
			return new IScreen[] { new DisplayScreen(_flocks), new FlocksScreen(_flocks) };
		}
	}
}