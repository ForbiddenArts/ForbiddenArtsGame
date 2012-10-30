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
 * HOW TO MAKE ANIMATIONS WORK
 * Make sure all the frames in the animation are in the same sheet, in a horizontal line
 *		width and height of the area assigned to each frame needs to be the same
 * Inherit this class
 * In the constructor:
 *      make srcRect be a rectange of where the the first frame is on the sheet, in pixels
 *          if the frame started at 100,20 and was 20px wide and 10px high, make a rectange with x = 100, y = 20, width = 20 and height = 10
 *      make origin be the centre of the frame from the top left corner of the rectangle
 *			eg (10,5)
 *      Make layer be whatever is needed
 *          foreground stuff should be 1.0f
 *      Set frameCount to be however many frames are in the animation
 * Call Draw from whatever entities you want having the sprite, with loc being where the centre of the sprite should be
 */

namespace ForbiddenArtsGame.code.images
{
	abstract class Animation : Sprite
	{
		protected int frameCount;
		protected int currentFrame = 0; //range of 0 to frameCount-1
		protected TimeSpan frameTime; // frames per second, default is 10
		protected TimeSpan sinceLastFrame;
		protected Rectangle firstFrameRect;

		public int cycleNum = 0;

		public Animation()
			: base()
		{
			frameTime = new TimeSpan(TimeSpan.TicksPerSecond/10);
			sinceLastFrame = new TimeSpan(0);
		}

		public override void Draw(GameTime gameTime, Vector2 loc, float rotation = 0.0f)
		{
			if(firstFrameRect == null)
			{
				firstFrameRect = new Rectangle(srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height);
			}
			sinceLastFrame += gameTime.ElapsedGameTime;
			if (sinceLastFrame > frameTime)
			{
				sinceLastFrame -= frameTime;
				currentFrame++;
				if (currentFrame == frameCount)
				{
					currentFrame = 0;
					srcRect.X = firstFrameRect.X;
					cycleNum++;
				}
				else
				{
					srcRect.X += srcRect.Width;
				}
			}
			base.Draw(gameTime, loc, rotation);
		}

		public virtual void reset()
		{
			currentFrame = 0;
			srcRect.X = firstFrameRect.X;
			cycleNum = 0;
		}
	}
}
