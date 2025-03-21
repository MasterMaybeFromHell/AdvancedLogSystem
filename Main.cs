using Il2Cpp;
using MelonLoader;
using MasterHell.Config;

[assembly: MelonInfo(typeof(AdvancedLogSystem.Main), "AdvancedLogSystem", "1.5.0", "MasterHell", null)]
[assembly: MelonGame("ZeoWorks", "Slendytubbies 3")]
[assembly: MelonColor(255, 255, 165, 0)]

namespace AdvancedLogSystem
{
    public class Main : MelonMod
    {
        private ConfigManager _configManager = new();
        public static Config Config;
        public const string CONFIG_PATH = "UserData\\AdvancedLogSystem\\Config.json";
        private const string CUSTOM_SCRIPT_PATH = "UserData\\AdvancedLogSystem\\CustomScripts";

        public override void OnInitializeMelon()
        {
            SetupFolder();
            AdvancedLogger.DeleteAllLogFiles();
            Config = _configManager.Load(CONFIG_PATH);
            CustomCodeLoader.LoadAndExecuteScripts(CUSTOM_SCRIPT_PATH);
            AdvancedLogger.LogMessage("[Mod Initialized]");
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName != "MainMenu" && sceneName != "Updater" && sceneName != "Banned")
            {
                AdvancedLogger.LogMessage
                (
                    $"[Room] Server Name: { AdvancedLogger.Translit(PhotonNetwork.room.Name) }, " +
                    $"Game Mode: { PhotonNetwork.room.customProperties["GM001'"].ToString() }, " +
                    $"Map: { PhotonNetwork.room.customProperties["MN002'"].ToString() }."
                );
                
                AdvancedLogger.LogMessage
                (
                    $"[Room] Master Client Nick: { AdvancedLogger.Translit(PhotonNetwork.masterClient.NickName) }, " +
                    $"Server Open: { PhotonNetwork.room.IsOpen }, " +
                    $"Server Visible: { PhotonNetwork.room.IsVisible }."
                );
            }

            AdvancedLogger.LogMessage($"[Map] Scene ID: { buildIndex }, Scene Name: { sceneName }.");

            Config = _configManager.Load(CONFIG_PATH);
        }

        private void SetupFolder()
        {
            if (!Directory.Exists("UserData\\AdvancedLogSystem"))
                Directory.CreateDirectory("UserData\\AdvancedLogSystem");
        }
    }
}