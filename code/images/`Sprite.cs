//has a ` in the filename to make sure it is at the top of the folder

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
 * HOW TO MAKE SPRITES WORK
 * Inherit this class
 * In the constructor:
 *      load the sprite sheet and store in image
 *      make srcRect be a rectange of where the sprite is on the sheet, in pixels
 *          if the sprite started at 100,20 and was 20px wide and 10px high, make a rectange with x = 100, y = 20, width = 20 and height = 10
 *      make origin be the centre of that
 *          TODO: check whether it needs to include the x and y of the rectange
 *      Make layer be whatever is needed
 *          foreground stuff should be 1.0f
 * Call Draw from whatever entities you want having the sprite, with loc being where the centre of the sprite should be
 */

namespace ForbiddenArtsGame.code
{
    //This is specifically for static images that are part of a sprite sheet
    //Use Animation for animations
    abstract class Sprite
    {
		//function sourced from http://gamedev.stackexchange.com/a/15201 with slight modifications
		public static bool PerPixelCollision(Sprite a, Vector2 aLoc, Sprite b, Vector2 bLoc)
		{
			// Get Color data of each Texture
			Color[] bitsA = new Color[a.sizeX * a.sizeY];
			a.image.GetData(0, a.srcRect, bitsA, 0, 0);
			Color[] bitsB = new Color[b.sizeX * b.sizeY];
			b.image.GetData(0, b.srcRect, bitsB, 0, 0);

			// Calculate the intersecting rectangle
			int x1 = Math.Max((int)aLoc.X - (a.sizeX / 2), (int)bLoc.X - (b.sizeX / 2));
			int x2 = Math.Min((int)aLoc.X + (a.sizeX / 2), (int)bLoc.X + (b.sizeX / 2));

			int y1 = Math.Max((int)aLoc.Y - (a.sizeY / 2), (int)bLoc.Y - (b.sizeY / 2));
			int y2 = Math.Min((int)aLoc.Y + (a.sizeY / 2), (int)bLoc.Y + (b.sizeY / 2));

			// For each single pixel in the intersecting rectangle
			for (int y = y1; y < y2; ++y)
			{
				for (int x = x1; x < x2; ++x)
				{
					// Get the color from each texture
					Color A = bitsA[(x - ((int)aLoc.X - (a.sizeX / 2))) + (y - ((int)aLoc.Y - (a.sizeY / 2))) * a.sizeX];
					Color B = bitsB[(x -((int)bLoc.Y + (b.sizeY / 2))) + (y - ((int)bLoc.Y + (b.sizeY / 2))) * b.sizeY];

					if (A.A != 0 && B.A != 0) // If both colors are not transparent (the alpha channel is not 0), then there is a collision
					{
						return true;
					}
				}
			}
			// If no collision occurred by now, we're clear.
			return false;
		}


        protected Texture2D image;
        protected Rectangle srcRect;	//on the sheet
        protected Vector2 origin;
        protected float layer = 1.0f;
        //all of these in pixels
        protected int sheetLocX { get { return srcRect.Left; } }
        protected int sheetLocY { get { return srcRect.Top; } }
        public int sizeX { get { return srcRect.Width; } }
        public int sizeY { get { return srcRect.Height; } }

        public virtual void Draw(GameTime gameTime, Vector2 loc, float rotation = 0.0f)
        {
			Vector2 relativeloc = loc - Settings.CameraLoc;
			//make sure its actually on the screen before drawing
			bool onScreen = (relativeloc.X + this.sizeX / 2 > 0) || (relativeloc.X - this.sizeX / 2 < Settings.screenX)
				|| (relativeloc.Y + this.sizeY / 2 > 0) || (relativeloc.X - this.sizeY / 2 < Settings.screenY);
			if(onScreen)
			   Settings.spriteBatch.Draw(image, relativeloc, srcRect, Color.White, rotation, origin, 1.0f, SpriteEffects.None, layer);
        }

        public void setLayer(float _layer)
        {
            if (_layer > 0.0f && _layer < 1.0f)
				layer = _layer;
        }
    }
}
