namespace UnityEngine
{
    public static class Random
    {
        static readonly System.Random random = new System.Random();

        public static int Range(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
