using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Mirror;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Axwabo.Helpers {

    /// <summary>
    /// Helper methods for Unity classes.
    /// </summary>
    public static class UnityHelper {

        #region Distance

        #region Squared distance

        /// <summary>
        /// Gets the squared distance between two <see cref="Vector3">vectors</see>.
        /// </summary>
        /// <param name="a">The source vector.</param>
        /// <param name="b">The destination vector.</param>
        /// <returns>The squared distance between the two.</returns>
        public static float DistanceSquared(this Vector3 a, Vector3 b) => (a - b).sqrMagnitude;

        /// <summary>
        /// Gets the squared distance between two <see cref="GameObject">GameObjects</see>.
        /// </summary>
        /// <param name="a">The source object.</param>
        /// <param name="b">The destination object.</param>
        /// <returns>The squared distance between the two.</returns>
        public static float DistanceSquared(this GameObject a, GameObject b) => (a.transform.position - b.transform.position).sqrMagnitude;

        /// <summary>
        /// Gets the squared distance between a <see cref="GameObject"/> and a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="a">The source object.</param>
        /// <param name="b">The destination vector.</param>
        /// <returns>The squared distance between the two.</returns>
        public static float DistanceSquared(this GameObject a, Vector3 b) => (a.transform.position - b).sqrMagnitude;

        /// <summary>
        /// Gets the squared distance between a <see cref="Vector3"/> and a <see cref="GameObject"/>.
        /// </summary>
        /// <param name="a">The source vector.</param>
        /// <param name="b">The destination object.</param>
        /// <returns>The squared distance between the two.</returns>
        public static float DistanceSquared(this Vector3 a, GameObject b) => (a - b.transform.position).sqrMagnitude;

        /// <summary>
        /// Gets the squared distance between the positions of two <see cref="Component">Components</see>.
        /// </summary>
        /// <param name="a">A component on the source object.</param>
        /// <param name="b">A component on the destination object.</param>
        /// <returns>The squared distance between the two.</returns>
        public static float DistanceSquared(this Component a, Component b) => (a.transform.position - b.transform.position).sqrMagnitude;

        /// <summary>
        /// Gets the squared distance between a <see cref="Component"/> and a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="a">A component on the source object.</param>
        /// <param name="b">The destination vector.</param>
        /// <returns>The squared distance between the two.</returns>
        public static float DistanceSquared(this Component a, Vector3 b) => (a.transform.position - b).sqrMagnitude;

        /// <summary>
        /// Gets the squared distance between a <see cref="Vector3"/> and a <see cref="Component"/>.
        /// </summary>
        /// <param name="a">The source vector.</param>
        /// <param name="b">A component on the destination object.</param>
        /// <returns>The squared distance between the two.</returns>
        public static float DistanceSquared(this Vector3 a, Component b) => (a - b.transform.position).sqrMagnitude;

        /// <summary>
        /// Gets the squared distance between two <see cref="Transform">Transforms</see>.
        /// </summary>
        /// <param name="a">The source transform.</param>
        /// <param name="b">The destination transform.</param>
        /// <returns>The squared distance between the two.</returns>
        public static float DistanceSquared(this Transform a, Transform b) => (a.position - b.position).sqrMagnitude;

        /// <summary>
        /// Gets the squared distance between a <see cref="Transform"/> and a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="a">The source transform.</param>
        /// <param name="b">The destination vector.</param>
        /// <returns>The squared distance between the two.</returns>
        public static float DistanceSquared(this Transform a, Vector3 b) => (a.position - b).sqrMagnitude;

        /// <summary>
        /// Gets the squared distance between a <see cref="Vector3"/> and a <see cref="Transform"/>.
        /// </summary>
        /// <param name="a">The source vector.</param>
        /// <param name="b">The destination transform.</param>
        /// <returns>The squared distance between the two.</returns>
        public static float DistanceSquared(this Vector3 a, Transform b) => (a - b.transform.position).sqrMagnitude;

        #endregion

        #region Distance checks

        /// <summary>
        /// Checks if the distance between two <see cref="Vector3">vectors</see> is less than or equal to a value.
        /// </summary>
        /// <param name="a">The source vector.</param>
        /// <param name="b">The destination vector.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns>Whether the distance is in range.</returns>
        public static bool IsWithinDistance(this Vector3 a, Vector3 b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

        /// <summary>
        /// Checks if the distance between two <see cref="GameObject">GameObjects</see> is less than or equal to a value.
        /// </summary>
        /// <param name="a">The source object.</param>
        /// <param name="b">The destination object.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns>Whether the distance is in range.</returns>
        public static bool IsWithinDistance(this GameObject a, GameObject b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

        /// <summary>
        /// Checks if the distance between two a <see cref="GameObject"/> and a <see cref="Vector3"/> is less than or equal to a value.
        /// </summary>
        /// <param name="a">The source object.</param>
        /// <param name="b">The destination vector.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns>Whether the distance is in range.</returns>
        public static bool IsWithinDistance(this GameObject a, Vector3 b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

        /// <summary>
        /// Checks if the distance between a <see cref="Vector3"/> and a <see cref="GameObject"/> is less than or equal to a value.
        /// </summary>
        /// <param name="a">The source vector.</param>
        /// <param name="b">The destination object.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns>Whether the distance is in range.</returns>
        public static bool IsWithinDistance(this Vector3 a, GameObject b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

        /// <summary>
        /// Checks if the distance between two <see cref="Component">Components</see> is less than or equal to a value.
        /// </summary>
        /// <param name="a">A component on the source object.</param>
        /// <param name="b">A component on the destination object.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns>Whether the distance is in range.</returns>
        public static bool IsWithinDistance(this Component a, Component b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

        /// <summary>
        /// Checks if the distance between a <see cref="Component"/> and a <see cref="Vector3"/> is less than or equal to a value.
        /// </summary>
        /// <param name="a">A component on the source object.</param>
        /// <param name="b">The destination vector.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns>Whether the distance is in range.</returns>
        public static bool IsWithinDistance(this Component a, Vector3 b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

        /// <summary>
        /// Checks if the distance between a <see cref="Vector3"/> and a <see cref="Component"/> is less than or equal to a value.
        /// </summary>
        /// <param name="a">The source vector.</param>
        /// <param name="b">A component on the destination object.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns>Whether the distance is in range.</returns>
        public static bool IsWithinDistance(this Vector3 a, Component b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

        /// <summary>
        /// Checks if the distance between two <see cref="Transform">Transforms</see> is less than or equal to a value.
        /// </summary>
        /// <param name="a">The source transform.</param>
        /// <param name="b">The destination transform.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns>Whether the distance is in range.</returns>
        public static bool IsWithinDistance(this Transform a, Transform b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

        /// <summary>
        /// Checks if the distance between a <see cref="Transform"/> and a <see cref="Vector3"/> is less than or equal to a value.
        /// </summary>
        /// <param name="a">The source transform.</param>
        /// <param name="b">The destination vector.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns>Whether the distance is in range.</returns>
        public static bool IsWithinDistance(this Transform a, Vector3 b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

        /// <summary>
        /// Checks if the distance between a <see cref="Vector3"/> and a <see cref="Transform"/> is less than or equal to a value.
        /// </summary>
        /// <param name="a">The source vector.</param>
        /// <param name="b">The destination transform.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns>Whether the distance is in range.</returns>
        public static bool IsWithinDistance(this Vector3 a, Transform b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

        #endregion

        #endregion

        #region Component helpers

        /// <summary>
        /// Gets an already existing component on a GameObject or adds it.
        /// </summary>
        /// <param name="gameObject">The object to get the component from.</param>
        /// <typeparam name="T">The type of the component to get.</typeparam>
        /// <returns>An already existing or newly created component.</returns>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component =>
            gameObject != null ? gameObject.TryGetComponent(out T component) ? component : gameObject.AddComponent<T>() : throw new ArgumentNullException(nameof(gameObject));

        /// <summary>
        /// Gets an already existing component on a GameObject or adds it.
        /// </summary>
        /// <param name="component">A component on the object to get the component of type <typeparamref name="T"/> from.</param>
        /// <typeparam name="T">The type of the component to get.</typeparam>
        /// <returns>An already existing or newly created component.</returns>
        public static T GetOrAddComponent<T>(this Component component) where T : Component => GetOrAddComponent<T>(component.gameObject);

        /// <summary>
        /// Destroys the component of type <typeparamref name="T"/> on a GameObject. 
        /// </summary>
        /// <param name="gameObject">The object to remove the component from.</param>
        /// <typeparam name="T">The type of component to destroy.</typeparam>
        /// <returns>If the component was found and will be destroyed at the end of the frame.</returns>
        /// <footer><a href="https://docs.unity3d.com/ScriptReference/Object.Destroy.html">Object.Destroy on docs.unity3d.com</a></footer>
        public static bool DestroyComponent<T>(this GameObject gameObject) where T : Component {
            if (gameObject == null || !gameObject.TryGetComponent(out T component))
                return false;
            Object.Destroy(component);
            return true;
        }

        /// <summary>
        /// Destroys the component of type <typeparamref name="T"/> on a GameObject. 
        /// </summary>
        /// <param name="component">A component on the object to remove the component of type <typeparamref name="T"/> from.</param>
        /// <typeparam name="T">The type of component to destroy.</typeparam>
        /// <returns>If the component was found and will be destroyed at the end of the frame.</returns>
        /// <footer><a href="https://docs.unity3d.com/ScriptReference/Object.Destroy.html">Object.Destroy on docs.unity3d.com</a></footer>
        public static bool DestroyComponent<T>(this Component component) where T : Component {
            if (component == null || !component.gameObject.TryGetComponent(out T c))
                return false;
            Object.Destroy(c);
            return true;
        }

        /// <summary>
        /// Immediately destroys the component of type <typeparamref name="T"/> on a GameObject. 
        /// </summary>
        /// <param name="gameObject">The object to remove the component from.</param>
        /// <typeparam name="T">The type of component to destroy.</typeparam>
        /// <returns>If the component was found and was destroyed.</returns>
        /// <footer><a href="https://docs.unity3d.com/ScriptReference/Object.DestroyImmediate.html">Object.DestroyImmediate on docs.unity3d.com</a></footer>
        public static bool DestroyComponentImmediate<T>(this GameObject gameObject) where T : Component {
            if (!gameObject.TryGetComponent(out T component))
                return false;
            Object.DestroyImmediate(component);
            return true;
        }

        /// <summary>
        /// Immediately destroys the component of type <typeparamref name="T"/> on a GameObject. 
        /// </summary>
        /// <param name="component">A component on the object to remove the component of type <typeparamref name="T"/> from.</param>
        /// <typeparam name="T">The type of component to destroy.</typeparam>
        /// <returns>If the component was found and was destroyed.</returns>
        /// <footer><a href="https://docs.unity3d.com/ScriptReference/Object.DestroyImmediate.html">Object.DestroyImmediate on docs.unity3d.com</a></footer>
        public static bool DestroyComponentImmediate<T>(this Component component) where T : Component {
            if (!component.gameObject.TryGetComponent(out T c))
                return false;
            Object.DestroyImmediate(c);
            return true;
        }

        #endregion

        #region Fake SyncVars

        #region Cache

        private const byte LdcI8 = 33;

        private static readonly Dictionary<Type, MethodInfo> WriterExtensions = new();

        private static readonly Dictionary<Type, Dictionary<string, ulong>> SyncVarDirtyBits = new();

        /// <summary>
        /// Attempts to get a Mirror writer method for a type.
        /// </summary>
        /// <param name="type">The type to get the writer method for.</param>
        /// <param name="info">The writer method.</param>
        /// <returns>Whether the writer method was found.</returns>
        public static bool TryGetWriterExtension(Type type, out MethodInfo info) {
            if (WriterExtensions.Count != 0)
                return WriterExtensions.TryGetValue(type, out info);
            foreach (var methodInfo in typeof(NetworkWriterExtensions).GetMethods().Where(x => !x.IsGenericMethod && x.GetParameters().Length == 2))
                WriterExtensions.Add(methodInfo.GetParameters().First(x => x.ParameterType != typeof(NetworkWriter)).ParameterType, methodInfo);
            foreach (var methodInfo in typeof(GeneratedNetworkCode).GetMethods().Where(x => !x.IsGenericMethod && x.GetParameters().Length == 2))
                WriterExtensions.Add(methodInfo.GetParameters().First(x => x.ParameterType != typeof(NetworkWriter)).ParameterType, methodInfo);
            foreach (var t in typeof(ServerConsole).Assembly.GetTypes().Where(x => x.Name.EndsWith("Serializer")))
            foreach (var methodInfo in t.GetMethods().Where(x => x.ReturnType == typeof(void) && x.Name.StartsWith("Write")))
                WriterExtensions.Add(methodInfo.GetParameters().First(x => x.ParameterType != typeof(NetworkWriter)).ParameterType, methodInfo);
            return WriterExtensions.TryGetValue(type, out info);
        }

        /// <summary>
        /// Attempts to get the dirty bit value for a SyncVar.
        /// </summary>
        /// <param name="type">The type of the <see cref="NetworkBehaviour"/> containing the SyncVar.</param>
        /// <param name="name">The name of the SyncVar.<br/>A property name should be passed that is defined in the containing class with the name as the SyncVar and prefix of "Network".</param>
        /// <param name="dirtyBit">The dirty bit value.</param>
        /// <returns>Whether the dirty bit was found.</returns>
        /// <example>
        /// To get the dirty bit value of <b>field "PlayerName"</b>, the property would be named <b>NetworkPlayerName</b><br/>
        /// <b>Code:</b>
        /// <code>TryGetDirtyBit(myType, "NetworkPlayerName", out ulong dirtyBit);</code>
        /// </example>
        public static bool TryGetDirtyBit(Type type, string name, out ulong dirtyBit) {
            if (SyncVarDirtyBits.Count == 0)
                foreach (var propertyInfo in typeof(ServerConsole).Assembly.GetTypes().SelectMany(x => x.GetProperties()).Where(m => m.Name.StartsWith("Network"))) {
                    var il = propertyInfo.GetSetMethod()?.GetMethodBody()?.GetILAsByteArray();
                    var t = propertyInfo.DeclaringType;
                    if (t == null || il == null)
                        continue;
                    var dict = SyncVarDirtyBits.TryGetValue(t, out var x)
                        ? x
                        : SyncVarDirtyBits[t] = new Dictionary<string, ulong>();
                    dict[propertyInfo.Name] = il[il.LastIndexOf(LdcI8) + 1];
                }

            if (SyncVarDirtyBits.TryGetValue(type, out var d))
                return d.TryGetValue(name, out dirtyBit);
            dirtyBit = default;
            return false;
        }

        #endregion

        #region Sending

        /// <summary>
        /// Sends a fake SyncVar to the given <see cref="NetworkConnection"/>.
        /// </summary>
        /// <param name="connection">The connection to send the fake SyncVar to.</param>
        /// <param name="behavior">The <see cref="NetworkBehaviour"/> that contains the SyncVar.</param>
        /// <param name="property">The name of the SyncVar.<br/>A property name should be passed that is defined in the containing class with the name as the SyncVar and prefix of "Network".</param>
        /// <param name="valueType">The type used to serialize the value</param>
        /// <param name="value">The fake value of the SyncVar.</param>
        /// <example>
        /// To send a fake value of <b>field "PlayerName"</b>, the property would be named <b>NetworkPlayerName</b><br/>
        /// <b>Code:</b>
        /// <code>myConnection.SendFakeSyncVarOfType(myBehavior, "NetworkPlayerName", typeof(string), "Fake Name");</code>
        /// </example>
        /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="valueType"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="property"/> was not found on the behavior or no serializer was found for the given <paramref name="valueType"/>.</exception>
        /// <seealso cref="SendFakeSyncVarGeneric{T}"/>
        public static void SendFakeSyncVarOfType(this NetworkConnection connection, NetworkBehaviour behavior, string property, Type valueType, object value) {
            if (valueType == null)
                throw new ArgumentNullException(nameof(valueType));
            if (!TryGetDirtyBit(behavior.GetType(), property, out var dirtyBit))
                throw new ArgumentException($"Could not find dirty bit for {property} on {behavior.GetType().FullName}");
            if (!TryGetWriterExtension(valueType, out var writer))
                throw new ArgumentException($"Could not find writer extension for {valueType.FullName}");
            var owner = NetworkWriterPool.GetWriter();
            var observer = NetworkWriterPool.GetWriter();
            WriteCustomSyncVar(behavior, CustomSyncVarGenerator, owner, observer);
            connection.Send(new UpdateVarsMessage {
                netId = behavior.netId,
                payload = owner.ToArraySegment()
            });
            NetworkWriterPool.Recycle(owner);
            NetworkWriterPool.Recycle(observer);

            void CustomSyncVarGenerator(NetworkWriter targetWriter) {
                targetWriter.WriteUInt64(dirtyBit);
                writer.Invoke(null, new[] {
                    targetWriter,
                    value
                });
            }
        }

        /// <summary>
        /// Sends a fake SyncVar to the given <see cref="NetworkConnection"/>.
        /// </summary>
        /// <param name="connection">The connection to send the fake SyncVar to.</param>
        /// <param name="behavior">The <see cref="NetworkBehaviour"/> that contains the SyncVar.</param>
        /// <param name="property">The name of the SyncVar.<br/>A property name should be passed that is defined in the containing class with the name as the SyncVar and prefix of "Network".</param>
        /// <param name="value">The fake value of the SyncVar.</param>
        /// <typeparam name="T">The type used to serialize the SyncVar.</typeparam>
        /// <remarks>
        /// Useful when a base type is used to serialize a SyncVar, but the actual value is a derived type; or if the value is null.
        /// </remarks>
        /// <example>
        /// To send a fake value of <b>field "FirearmController"</b>, the property would be named <b>NetworkFirearmController</b><br/>
        /// <b>Code:</b>
        /// <code>myConnection.SendFakeSyncVarGeneric&lt;FirearmBase&gt;(myBehavior, "NetworkFirearmController", new DerivedFirearmController());</code>
        /// </example>
        /// <seealso cref="SendFakeSyncVarOfType"/>
        public static void SendFakeSyncVarGeneric<T>(this NetworkConnection connection, NetworkBehaviour behavior, string property, object value) =>
            SendFakeSyncVarOfType(connection, behavior, property, typeof(T), value);

        /// <summary>
        /// Sends a fake SyncVar to the given <see cref="NetworkConnection"/>.
        /// </summary>
        /// <param name="connection">The connection to send the fake SyncVar to.</param>
        /// <param name="behavior">The <see cref="NetworkBehaviour"/> that contains the SyncVar.</param>
        /// <param name="property">The name of the SyncVar.<br/>A property name should be passed that is defined in the containing class with the name as the SyncVar and prefix of "Network".</param>
        /// <param name="value">The fake value of the SyncVar.</param>
        /// <example>
        /// To send a fake value of <b>field "PlayerName"</b>, the property would be named <b>NetworkPlayerName</b><br/>
        /// <b>Code:</b>
        /// <code>myConnection.SendFakeSyncVar(myBehavior, "NetworkPlayerName", "Fake Name");</code>
        /// </example>
        public static void SendFakeSyncVar(this NetworkConnection connection, NetworkBehaviour behavior, string property, object value) =>
            SendFakeSyncVarOfType(connection, behavior, property, value?.GetType(), value);

        /// <summary>
        /// Re-synchronizes the value of a SyncVar for the given <see cref="NetworkConnection"/>.
        /// </summary>
        /// <param name="connection">The connection to send the SyncVar to.</param>
        /// <param name="behavior">The <see cref="NetworkBehaviour"/> that contains the SyncVar.</param>
        /// <param name="property">The name of the SyncVar.<br/>A property name should be passed that is defined in the containing class with the name as the SyncVar and prefix of "Network".</param>
        /// <example>
        /// To resend the value of <b>field "MySyncVar"</b>, the property would be named <b>NetworkMySyncVar</b><br/>
        /// <b>Code:</b>
        /// <code>myConnection.ReSyncSyncVar(myBehavior, "NetworkMySyncVar");</code>
        /// </example>
        public static void ReSyncSyncVar(this NetworkConnection connection, NetworkBehaviour behavior, string property) {
            var prop = AccessTools.PropertyGetter(behavior.GetType(), property);
            SendFakeSyncVarOfType(connection, behavior, property, prop.ReturnType, prop.Invoke(behavior, null));
        }

        /// <summary>
        /// Writes a custom SyncVar value to the writer.
        /// </summary>
        /// <param name="behavior">The <see cref="NetworkBehaviour"/> that contains the SyncVar.</param>
        /// <param name="syncVarGenerator">A function that serializes the value to the writer.</param>
        /// <param name="owner">The writer to write the value to for the owner.</param>
        /// <param name="observer">The writer to write the value to for the observers.</param>
        public static void WriteCustomSyncVar(
            NetworkBehaviour behavior,
            Action<NetworkWriter> syncVarGenerator,
            NetworkWriter owner,
            NetworkWriter observer
        ) {
            var behaviorIndex = (byte) behavior.netIdentity.NetworkBehaviours.IndexOf(behavior);
            owner.WriteByte(behaviorIndex);
            var start = owner.Position;
            owner.WriteInt32(0);
            var secondZero = owner.Position;
            behavior.SerializeObjectsDelta(owner);
            syncVarGenerator?.Invoke(owner);
            var serializedPosition = owner.Position;
            owner.Position = start;
            owner.WriteInt32(serializedPosition - secondZero);
            owner.Position = serializedPosition;
            if (behavior.syncMode == SyncMode.Observers)
                return;
            var segment = owner.ToArraySegment();
            observer.WriteBytes(segment.Array, start, owner.Position - start);
        }

        #endregion

        #endregion

    }

}
