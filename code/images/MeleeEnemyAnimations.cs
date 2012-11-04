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
			firstFrameRect = new Rectangle(0, 0, 281, 270);
			srcRect = new Rectangle(0, 0, 281, 270);
			origin = new Vector2(140, 135);
			frameCount = 11;
			image = SheetHandler.getSheet("characters/Melee");
		}
	}

	class MeleeEnemyAttack : Animation
	{
		public MeleeEnemyAttack()
			: base()
		{
			firstFrameRect = new Rectangle(0, 540, 281, 270);
			srcRect = new Rectangle(0, 540, 281, 270);
			origin = new Vector2(140, 135);
			frameCount = 9;
			image = SheetHandler.getSheet("characters/Melee");
		}
	}

	class MeleeEnemyIdle : Animation
	{
		public MeleeEnemyIdle()
			: base()
		{
			firstFrameRect = new Rectangle(0, 270, 281, 270);
			srcRect = new Rectangle(0, 270, 281, 270);
			origin = new Vector2(140, 135);
			frameCount = 8;
			image = SheetHandler.getSheet("characters/Melee");
		}
	}

	class MeleeEnemyDead : Animation
	{
		public MeleeEnemyDead()
			: base()
		{
			firstFrameRect = new Rectangle(0, 810, 281, 270);
			srcRect = new Rectangle(0, 810, 281, 270);
			origin = new Vector2(140, 135);
			frameCount = 1;
			image = SheetHandler.getSheet("characters/Melee");
		}
	}
}
