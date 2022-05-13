using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;


namespace TrashHistory {
	public partial class TrashHistoryMod : Mod {
		public override void PostUpdateInput() {
			if( Main.gameMenu ) {
				return;
			}

			if( PlayerInput.IgnoreMouseInterface ) {
				return;
			}

			//

			Rectangle area = TrashHistoryMod.GetTrashSlotScreenArea_Local( true );

			if( !area.Contains(Main.mouseX, Main.mouseY) ) {
				return;
			}

			//

			var myplayer = Main.LocalPlayer.GetModPlayer<TrashHistoryPlayer>();

			//

			if( Main.mouseLeftRelease && Main.mouseLeft && this.IsHoldingShift ) {
				myplayer.ClearTrashHistory();

				//

				Main.PlaySound( SoundID.Shatter );
			}

			//

			if( Main.mouseRightRelease && Main.mouseRight ) {
				myplayer.AttemptTrashPullIntoInventory( 10 );
			}
		}
	}
}