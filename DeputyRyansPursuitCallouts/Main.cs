using System;
using LSPD_First_Response.Mod.API;
using Rage;
using DeputyRyansPursuitCallouts.Callouts;

namespace DeputyRyansPursuitCallouts
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            Functions.OnOnDutyStateChanged += OnDutyStateChanged;
            Game.LogTrivial("DeputyRyansPursuitCallouts has been initialized.");

            Game.DisplayNotification("3dtextures", "mpgroundlogo_cops", "DeputyRyansPursuitCallouts", "~g~Callouts Initialized", "All callouts have been successfully loaded.");
        }

        private void OnDutyStateChanged(bool onDuty)
        {
            if (onDuty)
            {
                Functions.RegisterCallout(typeof(ArmedRobberyGetaway));
                Functions.RegisterCallout(typeof(DrunkDriverPursuit));
                Functions.RegisterCallout(typeof(HitAndRunPursuit));
                Functions.RegisterCallout(typeof(LargeVehiclePursuit));
                Functions.RegisterCallout(typeof(LargeVehiclePursuit2));
                Functions.RegisterCallout(typeof(PursuitInProgress));
                Functions.RegisterCallout(typeof(RecklessDriverPursuit));
                Functions.RegisterCallout(typeof(RoadRagePursuit));
                Functions.RegisterCallout(typeof(StolenVehiclePursuit));
                Functions.RegisterCallout(typeof(VehicleTheftPursuit));
                Functions.RegisterCallout(typeof(VehicleTheftPursuit2));
            }
        }

        public override void Finally()
        {
            Game.LogTrivial("DeputyRyansPursuitCallouts has been cleaned up.");
        }
    }
}
