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
using ForbiddenArtsGame.code.images;

/*
 * The initial menu you see when you start the game.
 */

namespace ForbiddenArtsGame.code.states
{
    class StartMenu : GameState
    {
		enum Options { NewGame, LoadGame, Options, Exit };
		Rectangle[] optionRects;
		Texture2D[,] optionTexts;
		Options currentOption;
		Point oldMouseLoc;
		bool movedController = false;

		enum MenuState { Closed, Animating, Open };
		MenuState menuState = MenuState.Closed;
		Texture2D[] menuSheets;
		enum SheetNum { One = 0, Two, Three, Four, Five, Six };
		SheetNum sheetNum = SheetNum.One;
		enum SheetPosX { One = 0, Two, Three };
		SheetPosX sheetPosX = SheetPosX.One;
		enum SheetPosY { One = 0, Two, Three, Four, Five };
		SheetPosY sheetPosY = SheetPosY.One;

		TimeSpan betweenMenuFrames;
		TimeSpan sinceLastFrame;

		bool needMouseRelease = false;

        public StartMenu()
            : base()
        {
            drawParent = false;
			currentOption = Options.NewGame;
			Settings.Fullscreen = true;

			optionRects = new Rectangle[(int)Options.Exit + 1] {
				new Rectangle(741,332,160,49),//new game
				new Rectangle(741,379,160,68),//load game
				new Rectangle(741,444,160,76),//options
				new Rectangle(741,517,160,43)//exit
			};

			oldMouseLoc = Point.Zero;

			menuSheets = new Texture2D[6] {
				SheetHandler.getSheet("menu/menuSheet1_0"),
				null,
				null,
				null,
				null,
				null
			};

			betweenMenuFrames = new TimeSpan(TimeSpan.TicksPerSecond / 20);
			sinceLastFrame = TimeSpan.Zero;
        }

        public override void Draw(GameTime gameTime)
        {
            if (child != null && (child.DrawParent() == false))
            {
                child.Draw(gameTime);
                return;
            }

			//Settings.spriteBatch.Draw(wholeBackground, new Rectangle(0, 0, Settings.screenX, Settings.screenY), Color.White);
			//menuBackground.Draw(gameTime, new Vector2(Settings.screenX / 2, Settings.screenY / 2));
            /*if (animationDone) {
                Settings.spriteBatch.Draw(optionTexts[(int)Options.NewGame, currentOption == Options.NewGame ? 1 : 0], optionRects[(int)Options.NewGame], Color.White);
                Settings.spriteBatch.Draw(optionTexts[(int)Options.LoadGame, currentOption == Options.LoadGame ? 1 : 0], optionRects[(int)Options.LoadGame], Color.White);
                Settings.spriteBatch.Draw(optionTexts[(int)Options.Options, currentOption == Options.Options ? 1 : 0], optionRects[(int)Options.Options], Color.White);
                Settings.spriteBatch.Draw(optionTexts[(int)Options.Exit, currentOption == Options.Exit ? 1 : 0], optionRects[(int)Options.Exit], Color.White);
            }
            else*/
           // menuStart.Draw(gameTime, new Vector2(-300, 0));
			sinceLastFrame += gameTime.ElapsedGameTime;
			if (sinceLastFrame > betweenMenuFrames)
			{
				sinceLastFrame -= betweenMenuFrames;
				if (menuState == MenuState.Animating)
				{
					if (sheetPosX == SheetPosX.Three)
					{
						sheetPosX = SheetPosX.One;
						if (sheetPosY == SheetPosY.Five)
						{
							sheetPosY = SheetPosY.One;
							if (sheetNum == SheetNum.Six)
							{
								sheetPosX = SheetPosX.Three;
								sheetPosY = SheetPosY.Five;
								menuState = MenuState.Open;

								optionTexts = new Texture2D[(int)Options.Exit + 1, 2] {
									{ SheetHandler.getSheet("menu/new-"), SheetHandler.getSheet("menu/new+") },//new game
									{ SheetHandler.getSheet("menu/load-"), SheetHandler.getSheet("menu/load+") },//load game
									{ SheetHandler.getSheet("menu/options-"), SheetHandler.getSheet("menu/options+") },//options
									{ SheetHandler.getSheet("menu/quit-"), SheetHandler.getSheet("menu/quit+") }//exit
								};
							}
							else
							{
								sheetNum++;
								if (menuSheets[(int)sheetNum] == null)
								{
									menuSheets[(int)sheetNum] = SheetHandler.getSheet("menu/menuSheet1_" + (int)sheetNum);
								}
							}
						}
						else
						{
							sheetPosY++;
						}
					}
					else
					{
						sheetPosX++;
					}
				}
			}

			Texture2D currentSheet = menuSheets[(int)sheetNum];
			Rectangle srcRect = new Rectangle(1280 * (int)sheetPosX, 720 * (int)sheetPosY, 1280, 720);
			Settings.spriteBatch.Draw(currentSheet, Vector2.Zero, srcRect, Color.White);

			if (menuState == MenuState.Open)
			{
				if(currentOption == Options.NewGame)
				{
					Settings.spriteBatch.Draw(optionTexts[0, 1], optionRects[0], Color.White);
				}
				else
				{
					Settings.spriteBatch.Draw(optionTexts[0, 0], optionRects[0], Color.White);
				}
				if (currentOption == Options.LoadGame)
				{
					Settings.spriteBatch.Draw(optionTexts[1, 1], optionRects[1], Color.White);
				}
				else
				{
					Settings.spriteBatch.Draw(optionTexts[1, 0], optionRects[1], Color.White);
				}
				if (currentOption == Options.Options)
				{
					Settings.spriteBatch.Draw(optionTexts[2, 1], optionRects[2], Color.White);
				}
				else
				{
					Settings.spriteBatch.Draw(optionTexts[2, 0], optionRects[2], Color.White);
				}
				if (currentOption == Options.Exit)
				{
					Settings.spriteBatch.Draw(optionTexts[3, 1], optionRects[3], Color.White);
				}
				else
				{
					Settings.spriteBatch.Draw(optionTexts[3, 0], optionRects[3], Color.White);
				}
			}
        }

