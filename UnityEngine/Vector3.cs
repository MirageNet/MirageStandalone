namespace UnityEngine
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;
        public object Value { get; set; }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            Value = null;
        }
    }
}
