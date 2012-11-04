using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForbiddenArtsGame.code.images;

namespace ForbiddenArtsGame.code.entities.projectiles
{
	class RangedProjectile:Projectile
	{
		public RangedProjectile(Vector2 loc, Vector2 vel, Entity source) : base(loc, vel, source) {
			currentSprite = new Arrow();
			timetolive = 60;
			damage = 10;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (collided == true)
				toBeRemoved.Add(this);
		}
	}
}
