using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace TrashHistory {
	public partial class TrashHistoryPlayer : ModPlayer {
		public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer ) {
				this.UpdateTrashState();
			}
		}

		public override void UpdateAutopause() {
			if( this.player.whoAmI == Main.myPlayer ) {
				this.UpdateTrashState();
			}
		}
		

		////////////////

		private void UpdateTrashState() {
			bool hasTrashItem = this.player.trashItem?.active == true && !this.player.trashItem.IsAir;

			if( this.LastSeenTrashSlotItem == null ) {
				if( hasTrashItem ) {
					this.LastSeenTrashSlotItem = this.player.trashItem;
				}
			}

			// Add trash slot-destroyed item to player's trash history
			if( hasTrashItem && this.player.trashItem != this.LastSeenTrashSlotItem ) {
				if( this.AttemptTrashStore(this.LastSeenTrashSlotItem) ) {
					TrashHistoryMod.Instance.AddTrashAlertPopup_Local();

					//

					this.LastSeenTrashSlotItem = this.player.trashItem;
				}
			}

			// Replenish any empty trash slot with item pulled from trash history
			if( !hasTrashItem ) {
				IList<Item> pulledTrash = this.AttemptTrashPull( 1 );

				if( pulledTrash.Count > 0 ) {
					this.player.trashItem = pulledTrash[0];

					hasTrashItem = true;

					this.LastSeenTrashSlotItem = pulledTrash[0];
				}
			}
		}
	}
}