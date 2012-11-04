//has a ` in the filename to make sure it is at the top of the folder

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
using ForbiddenArtsGame.code.entities.projectiles.spells;

namespace ForbiddenArtsGame.code.entities
{
    abstract class Entity
    {
		public static bool CollisionCheck(Entity a, Entity b, bool coarse = true)
		{
			if (a.BoundingBox.Intersects(b.BoundingBox))
				if (coarse)
					return Sprite.PerPixelCollision(a.currentSprite, a.loc, b.currentSprite, b.loc);
				else
					return true;
			else
				return false;
		}
		public static Vector2 VectorBetween(Entity a, Entity b)
		{
			return new Vector2(a.BoundingBox.Center.X - b.BoundingBox.Center.X, a.BoundingBox.Center.Y - b.BoundingBox.Center.Y);
		}

        protected Sprite currentSprite;
        protected Vector2 loc;
		public float LocX { get { return loc.X; } }
		public float LocY { get { return loc.Y; } }
		protected List<SpellEffect> effects;

		public virtual Rectangle BoundingBox
		{
			get
			{
				if (currentSprite != null)
					return new Rectangle((int)loc.X - currentSprite.sizeX / 2, (int)loc.Y - currentSprite.sizeY / 2, currentSprite.sizeX, currentSprite.sizeY);
				else
					return new Rectangle((int)loc.X, (int)loc.Y, 1, 1);
			}
		}
		//for passing which entities need to be added/removed from processing to the processing state
		public List<Entity> toBeAdded;
		public List<Entity> toBeRemoved;
		
		public Entity() : this(Vector2.Zero) { }
		public Entity(Vector2 _loc)
		{
			loc = _loc;
			effects = new List<SpellEffect>();
			toBeAdded = new List<Entity>();
			toBeRemoved = new List<Entity>();
		}

		public virtual void Draw(GameTime gameTime)
		{
			currentSprite.Draw(gameTime, loc);
		}
		public virtual void Update(GameTime gameTime)
		{
			foreach(SpellEffect effect in effects)
			{
				effect.Effect(this);
			}
		}
		public virtual void Updated()
		{
			toBeAdded.Clear();
			toBeRemoved.Clear();
		}
		public virtual void AddEffect(SpellEffect effect)
		{
			if (effect.instant)
				effect.Effect(this);
			else
			{
				effects.Add(effect);
			}
		}

		public virtual void Collide(Entity e)
		{
			return;
		}

		public Sprite Sprite
		{
			get { return currentSprite; }
			set { currentSprite = value; }
		}

		public virtual void InHole()
		{
			return;
		}
    }
}
