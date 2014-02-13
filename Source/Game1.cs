using FontBuddyLib;
using Microsoft.Xna.Framework.Graphics;
using Vector2Extensions;
using System.Collections.Generic;
using GameTimer;
using Microsoft.Xna.Framework;
using HadoukInput;
using Microsoft.Xna.Framework.Input;
using FlockBuddy;
using System;
using RandomExtensions;
using BasicPrimitiveBuddy;

namespace FlockBuddyFlockingDemo
{
	/// <summary>
	/// this dude verifies that all the controller wrapper is wrapping things for the inputwrapper correctly
	/// checks all controllers are being checked correctly
	/// checks the forward/back is being checked correctly
	/// checks that the scrubbed/powercurve is working correctly
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		#region Members

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private InputState m_Input = new InputState();

		Flock Dudes { get; set; }

		Flock BadGuys { get; set; }

		List<BaseEntity> Obstacles { get; set; }

		Random g_Random = new Random();

		Texture2D boidTexture;

		XNABasicPrimitive prim;
		bool DrawCells = false;
		bool drawNeighbors = false;
		bool drawVectors = false;

		#endregion //Members

		#region Methods

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferWidth = 1024;
			graphics.PreferredBackBufferHeight = 768;
			graphics.IsFullScreen = false;

			Reset1();
		}

		private void Reset()
		{
			//create the flock of dudes
			Dudes = new Flock();
			Dudes.SetWorldSize(new Vector2(1024.0f, 768.0f), true, true, 5, 4);
			for (int i = 0; i < 50; i++)
			{
				//create a random dude
				AddDude(g_Random.NextVector2(0.0f, 1024.0f, 0.0f, 768.0f),
					g_Random.NextVector2(-1.0f, 1.0f, -1.0f, 0.50f),
					50.0f + (g_Random.NextFloat() * 50.0f));
			}

			//create the flock of bad guys

			//create the obstacles

			//set the dudes to run away from bad guys

			//make the bad guys chase the dudes

			//make everybody avoid the obstacles
		}

		public void Reset1()
		{
			Dudes = new Flock();
			Dudes.SetWorldSize(new Vector2(1024.0f, 768.0f), true, true, 5, 4);

			//AddDude(new Vector2(600.0f, 100.0f),
			//		new Vector2(-0.5f, 1.5f),
			//		50.0f);

			AddDude(new Vector2(300.0f, 500.0f),
					new Vector2(-0.5f, -1.5f),
					50.0f);

			AddDude(new Vector2(100.0f, 500.0f),
					new Vector2(0.0f, -1.0f),
					50.0f);

		}

		public void AddDude(Vector2 pos, Vector2 heading, float speed)
		{
			heading.Normalize();

			var dude = new Boid(
						Dudes,
						pos,
						10.0f,
						heading,
						speed,
						1.0f,
						500.0f,
						0.5f,
						100.0f);

			//setup his behaviors
			dude.Behaviors.ActivateBehaviors(new EBehaviorType[] {
					EBehaviorType.alignment,
					EBehaviorType.cohesion,
					EBehaviorType.separation 
				});

			Dudes.AddDude(dude);
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			boidTexture = this.Content.Load<Texture2D>("boid.png");
			prim = new XNABasicPrimitive(GraphicsDevice, spriteBatch);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}

			m_Input.Update();

			//check if the player wants to reset the simulation
			if (CheckKeyDown(m_Input, Keys.Z))
			{
				Reset();
			}
			if (CheckKeyDown(m_Input, Keys.A))
			{
				Reset1();
			}

			//check if the player wants to reset the simulation
			if (CheckKeyDown(m_Input, Keys.X))
			{
				DrawCells = !DrawCells;
			}

			//check if we want to draw the neighbors
			if (CheckKeyDown(m_Input, Keys.C))
			{
				drawNeighbors = !drawNeighbors;
			}

			//check if we want to draw the neighbors
			if (CheckKeyDown(m_Input, Keys.V))
			{
				drawVectors = !drawVectors;
			}

			//update the flock
			Dudes.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Gray);

			spriteBatch.Begin();

			if (DrawCells)
			{
				Dudes.DrawCells(prim);
			}

			foreach (Boid dude in Dudes.Dudes)
			{
				spriteBatch.Draw(boidTexture, dude.Position, null, Color.White, dude.Rotation, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
			}

			//draw neighbors?
			if (drawNeighbors)
			{
				Dudes.Dudes[0].DrawNeigbors(prim);
			}

			if (drawVectors)
			{
				Dudes.DrawVectors(prim);
			}
			
			spriteBatch.End();

			base.Draw(gameTime);
		}

		/// <summary>
		/// Check if a keyboard key was pressed this update
		/// </summary>
		/// <param name="rInputState">current input state</param>
		/// <param name="i">controller index</param>
		/// <param name="myKey">key to check</param>
		/// <returns>bool: key was pressed this update</returns>
		private bool CheckKeyDown(InputState rInputState, Keys myKey)
		{
			return (rInputState.CurrentKeyboardState.IsKeyDown(myKey) && rInputState.LastKeyboardState.IsKeyUp(myKey));
		}

		#endregion //Methods
	}
}
