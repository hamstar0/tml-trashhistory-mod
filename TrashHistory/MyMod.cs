using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;


namespace TrashHistory {
	public partial class TrashHistoryMod : Mod {
		public static TrashHistoryMod Instance => ModContent.GetInstance<TrashHistoryMod>();


		////////////////

		public static string GithubUserName => "hamstar0";

		public static string GithubProjectName => "tml-trashhistory-mod";



		////////////////

		public static Rectangle GetTrashSlotScreenArea_Local( bool zoomUI ) {
			Player plr = Main.LocalPlayer;
			float invScale = 0.85f;
			float zoom = zoomUI ? Main.UIScale : 1f;

			int trashScrLeft = (int)(448f * zoom);
			int trashScrTop = (int)(258f * zoom);

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

			int trashScrWidth = (int)((float)Main.inventoryBackTexture.Width * invScale * zoom);
			int trashScrHeight = (int)((float)Main.inventoryBackTexture.Height * invScale * zoom);

			return new Rectangle( trashScrLeft, trashScrTop, trashScrWidth, trashScrHeight );
		}


		public static int GetTrashNetIndex( Player player ) {
			return 58
				+ player.armor.Length
				+ player.dye.Length
				+ player.miscEquips.Length
				+ player.miscDyes.Length
				+ player.bank.item.Length
				+ player.bank2.item.Length
				+ 1;
		}



		////////////////

		public bool IsHoldingShift { get; internal set; }
	}
}