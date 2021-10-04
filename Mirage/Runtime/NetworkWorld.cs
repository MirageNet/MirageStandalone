using Mirage.Logging;
using UnityEngine;

namespace Mirage
{
    public class NetworkWorld
    {
        static readonly ILogger logger = LogFactory.GetLogger<NetworkWorld>();

        /// <summary>
        /// Time kept in this world
        /// </summary>
        public NetworkTime Time { get; } = new NetworkTime();

        public NetworkWorld()
        {

        }
    }
}
