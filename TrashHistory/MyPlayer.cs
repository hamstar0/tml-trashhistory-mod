using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace TrashHistory {
	public partial class TrashHistoryPlayer : ModPlayer {
		public IReadOnlyList<Item> TrashHistory { get; private set; }


		////////////////

		private List<Item> _TrashHistory = new List<Item>();



		////////////////
		
		public TrashHistoryPlayer() : base() {
			this.TrashHistory = this._TrashHistory.AsReadOnly();
		}

		public override void Initialize() {
			this._TrashHistory.Clear();
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

		internal void AttemptTrashGrab() {
Main.NewText( "Attempted to extract 1 item from trash" );
		}

		internal void AttemptTrashGrabBulk() {
Main.NewText( "Attempted to extract several items from trash" );
		}
	}
}