using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace TrashHistory {
	public partial class TrashHistoryPlayer : ModPlayer {
		private List<Item> TrashStore = new List<Item>();

		private bool IsTrashStacked = true;



		////////////////

		public override void Initialize() {
			this.TrashStore.Clear();
			this.IsTrashStacked = true;
		}


		////////////////

		public override void Load( TagCompound tag ) {
			this.TrashStore.Clear();

			//

			if( !tag.ContainsKey("history_count") ) {
				return;
			}

			//

			int count = tag.GetInt( "history_count" );

			for( int i=0; i<count; i++ ) {
				Item item = tag.Get<Item>( $"history_{i}" );

				this.TrashStore.Add( item );
			}
		}


		public override TagCompound Save() {
			int count = this.TrashStore.Count;

			var tag = new TagCompound { { "history_count", count } };

			//

			for(int i=0; i<count; i++ ) {
				tag[ $"history_{i}" ] = this.TrashStore[i];
			}

			//

			return tag;
		}
	}
}