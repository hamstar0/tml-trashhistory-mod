using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;


namespace TrashHistory {
	public partial class TrashHistoryMod : Mod {
		private void HandleInterface_Local() {
			if( PlayerInput.IgnoreMouseInterface ) {
				return;
			}

			//

			Rectangle trashArea = TrashHistoryMod.GetTrashSlotScreenArea_Local();

			if( !trashArea.Contains(Main.mouseX, Main.mouseY) ) {
				return;
			}

			//

			//if( Main.mouseLeftRelease && Main.mouseLeft ) {
			//	bool hasTrashItem = Main.LocalPlayer.trashItem?.active == true && !Main.LocalPlayer.trashItem.IsAir;
			//	bool hasMouseItem = Main.mouseItem?.active == true && !Main.mouseItem.IsAir;
			//
			//	if( !hasTrashItem && !hasMouseItem ) {
			//		myplayer.AttemptTrashGrab();
			//	}
			//}
			if( Main.mouseRightRelease && Main.mouseRight ) {
				var myplayer = Main.LocalPlayer.GetModPlayer<TrashHistoryPlayer>();

				myplayer.AttemptTrashPullIntoInventory( 10 );
			}
		}
	}
}