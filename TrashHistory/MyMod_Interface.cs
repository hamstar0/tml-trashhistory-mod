using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;


namespace TrashHistory {
	public partial class TrashHistoryMod : Mod {
		private void HandleInterface() {
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

			int trashScrRight = trashScrLeft + (int)((float)Main.inventoryBackTexture.Width * invScale);
			int trashScrBot = trashScrTop + (int)((float)Main.inventoryBackTexture.Height * invScale);

			//

			if( !PlayerInput.IgnoreMouseInterface ) {
				if( Main.mouseX >= trashScrLeft
							&& Main.mouseX <= trashScrRight
							&& Main.mouseY >= trashScrTop
							&& Main.mouseY <= trashScrBot ) {
					if( Main.mouseLeftRelease && Main.mouseLeft ) {
						if( plr.trashItem?.active != true && Main.mouseItem?.active != true ) {
							this.AttemptTrashGrab();
						}
					} else if( Main.mouseRightRelease && Main.mouseRight ) {
						this.AttemptTrashGrabBulk();
					}
				}
			}
		}


		////////////////

		private void AttemptTrashGrab() {
Main.NewText( "Attempted to extract 1 item from trash" );
		}

		private void AttemptTrashGrabBulk() {
Main.NewText( "Attempted to extract several items from trash" );
		}
	}
}