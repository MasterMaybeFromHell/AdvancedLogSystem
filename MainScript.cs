using System;
using System.IO;
using System.Collections;
using MelonLoader;
using UnityEngine;
using HarmonyLib;
using UnhollowerBaseLib;

namespace AdvancedLogSystem
{
    public class MainScript : MelonMod
    {
        public override void OnInitializeMelon()
        {
            SetupMod();
            Script.DeleteAllLogs();
            Script.Msg("[Mod Initialized]");
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName != "MainMenu" && sceneName != "Updater" && sceneName != "Banned")
            {
                Script.Msg
                (
                    $"[Room] Server Name: {Script.Translit(PhotonNetwork.room.Name)}, " +
                    $"Game Mode: {PhotonNetwork.room.customProperties["GM001'"].ToString()}, " +
                    $"Map: {PhotonNetwork.room.customProperties["MN002'"].ToString()}."
                );
                Script.Msg
                (
                    $"[Room] Master Client Nick: {Script.Translit(PhotonNetwork.masterClient.NickName)}, " +
                    $"Server Open: {PhotonNetwork.room.IsOpen}, " +
                    $"Server Visible: {PhotonNetwork.room.IsVisible}."
                );
            }
            Script.Msg($"[Map] Scene ID: {buildIndex}, Scene Name: {sceneName}.");
        }

