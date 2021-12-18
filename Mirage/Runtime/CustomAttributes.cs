using System;
using UnityEngine;

namespace Mirage
{
    /// <summary>
    /// Tell the weaver to generate  reader and writer for a class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class NetworkMessageAttribute : Attribute
    {
    }

    /// <summary>
    /// Converts a string property into a Scene property in the inspector
    /// </summary>
    public sealed class SceneAttribute : PropertyAttribute { }

    /// <summary>
    /// Used to show private SyncList in the inspector,
    /// <para> Use instead of SerializeField for non Serializable types </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ShowInInspectorAttribute : Attribute { }

    /// <summary>
    /// Draws UnityEvent as a foldout
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FoldoutEventAttribute : PropertyAttribute { }
}
