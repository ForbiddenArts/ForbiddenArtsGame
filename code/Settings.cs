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

namespace ForbiddenArtsGame.code
{
    static class Settings
    {
        public static void Initialize(ref GraphicsDeviceManager _graphics)
        {
            graphics = _graphics;
			graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			CameraLoc = Vector2.Zero;
        }

        private static GraphicsDeviceManager graphics;

		public static Vector2 CameraLoc;
		public static SpriteBatch spriteBatch { get; private set; }
        public static int screenX { get { return graphics.GraphicsDevice.Viewport.Width; } }
        public static int screenY { get { return graphics.GraphicsDevice.Viewport.Height; } }

		public static Keys keyMoveLeft = Keys.Left;
		public static Keys keyMoveRight = Keys.Right;
		public static Keys keyJump = Keys.Space;
		public static Keys keyInteract = Keys.Enter;
		public static Keys keyCastSpell1 = Keys.Z;
		public static Keys keyCastSpell2 = Keys.X;
		public static Keys keyMeleeAttack = Keys.C;
		public enum PadInput { LeftThumbstick, RightThumbstick, DPad };
		public static PadInput padMoveInput = PadInput.LeftThumbstick;
		public static Buttons buttonJump = Buttons.A;
		public static Buttons buttonInteract = Buttons.B;
		public static Buttons buttonCastSpell1 = Buttons.X;
		public static Buttons buttonCastSpell2 = Buttons.Y;
    }
}
