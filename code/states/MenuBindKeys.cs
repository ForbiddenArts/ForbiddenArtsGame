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
	class MenuBindKeys : GameState
	{
		enum Options { keyMoveLeft, keyMoveRight, keyJump, keyInteract, keyCastSpell1, keyCastSpell2, padMovement, buttonJump, buttonInteract, buttonCastSpell1, buttonCastSpell2, Back };
		bool binding = false;
		bool enterhasbeenreleased = true;
		Options currentOption = Options.keyMoveLeft;

		public MenuBindKeys()
		{
			drawParent = true;
		}

		//TODO:
		//Mouse detection
		//gamepad button rebinding
		public override bool Update(Microsoft.Xna.Framework.GameTime gameTime)
		{
			if (!enterhasbeenreleased)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Enter))
				{
					return false;
				}
				else
				{
					enterhasbeenreleased = true;
				}
			}
			if (binding)
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Escape) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back))
				{
					binding = false;
					return false;
				}
				if (Keyboard.GetState().GetPressedKeys().Length > 0)
				{
					switch (currentOption)
					{
						case Options.keyMoveLeft:
							Settings.keyMoveLeft = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case Options.keyMoveRight:
							Settings.keyMoveRight = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case Options.keyJump:
							Settings.keyJump = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case Options.keyInteract:
							Settings.keyInteract = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case Options.keyCastSpell1:
							Settings.keyCastSpell1 = Keyboard.GetState().GetPressedKeys()[0];
							break;
						case Options.keyCastSpell2:
							Settings.keyCastSpell2 = Keyboard.GetState().GetPressedKeys()[0];
							break;
					}
					binding = false;
					if (Keyboard.GetState().IsKeyDown(Keys.Enter))
					{
						enterhasbeenreleased = false;
					}
				}
			}
			else
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Escape) || GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Back))
					return true;
				if (Keyboard.GetState().IsKeyDown(Keys.Up))
				{
					if(currentOption != Options.keyMoveLeft)
						currentOption--;
				}
				if (Keyboard.GetState().IsKeyDown(Keys.Down))
				{
					if(currentOption != Options.Back)
						currentOption--;
				}
				if (Keyboard.GetState().IsKeyDown(Keys.Enter))
				{
					if (currentOption == Options.Back)
					{
						return true;
					}
					else
					{
						binding = true;
						enterhasbeenreleased = false;
					}
				}
			}
			return false;
		}

		public override void Draw(GameTime gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
