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
			bool prevTrashItemActive = this.LastKnownTrashItem?.active == true && !this.LastKnownTrashItem.IsAir;
			bool currTrashItemActive = this.player.trashItem?.active == true && !this.player.trashItem.IsAir;

			if( this.LastKnownTrashItem == null ) {
				if( currTrashItemActive ) {
					this.LastKnownTrashItem = this.player.trashItem;

					prevTrashItemActive = true;
				}
			} else {
				if( !currTrashItemActive ) {
					this.LastKnownTrashItem = null;

					prevTrashItemActive = false;
				}
			}

			//

			if( prevTrashItemActive ) {
				if( currTrashItemActive ) {
					if( this.player.trashItem != this.LastKnownTrashItem ) {
Main.NewText( "Trashed a perfectly good "+this.LastKnownTrashItem.HoverName );
						this._TrashHistory.Add( this.LastKnownTrashItem );

						TrashHistoryMod.Instance.AddTrashAlertPopup_Local( this.LastKnownTrashItem );

						this.LastKnownTrashItem = this.player.trashItem;
					}
				}
			}
		}
	}
}