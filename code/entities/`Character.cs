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
using ForbiddenArtsGame.code.entities.projectiles;

namespace ForbiddenArtsGame.code.entities
{
	abstract class Character : Mobile
	{
		public int health = 100;
		protected bool dead = false;
		public Rectangle attackRect { get { return new Rectangle(facing == Facing.Left ? (int)loc.X - currentSprite.sizeX : (int)loc.X, (int)loc.Y, currentSprite.sizeX, 1); } }

		public Character() : this(Vector2.Zero) { }
		public Character(Vector2 loc) : base(loc) { }

		public virtual void Collide(Projectile p)
		{
			return;
		}

		public virtual void Damage(int damage)
		{
			health -= damage;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (health <= 0)
				toBeRemoved.Add(this);
		}

		protected abstract void SetCurrentAnimation(string name);
	}
}
