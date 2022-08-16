using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AddComponentMenuAttribute : Attribute { public AddComponentMenuAttribute(string _) { } }

    [AttributeUsage(AttributeTargets.Class)]
    public class DisallowMultipleComponentAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class HeaderAttribute : Attribute { public HeaderAttribute(string _) { } }

    [AttributeUsage(AttributeTargets.Class)]
    public class HelpURLAttribute : Attribute { public HelpURLAttribute(string _) { } }

    [AttributeUsage(AttributeTargets.Field)]
    public class MinAttribute : Attribute { public MinAttribute(float _) { } }

    public abstract class PropertyAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireComponentAttribute : Attribute { public RequireComponentAttribute(Type _) { } }

    [AttributeUsage(AttributeTargets.Method)]
    public class RuntimeInitializeOnLoadMethodAttribute : Attribute { public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType _) { } }

    [AttributeUsage(AttributeTargets.Field)]
    public class SerializeFieldAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class TooltipAttribute : Attribute { public TooltipAttribute(string _) { } }

    [AttributeUsage(AttributeTargets.Field)]
    public class HideInInspector : Attribute { }

    [AttributeUsage(AttributeTargets.Field)]
    public class RangeAttribute : Attribute { public RangeAttribute(float _, float __) { } }
}

namespace UnityEngine.Serialization
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class FormerlySerializedAsAttribute : Attribute { public FormerlySerializedAsAttribute(string _) { } }
}

namespace UnityEditor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InitializeOnLoadMethodAttribute : Attribute { }
}
