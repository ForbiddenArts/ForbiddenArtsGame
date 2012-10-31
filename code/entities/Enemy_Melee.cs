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
using ForbiddenArtsGame.code.images;

namespace ForbiddenArtsGame.code.entities
{
	class Enemy_Melee : Character
	{
		private bool hasSeen = false;

		public Enemy_Melee(Vector2 loc) : base(loc)
		{
			currentSprite = new MeleeEnemySprite();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			Vector2 relativePlayerPosition = Settings.GetPositionFromCamera(loc);
			if (hasSeen)
			{
				if (relativePlayerPosition.X > -270 && relativePlayerPosition.X < 70)
					this.Attack();
				else if (relativePlayerPosition.X >= 80)
				{
					this.Move(new Vector2(-0.5f, 0));
				}
				else
				{
					this.Move(new Vector2(0.5f, 0));
				}
			}
			else
			{
				if (relativePlayerPosition.X > -700 && relativePlayerPosition.X < 700)
					hasSeen = true;
			}
		}

		protected override void SetCurrentAnimation(string name)
		{
			throw new NotImplementedException();
		}

		protected void Attack()
		{

		}
	}
}
