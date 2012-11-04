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
using ForbiddenArtsGame.code.images;

namespace ForbiddenArtsGame.code.entities
{
	class Hole : Entity
	{
		public Hole(Vector2 _loc)
			: base(_loc)
		{
			currentSprite = new HoleSprite();
		}

		public override Rectangle BoundingBox
		{
			get
			{
				return new Rectangle((int)loc.X - 311, (int)loc.Y - 1000, 430, 2000);
			}
		}
	}
}
