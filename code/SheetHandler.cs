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

namespace ForbiddenArtsGame.code
{
	static class SheetHandler
	{
		private static ContentManager content;
		public static ContentManager Content { get { return content; } }
		public static Dictionary<string, Texture2D> sheets;

		public static void Initialize(ContentManager _content)
		{
			content = _content;
			sheets = new Dictionary<string, Texture2D>();
		}

		public static Texture2D getSheet(string name)
		{
			Texture2D ret;
			sheets.TryGetValue(name, out ret);
			if (ret != null)
				return ret;
			//else
			ret = Content.Load<Texture2D>(name);
			sheets.Add(name, ret);
			return ret;
		}

		public static void LoadSheet(string name)
		{
			sheets.Add(name, Content.Load<Texture2D>(name));
		}
	}
}
