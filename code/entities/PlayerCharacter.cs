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
		public PlayerCharacter() : this(Vector2.Zero) { }
		public PlayerCharacter(Vector2 loc) : base(loc)
		{
			currentSprite = new PCSprite();
		}

		public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.Update(gameTime);
			KeyboardState keyboard = Keyboard.GetState();

			if (isMobile)
			{
				if (keyboard.IsKeyDown(Settings.keyMoveLeft))
				{
					this.Move(new Vector2(-2, 0));
				}
				if (keyboard.IsKeyDown(Settings.keyMoveRight))
				{
					this.Move(new Vector2(2, 0));
				}
				if (keyboard.IsKeyDown(Settings.keyJump) && onGround)
				{
					this.Move(new Vector2(0, -10));
					onGround = false;
				}
				if (keyboard.IsKeyDown(Settings.keyMeleeAttack))
				{
					toBeAdded.Add(new MeleeProjectile(loc, new Vector2(facing == Facing.Left ? -5 : 5, 0), this));
				}
			}
		}
	}
}
