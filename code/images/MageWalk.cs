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
	class MageWalk : Animation
	{
		public MageWalk()
			: base()
		{
			firstFrameRect = new Rectangle(0, 0, 242, 230);
			srcRect = new Rectangle(0, 0, 242, 230);
			origin = new Vector2(121, 115);
			frameCount = 11;
			image = SheetHandler.getSheet("characters/Mage");
		}
	}
}
