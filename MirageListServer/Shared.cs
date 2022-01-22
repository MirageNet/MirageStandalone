using System.Collections.Generic;
using Mirage.Serialization;

namespace Mirage.ListServer
{
    public static class Extensions
    {
        public static void WriteStringDictionary(this NetworkWriter writer, Dictionary<string, string> keyValuePairs)
        {
            WriteReadOnlyStringDictionary(writer, keyValuePairs);
        }

        public static Dictionary<string, string> ReadStringDictionary(this NetworkReader reader)
        {
            if (reader.ReadBoolean())
            {
                int count = reader.ReadPackedInt32();
                var keyValuePairs = new Dictionary<string, string>(capacity: count);
                for (int i = 0; i < count; i++)
                {
                    string key = reader.ReadString();
                    string value = reader.ReadString();
                    keyValuePairs.Add(key, value);
                }
                return keyValuePairs;
            }
            else
            {
                return null;
            }
        }

        public static void WriteReadOnlyStringDictionary(this NetworkWriter writer, IReadOnlyDictionary<string, string> keyValuePairs)
        {
            if (keyValuePairs == null)
            {
                writer.WriteBoolean(false);
            }
            else
            {
                writer.WriteBoolean(true);
                writer.WritePackedInt32(keyValuePairs.Count);
                foreach (KeyValuePair<string, string> kvp in keyValuePairs)
                {
                    writer.WriteString(kvp.Key);
                    writer.WriteString(kvp.Value);
                }
            }
        }

        public static IReadOnlyDictionary<string, string> ReadReadOnlyStringDictionary(this NetworkReader reader)
        {
            return ReadStringDictionary(reader);
        }
    }

    /// <summary>
    /// Request to be added to list
    /// </summary>
    [NetworkMessage]
    struct AddServer
    {
        public string DisplayName;
        [BitCount(16)] public int Port;
        public int PlayerCount;
        public int MaxPlayerCount;
        public Dictionary<string, string> ServerData;
    }

    /// <summary>
    /// Request to be added to list
    /// </summary>
    [NetworkMessage]
    struct UpdateServer
    {
        public string DisplayName;
        public int? PlayerCount;
        public int? MaxPlayerCount;
        public Dictionary<string, string> ServerData;
    }

    [NetworkMessage]
    struct KeepAlive
    {

    }

    [NetworkMessage]
    struct RemoveServer
    {
    }

    [NetworkMessage]
    struct GetServers
    {
    }

    [NetworkMessage]
    public struct GetServersReply
    {
        public Server[] servers;

        public struct Server
        {
            public string address;
            [BitCount(16)] public int port;

            public string DisplayName;
            public int PlayerCount;
            public int MaxPlayerCount;
            public IReadOnlyDictionary<string, string> ServerData;
        }
    }

    [NetworkMessage]
    struct Failed
    {
        public string MessageName;
        public string Reason;
    }
}
