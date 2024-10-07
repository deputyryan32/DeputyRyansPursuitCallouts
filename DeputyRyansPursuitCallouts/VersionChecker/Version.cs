using Rage;
using System;
using System.Net;

namespace DeputyRyansPursuitCallouts.VersionChecker
{
    public class PluginCheck
    {
        // Check for updates
        public static bool IsUpdateAvailable()
        {
            string curVersion = "Beta 3"; // Current version
            Uri latestVersionUri = new Uri("https://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=48930&textOnly=1"); // URL to store the current version
            WebClient webClient = new WebClient();
            string receivedData = string.Empty;

            try
            {
                receivedData = webClient.DownloadString(latestVersionUri).Trim();
            }
            catch (WebException)
            {
                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "~w~DeputyRyansPursuitCallouts Warning", "~r~Failed to check for an update", "Please ensure you are ~y~connected~w~ to the internet or try to ~y~reload~w~ the plugin.");
                Game.Console.Print();
                Game.Console.Print("================================================== DeputyRyansPursuitCallouts ===================================================");
                Game.Console.Print();
                Game.Console.Print("[WARNING]: Failed to check for an update.");
                Game.Console.Print("[LOG]: Please ensure you are connected to the internet or try to reload the plugin.");
                Game.Console.Print();
                Game.Console.Print("================================================== DeputyRyansPursuitCallouts ===================================================");
                Game.Console.Print();
                return false;
            }

            if (receivedData != curVersion)
            {
                Game.DisplayNotification("commonmenu", "mp_alerttriangle", "~w~DeputyRyansPursuitCallouts Warning", "~y~A new Update is available!", "Current Version: ~r~" + curVersion + "~w~<br>New Version: ~o~" + receivedData + "<br>~r~Please update to the latest build!");
                Game.Console.Print();
                Game.Console.Print("================================================== DeputyRyansPursuitCallouts ===================================================");
                Game.Console.Print();
                Game.Console.Print("[WARNING]: A new version of DeputyRyansPursuitCallouts is available! Update to the latest build or play at your own risk.");
                Game.Console.Print("[LOG]: Current Version:  " + curVersion);
                Game.Console.Print("[LOG]: New Version:  " + receivedData);
                Game.Console.Print();
                Game.Console.Print("================================================== DeputyRyansPursuitCallouts ===================================================");
                Game.Console.Print();
                return true;
            }
            else
            {
                Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~DeputyRyansPursuitCallouts", "", "Detected the ~g~latest~w~ build of ~y~DeputyRyansPursuitCallouts~w~!");
                return false;
            }
        }
    }
}
