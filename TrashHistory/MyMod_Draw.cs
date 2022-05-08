using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;


namespace TrashHistory {
	public partial class TrashHistoryMod : Mod {
		public override void ModifyInterfaceLayers( List<GameInterfaceLayer> layers ) {
			if( Main.gameMenu ) {
				return;
			}

			int layerIdx = layers.FindIndex( layer => layer.Name.Equals("Vanilla: Mouse Over") );
			if( layerIdx == -1 ) {
				return;
			}

			//

			var infoLayer = new LegacyGameInterfaceLayer(
				"TrashHistory: Trash Info",
				() => {
					if( Main.gameMenu ) {
						return true;
					}

					//

					this.HandleInterface();

					this.DrawTrashInfo();

					//

					return true;
				},
				InterfaceScaleType.Game
			);

			//

			layers.Insert( layerIdx + 1, infoLayer );
		}


		////////////////

		private void DrawTrashInfo() {
			// TODO
		}
	}
}