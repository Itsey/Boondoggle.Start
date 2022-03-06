namespace Boondoggle.Playah {
    using Plisky.Boondoggle2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    /// <summary>
    /// Amazing Bot is an empty implementation for you to place your bot code into.
    /// </summary>
    public class DemoBot : DroneControlUnitBase {

        
        protected override void DroneArenaPreparePassive() {
            // Prepare for battle is called prior to the battle start.

            // Setting a Fanfare message displays your own personal message
            this.FanfareMessage = "Geronimo.......";

            // The minimum equipment you would need is a Scanner, A PowerPack and Weapon, check the documentation
            // for all of the possible types of weaponry, powerpacks and scanners that are available.
            /* TODO InstallEquipment(KnownEquipmentIds.DefaultScanner, "MyScanner", MountPoint.Internal);
            InstallEquipment(KnownEquipmentIds.DefaultPowerpack, "PowerPack", MountPoint.Internal);
            InstallEquipment(KnownEquipmentIds.WeaponType_Rifle_Instance_1, "Rifle", MountPoint.Turret);*/
        }

        protected override void DroneTakeAction(int turn, int tick, LastTickRecord ltr) {
            var lastScan = (ScanEquipmentUseResult)UseEquipment("MyScanner");

            if (lastScan.NumberOfPOI > 0) {

                // Check Each Of the points of interest that were returned in the scan.
                foreach (var v in lastScan.GetPointsOfInterest()) {

                    // Check whether the point of interest is a Bot, if it is then we can open fire at the bot.  
                    if (lastScan.GetResultAtPosition(v.ScanLocation) == ScanTileResult.Bot) {
                        FireWeapon(v.POIIdentity, "Rifle");
                    }
                }
            }
        }

        

        protected override void DroneTurnStartPassive(int turn) {
            // TODO: Optional - Implement Turn Start Initialisation
        }


        public DemoBot() : base("Demo","1.0.0.0") { }
    }
}
