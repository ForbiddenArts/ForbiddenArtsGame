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

/*
 * The initial menu you see when you start the game.
 */

namespace ForbiddenArtsGame.code.states
{
    class StartMenu : GameState
    {
        enum Options { NewGame, LoadGame, Options, Exit };
		Options currentOption;

        public StartMenu()
            : base()
        {
            drawParent = false;
			currentOption = Options.NewGame;
        }

        public override void Draw(GameTime gameTime)
        {
            if (child != null && (child.DrawParent() == false))
            {
                child.Draw(gameTime);
                return;
            }

			Settings.spriteBatch.Draw(SheetHandler.getSheet("background"), new Rectangle(0, 0, Settings.screenX, Settings.screenY), Color.White);

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
			if (Keyboard.GetState().IsKeyDown(Keys.Enter))
			{
				switch (currentOption)
				{
					case Options.NewGame:
						break;
					case Options.LoadGame:
						break;
					case Options.Options:
						break;
					case Options.Exit:
						return true;
				}
			}

            return false;
        }
    }
}
