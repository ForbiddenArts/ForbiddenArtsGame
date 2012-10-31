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
using ForbiddenArtsGame.code.entities.projectiles;
using ForbiddenArtsGame.code.entities.projectiles.spells;
using ForbiddenArtsGame.code.images;

namespace ForbiddenArtsGame.code.entities
{
	class PlayerCharacter : Character
	{
		protected bool onGround = false;
		Spell spellA, spellB;
		double coolDownEnd;

		public PlayerCharacter() : this(Vector2.Zero) {
		}
		public PlayerCharacter(Vector2 loc) : base(loc)
		{
			currentSprite = new PCSprite();
			spellA = new Immobolise();
			spellB = new Fireball();
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
				if (keyboard.IsKeyDown(Settings.keyCastSpell1))
				{
					if(CanCast(gameTime))
						this.Cast(0);
				}
				if (keyboard.IsKeyDown(Settings.keyCastSpell2))
				{
					if(CanCast(gameTime))
						this.Cast(1);
				}
			}

			//if (loc.X >= 10000)
			//{
			//    this.Sprite.Overlay = Color.PaleVioletRed;
			//    //something to do with mobile melee enemies and the entities list
			//    if(
			//}
		}

		protected virtual void Attack()
		{
			Vector2 projLoc;
			Vector2 projVel;
			if (Keyboard.GetState().IsKeyDown(Settings.keyMoveLeft))
			{
				projLoc = new Vector2(loc.X - 100, loc.Y);
				projVel = new Vector2(-5, 0);
			}
			else if (Keyboard.GetState().IsKeyDown(Settings.keyMoveLeft))
			{
				projLoc = new Vector2(loc.X + 100, loc.Y);
				projVel = new Vector2(5, 0);
			}
			else
			{
				projLoc = new Vector2(loc.X + 100 * (int)facing, loc.Y); ;
				projVel = new Vector2(5 * (int)facing, 0);
			}
			toBeAdded.Add(new MeleeProjectile(projLoc, projVel, this));
		}

		protected virtual void Cast(int spellButton)
		{
			if (spellButton == 0)
			{
				spellA = new Immobolise();
				toBeAdded.Add(spellA.Cast(loc, new Vector2((int)facing, 0), this));
			}
			if (spellButton == 1)
			{
				spellB = new Fireball();
				toBeAdded.Add(spellB.Cast(loc, new Vector2((int)facing, 0), this));
			}
		}

		private bool CanCast(GameTime gameTime)
		{
			if (gameTime.TotalGameTime.TotalSeconds > coolDownEnd)
			{
				coolDownEnd = gameTime.TotalGameTime.TotalSeconds+1;
				return true;
			}
			return false;
		}

		
	}
}
