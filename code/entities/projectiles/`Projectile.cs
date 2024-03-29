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

namespace ForbiddenArtsGame.code.entities.projectiles
{
	abstract class Projectile : Mobile
	{
		protected Entity source; //dont hit what fired it
		protected int damage = 10;
		protected int timetolive = 1;
		protected int lifetime = 0;
		protected bool collided = false;
		public Projectile(Vector2 loc, Vector2 vel, Entity source)
		{
			this.loc = loc;
			this.velocity = vel;
			this.source = source;
			updateVelMult = 1.0f;
		}

		public Projectile()
		{

		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			lifetime++;
			if (lifetime >= timetolive)
			{
				toBeRemoved.Add(this);
			}
		}

		public override void Collide(Entity e)
		{
			if (e is Character)
			{
				Character c = (Character)e;
				if (c == source)
					return;
				switch(Collision(e.BoundingBox))
				{
					case CollisionDirection.left:
						c.Damage(damage);
						collided = true;
						break;
					case CollisionDirection.right:
						c.Damage(damage);
						collided = true;
						break;
					default:
						c.Damage(damage);
						collided = true;
						break;
				}
			}
		}
	}
}
