using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


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
			if( this.player.trashItem?.active == true && !this.player.trashItem.IsAir ) {
Main.NewText( "Trashed a perfectly good " + this.player.trashItem.HoverName );
				this._TrashHistory.Add( this.player.trashItem );

				//

				TrashHistoryMod.Instance.AddTrashAlertPopup_Local( this.player.trashItem );

				//

				this.player.trashItem = new Item();
			}
		}
	}
}