﻿using CalloutInterfaceAPI;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using LSPD_First_Response.Mod.API;

namespace DeputyRyansPursuitCallouts.Callouts
{
    [CalloutInterface("Vehicle Theft Pursuit", CalloutProbability.High, "A suspect is fleeing after stealing a vehicle.", "Code 3", "LSPD")]
    public class VehicleTheftPursuit2 : Callout
    {
        private Vector3 spawnPoint;
        private Ped suspect;
        private Vehicle stolenVehicle;
        private Blip suspectBlip;
        private LHandle pursuit;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(600f));
            stolenVehicle = new Vehicle("SULTAN", spawnPoint);

            if (!stolenVehicle.Exists())
                return false;

            suspect = stolenVehicle.CreateRandomDriver();

            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 35f);
            AddMinimumDistanceCheck(50f, spawnPoint);

            CalloutMessage = "Vehicle Theft Pursuit";
            CalloutPosition = spawnPoint;
            LSPD_First_Response.Mod.API.Functions.PlayScannerAudio("CRIME_VEHICLE_THEFT_01");

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            suspectBlip = stolenVehicle.AttachBlip();
            suspectBlip.IsFriendly = false;

            suspect.Tasks.CruiseWithVehicle(stolenVehicle, 100f, VehicleDrivingFlags.Emergency);

            CalloutInterfaceAPI.Functions.SendMessage(this, "Officer, a suspect has stolen a vehicle and is fleeing the scene. Pursue with caution.");

            pursuit = LSPD_First_Response.Mod.API.Functions.CreatePursuit();
            LSPD_First_Response.Mod.API.Functions.AddPedToPursuit(pursuit, suspect);
            LSPD_First_Response.Mod.API.Functions.SetPursuitIsActiveForPlayer(pursuit, true);

            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            base.Process();

            if (!suspect.IsAlive || LSPD_First_Response.Mod.API.Functions.IsPedArrested(suspect))
            {
                End();
            }
        }

        public override void End()
        {
            base.End();

            if (suspectBlip.Exists()) suspectBlip.Delete();
            if (suspect.Exists()) suspect.Dismiss();
            if (stolenVehicle.Exists()) stolenVehicle.Dismiss();
            Game.LogTrivial("VehicleTheftPursuit callout has ended.");
        }
    }
}
