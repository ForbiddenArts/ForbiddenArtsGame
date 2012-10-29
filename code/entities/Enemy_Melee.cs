﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ForbiddenArtsGame.code.images;

namespace ForbiddenArtsGame.code.entities
{
	class Enemy_Melee : Character
	{
		public Enemy_Melee(Vector2 loc) : base(loc)
		{
			currentSprite = new MeleeEnemySprite();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			Vector2 relativePlayerPosition = Settings.GetPositionFromCamera(loc);
			if (relativePlayerPosition.X > -250 && relativePlayerPosition.X < 50)
				this.Attack();
			else if (relativePlayerPosition.X >= 50)
			{
				this.Move(new Vector2(-0.5f, 0));
			}
			else
			{
				this.Move(new Vector2(0.5f, 0));
			}
		}

		protected void Attack()
		{

		}
	}
}
