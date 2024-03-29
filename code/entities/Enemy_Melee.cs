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
using ForbiddenArtsGame.code.images;
using ForbiddenArtsGame.code.entities.projectiles;

namespace ForbiddenArtsGame.code.entities
{
	class Enemy_Melee : Character
	{
		private bool hasSeen = false;
		string currentSpriteName;
		Dictionary<string, Animation> sprites;
		TimeSpan nextAttackAvailable;

		public Enemy_Melee(Vector2 loc) : base(loc)
		{
			currentSprite = new MeleeEnemyIdle();
			sprites = new Dictionary<string, Animation>();
			sprites.Add("walk", new MeleeEnemyWalk());
			sprites.Add("idle", new MeleeEnemyIdle());
			sprites.Add("attack", new MeleeEnemyAttack());
			nextAttackAvailable = new TimeSpan(0);
		}

		public override void Update(GameTime gameTime)
		{
			if (dead)
			{
				return;
			}
			if (health <= 0)
			{
				dead = true;
				currentSprite = new MeleeEnemyDead();
			}
			base.Update(gameTime);
			Vector2 relativePlayerPosition = Settings.GetPositionFromCamera(loc);
			if (hasSeen)
			{
				if (relativePlayerPosition.X > -500 && relativePlayerPosition.X < 300)
				{
					if (velocity.Length() > 0)
					{
						Vector2 movement = velocity;
						movement.Normalize();
						movement = movement * -1;
						Move(movement);
						if ((velocity.X > 0 && movement.X > 0) || (velocity.X < 0 && movement.X < 0))
							velocity = Vector2.Zero;
					}
					else
						this.Attack(gameTime);
				}
				else if (relativePlayerPosition.X >= 300)
				{
					this.Move(new Vector2(-0.5f, 0));
				}
				else
				{
					this.Move(new Vector2(0.5f, 0));
				}
			}
			else
			{
				if (relativePlayerPosition.X > -700 && relativePlayerPosition.X < 600)
					hasSeen = true;
			}
			if (velocity.Length() > 0.5f && currentSpriteName == "idle")
			{
				SetCurrentAnimation("walk");
			}
			else if (velocity.Length() <= 0.5f && currentSpriteName == "walk")
			{
				SetCurrentAnimation("idle");
			}
			else
			{
				if (((Animation)currentSprite).cycleNum >= 1 && currentSpriteName != "walk")
					SetCurrentAnimation("idle");
			}
		}

		protected override void SetCurrentAnimation(string name)
		{
			Animation temp;
			sprites.TryGetValue(name, out temp);
			if ((name == "walk" || name == "idle") && temp == currentSprite)
			{
				return;
			}
			temp.reset();
			currentSprite = temp;
			currentSpriteName = name;
		}

		protected void Attack(GameTime gameTime)
		{
			if (gameTime.TotalGameTime < nextAttackAvailable)
				return;
			SetCurrentAnimation("attack");
			Vector2 projLoc = new Vector2(loc.X + 100 * (int)facing, loc.Y); ;
			Vector2 projVel = new Vector2(5 * (int)facing, 0);
			toBeAdded.Add(new MeleeProjectile(projLoc, projVel, this));
			nextAttackAvailable = gameTime.TotalGameTime + new TimeSpan(TimeSpan.TicksPerSecond);
		}
	}
}
