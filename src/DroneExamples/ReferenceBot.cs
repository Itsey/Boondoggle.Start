﻿namespace Boondoggle.Playah {
    using Plisky.Boondoggle2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Reference_Bot is a fully functional reference implementation of a Bot, showing examples of navigation, scanning and weapons usage.  Its primary goal is to serve
    /// as documentation for how to build a Boondoggle Bot.
    /// </summary>
    public class Reference_Bot : DroneControlUnitBase {

        /// <summary>
        /// There are tjree primary interface methods that are called by the engine for your BOT.  The first element is the ActualPrepareForBattle
        /// which occurs prior to the battle start.
        /// </summary>
        protected override void DroneArenaPreparePassive() {
            // Prepare for battle is called once, prior to the battle start.  There is no time constraint on this method but no usage of installed equpment is permitted
            // You are able to initialise and Install Equipment but you can not, for example, scan.

            // Setting a Fanfare message displays your own personal message
            this.FanfareMessage = "Geronimo.......";

            // The minimum equipment you would need is a Scanner, A PowerPack and Weapon, check the documentation
            // for all of the possible types of weaponry, powerpacks and scanners that are available.
            /*InstallEquipment(KnownEquipmentIds.DefaultScanner, "MyScanner", MountPoint.Internal);
            InstallEquipment(KnownEquipmentIds.DefaultPowerpack, "PowerPack", MountPoint.Internal);
            InstallEquipment(KnownEquipmentIds.WeaponType_Rifle_Instance_1, "Rifle", MountPoint.Turret);*/

        }

        /// <summary>
        /// The next interface point occurs once per turn, prior to the first tick.  It counts as an action prior to any of the ticks occuring. In the
        /// reference bot implementation reference bot chooses to perform its scans during the turn intialise.  Scans can however be performed at any
        /// point during the turn.
        /// </summary>
        /// <param name="turn">The turn number that has just started.</param>
        protected override void DroneTurnStartPassive(int turn) {
           lastScan = (ScanEquipmentUseResult)UseEquipment("MyScanner");
        }

        /// <summary>
        /// The main interface point called from the egine is ActualTakeAction, which is called for each tick.  Each tick is called passing a LastTickRecord
        /// whcih describes what happened to the bot during the last tick.  During the first tick no events will be returned in the last tick record.  
        /// </summary>
        /// <param name="turn">The integer turn number - increments from 1 thorugh to the end of the battle</param>
        /// <param name="tick">The integer tick number 1 through to 10</param>
        /// <param name="tdr">A description of the events that occured last turn.</param>
        protected override void DroneTakeAction(int turn, int tick, LastTickRecord tdr) {

            // Reference Bot Always Accellerates to 5 Speed and drives around at that speed
            if (this.CurrentSpeed < 5) {
                Accelerate();
            }

            // A Point of interest indicates something that is not a wall or floor in the scan results.  This 
            // will be an object such as a BOT - in this instance if right turn bot finds another bot it will open fire.
            if (!disableWeapons) {

                // This bot disables its weapons when it runs out of ammunition.
                if (lastScan.NumberOfPOI > 0) {


                    BotWriteMessage("Point of interest found");

                    // Check Each Of the points of interest that were returned in the scan.
                    foreach (var v in lastScan.GetPointsOfInterest()) {

                        // Check whether the point of interest is a Bot, if it is then we can open fire at the bot.  
                        if (lastScan.GetResultAtPosition(v.ScanLocation) == ScanTileResult.Bot) {
                            BotWriteMessage("FIRING : " + v.POIIdentity.ToString());

                            var result = FireWeapon(v.POIIdentity, "Rifle");

                            // N.B. This will also fail for other reasons, for example if the weapon is on cooldown.
                            if (result.State == UsageEndState.Fail_NoAmmo) {
                                disableWeapons = true;
                            }
                        }
                    }
                }
            }

            // Find which direction you are heading in, from the bots Current Heading
            double nd = CurrentHeading;

            // This is a helper method that checks the scan results and sees whether there is a wall 
            // or blocker in the way.
            if (!DroneUtilities.IsThisDirectionClear(lastScan, nd)) {

                // DO NOT USE THIS METHOD
                //TestUtils.DumpScanResult(lastScan, turn, tick, this.Name);

                // This is where your navigation logic goes, this is where you write the code to determine
                // how your bot should navigate around.
                double newHeading = GetNewHeading(nd);

                if (newHeading != nd) {
                    ChangeHeading(newHeading);
                } else {
                    BotWriteMessage("No new heading found, stopping.");
                    Decelerate();
                }
            }
        }

        /// <summary>
        /// The constructor must take no parameters, but you should call InitialiseDetails to provide the name and version
        /// of your Bot.  If you do not do this then it will be given a default name and version.
        /// </summary>
        public Reference_Bot() : base("Reference Right Turn Bot", "1.0") {            
        }

        // From here down are implementation specific details.  You can delete all this and replace it with your own implementation detail.

        // Holds the results of the last scan operation
        private ScanEquipmentUseResult lastScan;
        // This is implementation specific, used by the logic in RTB to detemrin when to fire.
        private bool disableWeapons = false;
        // This is implementation specifc, use 
        

 

        /// <summary>
        /// This should be replaced by your own implementation
        /// </summary>        
        private double GetNewHeading(double nd) {
            int loopCount = 0;

            while (!DroneUtilities.IsThisDirectionClear(lastScan,nd)) {
                nd = nd + 90;
                nd = nd % 360;
                loopCount++;
            }
            return nd;
        }

        /// <summary>
        /// NOT SURE WHAT TO DO WTIH THIS
        /// </summary>        
        private void BotWriteMessage(string p) {
            //Bilge.Warning("BOT - " + this.Name + " : " + p);
        }


    }
}
