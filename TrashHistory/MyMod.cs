using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;


namespace TrashHistory {
	public partial class TrashHistoryMod : Mod {
		public static TrashHistoryMod Instance => ModContent.GetInstance<TrashHistoryMod>();


		////////////////

		public static string GithubUserName => "hamstar0";

		public static string GithubProjectName => "tml-trashhistory-mod";



		////////////////

		public static Rectangle GetTrashSlotScreenArea_Local() {
			Player plr = Main.LocalPlayer;
			float invScale = 0.85f;

			int trashScrLeft = 448;
			int trashScrTop = 258;

			if( (plr.chest != -1 || Main.npcShop > 0) && !Main.recBigList ) {
				trashScrTop += 168;
				trashScrLeft += 5;

				invScale = 0.755f;
			} else if( (plr.chest == -1 || Main.npcShop == -1) && Main.trashSlotOffset != Point16.Zero ) {
				trashScrLeft += Main.trashSlotOffset.X;
				trashScrTop += Main.trashSlotOffset.Y;

				invScale = 0.755f;
			}

			//

			int trashScrWidth = (int)((float)Main.inventoryBackTexture.Width * invScale);
			int trashScrHeight = (int)((float)Main.inventoryBackTexture.Height * invScale);

			return new Rectangle( trashScrLeft, trashScrTop, trashScrWidth, trashScrHeight );
		}
	}
}