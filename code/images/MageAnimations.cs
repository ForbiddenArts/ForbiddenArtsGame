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

	class MageMelee : Animation
	{
		public MageMelee()
			: base()
		{
			firstFrameRect = new Rectangle(0, 230, 242, 230);
			srcRect = new Rectangle(0, 230, 242, 230);
			origin = new Vector2(121, 115);
			frameCount = 9;
			image = SheetHandler.getSheet("characters/Mage");
		}
	}
	
	class MageCast : Animation
	{
		public MageCast()
			: base()
		{
			firstFrameRect = new Rectangle(0, 460, 242, 230);
			srcRect = new Rectangle(0, 460, 242, 230);
			origin = new Vector2(121, 115);
			frameCount = 8;
			image = SheetHandler.getSheet("characters/Mage");
		}
	}

	class MageIdle : Animation
	{
		public MageIdle()
			: base()
		{
			firstFrameRect = new Rectangle(0, 690, 242, 230);
			srcRect = new Rectangle(0, 690, 242, 230);
			origin = new Vector2(121, 115);
			frameCount = 9;
			image = SheetHandler.getSheet("characters/Mage");
		}
	}

	class MageJump : Animation
	{
		public MageJump()
			: base()
		{
			firstFrameRect = new Rectangle(0, 920, 242, 230);
			srcRect = new Rectangle(0, 920, 242, 230);
			origin = new Vector2(121, 115);
			frameCount = 9;
			image = SheetHandler.getSheet("characters/Mage");
		}
	}

	class MageFall : Animation
	{
		public MageFall()
			: base()
		{
			firstFrameRect = new Rectangle(484, 1150, 242, 230);
			srcRect = new Rectangle(0, 1150, 242, 230);
			origin = new Vector2(121, 115);
			frameCount = 1;
			currentFrame = -2;
			image = SheetHandler.getSheet("characters/Mage");
		}
	}
}
