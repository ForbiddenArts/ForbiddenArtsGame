using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ForbiddenArtsGame.code.images;

namespace ForbiddenArtsGame.code {
    class ParallaxComponent : Sprite {
        private float xSpeed;
        private float zIndex;
        private Rectangle cellBounds;
        private Vector2 location;
        public ParallaxComponent(float x_ratio, int y_position, float depth, string src)
            : base() {
            xSpeed = x_ratio;
            image = SheetHandler.getSheet(src);
            zIndex = depth;
            cellBounds = new Rectangle(0, 0, image.Width, image.Height);
            location = new Vector2(0, y_position);
        }
        public virtual void Draw(GameTime gameTime, float rotation = 0.0f) {
            Vector2 relativeloc = location - (Settings.CameraLoc * xSpeed);
            Vector2 newloc = relativeloc;
            newloc = relativeloc;
            while (newloc.X + cellBounds.Width >= 0) {
                Settings.spriteBatch.Draw(image, newloc, cellBounds, Color.White, rotation, origin, 1.0f, SpriteEffects.None, zIndex);
                newloc.X -= cellBounds.Width;
            }
            newloc = relativeloc;
            while (newloc.X < Settings.screenX + cellBounds.Width) {
                Settings.spriteBatch.Draw(image, newloc, cellBounds, Color.White, rotation, origin, 1.0f, SpriteEffects.None, zIndex);
                newloc.X += cellBounds.Width;
            }
        }
    }
}
