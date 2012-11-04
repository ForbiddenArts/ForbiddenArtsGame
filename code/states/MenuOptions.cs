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

namespace ForbiddenArtsGame.code.states
{
	class MenuOptions : GameState
	{
		Texture2D sheet;
		Texture2D MusicVolumeSheet;
		Texture2D MusicTitle;
		Rectangle MusicVolRect;
		Texture2D EffectsVolumeSheet;
		Texture2D EffectsTitle;
		Rectangle EffectVolRect;
		Texture2D[,] FullscreenCheckbox;
		Rectangle FullScreenArea;
		Point MouseLoc;
		Texture2D[,] KeybindTexts;
		Rectangle[] KeybindAreas;
		SpriteFont font;
		Texture2D Mainmenu;
		Rectangle Mainrect;
		enum BindSelected { Left, Right, Jump, Interact, Melee, Spell1, Spell2, Inventory }
		BindSelected selectedBind;
		bool binding = false;
		public MenuOptions()
		{
			sheet = SheetHandler.getSheet("menu/options/main");
			MusicVolumeSheet = SheetHandler.getSheet("menu/options/opt_musicBar10-");
			MusicTitle = SheetHandler.getSheet("menu/options/opt_music+");
			MusicVolRect = new Rectangle(314, 260, 294, 44);
			EffectsVolumeSheet = SheetHandler.getSheet("menu/options/opt_soundfxBar10");
			EffectsTitle = SheetHandler.getSheet("menu/options/opt_soundfx+");
			EffectVolRect = new Rectangle(314, 409, 294, 44);
			FullscreenCheckbox = new Texture2D[,]{
				{SheetHandler.getSheet("menu/options/opt_fullCheck-"), SheetHandler.getSheet("menu/options/opt_fullCheck-+")},
				{SheetHandler.getSheet("menu/options/opt_fullCheck+-"), SheetHandler.getSheet("menu/options/opt_fullCheck++")}
			};
			FullScreenArea = new Rectangle(452,494,74,105);
			KeybindTexts = new Texture2D[,]{
				{SheetHandler.getSheet("menu/options/opt_left-"), SheetHandler.getSheet("menu/options/opt_left+")},
				{SheetHandler.getSheet("menu/options/opt_right-"), SheetHandler.getSheet("menu/options/opt_right+")},
				{SheetHandler.getSheet("menu/options/opt_jump-"), SheetHandler.getSheet("menu/options/opt_jump+")},
				{SheetHandler.getSheet("menu/options/opt_interact-"), SheetHandler.getSheet("menu/options/opt_interact+")},
				{SheetHandler.getSheet("menu/options/opt_melee-"), SheetHandler.getSheet("menu/options/opt_melee+")},
				{SheetHandler.getSheet("menu/options/opt_spell1-"), SheetHandler.getSheet("menu/options/opt_spell1+")},
				{SheetHandler.getSheet("menu/options/opt_spell2-"), SheetHandler.getSheet("menu/options/opt_spell2+")},
				{SheetHandler.getSheet("menu/options/opt_inventory-"), SheetHandler.getSheet("menu/options/opt_inventory+")}
			};
			KeybindAreas = new Rectangle[]{
				new Rectangle(733,173,82,25),
				new Rectangle(732,198,82,25),
				new Rectangle(733,223,82,23),
				new Rectangle(732,246,82,23),
				new Rectangle(733,269,82,24),
				new Rectangle(733,293,82,24),
				new Rectangle(733,319,82,26),
				new Rectangle(733,343,82,26)
			};
			font = SheetHandler.getSpriteFont();
			Mainmenu = SheetHandler.getSheet("menu/options/opt_main+");
			Mainrect = new Rectangle(806, 491, 140, 38);
		}

		public override bool Update(GameTime gameTime)
		{
			if (binding)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Escape) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back))
				{
					binding = false;
				}
				else if (Keyboard.GetState().GetPressedKeys().Length > 0)
				{
					switch (selectedBind)
					{
						case BindSelected.Left:
							Settings.keyMoveLeft = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case BindSelected.Right:
							Settings.keyMoveRight = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case BindSelected.Jump:
							Settings.keyJump = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case BindSelected.Interact:
							Settings.keyInteract = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case BindSelected.Melee:
							Settings.keyMeleeAttack = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case BindSelected.Spell1:
							Settings.keyCastSpell1 = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case BindSelected.Spell2:
							Settings.keyCastSpell2 = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case BindSelected.Inventory:
							break;
					}
					binding = false;
					return false;
				}
			}
			MouseLoc = new Point(Mouse.GetState().X, Mouse.GetState().Y);
			if (Mainrect.Contains(MouseLoc) && Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				return true;
			}
			if (FullScreenArea.Contains(MouseLoc))
			{
				if (Mouse.GetState().LeftButton == ButtonState.Pressed)
				{
					Settings.Fullscreen = !Settings.Fullscreen;
				}
			}
			else if (MusicVolRect.Contains(MouseLoc))
			{
				if (Mouse.GetState().LeftButton == ButtonState.Pressed)
				{
					Settings.MusicVolume = (MouseLoc.X - MusicVolRect.Left) / (float)MusicVolRect.Width;
				}
			}
			else if (EffectVolRect.Contains(MouseLoc))
			{
				if (Mouse.GetState().LeftButton == ButtonState.Pressed)
				{
					Settings.EffectsVolume = (MouseLoc.X - EffectVolRect.Left) / (float)EffectVolRect.Width;
				}
			}
			else if (Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				for (int iii = 0; iii < KeybindAreas.Length; iii++)
				{
					Rectangle r = KeybindAreas[iii];
					if (r.Contains(MouseLoc))
					{
						binding = true;
						selectedBind = (BindSelected)iii;
						break;
					}
				}
			}
			return false;
		}

		public override void Draw(GameTime gameTime)
		{
			Settings.spriteBatch.Draw(sheet, Vector2.Zero, Color.White);//background
			Settings.spriteBatch.Draw(MusicVolumeSheet, new Vector2(303, 216), new Rectangle(0, 0, (int)(320 * Settings.MusicVolume), 129), Color.White);//music bar
			if(MusicVolRect.Contains(MouseLoc))
			{
				Settings.spriteBatch.Draw(MusicTitle, new Vector2(301, 217), Color.White);
			}
			Settings.spriteBatch.Draw(EffectsVolumeSheet, new Vector2(301, 351), new Rectangle(0, 0, (int)(322 * Settings.EffectsVolume), 167), Color.White);//effects bar
			if (EffectVolRect.Contains(MouseLoc))
			{
				Settings.spriteBatch.Draw(EffectsTitle, new Vector2(308, 461), Color.White);
			}
			Settings.spriteBatch.Draw(FullscreenCheckbox[FullScreenArea.Contains(MouseLoc) ? 1 : 0, Settings.Fullscreen ? 1 : 0], FullScreenArea, Color.White);
			for (int iii = 0; iii < KeybindAreas.GetLength(0); iii++)
			{
				Settings.spriteBatch.Draw(KeybindTexts[iii, KeybindAreas[iii].Contains(MouseLoc) ? 1 : 0], KeybindAreas[iii], Color.White);
			}
			if (Mainrect.Contains(MouseLoc))
				Settings.spriteBatch.Draw(Mainmenu, Mainrect, Color.White);
		}
	}
}
