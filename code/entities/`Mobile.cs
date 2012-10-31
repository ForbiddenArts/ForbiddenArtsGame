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
	enum CollisionDirection
	{
		left,right,up,down,all,none,fallThrough
	}

	abstract class Mobile : Entity
	{
		protected bool isMobile = true;
		protected enum Facing { Left = -1, Right = 1 };
		protected Facing facing = Facing.Right;
		protected Vector2 velocity = Vector2.Zero;
		protected float updateVelMult = 0.95f;
		public Mobile() : this(Vector2.Zero) { }
		public Mobile(Vector2 loc) : base(loc) { }

		public void Move(Vector2 delta)
		{
			if (Double.IsNaN(delta.X) || Double.IsNaN(delta.Y))
				throw new ArgumentException("Movement delta has a NaN component", "delta");
			velocity += delta;
		}

		public void Immobilise()
		{
			isMobile = false;
		}
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (isMobile)
			{
				loc += velocity;
				if (velocity.Length() > 0.1)
					velocity = Vector2.Multiply(velocity, updateVelMult);
				else
					velocity = Vector2.Zero;

				if (velocity.X > 0)
					facing = Facing.Right;
				else if (velocity.X < 0)
					facing = Facing.Left;

				if (loc.Y > Settings.screenY - 200)
				{
					loc.Y = Settings.screenY - 200;
					velocity.Y = 0;
				}
				else if (loc.Y < Settings.screenY - 200)
				{
					velocity.Y += 0.8f;
				}
			}
			else
			{
				velocity = Vector2.Zero;
			}
		}
		public override void Draw(GameTime gameTime)
		{
			currentSprite.Draw(gameTime, loc, 0.0f, (int)facing);
		}
		/// <summary>
		/// Checks for collision and performs action
		/// </summary>
		/// <param name="e">Entity to test for collision</param>
		public override void Collide(Entity e)
		{
			base.Collide(e);
			if (isMobile)
			{
				switch (Collision(e.BoundingBox))
				{
					case CollisionDirection.all: 
						Vector2 m = new Vector2(e.BoundingBox.Center.X - BoundingBox.Center.X, e.BoundingBox.Center.Y - BoundingBox.Center.Y);
						if (m.Length() > 0)
						{
							m.Normalize();
						}
						this.Move(m * -3);
						break;
					case CollisionDirection.left: this.Move(Vector2.UnitX * 3); break;
					case CollisionDirection.right: this.Move(Vector2.UnitX * -3); break;
					case CollisionDirection.up: this.Move(Vector2.UnitY * -3); break;
					case CollisionDirection.down: this.Move(Vector2.UnitY * 3); break;
					case CollisionDirection.fallThrough:
						Vector2 m2 = new Vector2(e.BoundingBox.Center.X - BoundingBox.Center.X, e.BoundingBox.Center.Y - BoundingBox.Center.Y);
						if (m2.Length() > 1)
						{
							m2.Normalize();
						}
						this.Move(m2 * -3);
						break;
				}				
			}
		}

		/// <summary>
		/// Returns direction of collision between bounding boxes
		/// </summary>
		/// <param name="otherBoundingBox">Not this.BoundingBox</param>
		/// <returns>Direction of collision</returns>
		protected CollisionDirection Collision(Rectangle otherBoundingBox)
		{
			Rectangle thisBox = BoundingBox;
			Rectangle thatBox = otherBoundingBox;

			//entirely inside
			if (thisBox.Contains(thatBox))
			{
				return CollisionDirection.all;
			}
			else if (thatBox.Contains(thisBox))
			{
				//Vector2 m = new Vector2(thisBox.Center.X - thatBox.Center.X, thisBox.Center.Y - thatBox.Center.Y);
				//m.Normalize();
				//this.Move(m * 3);
				return CollisionDirection.all;
			}	//inside from above/below
			else if ((thisBox.Left < thatBox.Left && thisBox.Right > thatBox.Right) || (thisBox.Left > thatBox.Left && thisBox.Right < thatBox.Right))
			{
				if (thisBox.Center.Y > thatBox.Center.Y)
					return CollisionDirection.down;
				else
					return CollisionDirection.up;
			}	//inside from left/right
			else if ((thisBox.Top < thatBox.Top && thisBox.Bottom > thatBox.Bottom) || (thisBox.Top > thatBox.Top && thisBox.Bottom < thatBox.Bottom))
			{
				if (thisBox.Center.X > thatBox.Center.X)
					return CollisionDirection.left;
				else
					return CollisionDirection.right;
			}	//inside from diagonal
			else
			{
				//Vector2 m = new Vector2(thatBox.Center.X - thisBox.Center.X, thatBox.Center.Y - thisBox.Center.Y);
				//m.Normalize();
				//this.Move(m * 3);
				return CollisionDirection.fallThrough;
			}
		}
	}
}
