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
		enum Options { GeneralVolume, MusicToggle, MusicVolume, Keybind, Back };

		public MenuOptions()
		{
			drawParent = true;
		}

		public override bool Update(GameTime gameTime)
		{
			throw new NotImplementedException();
		}

		public override void Draw(GameTime gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
