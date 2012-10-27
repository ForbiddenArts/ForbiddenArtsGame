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


namespace ForbiddenArtsGame.code.images
{
	class MenuBackground : Sprite
	{
		protected Rectangle topBorder;
		protected Rectangle bottomBorder;
		public MenuBackground()
		{
			image = SheetHandler.getSheet("menu/menubackground");
			topBorder = new Rectangle(0, 0, 200, 10);
			srcRect = new Rectangle(0, 30, 200, 10);
			bottomBorder = new Rectangle(0, 60, 200, 10);
		}

		public override void Draw(GameTime gameTime, Vector2 loc, float rotation = 0.0f)
		{
			Settings.spriteBatch.Draw(image, new Rectangle((Settings.screenX / 2) - (topBorder.Width / 2), 100, topBorder.Width, topBorder.Height), topBorder, Color.White);
			for (int iii = 100; iii < 500; iii += 30)
			{
				Settings.spriteBatch.Draw(image, srcRect, new Rectangle((Settings.screenX / 2) - (srcRect.Width / 2), 100 + (30 * iii), srcRect.Width, srcRect.Height), Color.White);
			}
			Settings.spriteBatch.Draw(image, new Rectangle((Settings.screenX / 2) - (srcRect.Width / 2), 500, bottomBorder.Width, bottomBorder.Height), bottomBorder, Color.White);
		}
	}
}