        private void SetupMod()
        {
            if (!Directory.Exists("UserData\\AdvancedLogSystem"))
            {
                Directory.CreateDirectory("UserData\\AdvancedLogSystem");
            }
        }
    }

    public class Script
    {
        public static int _numberOfLog = 1;
        public static string _currentLog = "log.txt";
        public static bool _isOn = false;

        public static void Msg(string msg)
        {
            MelonLogger.Msg(msg);
            WriteData(msg);
        }

        public static void WriteData(string text)
        {
            using (StreamWriter streamWriter = new StreamWriter($"UserData\\AdvancedLogSystem\\{_currentLog}", true))
            {
                streamWriter.WriteLine(text);
                streamWriter.Close();
            }
        }

        public static void NewLog()
        {
            _numberOfLog++;
            _currentLog = $"log{_numberOfLog}.txt";
        }

        public static void DeleteAllLogs()
        {
            if (Directory.Exists("UserData\\AdvancedLogSystem"))
            {
                foreach (string text in Directory.GetFiles("UserData\\AdvancedLogSystem\\"))
                {
                    File.Delete(text);
                }
            }
        }

        //Translit by Vantablack
        public static string Translit(string str)
        {
            string[] array = new string[]
            {
                "A",
                "B",
                "V",
                "G",
                "D",
                "E",
                "Yo",
                "Zh",
                "Z",
                "I",
                "Y",
                "K",
                "L",
                "M",
                "N",
                "O",
                "P",
                "R",
                "S",
                "T",
                "U",
                "F",
                "Kh",
                "Ts",
                "Ch",
                "Sh",
                "Shch",
                "\"",
                "Y",
                "'",
                "E",
                "Yu",
                "Ya"
            };
            string[] array2 = new string[]
            {
                "a",
                "b",
                "v",
                "g",
                "d",
                "e",
                "yo",
                "zh",
                "z",
                "i",
                "y",
                "k",
                "l",
                "m",
                "n",
                "o",
                "p",
                "r",
                "s",
                "t",
                "u",
                "f",
                "kh",
                "ts",
                "ch",
                "sh",
                "shch",
                "\"",
                "y",
                "'",
                "e",
                "yu",
                "ya"
            };
            string[] array3 = new string[]
            {
                "А",
                "Б",
                "В",
                "Г",
                "Д",
                "Е",
                "Ё",
                "Ж",
                "З",
                "И",
                "Й",
                "К",
                "Л",
                "М",
                "Н",
                "О",
                "П",
                "Р",
                "С",
                "Т",
                "У",
                "Ф",
                "Х",
                "Ц",
                "Ч",
                "Ш",
                "Щ",
                "Ъ",
                "Ы",
                "Ь",
                "Э",
                "Ю",
                "Я"
            };
            string[] array4 = new string[]
            {
                "а",
                "б",
                "в",
                "г",
                "д",
                "е",
                "ё",
                "ж",
                "з",
                "и",
                "й",
                "к",
                "л",
                "м",
                "н",
                "о",
                "п",
                "р",
                "с",
                "т",
                "у",
                "ф",
                "х",
                "ц",
                "ч",
                "ш",
                "щ",
                "ъ",
                "ы",
                "ь",
                "э",
                "ю",
                "я"
            };
            for (int i = 0; i <= 32; i++)
            {
                str = str.Replace(array3[i], array[i]);
                str = str.Replace(array4[i], array2[i]);
            }
            return str;
        }

        public static IEnumerator WaitBefore(float time, Action action)
        {
            if (_isOn)
            { yield break; }
            _isOn = true;
            yield return new WaitForSeconds(time);
            action();
            _isOn = false;
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "NOOU", new Type[]
    {
        typeof(string),
        typeof(Vector3),
        typeof(Quaternion),
        typeof(byte)
    })]
    public static class SpawnPatch2
    {
        [HarmonyPrefix]
        private static void Prefix(string prefabName, Vector3 position, Quaternion rotation)
        {
            Script.Msg
            (
                $"[Spawn] Instantiated: {prefabName}, " +
                $"Position: {new Vector3(position.x, position.y, position.z).ToString()}, " +
                $"Rotation: {new Vector3(rotation.x, rotation.y, rotation.z).ToString()}."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "NOOU2")]
    public static class SpawnPatch
    {
        [HarmonyPrefix]
        private static void Prefix(string prefabName, Vector3 position, Quaternion rotation)
        {
            Script.Msg
            (
                $"[Spawn] Instantiated: {prefabName}, " +
                $"Position: {new Vector3(position.x, position.y, position.z).ToString()}, " +
                $"Rotation: {new Vector3(rotation.x, rotation.y, rotation.z).ToString()}."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "Destroy", new Type[]
    {
        typeof(PhotonView),
    })]
    public static class DestroyPatch
    {
        [HarmonyPrefix]
        private static void Prefix(PhotonView targetView)
        {
            Script.Msg($"[Destroy] Destroyed: {Script.Translit(targetView.name)}.");
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "Destroy", new Type[]
{
        typeof(GameObject),
})]
    public static class DestroyPatch2
    {
        [HarmonyPrefix]
        private static void Prefix(GameObject targetGo)
        {
            Script.Msg($"[Destroy] Destroyed: {Script.Translit(targetGo.name)}.");
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "DestroyAll")]
    public static class DestroyAllPatch
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            Script.Msg($"[Destroy] Destroy All: Been Used.");
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "DestroyPlayerObjects", new Type[]
    {
        typeof(PhotonPlayer),
    })]
    public static class DestroyPlayerObjectsPatch
    {
        [HarmonyPrefix]
        private static void Prefix(PhotonPlayer targetPlayer)
        {
            MelonCoroutines.Start(Script.WaitBefore(3f, 
            delegate 
            { Script.Msg($"[Destroy] Destroyed Player: {Script.Translit(targetPlayer.name)}."); }));
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "RPC", new Type[]
    {
        typeof(PhotonView),
        typeof(string),
        typeof(PhotonTargets),
        typeof(bool),
        typeof(Il2CppReferenceArray<Il2CppSystem.Object>),
    })]
    public static class PhotonNetworkRPCPatch
    {
        [HarmonyPrefix]
        private static void Prefix(PhotonView view, string methodName, PhotonTargets target,
        bool encrypt, Il2CppReferenceArray<Il2CppSystem.Object> parameters)
        {
            MelonCoroutines.Start(Script.WaitBefore(1f, 
            delegate 
            {
                Script.Msg
                (
                    $"[RPC] PhotonView: {Script.Translit(view.name)}, " +
                    $"RPC Called: {methodName}, " +
                    $"Target: {target}."
                );
            }));
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "RPC", new Type[]
    {
        typeof(PhotonView),
        typeof(string),
        typeof(PhotonPlayer),
        typeof(bool),
        typeof(Il2CppReferenceArray<Il2CppSystem.Object>),
    })]
    public static class PhotonNetworkRPCPatch2
    {
        [HarmonyPrefix]
        private static void Prefix(PhotonView view, string methodName, PhotonPlayer targetPlayer,
        bool encrpyt, Il2CppReferenceArray<Il2CppSystem.Object> parameters)
        {
            MelonCoroutines.Start(Script.WaitBefore(1f,
            delegate
            {
                Script.Msg
                (
                    $"[RPC] PhotonView: {Script.Translit(view.name)}, " +
                    $"RPC Called: {methodName}, " +
                    $"Target: {Script.Translit(targetPlayer.name)}."
                );
            }));
        }
    }


    [HarmonyPatch(typeof(PhotonNetwork), "RaiseEvent")]
    public static class RaiseEventPatch
    {
        [HarmonyPrefix]
        private static void Prefix(byte eventCode, Il2CppSystem.Object eventContent,
        bool sendReliable, RaiseEventOptions options)
        {
            Script.Msg
            (
                $"[RPC] Event Code: {eventCode}, " +
                $"Event Content:  {eventCode}."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonView), "RPC", new Type[]
    {
        typeof(string),
        typeof(PhotonPlayer),
        typeof(Il2CppReferenceArray<Il2CppSystem.Object>),
    })]
    public static class RPCPatch
    {
        [HarmonyPrefix]
        private static void Prefix(string methodName, PhotonPlayer targetPlayer, 
        Il2CppReferenceArray<Il2CppSystem.Object> parameters)
        {
            Script.Msg
            (
                $"[RPC] RPC Called: {methodName}, " +
                $"Target: {Script.Translit(targetPlayer.name)}."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonView), "RPC", new Type[]
    {
        typeof(string),
        typeof(PhotonTargets),
        typeof(Il2CppReferenceArray<Il2CppSystem.Object>),
    })]
    public static class RPCPatch2
    {
        [HarmonyPrefix]
        private static void Prefix(string methodName, PhotonTargets target,
        Il2CppReferenceArray<Il2CppSystem.Object> parameters)
        {
            Script.Msg
            (
                $"[RPC] RPC Called: {methodName}, " +
                $"Target: {target}."
            );
        }
    }

    [HarmonyPatch(typeof(MultiplayerChat), "HLIDELGJEON")]
    public static class MultiplayerChatPatch
    {
        [HarmonyPrefix]
        private static void Prefix(string NAEFFPHCJKL, string NOFLIGCKLDF, 
        string PBPBALMOMEM, MultiplayerChat __instance)
        {
            Script.Msg($"[Chat] Chat Msg:{Script.Translit(NAEFFPHCJKL)}{Script.Translit(NOFLIGCKLDF)}");
        }
    }

    [HarmonyPatch(typeof(WhoKilledWho), "OnPhotonPlayerConnected")]
    public static class OnPhotonPlayerConnected
    {
        [HarmonyPrefix]
        private static void Prefix(PhotonPlayer otherPlayer, WhoKilledWho __instance)
        {
            Script.Msg($"[Room] Player Join: {Script.Translit(otherPlayer.name)}.");
        }
    }

    [HarmonyPatch(typeof(WhoKilledWho), "OnPhotonPlayerDisconnected")]
    public static class OnPhotonPlayerDisconnected
    {
        [HarmonyPrefix]
        private static void Prefix(PhotonPlayer ANLAKOKFGCJ, WhoKilledWho __instance)
        {
            Script.Msg($"[Room] Player Left: {Script.Translit(ANLAKOKFGCJ.name)}.");
        }
    }


    [HarmonyPatch(typeof(RoomMultiplayerMenu), "OnLeftRoom")]
    public static class RMMPatch
    {
        [HarmonyPrefix]
        private static void Prefix(RoomMultiplayerMenu __instance)
        {
            Script.Msg($"[Room] Player Left: You.");
            Script.NewLog();
        }
    }

    [HarmonyPatch(typeof(WhoKilledWho), "networkAddMessage")]
    public static class WKWPatch
    {
        [HarmonyPrefix]
        private static void Prefix(string killer, string killed, string middleText, string teamName)
        {
            Script.Msg
            (
                $"[WKW] Killer: {Script.Translit(killer)}, " +
                $"Killed: {Script.Translit(killed)}, " + 
                $"Middle Text: {Script.Translit(middleText)}, " +
                $"Team Name: {teamName}."
            );
        }
    }
}
