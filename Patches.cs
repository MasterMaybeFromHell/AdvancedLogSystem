using HarmonyLib;
using Il2Cpp;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace AdvancedLogSystem
{
    [HarmonyPatch(typeof(PhotonNetwork), "NOOU2")]
    public static class SpawnPatch
    {
        [HarmonyPostfix]
        private static void Postfix(string prefabName, Vector3 position, Quaternion rotation)
        {
            if (!Main.Config.SpawnLog) return;

            AdvancedLogger.LogMessage
            (
                $"[Spawn] Instantiated: { prefabName }, " +
                $"Position: { new Vector3(position.x, position.y, position.z) }, " +
                $"Rotation: { new Vector3(rotation.x, rotation.y, rotation.z) }."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "NOOU",
    [
        typeof(string),
        typeof(Vector3),
        typeof(Quaternion),
        typeof(byte)
    ])]
    public static class SpawnPatch2
    {
        [HarmonyPostfix]
        private static void Postfix(string prefabName, Vector3 position, Quaternion rotation)
        {
            if (!Main.Config.SpawnLog) return;

            AdvancedLogger.LogMessage
            (
                $"[Spawn] Instantiated: { prefabName }, " +
                $"Position: { new Vector3(position.x, position.y, position.z) }, " +
                $"Rotation: { new Vector3(rotation.x, rotation.y, rotation.z) }."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "Destroy",
    [
        typeof(PhotonView),
    ])]
    public static class DestroyPatch
    {
        [HarmonyPostfix]
        private static void Postfix(PhotonView targetView)
        {
            if (!Main.Config.DestroyLog) return;

            AdvancedLogger.LogMessage
            (
                $"[Destroy] Destroyed: { AdvancedLogger.Translit(targetView.name) }."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "Destroy",
    [
        typeof(GameObject),
    ])]
    public static class DestroyPatch2
    {
        [HarmonyPostfix]
        private static void Postfix(GameObject targetGo)
        {
            if (!Main.Config.DestroyLog) return;

            AdvancedLogger.LogMessage
            (
                $"[Destroy] Destroyed: { AdvancedLogger.Translit(targetGo.name) }."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "DestroyAll")]
    public static class DestroyAllPatch
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            if (!Main.Config.DestroyLog) return;

            AdvancedLogger.LogMessage
            (
                $"[Destroy] Destroy All: Been Used."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "DestroyPlayerObjects",
    [
        typeof(PhotonPlayer),
    ])]
    public static class DestroyPlayerObjectsPatch
    {
        [HarmonyPostfix]
        private static void Postfix(PhotonPlayer targetPlayer)
        {
            if (!Main.Config.DestroyLog) return;

            AdvancedLogger.LogMessage
            (
                $"[Destroy] Destroyed Player: { AdvancedLogger.Translit(targetPlayer.name) }."
            ); 
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "RPC",
    [
        typeof(PhotonView),
        typeof(string),
        typeof(PhotonTargets),
        typeof(bool),
        typeof(Il2CppReferenceArray<Il2CppSystem.Object>),
    ])]
    public static class PhotonNetworkRPCPatch
    {
        [HarmonyPostfix]
        private static void Postfix(PhotonView view, string methodName, PhotonTargets target,
            bool encrypt, Il2CppReferenceArray<Il2CppSystem.Object> parameters)
        {
            if (!Main.Config.RPCLog) return;

            AdvancedLogger.LogMessage
            (
                $"[RPC] PhotonView: { AdvancedLogger.Translit(view.name) }, " +
                $"RPC Called: { methodName }, " +
                $"Target: { target }."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), "RPC",
    [
        typeof(PhotonView),
        typeof(string),
        typeof(PhotonPlayer),
        typeof(bool),
        typeof(Il2CppReferenceArray<Il2CppSystem.Object>),
    ])]
    public static class PhotonNetworkRPCPatch2
    {
        [HarmonyPostfix]
        private static void Postfix(PhotonView view, string methodName, PhotonPlayer targetPlayer,
            bool encrpyt, Il2CppReferenceArray<Il2CppSystem.Object> parameters)
        {
            if (!Main.Config.RPCLog) return;

            AdvancedLogger.LogMessage
            (
                $"[RPC] PhotonView: { AdvancedLogger.Translit(view.name) }, " +
                $"RPC Called: { methodName }, " +
                $"Target: { AdvancedLogger.Translit(targetPlayer.name) }."
            );
        }
    }


    [HarmonyPatch(typeof(PhotonNetwork), "RaiseEvent")]
    public static class RaiseEventPatch
    {
        [HarmonyPostfix]
        private static void Postfix(byte eventCode, Il2CppSystem.Object eventContent,
            bool sendReliable, RaiseEventOptions options)
        {
            if (!Main.Config.RPCLog) return;

            AdvancedLogger.LogMessage
            (
                $"[RPC] Event Code: { eventCode }, " +
                $"Event Content:  { eventCode }."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonView), "RPC",
    [
        typeof(string),
        typeof(PhotonPlayer),
        typeof(Il2CppReferenceArray<Il2CppSystem.Object>),
    ])]
    public static class RPCPatch
    {
        [HarmonyPostfix]
        private static void Postfix(string methodName, PhotonPlayer targetPlayer,
            Il2CppReferenceArray<Il2CppSystem.Object> parameters)
        {
            if (!Main.Config.RPCLog) return;

            AdvancedLogger.LogMessage
            (
                $"[RPC] RPC Called: { methodName }, " +
                $"Target: { AdvancedLogger.Translit(targetPlayer.name) }."
            );
        }
    }

    [HarmonyPatch(typeof(PhotonView), "RPC",
    [
        typeof(string),
        typeof(PhotonTargets),
        typeof(Il2CppReferenceArray<Il2CppSystem.Object>),
    ])]
    public static class RPCPatch2
    {
        [HarmonyPostfix]
        private static void Postfix(string methodName, PhotonTargets target,
            Il2CppReferenceArray<Il2CppSystem.Object> parameters)
        {
            if (!Main.Config.RPCLog) return;

            AdvancedLogger.LogMessage
            (
                $"[RPC] RPC Called: { methodName }, " +
                $"Target: { target }."
            );
        }
    }

    [HarmonyPatch(typeof(MultiplayerChat), "HLIDELGJEON")]
    public static class MultiplayerChatPatch
    {
        [HarmonyPostfix]
        private static void Postfix(string NAEFFPHCJKL, string NOFLIGCKLDF,
            string PBPBALMOMEM, MultiplayerChat __instance)
        {
            if (!Main.Config.ChatLog) return;

            AdvancedLogger.LogMessage
            (
                $"[Chat] Chat Msg: { AdvancedLogger.Translit(NAEFFPHCJKL) }" +
                $"{ AdvancedLogger.Translit(NOFLIGCKLDF) }"
            );
        }
    }

    [HarmonyPatch(typeof(WhoKilledWho), "OnPhotonPlayerConnected")]
    public static class OnPhotonPlayerConnected
    {
        [HarmonyPostfix]
        private static void Postfix(PhotonPlayer otherPlayer, WhoKilledWho __instance)
        {
            if (!Main.Config.OnPlayerConnectLog) return;

            AdvancedLogger.LogMessage
            (
                $"[Room] Player Join: { AdvancedLogger.Translit(otherPlayer.name) }."
            );
        }
    }

    [HarmonyPatch(typeof(WhoKilledWho), "OnPhotonPlayerDisconnected")]
    public static class OnPhotonPlayerDisconnected
    {
        [HarmonyPostfix]
        private static void Postfix(PhotonPlayer ANLAKOKFGCJ, WhoKilledWho __instance)
        {
            if (!Main.Config.OnPlayerDisconnectLog) return;

            AdvancedLogger.LogMessage
            (
                $"[Room] Player Left: { AdvancedLogger.Translit(ANLAKOKFGCJ.name) }."
            );
        }
    }


    [HarmonyPatch(typeof(RoomMultiplayerMenu), "OnLeftRoom")]
    public static class RMMPatch
    {
        [HarmonyPostfix]
        private static void Postfix(RoomMultiplayerMenu __instance)
        {
            if (!Main.Config.OnPlayerDisconnectLog) return;

            AdvancedLogger.LogMessage($"[Room] Player Left: You.");
            AdvancedLogger.CreateNewLogFile();
        }
    }

    [HarmonyPatch(typeof(WhoKilledWho), "networkAddMessage")]
    public static class WKWPatch
    {
        [HarmonyPostfix]
        private static void Postfix(string killer, string killed, string middleText, string teamName)
        {
            if (!Main.Config.WKWLog) return;

            AdvancedLogger.LogMessage
            (
                $"[WKW] Killer: { AdvancedLogger.Translit(killer) }, " +
                $"Killed: { AdvancedLogger.Translit(killed) }, " +
                $"Middle Text: { AdvancedLogger.Translit(middleText) }, " +
                $"Team Name: { teamName }."
            );
        }
    }
}
