using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
		private static Thread loadThread;
		public static ContentManager Content { get { return content; } }
		public static Dictionary<string, Texture2D> sheets;

		public static void Initialize(ContentManager _content)
		{
			content = _content;
			sheets = new Dictionary<string, Texture2D>();
			loadThread = new Thread(new ThreadStart(SheetHandler.LoadAll));
			loadThread.Start();
		}

		public static bool hasSheet(string name)
		{
			return sheets.ContainsKey(name);
		}

		public static Texture2D getSheet(string name)
		{
			Texture2D ret;
			sheets.TryGetValue(name, out ret);
			if (ret != null)
				return ret;
			//else
			try
			{
				ret = Content.Load<Texture2D>(name);
			}
			catch(ArgumentException ex)
			{
				sheets.TryGetValue(name, out ret);
				if (ret == null)
					return getSheet(name);
				else
					return ret;
			}
			sheets.Add(name, ret);
			return ret;
		}

		public static void LoadSheet(string name)
		{
			sheets.Add(name, Content.Load<Texture2D>(name));
		}

		//run in a separate thread to the one everything else is, menu stuff at the start because it is needed first
		public static void LoadAll()
		{
			SheetHandler.LoadSheet("menu/spriteSheetpart1");
			SheetHandler.LoadSheet("menu/spriteSheetpart2");
			SheetHandler.LoadSheet("menu/spriteSheetpart3");
			SheetHandler.LoadSheet("menu/spriteSheetpart4");
			SheetHandler.LoadSheet("menu/spriteSheetpart5");
			SheetHandler.LoadSheet("menu/spriteSheetpart6");
			SheetHandler.LoadSheet("sceneL0");
			SheetHandler.LoadSheet("sceneL1");
			SheetHandler.LoadSheet("sceneL2");
			SheetHandler.LoadSheet("sceneL3");
			SheetHandler.LoadSheet("sceneL4");
			SheetHandler.LoadSheet("healthsheet");
			SheetHandler.LoadSheet("spelluisheet");
			SheetHandler.LoadSheet("characters/Mage");
			SheetHandler.LoadSheet("characters/Melee");
		}

		public static void FinishLoad()
		{
			if (loadThread.IsAlive)
				loadThread.Join();
		}
	}
}
