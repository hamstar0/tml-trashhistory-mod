using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
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

			var myplayer = Main.LocalPlayer.GetModPlayer<TrashHistoryPlayer>();

			if( Main.mouseLeftRelease && Main.mouseLeft ) {
				bool hasTrashItem = Main.LocalPlayer.trashItem?.active == true && !Main.LocalPlayer.trashItem.IsAir;
				bool hasMouseItem = Main.mouseItem?.active == true && !Main.mouseItem.IsAir;

				if( !hasTrashItem && !hasMouseItem ) {
					myplayer.AttemptTrashGrab();
				}
			} else if( Main.mouseRightRelease && Main.mouseRight ) {
				myplayer.AttemptTrashGrabBulk();
			}
		}
	}
}