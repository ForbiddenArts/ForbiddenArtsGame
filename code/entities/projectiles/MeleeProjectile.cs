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

namespace ForbiddenArtsGame.code.entities.projectiles
{
	class MeleeProjectile : Projectile
	{
		public MeleeProjectile(Vector2 loc, Vector2 vel, Entity source)
			: base(loc, vel, source)
		{
            currentSprite = new TempProj();
		}
	}
}
