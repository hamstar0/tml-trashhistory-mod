using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

			//

			var infoLayer = new LegacyGameInterfaceLayer(
				$"{this.DisplayName}: Trash Info",
				() => {
					if( !Main.gameMenu ) {
						this.HandleInterface_Local();

						this.DrawTrashStats_If();
						this.DrawTrashAlertPopups();
					}
					return true;
				},
				InterfaceScaleType.Game
			);

			var mouseLayer = new LegacyGameInterfaceLayer(
				$"{this.DisplayName}: Mouse Icon",
				() => {
					if( !Main.gameMenu ) {
						this.DrawMouseIcon_If();
					}
					return true;
				},
				InterfaceScaleType.Game
			);

			//

			int mouseTextLayerIdx = layers.FindIndex( layer => layer.Name.Equals("Vanilla: Mouse Text") );
			if( mouseTextLayerIdx != -1 ) {
				layers.Insert( mouseTextLayerIdx, infoLayer );
			}

			int cursorLayerIdx = layers.FindIndex( layer => layer.Name.Equals("Vanilla: Cursor") );
			if( cursorLayerIdx != -1 ) {
				layers.Insert( cursorLayerIdx + 1, mouseLayer );
			}
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

			//bool canHoverAlert = trashHistCount > 0
			//	&& area.Contains( Main.mouseX, Main.mouseY )
			//	&& (Main.mouseItem?.active != true || Main.mouseItem.IsAir)
			//	&& (Main.LocalPlayer.trashItem?.active != true || Main.LocalPlayer.trashItem.IsAir);

			Utils.DrawBorderStringFourWay(
				sb: Main.spriteBatch,
				font: Main.fontMouseText,
				text: text,
				x: area.Center.X,
				y: area.Top + 10,
				textColor: new Color( 255, 224, 96 ),
				borderColor: Color.Black,
				origin: dim * 0.5f,
				scale: 0.6f	//canHoverAlert ? 0.7f : 0.6f
			);
		}


		////////////////

		private void DrawMouseIcon_If() {
			Rectangle area = TrashHistoryMod.GetTrashSlotScreenArea_Local();

			if( !area.Contains(Main.mouseX, Main.mouseY) ) {
				return;
			}

			//

			var myplayer = Main.LocalPlayer.GetModPlayer<TrashHistoryPlayer>();

			if( myplayer.TrashHistoryCount <= 0 ) {
				return;
			}

			//

			Texture2D mouseTex = ModContent.GetTexture( "FindableManaCrystals/MouseRightIcon" );

			Vector2 pos = Main.MouseScreen + new Vector2( -32f, -16f );

			//

			Main.spriteBatch.Draw(
				texture: mouseTex,
				position: pos,
				color: Color.White * 0.3f
			);
		}
	}
}