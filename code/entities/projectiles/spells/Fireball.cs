using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ForbiddenArtsGame.code.images;

namespace ForbiddenArtsGame.code.entities.projectiles.spells
{
	class Fireball:Spell
	{
		public static bool isAlive;
		public Fireball()
		{
			name = "Fireball";
			currentSprite = new SpellSprite(1);
			damage = 0;
			timetolive = 60;
		}

		public override Spell Cast(Vector2 loc, Vector2 dir, Entity source)
		{
			isAlive = true;
			this.source = source;
			velocity = 20 * dir;
			this.loc = new Vector2(loc.X+(200*dir.X),loc.Y);
			return this;
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (lifetime >= timetolive)
			{
				//toBeRemoved.Add(this);
				isAlive = false;
			}
		}

		public override void Collide(Entity e)
		{
			if (isAlive)
			{
				Character entity = e as Character;
				if (entity != null)
				{
					if (e == source)
						return;
					e.AddEffect(new FireballEffect());
					toBeRemoved.Add(this);
					isAlive = false;
				}
			}
		}
	}

	/// <summary>
	/// Deals damage to character
	/// </summary>
	class FireballEffect : SpellEffect
	{
		public FireballEffect()
		{
			instant = true;
		}
		public override void Effect(entities.Entity e)
		{
			Character spellHit = (Character)e;
			if (spellHit != null)
			{
				spellHit.Damage(20);
				spellHit.Sprite.Overlay = Color.Red;
			}
		}
	}
}
