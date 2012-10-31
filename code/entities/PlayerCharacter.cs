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
using ForbiddenArtsGame.code.entities.projectiles.spells;
using ForbiddenArtsGame.code.images;

namespace ForbiddenArtsGame.code.entities
{
	class PlayerCharacter : Character
	{
		protected bool onGround = false;
		Spell spellA, spellB;
		double coolDownEnd;
		Dictionary<string, Animation> sprites;
		string currentSpriteName;
		TimeSpan nextAttackAvailable;
		Texture2D HealthSheet;
		Texture2D SpellSheet;

		public PlayerCharacter() : this(Vector2.Zero) {
		}
		public PlayerCharacter(Vector2 loc) : base(loc)
		{
			spellA = new Immobolise();
			spellB = new Fireball();

			sprites = new Dictionary<string, Animation>();
			sprites.Add("cast", new MageCast());
			sprites.Add("fall", new MageFall());
			sprites.Add("idle", new MageIdle());
			sprites.Add("jump", new MageJump());
			sprites.Add("melee", new MageMelee());
			sprites.Add("walk", new MageWalk());
			SetCurrentAnimation("idle");

			nextAttackAvailable = new TimeSpan(0);

			HealthSheet = SheetHandler.getSheet("healthsheet");
			SpellSheet = SheetHandler.getSheet("spelluisheet");
		}

		public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
		{
			if (dead)
			{
				return;
			}
			if (health <= 0)
			{
				dead = true;
				currentSprite = new MageDead();
			}
			base.Update(gameTime);
			KeyboardState keyboard = Keyboard.GetState();

			Settings.CameraLoc.X = loc.X - Settings.screenX / 2 + 100;

			if (loc.Y == Settings.screenY - 200)
				onGround = true;
			else
				onGround = false;

			//input handling
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
					SetCurrentAnimation("jump");
				}
				if (keyboard.IsKeyDown(Settings.keyMeleeAttack))
				{
					this.Attack(gameTime);
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

			//a bit of current image handling
			if (!onGround)
			{
				if (currentSpriteName != "fall" && currentSpriteName != "jump")
				{
					SetCurrentAnimation("fall");
				}
			}
			else
			{
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

			//if (loc.X >= 10000)
			//{
			//    this.Sprite.Overlay = Color.PaleVioletRed;
			//    //something to do with mobile melee enemies and the entities list
			//    if(
			//}
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

		protected virtual void Attack(GameTime gameTime)
		{
			if (gameTime.TotalGameTime < nextAttackAvailable)
				return;
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
			SetCurrentAnimation("melee");
			nextAttackAvailable = gameTime.TotalGameTime + new TimeSpan(0, 0, 1);
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
			SetCurrentAnimation("cast");
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

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			Settings.spriteBatch.Draw(HealthSheet, new Rectangle(0, 0, 145, 156), new Rectangle(0, 0, 145, 156), Color.White);//background thing
			Settings.spriteBatch.Draw(HealthSheet, new Rectangle(0, 27, 145, 126 * (health / 100)), new Rectangle(145, 27, 145, 126 * (health / 100)), Color.White);

			Settings.spriteBatch.Draw(SpellSheet, new Rectangle(Settings.screenX - 144, 0, 144, 153), new Rectangle(0, 0, 144, 153), Color.White);//background thing
			Settings.spriteBatch.Draw(SpellSheet, new Rectangle(Settings.screenX - 142, 27, 109, 102), new Rectangle(144, 0, 109, 102), Color.White);
			Settings.spriteBatch.Draw(SpellSheet, new Rectangle(Settings.screenX - 119, 41, 100, 110), new Rectangle(253, 0, 100, 110), Color.White);
		}
	}
}
