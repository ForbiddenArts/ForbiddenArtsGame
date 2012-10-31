using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ForbiddenArtsGame.code.images
{
	class SpellSprite:Sprite
	{
		public SpellSprite(int colour)
		{
			switch (colour)
			{
				case 0: overlay = Color.Green; break;
				case 1: overlay = Color.OrangeRed; break;
			}
			image = SheetHandler.getSheet("GameDevSpell");
			srcRect = image.Bounds;
			origin = new Vector2(srcRect.Center.X, srcRect.Center.Y);
		}
	}
}
