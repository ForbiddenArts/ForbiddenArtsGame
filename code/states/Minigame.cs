using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ForbiddenArtsGame.code.states
{
	class Minigame : GameState
	{
		enum TileContents { Emp, Wal, Box, Man, Lad, Gol, BoxInGoal, ManInGoal };
		TileContents[,] map;
		Point[] GoalPoints;
		Point playerLoc;

		Texture2D spriteSheet;
		Rectangle WallSrc, BoxSrc, ManSrc, LadderSrc, GoalSrc, BoxInGoalSrc, ManInGoalSrc;

		KeyboardState lastState;
		TimeSpan untilNextControllerMove;

		public Minigame()
		{
			spriteSheet = SheetHandler.getSheet("famg");
			LoadMap();
			WallSrc = new Rectangle(0, 0, 20, 20);
			BoxSrc = new Rectangle(20, 0, 20, 20);
			ManSrc = new Rectangle(40, 0, 20, 20);
			LadderSrc = new Rectangle(60, 0, 20, 20);
			GoalSrc = new Rectangle(0, 20, 20, 20);
			BoxInGoalSrc = new Rectangle(20, 20, 20, 20);
			ManInGoalSrc = new Rectangle(40, 20, 20, 20);

			lastState = Keyboard.GetState();
			untilNextControllerMove = TimeSpan.Zero;
		}

		protected void LoadMap()
		{
			map = new TileContents[12, 10]{
				{TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Wal, TileContents.Wal, TileContents.Wal, TileContents.Emp},
				{TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Wal, TileContents.Lad, TileContents.Wal, TileContents.Emp},
				{TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Wal, TileContents.Gol, TileContents.Wal, TileContents.Emp},
				{TileContents.Emp, TileContents.Emp, TileContents.Wal, TileContents.Wal, TileContents.Wal, TileContents.Wal, TileContents.Wal, TileContents.Gol, TileContents.Wal, TileContents.Emp},
				{TileContents.Emp, TileContents.Wal, TileContents.Wal, TileContents.Emp, TileContents.Box, TileContents.Emp, TileContents.Emp, TileContents.Gol, TileContents.Wal, TileContents.Emp},
				{TileContents.Emp, TileContents.Wal, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Box, TileContents.Box, TileContents.Emp, TileContents.Wal, TileContents.Emp},
				{TileContents.Emp, TileContents.Wal, TileContents.Emp, TileContents.Emp, TileContents.Box, TileContents.Wal, TileContents.Emp, TileContents.Emp, TileContents.Wal, TileContents.Emp},
				{TileContents.Emp, TileContents.Wal, TileContents.Wal, TileContents.Wal, TileContents.Emp, TileContents.Wal, TileContents.Wal, TileContents.Emp, TileContents.Wal, TileContents.Emp},
				{TileContents.Emp, TileContents.Wal, TileContents.Emp, TileContents.Emp, TileContents.Box, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Wal, TileContents.Emp},
				{TileContents.Emp, TileContents.Wal, TileContents.Man, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Wal, TileContents.Wal, TileContents.Wal, TileContents.Emp},
				{TileContents.Emp, TileContents.Wal, TileContents.Wal, TileContents.Wal, TileContents.Wal, TileContents.Wal, TileContents.Wal, TileContents.Emp, TileContents.Emp, TileContents.Emp},
				{TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp, TileContents.Emp},
			};

			GoalPoints = new Point[3]{
				new Point(2,7),
				new Point(3,7),
				new Point(4,7),
			};

			playerLoc = new Point(9, 2);
		}

		public override StateReturn Update(GameTime gameTime)
		{
			KeyboardState newKeyboard = Keyboard.GetState();
			if (untilNextControllerMove != TimeSpan.Zero)
			{
				untilNextControllerMove -= gameTime.ElapsedGameTime;
				if (untilNextControllerMove < TimeSpan.Zero)
					untilNextControllerMove = TimeSpan.Zero;
			}

			if (Keyboard.GetState().IsKeyDown(Keys.R) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
				LoadMap();

			bool won = true;
			foreach (Point p in GoalPoints)
			{
				if (map[p.X, p.Y] != TileContents.BoxInGoal) won = false;
			}
			if (won) return StateReturn.True;

			if ((newKeyboard.IsKeyDown(Keys.Up) && !lastState.IsKeyDown(Keys.Up)) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.5 && untilNextControllerMove == TimeSpan.Zero))
			{
				untilNextControllerMove = new TimeSpan(TimeSpan.TicksPerSecond / 4);
				Point moveTo = new Point(playerLoc.X, playerLoc.Y - 1);
				Point boxMoveTo = new Point(playerLoc.X, playerLoc.Y - 2);
				switch (map[moveTo.X, moveTo.Y])
				{
					case TileContents.Emp:
						map[moveTo.X, moveTo.Y] = TileContents.Man;
						if (map[playerLoc.X, playerLoc.Y] == TileContents.ManInGoal)
							map[playerLoc.X, playerLoc.Y] = TileContents.Gol;
						else
							map[playerLoc.X, playerLoc.Y] = TileContents.Emp;
						playerLoc = moveTo;
						break;
					case TileContents.Box:
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Emp)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.Box;
							map[moveTo.X, moveTo.Y] = TileContents.Emp;
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.BoxInGoal;
							map[moveTo.X, moveTo.Y] = TileContents.Emp;
							break;
						}
						break;
					case TileContents.BoxInGoal:
						if (map[boxMoveTo.X, boxMoveTo.Y] != TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.Box;
							map[moveTo.X, moveTo.Y] = TileContents.Gol;
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.BoxInGoal;
							map[moveTo.X, moveTo.Y] = TileContents.Gol;
							break;
						}
						break;
					case TileContents.Gol:
						map[moveTo.X, moveTo.Y] = TileContents.ManInGoal;
						if (map[playerLoc.X, playerLoc.Y] == TileContents.ManInGoal)
							map[playerLoc.X, playerLoc.Y] = TileContents.Gol;
						else
							map[playerLoc.X, playerLoc.Y] = TileContents.Emp;
						playerLoc = moveTo;
						break;
				}
			}
			else if (newKeyboard.IsKeyDown(Keys.Down) && !lastState.IsKeyDown(Keys.Down) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.5 && untilNextControllerMove == TimeSpan.Zero))
			{
				untilNextControllerMove = new TimeSpan(TimeSpan.TicksPerSecond / 4);
				Point moveTo = new Point(playerLoc.X, playerLoc.Y + 1);
				Point boxMoveTo = new Point(playerLoc.X, playerLoc.Y + 2);
				switch (map[moveTo.X, moveTo.Y])
				{
					case TileContents.Emp:
						map[moveTo.X, moveTo.Y] = TileContents.Man;
						if (map[playerLoc.X, playerLoc.Y] == TileContents.ManInGoal)
							map[playerLoc.X, playerLoc.Y] = TileContents.Gol;
						else
							map[playerLoc.X, playerLoc.Y] = TileContents.Emp;
						playerLoc = moveTo;
						break;
					case TileContents.Box:
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Wal || map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Lad)
						{
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Emp)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.Box;
							map[moveTo.X, moveTo.Y] = TileContents.Emp;
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.BoxInGoal;
							map[moveTo.X, moveTo.Y] = TileContents.Emp;
							break;
						}
						break;
					case TileContents.BoxInGoal:
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Wal || map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Lad)
						{
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] != TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.Box;
							map[moveTo.X, moveTo.Y] = TileContents.Gol;
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.BoxInGoal;
							map[moveTo.X, moveTo.Y] = TileContents.Gol;
							break;
						}
						break;
					case TileContents.Gol:
						map[moveTo.X, moveTo.Y] = TileContents.ManInGoal;
						if (map[playerLoc.X, playerLoc.Y] == TileContents.ManInGoal)
							map[playerLoc.X, playerLoc.Y] = TileContents.Gol;
						else
							map[playerLoc.X, playerLoc.Y] = TileContents.Emp;
						playerLoc = moveTo;
						break;
				}
			}
			else if (newKeyboard.IsKeyDown(Keys.Left) && !lastState.IsKeyDown(Keys.Left) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.5 && untilNextControllerMove == TimeSpan.Zero))
			{
				untilNextControllerMove = new TimeSpan(TimeSpan.TicksPerSecond / 4);
				Point moveTo = new Point(playerLoc.X - 1, playerLoc.Y);
				Point boxMoveTo = new Point(playerLoc.X - 2, playerLoc.Y);
				switch (map[moveTo.X, moveTo.Y])
				{
					case TileContents.Emp:
						map[moveTo.X, moveTo.Y] = TileContents.Man;
						if (map[playerLoc.X, playerLoc.Y] == TileContents.ManInGoal)
							map[playerLoc.X, playerLoc.Y] = TileContents.Gol;
						else
							map[playerLoc.X, playerLoc.Y] = TileContents.Emp;
						playerLoc = moveTo;
						break;
					case TileContents.Box:
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Wal || map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Lad)
						{
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Emp)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.Box;
							map[moveTo.X, moveTo.Y] = TileContents.Emp;
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.BoxInGoal;
							map[moveTo.X, moveTo.Y] = TileContents.Emp;
							break;
						}
						break;
					case TileContents.BoxInGoal:
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Wal || map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Lad)
						{
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] != TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.Box;
							map[moveTo.X, moveTo.Y] = TileContents.Gol;
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.BoxInGoal;
							map[moveTo.X, moveTo.Y] = TileContents.Gol;
							break;
						}
						break;
					case TileContents.Gol:
						map[moveTo.X, moveTo.Y] = TileContents.ManInGoal;
						if (map[playerLoc.X, playerLoc.Y] == TileContents.ManInGoal)
							map[playerLoc.X, playerLoc.Y] = TileContents.Gol;
						else
							map[playerLoc.X, playerLoc.Y] = TileContents.Emp;
						playerLoc = moveTo;
						break;
				}
			}
			else if (newKeyboard.IsKeyDown(Keys.Right) && !lastState.IsKeyDown(Keys.Right) || (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.5 && untilNextControllerMove == TimeSpan.Zero))
			{
				untilNextControllerMove = new TimeSpan(TimeSpan.TicksPerSecond / 4);
				Point moveTo = new Point(playerLoc.X + 1, playerLoc.Y);
				Point boxMoveTo = new Point(playerLoc.X + 2, playerLoc.Y);
				switch (map[moveTo.X, moveTo.Y])
				{
					case TileContents.Emp:
						map[moveTo.X, moveTo.Y] = TileContents.Man;
						if (map[playerLoc.X, playerLoc.Y] == TileContents.ManInGoal)
							map[playerLoc.X, playerLoc.Y] = TileContents.Gol;
						else
							map[playerLoc.X, playerLoc.Y] = TileContents.Emp;
						playerLoc = moveTo;
						break;
					case TileContents.Box:
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Wal || map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Lad)
						{
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Emp)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.Box;
							map[moveTo.X, moveTo.Y] = TileContents.Emp;
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.BoxInGoal;
							map[moveTo.X, moveTo.Y] = TileContents.Emp;
							break;
						}
						break;
					case TileContents.BoxInGoal:
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Wal || map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Lad)
						{
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] != TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.Box;
							map[moveTo.X, moveTo.Y] = TileContents.Gol;
							break;
						}
						if (map[boxMoveTo.X, boxMoveTo.Y] == TileContents.Gol)
						{
							map[boxMoveTo.X, boxMoveTo.Y] = TileContents.BoxInGoal;
							map[moveTo.X, moveTo.Y] = TileContents.Gol;
							break;
						}
						break;
					case TileContents.Gol:
						map[moveTo.X, moveTo.Y] = TileContents.ManInGoal;
						if (map[playerLoc.X, playerLoc.Y] == TileContents.ManInGoal)
							map[playerLoc.X, playerLoc.Y] = TileContents.Gol;
						else
							map[playerLoc.X, playerLoc.Y] = TileContents.Emp;
						playerLoc = moveTo;
						break;
				}
			}

			lastState = newKeyboard;
			return StateReturn.False;
		}

		public override void Draw(GameTime gameTime)
		{
			for (int jjj = 0; jjj < map.GetLength(1); jjj++)
			{
				for (int iii = 0; iii < map.GetLength(0); iii++)
				{
					switch(map[iii,jjj])
					{
						case TileContents.Emp:
							break;
						case TileContents.Wal:
							Settings.spriteBatch.Draw(spriteSheet, new Vector2(iii * 20, jjj * 20), WallSrc, Color.White);
							break;
						case TileContents.Box:
							Settings.spriteBatch.Draw(spriteSheet, new Vector2(iii * 20, jjj * 20), BoxSrc, Color.White);
							break;
						case TileContents.Man:
							Settings.spriteBatch.Draw(spriteSheet, new Vector2(iii * 20, jjj * 20), ManSrc, Color.White);
							break;
						case TileContents.Lad:
							Settings.spriteBatch.Draw(spriteSheet, new Vector2(iii * 20, jjj * 20), LadderSrc, Color.White);
							break;
						case TileContents.Gol:
							Settings.spriteBatch.Draw(spriteSheet, new Vector2(iii * 20, jjj * 20), GoalSrc, Color.White);
							break;
						case TileContents.BoxInGoal:
							Settings.spriteBatch.Draw(spriteSheet, new Vector2(iii * 20, jjj * 20), BoxInGoalSrc, Color.White);
							break;
						case TileContents.ManInGoal:
							Settings.spriteBatch.Draw(spriteSheet, new Vector2(iii * 20, jjj * 20), ManInGoalSrc, Color.White);
							break;
					}
				}
			}
		}
	}
}