		//TODO: Mouse detection
        public override bool Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (child != null)
            {
				if (child.Update(gameTime))
				{
					child = null;
					needMouseRelease = true;
				}
				else
					return false;
            }

			if (menuState == MenuState.Closed)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Mouse.GetState().LeftButton == ButtonState.Pressed || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
				{
					menuState = MenuState.Animating;
				}
			}
			else if (menuState == MenuState.Animating)
			{
				return false;
			}
			else
			{
				//check for movement between options by mouse or up/down
				if (Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.5 && !movedController)
				{
					movedController = true;
					if (currentOption != Options.NewGame)
					{
						currentOption--;
						////disabled options, backwards due to traversing options in reverse
						//if (currentOption == Options.LoadGame)
						//{
						//    currentOption--;
						//}
					}
				}
				if (Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.5 && !movedController)
				{
					movedController = true;
					if (currentOption != Options.Exit)
						currentOption++;
					////disabled options
					//if (currentOption == Options.LoadGame)
					//{
					//    currentOption++;
					//}
				}
				if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > -0.5 && GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0.5)
					movedController = false;
				Point newMouseLoc = new Point(Mouse.GetState().X, Mouse.GetState().Y);

				if (Mouse.GetState().LeftButton == ButtonState.Pressed)
				{
					if (!needMouseRelease)
					{
						for (int iii = 0; iii < optionRects.Length; iii++)
						{
							Rectangle r = optionRects[iii];
							if (r.Contains(newMouseLoc))
							{
								currentOption = (Options)iii;
								switch (currentOption)
								{
									case Options.NewGame:
										doNewGame();
										break;
									case Options.LoadGame:
										doLoadGame();
										break;
									case Options.Options:
										doOptions();
										break;
									case Options.Exit:
										return true;
								}
							}
						}
					}
				}
				else
					needMouseRelease = false;

				if (newMouseLoc != oldMouseLoc)
				{
					for (int iii = 0; iii < optionRects.Length; iii++ )
					{
						Rectangle r = optionRects[iii];
						if (r.Contains(newMouseLoc))
						{
							currentOption = (Options)iii;
						}
					}
				}

				//check for option activation by enter or lmb
				if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
				{
					switch (currentOption)
					{
						case Options.NewGame:
							doNewGame();
							break;
						case Options.LoadGame:
							doLoadGame();
							break;
						case Options.Options:
							doOptions();
							break;
						case Options.Exit:
							return true;
					}
				}
				oldMouseLoc = newMouseLoc;
			}
            return false;
        }

		protected void doNewGame()
		{
			child = new MainGame();
		}

		protected void doLoadGame()
		{

		}

		protected void doOptions()
		{
			child = new MenuOptions();
		}
    }
}
