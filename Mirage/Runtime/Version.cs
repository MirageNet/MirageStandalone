namespace Mirage
{
    public static class Version
    {
        public static readonly string Current = typeof(NetworkPlayer).Assembly.GetName().Version.ToString();
    }
}
