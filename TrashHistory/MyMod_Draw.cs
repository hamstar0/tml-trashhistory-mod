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

					this.HandleInterface_Local();

					this.DrawTrashStats_If();
					this.DrawTrashAlertPopups();

					//

					return true;
				},
				InterfaceScaleType.Game
			);

			//

			layers.Insert( layerIdx + 1, infoLayer );
		}


		////////////////

		private void DrawTrashStats_If() {
			if( !Main.playerInventory ) {
				return;
			}

			//

			var myplayer = Main.LocalPlayer.GetModPlayer<TrashHistoryPlayer>();

			int trashHistCount = myplayer.TrashHistoryCount;

			Rectangle area = TrashHistoryMod.GetTrashSlotScreenArea_Local();

			string text = $"{trashHistCount} items";

			Vector2 dim = Main.fontMouseText.MeasureString( text );

			//

			bool canHoverAlert = trashHistCount > 0
				&& area.Contains( Main.mouseX, Main.mouseY )
				&& (Main.mouseItem?.active != true || Main.mouseItem.IsAir)
				&& (Main.LocalPlayer.trashItem?.active != true || Main.LocalPlayer.trashItem.IsAir);

			//

			Utils.DrawBorderStringFourWay(
				sb: Main.spriteBatch,
				font: Main.fontMouseText,
				text: text,
				x: area.Center.X,
				y: area.Top + 10,
				textColor: new Color( 255, 224, 96 ),
				borderColor: Color.Black,
				origin: dim * 0.5f,
				scale: canHoverAlert ? 0.7f : 0.6f
			);
		}
	}
}