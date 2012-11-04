﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
            CameraLoc = new Vector2(-screenX/2, 0);

			string iniPath = @"settings.ini";
			if (File.Exists(iniPath))
			{
				string[] lines = File.ReadAllLines(iniPath);
				foreach (string s in lines)
				{
					switch (s.Substring(0, s.IndexOf(' ')))
					{
						case "left":
							keyMoveLeft = (Keys)int.Parse(s.Substring(s.IndexOf(' ') + 1));
							break;
						case "right":
							keyMoveRight = (Keys)int.Parse(s.Substring(s.IndexOf(' ') + 1));
							break;
						case "jump":
							keyJump = (Keys)int.Parse(s.Substring(s.IndexOf(' ') + 1));
							break;
						case "melee":
							keyMeleeAttack = (Keys)int.Parse(s.Substring(s.IndexOf(' ') + 1));
							break;
						case "spell1":
							keyCastSpell1 = (Keys)int.Parse(s.Substring(s.IndexOf(' ') + 1));
							break;
						case "spell2":
							keyCastSpell2 = (Keys)int.Parse(s.Substring(s.IndexOf(' ') + 1));
							break;
						case "music":
							MusicVolume = float.Parse(s.Substring(s.IndexOf(' ') + 1));
							break;
						case "effects":
							EffectsVolume = float.Parse(s.Substring(s.IndexOf(' ') + 1));
							break;
					}
				}
			}
			Song music = SheetHandler.GetMusic();
			MediaPlayer.Play(music);
			MediaPlayer.IsRepeating = true;
			MediaPlayer.Volume = MusicVolume;
        }

		public static void SaveKeybinds()
		{
			string iniPath = @"settings.ini";
			string[] text = new string[]{
				"left " + (int)keyMoveLeft,
				"right " + (int)keyMoveRight,
				"jump " + (int)keyJump,
				"melee " + (int)keyMeleeAttack,
				"spell1 " + (int)keyCastSpell1,
				"spell2 " + (int)keyCastSpell2,
				"music " + MusicVolume,
				"effects " + EffectsVolume
			};
			File.WriteAllLines(iniPath, text);
		}

        private static GraphicsDeviceManager graphics;
		public static bool Fullscreen { get { return graphics.IsFullScreen; } set { graphics.IsFullScreen = value; graphics.ApplyChanges(); } }
		public static Vector2 CameraLoc;
		public static Vector2 GetPositionFromCamera(Vector2 loc)
		{
			Vector2 ret = loc - CameraLoc;
			ret.X -= screenX / 2;
			ret.Y -= screenY / 2;
			return ret;
		}

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

		public static float MusicVolume { get { return musicVolume; } set { musicVolume = value; MediaPlayer.Volume = value; } }
		private static float musicVolume = 1.0f;
		public static float EffectsVolume = 1.0f;
    }
}
