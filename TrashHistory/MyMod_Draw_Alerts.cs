using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TrashHistory {
	public partial class TrashHistoryMod : Mod {
		public const int AlertMaxDuration = 30;



		////////////////

		private IList<(int duration, int scrX, int scrY)> TrashAlerts = new List<(int, int, int)>();



		////////////////

		private void DrawTrashAlertPopups() {
			var removedIdxs = new List<int>();

			for( int i=0; i<this.TrashAlerts.Count; i++ ) {
				(int duration, int scrX, int scrY) = this.TrashAlerts[i];

				this.DrawTrashAlertPopup( (float)duration / (float)TrashHistoryMod.AlertMaxDuration, scrX, scrY );

				//

				scrY += 1;
				duration -= 1;

				if( duration > 0 ) {
					this.TrashAlerts[i] = (duration, scrX, scrY);
				} else {
					removedIdxs.Add( i );
				}
			}

			//

			for( int i=removedIdxs.Count - 1; i>=0; i-- ) {
				this.TrashAlerts.RemoveAt( i );
			}
		}

		////////////////
		
		private void DrawTrashAlertPopup( float intensityPercent, int scrX, int scrY ) {
			Vector2 dim = Main.fontMouseText.MeasureString( "+1" );

			Utils.DrawBorderStringFourWay(
				sb: Main.spriteBatch,
				font: Main.fontMouseText,
				text: "+1",
				x: scrX,
				y: scrY,
				textColor: Color.White * intensityPercent,
				borderColor: Color.Black * intensityPercent,
				origin: dim * 0.5f,
				scale: 1f
			);
		}


		////////////////

		public void AddTrashAlertPopup_Local( Item trashedItem ) {
			Rectangle area = TrashHistoryMod.GetTrashSlotScreenArea_Local();
			int x = Main.rand.Next( area.Left+3, area.Right-3 );
			int y = Main.rand.Next( area.Top+3, area.Bottom-3 );

			this.TrashAlerts.Add( (TrashHistoryMod.AlertMaxDuration, x, y) );
		}
	}
}