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
using ForbiddenArtsGame.code.entities;

/*
 * Spell types are stored in a list of spells that a player has and can cast
 * they are instantiated with Spell.Cast
 * 
 * SpellEffects effecting an entity are stored in a list in that entity, and Effect is called each update cycle\
 *		unless instant = true, then it is called once when the effect starts and the effect isnt stored in the list
 * 
 */

namespace ForbiddenArtsGame.code.entities.projectiles.spells
{
	abstract class SpellEffect
	{
		public bool instant { get; protected set; }

		public SpellEffect()
		{
			instant = true;
		}

		public abstract void Effect(Entity e);
	}

	abstract class Spell : Projectile
	{
		public string name { get; protected set; }

		public Spell()
		{
			name = "UNKNOWN SPELL";
		}
		public abstract Spell Cast(Vector2 loc, Vector2 dir, Entity source);
	}
}
