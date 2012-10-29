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
using ForbiddenArtsGame.code.images;

namespace ForbiddenArtsGame.code.entities
{
	class PlayerCharacter : Character
	{
		protected bool onGround = false;

		public PlayerCharacter() : this(Vector2.Zero) { }
		public PlayerCharacter(Vector2 loc) : base(loc)
		{
			currentSprite = new PCSprite();
		}

		public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.Update(gameTime);
			KeyboardState keyboard = Keyboard.GetState();

			Settings.CameraLoc.X = loc.X - Settings.screenX / 2 + 100;

			if (loc.Y == Settings.screenY - 200)
				onGround = true;
			else
				onGround = false;

			if (isMobile)
			{
				if (keyboard.IsKeyDown(Settings.keyMoveLeft))
				{
					this.Move(new Vector2(-0.7f, 0));
				}
				if (keyboard.IsKeyDown(Settings.keyMoveRight))
				{
                    this.Move(new Vector2(0.7f, 0));
				}
				if (keyboard.IsKeyDown(Settings.keyJump) && onGround)
				{
					this.Move(new Vector2(0, -30));
				}
				if (keyboard.IsKeyDown(Settings.keyMeleeAttack))
				{
					this.Attack();
				}
			}
		}

		protected virtual void Attack()
		{
			toBeAdded.Add(new MeleeProjectile(loc, new Vector2(5 * (int)facing, 0), this));
		}
	}
}
