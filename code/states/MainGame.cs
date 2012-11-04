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

namespace ForbiddenArtsGame.code.states
{
	class MainGame : GameState
	{
		List<Entity> entities;
		Terrain terrain;

		PlayerCharacter PC;
		Hole hole;

		public MainGame()
		{
			PC = new PlayerCharacter(new Vector2(100, 200));
			hole = new Hole(new Vector2(6000, 660));
			terrain = new Terrain();
			entities = new List<Entity>();
			entities.Add(terrain);
			entities.Add(PC);
			entities.Add(new Enemy_Melee(new Vector2(1500, 200)));
			entities.Add(new Enemy_Ranged(new Vector2(4000, 200)));
			entities.Add(hole);
			entities.Add(new Enemy_Melee(new Vector2(8500, 200)));
			entities.Add(new Enemy_Ranged(new Vector2(8650, 200)));
		}

		public override bool Update(GameTime gameTime)
		{
			if (child != null)
			{
				if (child.Update(gameTime))
				{
					if (child is PauseMenu && ((PauseMenu)child).SelectedExit)
					{
						return true;
					}
					else
					{
						child = null;
					}
				}
				else
				{
					return false;
				}
			}

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				child = new MenuOptions(true);

			//check collisions between everything
			for (List<Entity>.Enumerator ee = entities.GetEnumerator(); ee.MoveNext(); )
			{
				//dont check things that have already been checked
				List<Entity>.Enumerator ff = entities.GetEnumerator();
				while (ee.Current != ff.Current)
				{
					ff.MoveNext();
				}
				//Changed from ee.MoveNext()
				for (; ff.MoveNext(); )
				{
					if (Entity.CollisionCheck(ee.Current, ff.Current, false))
					{
						ee.Current.Collide(ff.Current);
						ff.Current.Collide(ee.Current);
					}
				}
			}

			//update everything
			List<Entity> additions = new List<Entity>();
			List<Entity> removals = new List<Entity>();
			foreach (Entity e in entities)
			{
				e.Update(gameTime);
				if(e.toBeAdded.Count > 0)
				additions.AddRange(e.toBeAdded);
				removals.AddRange(e.toBeRemoved);
				e.Updated();
			}
			entities.AddRange(additions);
			foreach (Entity e in removals)
			{
				entities.Remove(e);
			}

			if (PC.LocY > 800)
			{
				child = new Minigame();
				entities.Remove(hole);
			}

			if (playerEnding())
			{
				//end game
				return true;
			}

			return false;
		}

		public override void Draw(GameTime gameTime)
		{
			if (child != null && !child.DrawParent())
			{
				child.Draw(gameTime);
				return;
			}
			foreach (Entity e in entities)
			{
				e.Draw(gameTime);
			}
			terrain.DrawFront(gameTime);
			if (child != null)
				child.Draw(gameTime);
		}

		public bool playerEnding()
		{
			bool end = false;
			PlayerCharacter charac = entities[1] as PlayerCharacter;
			if (charac != null)
			{
				if (charac.getXPosition() >= 10000)
				{
					if (!enemyUnaccounted())
					{
						end = true;
					}
				}
			}
			return end;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>false if all dead</returns>
		public bool enemyUnaccounted()
		{
			foreach (Entity e in entities)
			{
				Enemy_Melee charact = e as Enemy_Melee;
				if (charact != null)
				{
					if (charact.checkIsMobile() && !charact.checkIsDead())
					{
						return true;
					}
				}
				Enemy_Ranged character = e as Enemy_Ranged;
				if (character != null)
				{
					if (character.checkIsMobile() && !character.checkIsDead())
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
