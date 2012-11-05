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
		enum BindSelected { Left, Right, Jump, Interact, Melee, Spell1, Spell2, Inventory, None }
		BindSelected selectedBind;
        KeyboardState oldState;
		bool binding = false;
		bool NeedMouseLift = true;
        int selected = 0;
        int numOptions = 12;
		public MenuOptions(bool _drawParent)
			: this()
		{
			drawParent = _drawParent;
		}
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
            oldState = Keyboard.GetState();
		}

		public override bool Update(GameTime gameTime)
		{
			if (binding)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Escape) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back))
				{
					binding = false;
				}
				else if (Keyboard.GetState().GetPressedKeys().Length > 0 && oldState.IsKeyUp(Keys.Enter) && binding)
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
                            Settings.keyInventory = Keyboard.GetState().GetPressedKeys()[0];
							break;
                        }
					binding = false;
					return false;
				}
			}
			MouseLoc = new Point(Mouse.GetState().X, Mouse.GetState().Y);
			if (Mainrect.Contains(MouseLoc) && Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
                if ((!NeedMouseLift) || Keyboard.GetState().IsKeyDown(Keys.Enter))
					return true;
			}
			else
				NeedMouseLift = false;
            if (selected == 11 && Keyboard.GetState().IsKeyDown(Keys.Enter)) {
                NeedMouseLift = false;
				
                return true;
            }
			if (MusicVolRect.Contains(MouseLoc) || selected == 0)
			{
				if (Mouse.GetState().LeftButton == ButtonState.Pressed)
				{
					Settings.MusicVolume = (MouseLoc.X - MusicVolRect.Left) / (float)MusicVolRect.Width;
				}
                else if (selected == 0 && Keyboard.GetState().IsKeyDown(Keys.Left)) {
                    Settings.MusicVolume -= 0.05f;
                }
                else if (selected == 0 && Keyboard.GetState().IsKeyDown(Keys.Right)) {
                    Settings.MusicVolume += 0.05f;
                }
                if (Settings.MusicVolume < 0.0f) Settings.MusicVolume = 0;
                if (Settings.MusicVolume > 1.0f) Settings.MusicVolume = 1.0f;
			}
            if (EffectVolRect.Contains(MouseLoc) || selected == 1)
			{
				if (Mouse.GetState().LeftButton == ButtonState.Pressed)
				{
					Settings.EffectsVolume = (MouseLoc.X - EffectVolRect.Left) / (float)EffectVolRect.Width;
                }
                else if (selected == 1 && Keyboard.GetState().IsKeyDown(Keys.Left)) {
                    Settings.EffectsVolume -= 0.05f;
                }
                else if (selected == 1 && Keyboard.GetState().IsKeyDown(Keys.Right)) {
                    Settings.EffectsVolume += 0.05f;
                }
                if (Settings.EffectsVolume < 0.0f) Settings.EffectsVolume = 0;
                if (Settings.EffectsVolume > 1.0f) Settings.EffectsVolume = 1.0f;
			}
            if (FullScreenArea.Contains(MouseLoc) && Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
					Settings.Fullscreen = !Settings.Fullscreen;
            }
            if (selected == 2 && Keyboard.GetState().IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter)) {
                Settings.Fullscreen = !Settings.Fullscreen;
            }
			if (Mouse.GetState().LeftButton == ButtonState.Pressed)
			{
				for (int iii = 0; iii < KeybindAreas.Length; iii++)
				{
					Rectangle r = KeybindAreas[iii];
                    if (r.Contains(MouseLoc) || selected == iii - 2)
					{
						binding = true;
						selectedBind = (BindSelected)iii;
						break;
					}
				}
            }
            else if (selected > 2 && selected != 11 && Keyboard.GetState().IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter)) {
                        binding = true;
                        selectedBind = (BindSelected)(selected-3);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && binding == false && oldState.IsKeyUp(Keys.Up)) {
                selected--;
                if (selected < 0) selected = numOptions - 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && binding == false && oldState.IsKeyUp(Keys.Down)) {
                selected = (selected + 1) % numOptions;
            }
            oldState = Keyboard.GetState();
			return false;
		}

		public override void Draw(GameTime gameTime)
		{
			Settings.spriteBatch.Draw(sheet, Vector2.Zero, Color.White);//background
			Settings.spriteBatch.Draw(MusicVolumeSheet, new Vector2(303, 216), new Rectangle(0, 0, (int)(320 * Settings.MusicVolume), 129), Color.White);//music bar
			if(MusicVolRect.Contains(MouseLoc) || selected == 0)
			{
				Settings.spriteBatch.Draw(MusicTitle, new Vector2(301, 217), Color.White);
			}
			Settings.spriteBatch.Draw(EffectsVolumeSheet, new Vector2(301, 351), new Rectangle(0, 0, (int)(322 * Settings.EffectsVolume), 167), Color.White);//effects bar
			if (EffectVolRect.Contains(MouseLoc) || selected == 1)
			{
				Settings.spriteBatch.Draw(EffectsTitle, new Vector2(308, 461), Color.White);
			}
			Settings.spriteBatch.Draw(FullscreenCheckbox[FullScreenArea.Contains(MouseLoc) || selected == 2? 1 : 0, Settings.Fullscreen ? 1 : 0], FullScreenArea, Color.White);
			for (int iii = 0; iii < KeybindAreas.GetLength(0); iii++)
			{
				Settings.spriteBatch.Draw(KeybindTexts[iii, KeybindAreas[iii].Contains(MouseLoc) ? 1 : 0], KeybindAreas[iii], Color.White);
			}
			if (binding && selectedBind == BindSelected.Left)
			{

			}
			else
				Settings.spriteBatch.DrawString(font, Enum.Format(typeof(Keys), Settings.keyMoveLeft, "f"), new Vector2(830, 173), KeybindAreas[0].Contains(MouseLoc) || selected == 3? Color.Violet : Color.Black);
			if (binding && selectedBind == BindSelected.Right)
			{

			}
			else
				Settings.spriteBatch.DrawString(font, Enum.Format(typeof(Keys), Settings.keyMoveRight, "f"), new Vector2(830, 198), KeybindAreas[1].Contains(MouseLoc) || selected == 4 ? Color.Violet : Color.Black);
			if (binding && selectedBind == BindSelected.Jump)
			{

			}
			else
				Settings.spriteBatch.DrawString(font, Enum.Format(typeof(Keys), Settings.keyJump, "f"), new Vector2(830, 223), KeybindAreas[2].Contains(MouseLoc) || selected == 5 ? Color.Violet : Color.Black);
			if (binding && selectedBind == BindSelected.Interact)
			{

			}
			else
				Settings.spriteBatch.DrawString(font, Enum.Format(typeof(Keys), Settings.keyInteract, "f"), new Vector2(830, 246), KeybindAreas[3].Contains(MouseLoc) || selected == 6 ? Color.Violet : Color.Black);
			if (binding && selectedBind == BindSelected.Melee)
			{

			}
			else
				Settings.spriteBatch.DrawString(font, Enum.Format(typeof(Keys), Settings.keyMeleeAttack, "f"), new Vector2(830, 269), KeybindAreas[4].Contains(MouseLoc) || selected == 7 ? Color.Violet : Color.Black);
			if (binding && selectedBind == BindSelected.Spell1)
			{

			}
			else
				Settings.spriteBatch.DrawString(font, Enum.Format(typeof(Keys), Settings.keyCastSpell1, "f"), new Vector2(830, 293), KeybindAreas[5].Contains(MouseLoc) || selected == 8 ? Color.Violet : Color.Black);
			if (binding && selectedBind == BindSelected.Spell2)
			{

			}
			else
				Settings.spriteBatch.DrawString(font, Enum.Format(typeof(Keys), Settings.keyCastSpell2, "f"), new Vector2(830, 319), KeybindAreas[6].Contains(MouseLoc) || selected == 9 ? Color.Violet : Color.Black);
			if (binding && selectedBind == BindSelected.Inventory)
			{

			}
			else
				Settings.spriteBatch.DrawString(font, Enum.Format(typeof(Keys), Settings.keyInventory, "f"), new Vector2(830, 343), KeybindAreas[7].Contains(MouseLoc) || selected == 10 ? Color.Violet : Color.Black);
			if (Mainrect.Contains(MouseLoc) || selected == 11)
				Settings.spriteBatch.Draw(Mainmenu, Mainrect, Color.White);
		}
	}
}
