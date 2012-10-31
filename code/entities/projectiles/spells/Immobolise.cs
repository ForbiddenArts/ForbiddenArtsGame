using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForbiddenArtsGame.code.images;

namespace ForbiddenArtsGame.code.entities.projectiles.spells
{
	/// <summary>
	/// Immobolise prevents the character from moving their legs while allowing them to continue attacking.
	/// Effect is:
	/// </summary>
	class Immobolise : Spell
	{
		public static bool isAlive;
		public Immobolise()
		{
			name = "Immobolise";
			currentSprite = new MageCast();
			//Projectile vars
			damage = 0;
			timetolive = 60;
		}

		public override Spell Cast(Vector2 loc, Vector2 dir, Entity source)
		{
			isAlive = true;
			this.source = source;
			velocity = 5 * dir;
			this.loc = new Vector2(loc.X+100,loc.Y);
			return this;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (lifetime >= timetolive)
			{
				toBeRemoved.Add(this);
				isAlive = false;
			}
		}

		public override void Collide(Entity e)
		{
			Character entity = (Character)e;
			if (entity != null)
			{
				if (e == source)
					return;
				e.AddEffect(new ImmoboliseEffect());
				toBeRemoved.Add(this);
				isAlive = false;
			}
		}
	}

	class ImmoboliseEffect : SpellEffect
	{
		public override void Effect(entities.Entity e)
		{
			Character spellHit = (Character)e;
			if (spellHit != null)
			{
				spellHit.Move(new Vector2(1,-100));
			}
		}
	}
}
