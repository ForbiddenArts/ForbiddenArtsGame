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

		Texture2D wholeBackground;
		Sprite menuBackground;
		Point oldMouseLoc;

        public StartMenu()
            : base()
        {
            drawParent = false;
			currentOption = Options.NewGame;

			optionRects = new Rectangle[(int)Options.Exit + 1] {
				new Rectangle(100,100,100,30),//new game
				new Rectangle(100,200,100,30),//load game
				new Rectangle(100,300,100,30),//options
				new Rectangle(100,400,100,30)//exit
			};

			optionTexts = new Texture2D[(int)Options.Exit + 1, 2] {
				{ SheetHandler.getSheet("menu/ng-"), SheetHandler.getSheet("menu/ng+") },//new game
				{ SheetHandler.getSheet("menu/lg_"), null },//load game
				{ SheetHandler.getSheet("menu/op_"), null },//options
				{ SheetHandler.getSheet("menu/ex-"), SheetHandler.getSheet("menu/ex+") }//exit
			};

			wholeBackground = SheetHandler.getSheet("background");
			menuBackground = new MenuBackground();
			oldMouseLoc = Point.Zero;
        }

        public override void Draw(GameTime gameTime)
        {
            if (child != null && (child.DrawParent() == false))
            {
                child.Draw(gameTime);
                return;
            }

			Settings.spriteBatch.Draw(wholeBackground, new Rectangle(0, 0, Settings.screenX, Settings.screenY), Color.White);
			menuBackground.Draw(gameTime, new Vector2(Settings.screenX / 2, Settings.screenY / 2));
			Settings.spriteBatch.Draw(optionTexts[(int)Options.NewGame, currentOption == Options.NewGame ? 1 : 0], optionRects[(int)Options.NewGame], Color.White);
			Settings.spriteBatch.Draw(optionTexts[(int)Options.LoadGame, currentOption == Options.LoadGame ? 1 : 0], optionRects[(int)Options.LoadGame], Color.White);
			Settings.spriteBatch.Draw(optionTexts[(int)Options.Options, currentOption == Options.Options ? 1 : 0], optionRects[(int)Options.Options], Color.White);
			Settings.spriteBatch.Draw(optionTexts[(int)Options.Exit, currentOption == Options.Exit ? 1 : 0], optionRects[(int)Options.Exit], Color.White);

            if (child != null)
                child.Draw(gameTime);
        }

		//TODO: Mouse detection
        public override bool Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (child != null)
            {
                if (child.Update(gameTime))
                    child = null;
            }

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
				
			}

			//check for option activation by enter or lmb
			if (Keyboard.GetState().IsKeyDown(Keys.Enter))
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
