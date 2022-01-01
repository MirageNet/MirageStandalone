namespace UnityEngine
{
    public struct Rect
    {
        public float xMin;
        public float yMin;
        public float width;
        public float height;

        public Rect(float xMin, float yMin, float width, float height)
        {
            this.xMin = xMin;
            this.yMin = yMin;
            this.width = width;
            this.height = height;
        }
    }
}
