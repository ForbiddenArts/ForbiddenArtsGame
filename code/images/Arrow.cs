using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ForbiddenArtsGame.code.images
{
	class Arrow:Sprite
	{
		public Arrow()
		{
			image = SheetHandler.getSheet("GameDevArrow");
			srcRect = image.Bounds;
			origin = new Vector2(srcRect.Center.X, srcRect.Center.Y);
		}
	}
}
