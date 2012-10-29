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
	class Terrain : Entity
	{
        private int[] LAYER_Y_VALUES = { 504, 0, 0, 0, 0 };
        private float[] LAYER_X_SPEEDS = { 1.26f, 0.8f, 0.5f, 0.42f, 0.3f };
        private ParallaxComponent[] layers = new ParallaxComponent[5];

		public override Rectangle BoundingBox
		{
			get
			{
				return new Rectangle((int)loc.X, (int)loc.Y, 1, 1);
			}
		}

		public Terrain()
		{
            currentSprite = new TerrainSprite("transparent", new Rectangle(0, 0, 900, 100));
            loc = new Vector2(0, 660);
            for (int i = 0; i < 5; i++) {
                layers[i] = new ParallaxComponent(LAYER_X_SPEEDS[i], LAYER_Y_VALUES[i], (float)(0.5f - (i*0.1f)), "sceneL" + i);
            }
            
		}
        public override void Draw(GameTime gameTime)
        {
            for (int i = 4; i>=0; i--) {
                layers[i].Draw(gameTime);
            }
 	        base.Draw(gameTime);
        }
	}
}
