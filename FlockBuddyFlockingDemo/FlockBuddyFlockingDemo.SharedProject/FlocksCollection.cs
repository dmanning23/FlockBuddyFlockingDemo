using FlockBuddy;
using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrimitiveBuddy;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlockBuddyFlockingDemo
{
    public class FlocksCollection
    {
		public List<FlockManager> Flocks { get; set; }

		List<Color> Colors { get; set; }

		Primitive _prim;

		public FlocksCollection()
		{
			Flocks = new List<FlockManager>();

			Colors = new List<Color>()
			{
				Color.Red,
				Color.Orange,
				Color.Yellow,
				Color.Green,
				Color.Blue,
				Color.Purple,
				Color.Pink,
				Color.Brown,
				Color.White,
				Color.Black,
			};
		}

		public void LoadContent(GraphicsDevice graphics, SpriteBatch spriteBatch)
		{
			_prim = new Primitive(graphics, spriteBatch);
		}

		public void Update(GameClock time)
		{
			foreach (var flock in Flocks)
			{
				flock.Flock.Update(time);
			}
		}

		public Color GetColor(int i)
		{
			return (i < Colors.Count) ? Colors[i] : Color.Black;
		}

		public void Draw()
		{
			foreach (var flock in Flocks)
			{
				flock.Flock.Draw(_prim, flock.DebugColor);
				//flock.Flock.DrawWhiskers(_prim, Color.Aqua);
			}
		}
	}
}
