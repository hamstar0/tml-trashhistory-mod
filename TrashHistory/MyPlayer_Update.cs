using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TrashHistory {
	public partial class TrashHistoryPlayer : ModPlayer {
		private void UpdateTrashState() {
			bool hasTrashItem = this.player.trashItem?.active == true && !this.player.trashItem.IsAir;

			if( this.LastSeenTrashSlotItem == null ) {
				if( hasTrashItem ) {
					this.LastSeenTrashSlotItem = this.player.trashItem;
				}
			}

			// Add trash slot-destroyed item to player's trash history
			if( hasTrashItem && this.player.trashItem != this.LastSeenTrashSlotItem ) {
				bool trashStoreSuccess = this.AttemptTrashStore( this.LastSeenTrashSlotItem );

				if( trashStoreSuccess ) {
					TrashHistoryMod.Instance.AddTrashAlertPopup_Local();

					//

					this.LastSeenTrashSlotItem = this.player.trashItem;
				}
			}

			// Replenish any empty trash slot with item pulled from trash history
			if( !hasTrashItem ) {
//Main.NewText("stack "+string.Join(", ", this.TrashStore.Select(i=>i.HoverName)) );
				IList<Item> pulledTrash = this.AttemptTrashPull( 1 );

				if( pulledTrash.Count > 0 ) {
					this.player.trashItem = pulledTrash[0];
//Main.NewText("pulled "+pulledTrash[0].HoverName);

					if( Main.netMode == NetmodeID.MultiplayerClient ) {
						Main.clientPlayer.trashItem = this.player.trashItem;
					}

					hasTrashItem = true;

					this.LastSeenTrashSlotItem = pulledTrash[0];

					//

					/*if( Main.netMode == NetmodeID.MultiplayerClient ) {
						NetMessage.SendData(
							MessageID.SyncEquipment,
							-1,
							-1,
							null,
							this.player.whoAmI,
							GetTrashSlot
						);
					}*/
				}
			}
		}

		private void UpdateTrashStateDead_If() {
			if( this.LastSeenTrashSlotItem?.active != true ) {
				return;
			}

			if( this.player.trashItem?.active == true && !this.player.trashItem.IsAir ) {
				return;
			}

			//

			bool trashStoreSuccess = this.AttemptTrashStore( this.LastSeenTrashSlotItem );

			if( trashStoreSuccess ) {
				TrashHistoryMod.Instance.AddTrashAlertPopup_Local();
			}

			//

			this.LastSeenTrashSlotItem = null;
		}
	}
}
