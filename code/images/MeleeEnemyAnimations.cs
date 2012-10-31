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
	class MeleeEnemyWalk : Animation
	{
		public MeleeEnemyWalk()
			: base()
		{
			firstFrameRect = new Rectangle(0, 0, 281, 250);
			srcRect = new Rectangle(0, 0, 281, 250);
			origin = new Vector2(140, 115);
			frameCount = 1;
			image = SheetHandler.getSheet("characters/Melee");
		}
	}

	class MeleeEnemyAttack : Animation
	{
		public MeleeEnemyAttack()
			: base()
		{
			firstFrameRect = new Rectangle(0, 250, 281, 250);
			srcRect = new Rectangle(0, 250, 281, 250);
			origin = new Vector2(140, 115);
			frameCount = 1;
			image = SheetHandler.getSheet("characters/Melee");
		}
	}

	class MeleeEnemyIdle : Animation
	{
		public MeleeEnemyIdle()
			: base()
		{
			firstFrameRect = new Rectangle(0, 500, 281, 250);
			srcRect = new Rectangle(0, 500, 281, 250);
			origin = new Vector2(140, 115);
			frameCount = 1;
			image = SheetHandler.getSheet("characters/Melee");
		}
	}

	class MeleeEnemyDead : Animation
	{
		public MeleeEnemyDead()
			: base()
		{
			firstFrameRect = new Rectangle(0, 750, 281, 250);
			srcRect = new Rectangle(0, 750, 281, 250);
			origin = new Vector2(140, 115);
			frameCount = 1;
			image = SheetHandler.getSheet("characters/Melee");
		}
	}
}
