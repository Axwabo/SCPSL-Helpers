using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace Axwabo.Helpers {

    /// Extension methods for quicker use of Reflection.
    public static class ReflectionHelper {

        #region Get-set explicit type

        /// <summary>
        /// Gets the value of the instance field with the specified name.
        /// </summary>
        /// <param name="type">The type of the object containing the field.</param>
        /// <param name="obj">The object to get the value from.</param>
        /// <param name="field">The name of the instance field.</param>
        /// <returns>The obtained object.</returns>
        public static object Get(this Type type, object obj, string field) {
            var fieldInfo = AccessTools.Field(type, field);
            return fieldInfo?.GetValue(obj);
        }

        /// <summary>
        /// Gets the value of the instance field with the specified name as type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="type">The type of the object containing the field.</param>
        /// <param name="obj">The object to get the value from.</param>
        /// <param name="field">The name of the instance field.</param>
        /// <typeparam name="T">The type of the field's value.</typeparam>
        /// <returns>The obtained <typeparamref name="T"/> type object.</returns>
        /// <exception cref="InvalidCastException">Thrown if the field's value is not of type <typeparamref name="T"/>.</exception>
        public static T Get<T>(this Type type, object obj, string field) {
            return (T) Get(type, obj, field);
        }

        /// <summary>
        /// Sets the value of the instance field with the specified name.
        /// </summary>
        /// <param name="type">The type of the object containing the field.</param>
        /// <param name="obj">The object to set the value on.</param>
        /// <param name="field">The name of the instance field.</param>
        /// <param name="value">The new value to set.</param>
        /// <returns>The object itself.</returns>
        public static object Set(this Type type, object obj, string field, object value) {
            var fieldInfo = AccessTools.Field(type, field);
            if (fieldInfo != null)
                fieldInfo.SetValue(obj, value);
            return obj;
        }

        /// <summary>
        /// Gets the value of the instance property with the specified name.
        /// </summary>
        /// <param name="type">The type of the object containing the property.</param>
        /// <param name="obj">The object to get the value from.</param>
        /// <param name="property">The name of the instance property.</param>
        /// <returns>The obtained object.</returns>
        public static object GetProp(this Type type, object obj, string property) {
            var getter = AccessTools.PropertyGetter(type, property);
            return getter == null ? null : getter.Invoke(obj, Array.Empty<object>());
        }

        /// <summary>
        /// Sets the value of the instance property with the specified name.
        /// </summary>
        /// <param name="type">The type of the object containing the property.</param>
        /// <param name="obj">The object to set the value on.</param>
        /// <param name="property">The name of the instance property.</param>
        /// <param name="value">The new value to set.</param>
        /// <returns>The object itself.</returns>
        public static object SetProp(this Type type, object obj, string property, object value) {
            var setter = AccessTools.PropertySetter(type, property);
            if (setter != null)
                setter.Invoke(obj, new[] {value});
            return obj;
        }

        #endregion

        #region Get-set with implicit type

        /// <summary>
        /// Gets the value of the instance field with the specified name.
        /// </summary>
        /// <param name="obj">The object to get the value from.</param>
        /// <param name="field">The name of the instance field.</param>
        /// <returns>The obtained object.</returns>
        public static object Get(this object obj, string field) {
            return Get(obj.GetType(), obj, field);
        }

        /// <summary>
        /// Gets the value of the instance field with the specified name as type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="obj">The object to get the value from.</param>
        /// <param name="field">The name of the instance field.</param>
        /// <typeparam name="T">The type of the field's value.</typeparam>
        /// <returns>The obtained <typeparamref name="T"/> type object.</returns>
        /// <exception cref="InvalidCastException">Thrown if the field's value is not of type <typeparamref name="T"/>.</exception>
        public static T Get<T>(this object obj, string field) {
            return (T) Get(obj, field);
        }

        /// <summary>
        /// Sets the value of the instance field with the specified name.
        /// </summary>
        /// <param name="obj">The object to set the value on.</param>
        /// <param name="field">The name of the instance field.</param>
        /// <param name="value">The new value to set.</param>
        /// <returns>The object itself.</returns>
        public static T Set<T>(this T obj, string field, object value) {
            Set(obj.GetType(), obj, field, value);
            return obj;
        }

        /// <summary>
        /// Gets the value of the instance property with the specified name.
        /// </summary>
        /// <param name="obj">The object to get the value from.</param>
        /// <param name="property">The name of the instance property.</param>
        /// <returns>The obtained object.</returns>
        public static object GetProp(this object obj, string property) {
            return GetProp(obj.GetType(), obj, property);
        }

        /// <summary>
        /// Gets the value of the instance property with the specified name as type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="obj">The object to get the value from.</param>
        /// <param name="property">The name of the instance property.</param>
        /// <typeparam name="T">The type of the property's value.</typeparam>
        /// <returns>The obtained <typeparamref name="T"/> type object.</returns>
        /// <exception cref="InvalidCastException">Thrown if the property's value is not of type <typeparamref name="T"/>.</exception>
        public static T GetProp<T>(this object obj, string property) {
            return (T) GetProp(obj, property);
        }

        /// <summary>
        /// Sets the value of the instance property with the specified name.
        /// </summary>
        /// <param name="obj">The object to set the value on.</param>
        /// <param name="property">The name of the instance property.</param>
        /// <param name="value">The new value to set.</param>
        /// <typeparam name="T">The type of the instance object.</typeparam>
        /// <returns>The <typeparamref name="T"/> object itself.</returns>
        public static T SetProp<T>(this T obj, string property, object value) {
            return (T) SetProp(obj.GetType(), obj, property, value);
        }

        #endregion

        #region Instance calls

        /// <summary>
        /// Calls an instance method with the specified name.
        /// </summary>
        /// <param name="type">The type of the object containing the method.</param>
        /// <param name="obj">The object to call the method on.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="args">The arguments to pass to the method.</param>
        /// <returns>The value the method returned.</returns>
        public static object Call(this Type type, object obj, string methodName, params object[] args) {
            var method = AccessTools.Method(type, methodName, args.Select(e => e.GetType()).ToArray());
            return method == null ? null : method.Invoke(obj, args);
        }

        /// <summary>
        /// Calls an instance method with the specified name of return type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="type">The type of the object containing the method.</param>
        /// <param name="obj">The object to call the method on.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="args">The arguments to pass to the method.</param>
        /// <typeparam name="T">The type of the method's return value.</typeparam>
        /// <returns>The value the method returned.</returns>
        /// <exception cref="InvalidCastException">Thrown if the method's return value is not of type <typeparamref name="T"/>.</exception>
        public static T Call<T>(this Type type, object obj, string methodName, params object[] args) {
            return (T) Call(type, obj, methodName, args);
        }

        /// <summary>
        /// Calls an instance method with the specified name.
        /// </summary>
        /// <param name="obj">The object to call the method on.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="args">The arguments to pass to the method.</param>
        /// <returns>The value the method returned.</returns>
        public static object Call(this object obj, string methodName, params object[] args) {
            return Call(obj.GetType(), obj, methodName, args);
        }

        /// <summary>
        /// Calls an instance method with the specified name of return type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="obj">The object to call the method on.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="args">The arguments to pass to the method.</param>
        /// <typeparam name="T">The type of the method's return value.</typeparam>
        /// <returns>The value the method returned.</returns>
        /// <exception cref="InvalidCastException">Thrown if the method's return value is not of type <typeparamref name="T"/>.</exception>
        public static T Call<T>(this object obj, string methodName, params object[] args) {
            return (T) Call(obj, methodName, args);
        }

        #endregion

        #region Static get-set

        /// <summary>
        /// Gets the value of the static field with the specified name.
        /// </summary>
        /// <param name="type">The type containing the field.</param>
        /// <param name="field">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        public static object StaticGet(this Type type, string field) {
            var fieldInfo = AccessTools.Field(type, field);
            return fieldInfo == null ? null : fieldInfo.GetValue(null);
        }

        /// <summary>
        /// Gets the value of the static field with the specified name as type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="type">The type containing the field.</param>
        /// <param name="field">The name of the field.</param>
        /// <typeparam name="T">The type of the field's value.</typeparam>
        /// <returns>The value of the field.</returns>
        /// <exception cref="InvalidCastException">Thrown if the field's value is not of type <typeparamref name="T"/>.</exception>
        public static T StaticGet<T>(this Type type, string field) {
            return (T) StaticGet(type, field);
        }

        /// <summary>
        /// Sets the value of the static field with the specified name.
        /// </summary>
        /// <param name="type">The type containing the field.</param>
        /// <param name="field">The name of the field.</param>
        /// <param name="value">The new value to set.</param>
        public static void StaticSet(this Type type, string field, object value) {
            var fieldInfo = AccessTools.Field(type, field);
            if (fieldInfo != null)
                fieldInfo.SetValue(null, value);
        }

        #endregion

        #region Static property get-set

        /// <summary>
        /// Gets the value of the static property with the specified name.
        /// </summary>
        /// <param name="type">The type containing the property.</param>
        /// <param name="property">The name of the property.</param>
        /// <returns>The value of the property.</returns>
        public static object StaticGetProp(this Type type, string property) {
            var getter = AccessTools.PropertyGetter(type, property);
            return getter == null ? null : getter.Invoke(null, Array.Empty<object>());
        }

        /// <summary>
        /// Gets the value of the static property with the specified name as type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="type">The type containing the property.</param>
        /// <param name="property">The name of the property.</param>
        /// <typeparam name="T">The type of the property's value.</typeparam>
        /// <returns>The value of the property.</returns>
        /// <exception cref="InvalidCastException">Thrown if the property's value is not of type <typeparamref name="T"/>.</exception>
        public static T StaticGetProp<T>(this Type type, string property) {
            return (T) StaticGetProp(type, property);
        }

        /// <summary>
        /// Sets the value of the static property with the specified name.
        /// </summary>
        /// <param name="type">The type containing the property.</param>
        /// <param name="property">The name of the property.</param>
        /// <param name="value">The new value to set.</param>
        public static void StaticSetProp(this Type type, string property, object value) {
            var setter = AccessTools.PropertySetter(type, property);
            if (setter != null)
                setter.Invoke(null, new[] {value});
        }

        #endregion

        #region Static calls

        /// <summary>
        /// Calls a static method with the specified name.
        /// </summary>
        /// <param name="type">The type containing the method.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="args">The arguments to pass to the method.</param>
        /// <returns>The value the method returned.</returns>
        public static object StaticCall(this Type type, string methodName, params object[] args) {
            var method = AccessTools.Method(type, methodName, args.Select(e => e.GetType()).ToArray());
            return method != null ? method.Invoke(null, args) : default;
        }

        /// <summary>
        /// Calls a static method with the specified name of return type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="type">The type containing the method.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="args">The arguments to pass to the method.</param>
        /// <typeparam name="T">The type of the method's return value.</typeparam>
        /// <returns>The value the method returned.</returns>
        /// <exception cref="InvalidCastException">Thrown if the method's return value is not of type <typeparamref name="T"/>.</exception>
        public static T StaticCall<T>(this Type type, string methodName, params object[] args) {
            return (T) StaticCall(type, methodName, args);
        }

        #endregion

        /// <summary>
        /// Maps an enumerable of an unknown type (e.g. nested private class) to an inner field of that type.
        /// </summary>
        /// <param name="obj">The object containing the enumerable of the unknown type.</param>
        /// <param name="fieldName">The name of the field in the object.</param>
        /// <param name="innerField">The name of the field in the unknown type.</param>
        /// <param name="mapper">A function to convert the objects to type <typeparamref name="T"/>.</param>
        /// <typeparam name="T">The type of the object to convert to.</typeparam>
        /// <returns>An enumerable of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> MapUnknownTypeArray<T>(this object obj, string fieldName, string innerField, Func<object, T> mapper) {
            return MapUnknownTypeArray(obj, fieldName, o => mapper(Get(o, innerField)));
        }

        /// <summary>
        /// Maps an enumerable of an unknown type (e.g. nested private class) to a known type.
        /// </summary>
        /// <param name="obj">The object containing the enumerable of the unknown type.</param>
        /// <param name="fieldName">The name of the field in the object.</param>
        /// <param name="mapper">A function to convert the objects to type <typeparamref name="T"/>.</param>
        /// <typeparam name="T">The type of the object to convert to.</typeparam>
        /// <returns>An enumerable of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> MapUnknownTypeArray<T>(this object obj, string fieldName, Func<object, T> mapper) {
            return (from object o in Get<IEnumerable>(obj, fieldName) select mapper(o)).ToList();
        }

        #region Enums

        /// <summary>
        /// Gets the defined enums in the specified type.
        /// </summary>
        /// <param name="type">The type to get the enums from.</param>
        /// <typeparam name="T">The type of the enums.</typeparam>
        /// <returns>An enumerable of the enums.</returns>
        public static IEnumerable<T> Enums<T>(this Type type) {
            return Enum.GetValues(type).ToArray<T>();
        }

        /// <summary>
        /// Gets the defined enums in the specified type.
        /// </summary>
        /// <param name="enumType">The type to get the enums from.</param>
        /// <typeparam name="T">The type of the enums.</typeparam>
        /// <returns>An enumerable of the enums.</returns>
        /// <remarks>Can be used like <see cref="Enums{T}(System.Type)"/>, but instead of passing in the type, it needs an enum value.</remarks>
        public static IEnumerable<T> Enums<T>(this T enumType) where T : Enum {
            return Enum.GetValues(enumType.GetType()).ToArray<T>();
        }

        /// <summary>
        /// Gets the defined enums in the specified type except the given enum.
        /// </summary>
        /// <param name="except">The enum to exclude.</param>
        /// <typeparam name="T">The type of the enums.</typeparam>
        /// <returns>An enumerable of the enums.</returns>
        public static IEnumerable<T> EnumsExcept<T>(this T except) where T : Enum {
            return EnumsExcept(except, except);
        }

        /// <summary>
        /// Gets the defined enums in the specified type except the given enum.
        /// </summary>
        /// <param name="enumType">An enum instance that specifies the type of the enum.</param>
        /// <param name="except">The enum to exclude.</param>
        /// <typeparam name="T">The type of the enums.</typeparam>
        /// <returns>An enumerable of the enums.</returns>
        /// <remarks>Can be used like <see cref="Enums{T}(System.Type)"/>, but instead of passing in the type, it needs some enum value.</remarks>
        public static IEnumerable<T> EnumsExcept<T>(this T enumType, T except) where T : Enum {
            return Enums<T>(enumType.GetType()).Where(e => !e.Equals(except));
        }

        #endregion

    }

}
