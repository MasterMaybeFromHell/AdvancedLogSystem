namespace MasterHell.Config
{
    public struct Config
    {
        public Config()
        {
            SpawnLog = true;
            DestroyLog = true;
            RPCLog = true;
            ChatLog = true;
            OnPlayerDisconnectLog = true;
            OnPlayerConnectLog = true;
            WKWLog = true;
        }

        public bool SpawnLog { get; set; }
        public bool DestroyLog { get; set; }
        public bool RPCLog { get; set; }
        public bool ChatLog { get; set; }
        public bool OnPlayerDisconnectLog { get; set; }
        public bool OnPlayerConnectLog { get; set; }
        public bool WKWLog { get; set; }
    }
}