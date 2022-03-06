namespace Boondoggle.Playah {
    using Plisky.Boondoggle2;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DroneUtilities {
        private const int DISTANCETOCHECK = 10;

        /// <summary>
        /// Checks a scan result looking in a given direction - this implementaiton only supports 0,90,180 or 270 as directions.  It is a very basic utility to check
        /// the direction is clear in a scan.  Most helper methods are in the Bot Base Class but a few are placed here to help people get used to the code.
        /// </summary>        
        public static bool IsThisDirectionClear(ScanEquipmentUseResult lastScan, Double direction) {
            if (direction == 0) {
                for (int i = 1; i <= DISTANCETOCHECK; i++) {
                    if (lastScan.GetResultAtPosition(new Point(0, i)) != ScanTileResult.Unoccupied) {
                        return false;
                    }
                }
                return true;
            }
            if (direction == 90) {
                for (int i = 1; i <= DISTANCETOCHECK; i++) {
                    if (lastScan.GetResultAtPosition(new Point(i, 0)) != ScanTileResult.Unoccupied) {
                        return false;
                    }
                }
                return true;
            }

            if (direction == 180) {
                for (int i = -1; i >= -DISTANCETOCHECK; i--) {
                    if (lastScan.GetResultAtPosition(new Point(i, i)) != ScanTileResult.Unoccupied) {
                        return false;
                    }
                }
                return true;
            }
            if (direction == 270) {
                for (int i = -1; i >= -DISTANCETOCHECK; i--) {
                    if (lastScan.GetResultAtPosition(new Point(i, 0)) != ScanTileResult.Unoccupied) {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }
    }
}
