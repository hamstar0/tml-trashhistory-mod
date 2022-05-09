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

		 private Item _PrevSeenTrashItem = null;

		private void UpdateTrashState() {
			bool hasTrashItem = this.player.trashItem?.active == true && !this.player.trashItem.IsAir;

			if( this._PrevSeenTrashItem == null ) {
				if( hasTrashItem ) {
					this._PrevSeenTrashItem = this.player.trashItem;
				}
			}

			// Store trash slot-destroyed item
			if( hasTrashItem && this.player.trashItem != this._PrevSeenTrashItem ) {
				if( this.AttemptTrashStore(this._PrevSeenTrashItem) ) {
					TrashHistoryMod.Instance.AddTrashAlertPopup_Local();

					//

					this._PrevSeenTrashItem = this.player.trashItem;
				}
			}

			// Replenish trash slot with stored items
			if( !hasTrashItem ) {
				IList<Item> pulledTrash = this.AttemptTrashPull( 1 );

				if( pulledTrash.Count > 0 ) {
					this.player.trashItem = pulledTrash[0];
					hasTrashItem = true;

					this._PrevSeenTrashItem = pulledTrash[0];
				}
			}
		}
	}
}