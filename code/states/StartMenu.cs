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
		Texture2D[] activeOptionTexts;
		Options currentOption;
		Point oldMouseLoc;

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

        public StartMenu()
            : base()
        {
            drawParent = false;
			currentOption = Options.NewGame;

			optionRects = new Rectangle[(int)Options.Exit + 1] {
				new Rectangle(740,330,170,50),//new game
				new Rectangle(740,380,170,70),//load game
				new Rectangle(740,450,170,65),//options
				new Rectangle(740,515,170,50)//exit
			};

			/*
			optionTexts = new Texture2D[(int)Options.Exit + 1, 2] {
				{ SheetHandler.getSheet("menu/ng-"), SheetHandler.getSheet("menu/ng+") },//new game
				{ SheetHandler.getSheet("menu/lg_"), null },//load game
				{ SheetHandler.getSheet("menu/op_"), null },//options
				{ SheetHandler.getSheet("menu/ex-"), SheetHandler.getSheet("menu/ex+") }//exit
			};
			*/

			oldMouseLoc = Point.Zero;

			menuSheets = new Texture2D[6] {
				SheetHandler.getSheet("menu/spriteSheetpart1"),
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
							}
							else
							{
								sheetNum++;
								if (menuSheets[(int)sheetNum] == null)
								{
									menuSheets[(int)sheetNum] = SheetHandler.getSheet("menu/spriteSheetpart" + ((int)sheetNum + 1));
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
        }

		//TODO: Mouse detection
        public override bool Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (child != null)
            {
                if (child.Update(gameTime))
                    child = null;
            }

			if (menuState == MenuState.Closed)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Enter))
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
				if (Keyboard.GetState().IsKeyDown(Keys.Up))
				{
					if (currentOption != Options.NewGame)
					{
						currentOption--;
						//disabled options, backwards due to traversing options in reverse
						if (currentOption == Options.Options)
						{
							currentOption--;
						}
						if (currentOption == Options.LoadGame)
						{
							currentOption--;
						}
					}
				}
				if (Keyboard.GetState().IsKeyDown(Keys.Down))
				{
					if (currentOption != Options.Exit)
						currentOption++;
					//disabled options
					if (currentOption == Options.LoadGame)
					{
						currentOption++;
					}
					if (currentOption == Options.Options)
					{
						currentOption++;
					}
				}
				Point newMouseLoc = new Point(Mouse.GetState().X, Mouse.GetState().Y);
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
				if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Mouse.GetState().LeftButton == ButtonState.Pressed)
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
