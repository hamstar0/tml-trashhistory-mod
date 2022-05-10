using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace TrashHistory {
	public partial class TrashHistoryPlayer : ModPlayer {
		private List<Item> TrashStore = new List<Item>();

		private bool IsTrashStacked = true;

		////

		private Item LastSeenTrashSlotItem = null;


		////////////////

		public int TrashHistoryCount => this.TrashStore.Count;

		////

		public override bool CloneNewInstances => false;



		////////////////

		public override void Initialize() {
			this.TrashStore.Clear();
			this.LastSeenTrashSlotItem = null;
			this.IsTrashStacked = true;
		}


		////////////////

		public override void Load( TagCompound tag ) {
			this.TrashStore.Clear();
			this.LastSeenTrashSlotItem = null;
			this.IsTrashStacked = true;

			//

			if( !tag.ContainsKey("trash_history_count") ) {
				return;
			}

			//

			int count = tag.GetInt( "trash_history_count" );

			for( int i=0; i<count; i++ ) {
				TagCompound itemTag = tag.GetCompound( $"trash_history_{i}" );

				this.TrashStore.Add( ItemIO.Load(itemTag) );
			}

			//

			bool hasTrashItem = false;

			if( tag.ContainsKey("has_trash_item") ) {
				hasTrashItem = tag.GetBool( "has_trash_item" );

				if( hasTrashItem ) {
					TagCompound itemTag = tag.GetCompound( "trash_item" );

					this.player.trashItem = ItemIO.Load( itemTag );

					if( Main.netMode == NetmodeID.MultiplayerClient ) {
						Main.clientPlayer.trashItem = this.player.trashItem;
					}
				}
			}

			//

			this.LastSeenTrashSlotItem = this.player.trashItem;
		}


		public override TagCompound Save() {
			int count = this.TrashStore.Count;

			var tag = new TagCompound { { "trash_history_count", count } };

			//

			for(int i=0; i<count; i++ ) {
				tag[ $"trash_history_{i}" ] = ItemIO.Save( this.TrashStore[i] );
			}

			//

			bool hasTrashItem = this.player.trashItem?.active == true && !this.player.trashItem.IsAir;

			tag["has_trash_item"] = hasTrashItem;
			if( hasTrashItem ) {
				tag["trash_item"] = ItemIO.Save( this.player.trashItem );
			}

			//

			return tag;
		}


		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI == Main.myPlayer && !this.player.dead ) {
				this.UpdateTrashState();
			}
		}

		public override void UpdateAutopause() {
			if( this.player.whoAmI == Main.myPlayer && !this.player.dead ) {
				this.UpdateTrashState();
			}
		}

		public override void UpdateDead() {
			if( this.player.whoAmI == Main.myPlayer ) {
				this.UpdateTrashStateDead_If();
			}
		}
	}
}