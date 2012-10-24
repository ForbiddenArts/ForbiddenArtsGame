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
	class PCSprite : Sprite
	{
		public PCSprite()
		{
			image = SheetHandler.getSheet("Knight");
			srcRect = image.Bounds;
			origin = new Vector2(srcRect.Center.X, srcRect.Center.Y);
		}
	}
}
