using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace TrashHistory {
	public partial class TrashHistoryPlayer : ModPlayer {
		public IReadOnlyList<Item> TrashHistory { get; private set; }


		////////////////

		private List<Item> _TrashHistory = new List<Item>();

		////

		private Item LastKnownTrashItem = null;



		////////////////
		
		public TrashHistoryPlayer() : base() {
			this.TrashHistory = this._TrashHistory.AsReadOnly();
		}

		public override void Initialize() {
			this._TrashHistory.Clear();
			this.LastKnownTrashItem = null;
		}


		////////////////

		public override void Load( TagCompound tag ) {
			this._TrashHistory.Clear();

			//

			if( !tag.ContainsKey("history_count") ) {
				return;
			}

			//

			int count = tag.GetInt( "history_count" );

			for( int i=0; i<count; i++ ) {
				Item item = tag.Get<Item>( $"history_{i}" );

				this._TrashHistory.Add( item );
			}
		}


		public override TagCompound Save() {
			int count = this._TrashHistory.Count;

			var tag = new TagCompound { { "history_count", count } };

			//

			for(int i=0; i<count; i++ ) {
				tag[ $"history_{i}" ] = this._TrashHistory[i];
			}

			//

			return tag;
		}


		////////////////

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

		////

		private void UpdateTrashState() {
			bool prevTrashItemActive = this.LastKnownTrashItem?.active == true && !this.LastKnownTrashItem.IsAir;
			bool currTrashItemActive = this.player.trashItem?.active == true && !this.player.trashItem.IsAir;

			if( !prevTrashItemActive ) {
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
Main.NewText( $"trashed a perfectly good {this.LastKnownTrashItem.HoverName}" );
						this._TrashHistory.Add( this.LastKnownTrashItem );

						this.LastKnownTrashItem = this.player.trashItem;
					}
				}
			}
		}
	}
}