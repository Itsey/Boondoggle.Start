namespace Boondoggle.Playah {

    using Plisky.Boondoggle2;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Amazing Bot is an empty implementation for you to place your bot code into.
    /// </summary>
    public class MyAmazingDrone : DroneControlUnitBase {

        protected override void DroneTakeAction(int turn, int tick, LastTickRecord ltr) {
            // TODO: Implement actual Actions

            if (this.CurrentSpeed < 5) {
                Accelerate();
            }

            bool collisionAhead = false;
            var lastScan = (ScanEquipmentUseResult)UseEquipment("MyScanner");
            for (int i = 0; i < 5; i++) {
                
                if (lastScan.GetResultAtPosition(new Point(0, i)) != ScanTileResult.Unoccupied) {
                    collisionAhead = true;
                    break;
                }
            }

            if (collisionAhead) {
                ChangeHeading(CurrentHeading + 90);
            }
        }


        public MyAmazingDrone() : base("WalkthroughDrone", "1.0.0.0") { }
    }
}