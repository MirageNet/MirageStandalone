using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine
{
    public class Object
    {
        public static T Instantiate<T>(T target) where T : Object
        {
            throw new NotSupportedException();
        }
        public static T Instantiate<T>(T target, Vector3 position, Quaternion rotation) where T : Object
        {
            throw new NotSupportedException();
        }
        [Obsolete("Un-used")]
        public static void DontDestroyOnLoad(Object target) { }

        public static void Destroy(Object target)
        {
            AllObjects.Instance.all.Remove(target.InstanceID);
            target.Destroyed = true;
            if (target.hasAwoke)
                target.Invoke("OnDestroy");
        }

        public string name { get; set; } = "Object";

        internal bool hasAwoke;
        internal bool Destroyed;
        internal int InstanceID;

        internal Object() { }

        public override string ToString()
        {
            if (Destroyed)
                return "<NULL>";
            else
                return name;
        }

        internal virtual void Wake()
        {
            Invoke("Awake");
            hasAwoke = true;
        }
        internal void Invoke(string name)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo method = GetType().GetMethod(name, flags);
            if (method != null)
                method.Invoke(this, Array.Empty<object>());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Object other)
            {
                // false if only 1 is destroyed
                if (other.Destroyed != Destroyed)
                {
                    return false;
                }

                // true if both destroyed
                // todo is this correct?
                if (other.Destroyed && Destroyed)
                {
                    return true;
                }

                // if neither destroyed, then just check reference
                return ReferenceEquals(this, other);
            }
            else
            {
                // if other is not Object,
                //   then true if obj is null and this is destroyed
                return obj == null && Destroyed;
            }
        }
        public static bool operator ==(Object a, Object b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Object a, Object b)
        {
            return !a.Equals(b);
        }
        public static implicit operator bool(Object a)
        {
            return !a.Destroyed;
        }
    }

    internal class AllObjects
    {
        static AllObjects _instance;
        public static AllObjects Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AllObjects();
                return _instance;
            }
        }

        public Dictionary<int, Object> all;
    }
}
