namespace Mirage
{
    /// <summary>
    /// Static methods to bypass internal
    /// <para>
    /// this class only exists so that we can have the same NetworkServer class in main repo
    /// </para>
    /// </summary>
    public static class Updater
    {
        public static void Update(this NetworkServer server) => server.Update();
        public static void Update(this NetworkClient client) => client.Update();
    }
}
