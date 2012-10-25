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
	class TerrainSprite : Sprite
	{
		public TerrainSprite(String src, Rectangle bounds)
		{
			image = SheetHandler.getSheet(src);
			srcRect = bounds;
			origin = Vector2.Zero;
		}
	}
}
