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

		public MainGame()
		{
			terrain = new Terrain();
			entities = new List<Entity>();
			entities.Add(terrain);
			entities.Add(new PlayerCharacter(new Vector2(100, 200)));
			entities.Add(new Enemy_Melee(new Vector2(200, 200)));
		}

		public override bool Update(GameTime gameTime)
		{
			if (child != null)
			{
				if (child.Update(gameTime))
				{
					if (((PauseMenu)child) != null && ((PauseMenu)child).SelectedExit)
					{
						return true;
					}
					else
					{
						child = null;
					}
				}
			}

			//check collisions between everything
			for (List<Entity>.Enumerator ee = entities.GetEnumerator(); ee.MoveNext(); )
			{
				//dont check things that have already been checked
				List<Entity>.Enumerator ff = entities.GetEnumerator();
				while (ee.Current != ff.Current)
				{
					ff.MoveNext();
				}
				for (; ee.MoveNext(); )
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
				additions.AddRange(e.toBeAdded);
				removals.AddRange(e.toBeRemoved);
				e.Updated();
			}
			entities.AddRange(additions);
			foreach (Entity e in removals)
			{
				entities.Remove(e);
			}
			return false;
		}

		public override void Draw(GameTime gameTime)
		{
			foreach (Entity e in entities)
			{
				e.Draw(gameTime);
			}
			terrain.DrawFront(gameTime);
		}
	}
}
