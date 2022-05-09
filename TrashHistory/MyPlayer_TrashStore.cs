using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TrashHistory {
	public partial class TrashHistoryPlayer : ModPlayer {
		internal IList<Item> AttemptTrashPull( int amount ) {
			IList<Item> pulledItems = new List<Item>( amount );

			for( int i=0; i<amount; i++ ) {
				if( this.TrashStore.Count <= 0 ) {
					break;
				}

				//

				int storedSlotIdx = this.IsTrashStacked ? this.TrashStore.Count - 1 : 0;

				pulledItems.Add( this.TrashStore[storedSlotIdx] );

				//

				this.TrashStore.RemoveAt( storedSlotIdx );
			}

			//

			if( this.TrashStore.Count == 0 ) {
				bool hasTrashItem = this.player.trashItem?.active == true && !this.player.trashItem.IsAir;

				if( pulledItems.Count < amount ) {
					if( hasTrashItem ) {
						pulledItems.Add( this.player.trashItem );

						this.player.trashItem = new Item();

						hasTrashItem = false;
					}
				}

				if( !hasTrashItem ) {
					this.LastSeenTrashSlotItem = null;
				}
			}

			//

			return pulledItems;
		}

		////

		private bool AttemptTrashStore( Item item ) {
			return this.AttemptTrashStore( new Item[] { item } );
		}

		private bool AttemptTrashStore( IEnumerable<Item> items ) {
			this.TrashStore.AddRange( items );

			return true;
		}


		////////////////

		public void AttemptTrashPullIntoInventory( int amount ) {
			int emptyInvSlots = this.player.inventory
				.Take( this.player.inventory.Length - 1 )
				.Count( i => i?.active != true || i?.IsAir == true );

			int grabAmt = Math.Min( amount, emptyInvSlots );

			//

			IList<Item> pulledItems = this.AttemptTrashPull( grabAmt );

			//

			// Place pulled items into the world
			foreach( Item pulledItem in pulledItems.ToArray() ) {
				int emptyMainItemIdx = Array.FindIndex( Main.item, i => i?.active != true || i.IsAir );
				if( emptyMainItemIdx == -1 ) {
					break;
				}

				//

				Main.item[ emptyMainItemIdx ] = pulledItem;

				pulledItem.Center = this.player.MountedCenter;

				//

				pulledItems.Remove( pulledItem );

				//

				if( Main.netMode == NetmodeID.MultiplayerClient ) {
					NetMessage.SendData( MessageID.SyncItem, -1, -1, null, emptyMainItemIdx );
				}
			}

			// Return spilled items
			this.AttemptTrashStore( pulledItems );
		}
	}
}