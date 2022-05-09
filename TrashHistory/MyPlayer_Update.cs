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

			// Item has been trashed
			if( hasTrashItem && this.player.trashItem != this._PrevSeenTrashItem ) {
Main.NewText( "Trashed a perfectly good " + this.player.trashItem.HoverName );
				if( this.AttemptTrashStore(this._PrevSeenTrashItem) ) {
					TrashHistoryMod.Instance.AddTrashAlertPopup_Local();

					//

					this._PrevSeenTrashItem = this.player.trashItem;
				}
			}

			// Replenish trash slot with reserve items
			if( !hasTrashItem ) {
				IList<Item> pulledTrash = this.AttemptTrashPull( 1 );

				if( pulledTrash.Count > 0 ) {
					this.player.trashItem = pulledTrash[0];
				}
			}
		}
	}
}