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

namespace ForbiddenArtsGame.code.entities
{
	abstract class Mobile : Entity
	{
		protected bool onGround = false; //cant jump in mid air
		protected bool isMobile = true;
		protected enum Facing { Left, Right };
		protected Facing facing = Facing.Right;
		protected Vector2 velocity = Vector2.Zero;
		protected float updateVelMult = 0.9f;
		public Mobile() : this(Vector2.Zero) { }
		public Mobile(Vector2 loc) : base(loc) { }

		public void Move(Vector2 delta)
		{
			velocity += delta;
		}
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (isMobile)
			{
				loc += velocity;
				if (!onGround)
				{
					velocity.Y += 1f;
				}
				if (velocity.Length() > 0.1)
					velocity = Vector2.Multiply(velocity, updateVelMult);
				else
					velocity = Vector2.Zero;
				if (velocity.Y > 0)
					facing = Facing.Right;
				else if (velocity.Y < 0)
					facing = Facing.Left;
			}
			else
			{
				velocity = Vector2.Zero;
			}
		}
		public override void Collide(Entity e)
		{
			base.Collide(e);
			if ((Terrain)e != null)
			{
				if (velocity.Y > 0) velocity.Y = 0;
				onGround = true;
				this.loc.Y = e.BoundingBox.Top - currentSprite.sizeX / 2;
			}
			else
			{
				if (isMobile)
				{
					Rectangle thisBox = this.BoundingBox;
					Rectangle thatBox = e.BoundingBox;
					//entirely inside
					if (thisBox.Contains(thatBox))
					{
						Vector2 m = new Vector2(thatBox.Center.X - thisBox.Center.X, thatBox.Center.Y - thisBox.Center.Y);
						m.Normalize();
						this.Move(m * 3);
					}
					else if (thatBox.Contains(thisBox))
					{
						Vector2 m = new Vector2(thisBox.Center.X - thatBox.Center.X, thisBox.Center.Y - thatBox.Center.Y);
						m.Normalize();
						this.Move(m * 3);
					}	//inside from above/below
					else if ((thisBox.Left < thatBox.Left && thisBox.Right > thatBox.Right) || (thisBox.Left > thatBox.Left && thisBox.Right < thatBox.Right))
					{
						if (thisBox.Center.Y > thatBox.Center.Y)
							this.Move(Vector2.UnitY * 3);
						else
							this.Move(Vector2.UnitY * -3);
					}	//inside from left/right
					else if ((thisBox.Top < thatBox.Top && thisBox.Bottom > thatBox.Bottom) || (thisBox.Top > thatBox.Top && thisBox.Bottom < thatBox.Bottom))
					{
						if (thisBox.Center.X > thatBox.Center.X)
							this.Move(Vector2.UnitX * 3);
						else
							this.Move(Vector2.UnitX * -3);
					}	//inside from diagonal
					else
					{
						Vector2 m = new Vector2(thatBox.Center.X - thisBox.Center.X, thatBox.Center.Y - thisBox.Center.Y);
						m.Normalize();
						this.Move(m * 3);
					}
				}
			}
		}
	}
}
