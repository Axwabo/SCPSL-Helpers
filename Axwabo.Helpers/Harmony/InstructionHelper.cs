using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace Axwabo.Helpers.Harmony {

    /// A helper class to create <see cref="CodeInstruction">CodeInstructions</see>.
    public static class InstructionHelper {

        #region Basic Instructions

        /// <summary>
        /// Pushes a new object reference to a string literal stored in the metadata.
        /// </summary>
        /// <param name="s">The string to load.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the string.</returns>
        public static CodeInstruction String(string s) => new(OpCodes.Ldstr, s);

        /// <summary>
        /// Loads the argument at index 0 (this in non-static context) onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Ldarg_0"/>
        public static CodeInstruction This => new(OpCodes.Ldarg_0);

        /// <summary>
        /// Returns from the current method.
        /// </summary>
        /// <seealso cref="OpCodes.Ret"/>
        public static CodeInstruction Return => new(OpCodes.Ret);

        /// <summary>
        /// Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Dup"/>
        public static CodeInstruction Dupe => new(OpCodes.Dup);

        /// <summary>
        /// Removes the value currently on top of the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Pop"/>
        public static CodeInstruction Pop => new(OpCodes.Pop);

        /// <summary>
        /// Converts a value type to an object reference (type O).
        /// </summary>
        /// <param name="type">The type of value to convert to an object.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that converts a value type to an object.</returns>
        /// <seealso cref="OpCodes.Box"/>
        public static CodeInstruction Box(Type type) => new(OpCodes.Box, type);

        /// <summary>
        /// Converts a value type to an object reference (type O).
        /// </summary>
        /// <typeparam name="T">The type of value to convert to an object.</typeparam>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that converts a value type to an object.</returns>
        /// <seealso cref="OpCodes.Box"/>
        public static CodeInstruction Box<T>() => new(OpCodes.Box, typeof(T));

        /// <summary>
        /// Attempts to cast an object passed by reference to the specified class.
        /// </summary>
        /// <param name="type">The type to cast to.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that converts the object to a different type.</returns>
        /// <seealso cref="OpCodes.Castclass"/>
        public static CodeInstruction Cast(Type type) => new(OpCodes.Castclass, type);

        /// <summary>
        /// Attempts to cast an object passed by reference to the specified class.
        /// </summary>
        /// <typeparam name="T">The type to cast to.</typeparam>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that converts the object to a different type.</returns>
        /// <seealso cref="OpCodes.Castclass"/>
        public static CodeInstruction Cast<T>() => new(OpCodes.Castclass, typeof(T));

        #endregion

        #region Numbers

        /// <summary>
        /// Pushes the integer value of 0 onto the evaluation stack as an <see cref="int">int32</see>.
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_I4_0"/>
        public static CodeInstruction Int0 => new(OpCodes.Ldc_I4_0);

        /// <summary>
        /// Pushes the integer value of 1 onto the evaluation stack as an <see cref="Int32">int32</see>.
        /// </summary>
        /// <seealso cref="OpCodes.Ldarg_0"/>
        public static CodeInstruction Int1 => new(OpCodes.Ldc_I4_1);

        /// <summary>
        /// Pushes a supplied value of type int32 onto the evaluation stack as an <see cref="int">int32</see>.
        /// </summary>
        /// <param name="i">The integer to load.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the specific integer.</returns>
        /// <seealso cref="OpCodes.Ldc_I4"/>
        public static CodeInstruction Int(int i) => i switch {
            0 => Int0,
            1 => Int1,
            2 => new CodeInstruction(OpCodes.Ldc_I4_2),
            3 => new CodeInstruction(OpCodes.Ldc_I4_3),
            4 => new CodeInstruction(OpCodes.Ldc_I4_4),
            5 => new CodeInstruction(OpCodes.Ldc_I4_5),
            6 => new CodeInstruction(OpCodes.Ldc_I4_6),
            7 => new CodeInstruction(OpCodes.Ldc_I4_7),
            8 => new CodeInstruction(OpCodes.Ldc_I4_8),
            _ => new CodeInstruction(OpCodes.Ldc_I4, i)
        };

        /// <summary>
        /// Pushes 0 of type <see cref="Single">int32</see> onto the evaluation stack as type F (float).
        /// </summary>
        /// <seealso cref="OpCodes.Ldc_R4"/>
        public static CodeInstruction Float0 => new(OpCodes.Ldc_R4, 0f);

        /// <summary>
        /// Pushes a supplied value of type <see cref="Single">int32</see> onto the evaluation stack as type F (float).
        /// </summary>
        /// <param name="f">The float to load.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the specific float.</returns>
        /// <seealso cref="OpCodes.Ldc_R4"/>
        public static CodeInstruction Float(float f) => new(OpCodes.Ldc_R4, f);

        /// <summary>
        /// Loads the integer value of the specified enum.
        /// </summary>
        /// <param name="e">The enum to load.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the enum.</returns>
        /// <seealso cref="Int"/>
        /// <seealso cref="OpCodes.Ldc_I4"/>
        public static CodeInstruction LoadEnum(Enum e) => Int(Convert.ToInt32(e));

        #endregion

        #region Operators

        /// <summary>
        /// Adds two values and pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Add"/>
        public static CodeInstruction Add => new(OpCodes.Add);

        /// <summary>
        /// Subtracts one value from another and pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Sub"/>
        public static CodeInstruction Subtract => new(OpCodes.Sub);

        /// <summary>
        /// Multiplies two values and pushes the result on the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Mul"/>
        public static CodeInstruction Multiply => new(OpCodes.Mul);

        /// <summary>
        /// Divides two values and pushes the result as a floating-point (type F) or quotient (type int32) onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Div"/>
        public static CodeInstruction Divide => new(OpCodes.Div);

        /// <summary>
        /// Divides two values and pushes the remainder onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Rem"/>
        public static CodeInstruction Modulo => new(OpCodes.Rem);

        /// <summary>
        /// Computes the bitwise complement of the integer value on top of the stack and pushes the result onto the evaluation stack as the same type.
        /// </summary>
        /// <seealso cref="OpCodes.Not"/>
        public static CodeInstruction LogicalNot => new(OpCodes.Not);

        /// <summary>
        /// Computes the bitwise AND of two values and pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.And"/>
        public static CodeInstruction LogicalAnd => new(OpCodes.And);

        /// <summary>
        /// Computes the bitwise complement of the two integer values on top of the stack and pushes the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Xor"/>
        public static CodeInstruction LogicalOr => new(OpCodes.Or);

        /// <summary>
        /// Computes the bitwise XOR of the top two values on the evaluation stack, pushing the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Xor"/>
        public static CodeInstruction Xor => new(OpCodes.Xor);

        /// <summary>
        /// Shifts an integer value to the left (in zeroes) by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Shl"/>
        public static CodeInstruction ShiftLeft => new(OpCodes.Shl);

        /// <summary>
        /// Shifts an integer value (in sign) to the right by a specified number of bits, pushing the result onto the evaluation stack.
        /// </summary>
        /// <seealso cref="OpCodes.Shr"/>
        public static CodeInstruction ShiftRight => new(OpCodes.Shr);

        /// <summary>
        /// Compares two values.
        /// </summary>
        /// <seealso cref="OpCodes.Ceq"/>
        public static CodeInstruction Equality => new(OpCodes.Ceq);

        #endregion

        #region Arguments

        /// <summary>
        /// Loads an argument (referenced by a specified index value) onto the stack.
        /// </summary>
        /// <param name="index">The index of the argument.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the argument.</returns>
        /// <remarks>
        /// In instance methods, arg 0 is "this", 1 is the first argument, 2 is the second argument, and so on.
        /// </remarks>
        /// <seealso cref="OpCodes.Ldarg"/>
        public static CodeInstruction Ldarg(int index) => index switch {
            0 => This,
            1 => new CodeInstruction(OpCodes.Ldarg_1),
            2 => new CodeInstruction(OpCodes.Ldarg_2),
            3 => new CodeInstruction(OpCodes.Ldarg_3),
            _ => new CodeInstruction(OpCodes.Ldarg, index)
        };

        /// <summary>
        /// Stores the value on top of the evaluation stack in the argument slot at a specified index.
        /// </summary>
        /// <param name="index">The index of the argument.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the argument.</returns>
        /// <remarks>
        /// In instance methods, arg 0 is "this", 1 is the first argument, 2 is the second argument, and so on.
        /// </remarks>
        /// <seealso cref="OpCodes.Starg"/>
        public static CodeInstruction Starg(int index) => new(OpCodes.Starg, index);

        /// <summary>
        /// Loads an argument's address onto the evaluation stack.
        /// </summary>
        /// <param name="index">The index of the argument.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address the argument.</returns>
        /// <remarks>
        /// In instance methods, arg 0 is "this", 1 is the first argument, 2 is the second argument, and so on.
        /// </remarks>
        /// <seealso cref="OpCodes.Ldarga"/>
        public static CodeInstruction Ldarga(int index) => new(OpCodes.Ldarga, index);

        #endregion

        #region Locals

        /// <summary>
        /// Loads the local variable at a specific index onto the evaluation stack.
        /// </summary>
        /// <param name="index">The index of the local variable.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the local variable.</returns>
        /// <seealso cref="OpCodes.Ldloc"/>
        public static CodeInstruction Ldloc(int index) => index switch {
            0 => new CodeInstruction(OpCodes.Ldloc_0),
            1 => new CodeInstruction(OpCodes.Ldloc_1),
            2 => new CodeInstruction(OpCodes.Ldloc_2),
            3 => new CodeInstruction(OpCodes.Ldloc_3),
            _ => new CodeInstruction(OpCodes.Ldloc, index)
        };

        /// <summary>
        /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at a specified index.
        /// </summary>
        /// <param name="index">The index of the local variable.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the local variable.</returns>
        /// <seealso cref="OpCodes.Stloc"/>
        public static CodeInstruction Stloc(int index) => index switch {
            0 => new CodeInstruction(OpCodes.Stloc_0),
            1 => new CodeInstruction(OpCodes.Stloc_1),
            2 => new CodeInstruction(OpCodes.Stloc_2),
            3 => new CodeInstruction(OpCodes.Stloc_3),
            _ => new CodeInstruction(OpCodes.Stloc, index)
        };

        /// <summary>
        /// Loads the specific local variable based on a LocalBuilder instance.
        /// </summary>
        /// <param name="local">The LocalBuilder that contains the variable's index.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the local variable.</returns>
        /// <seealso cref="OpCodes.Ldloc"/>
        public static CodeInstruction Ldloc(LocalBuilder local) => Ldloc(local.LocalIndex);

        /// <summary>
        /// Stores the specific local variable based on a LocalBuilder instance.
        /// </summary>
        /// <param name="local">The LocalBuilder that contains the variable's index.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the local variable.</returns>
        /// <seealso cref="OpCodes.Stloc"/>
        public static CodeInstruction Stloc(LocalBuilder local) => Stloc(local.LocalIndex);

        /// <summary>
        /// Loads the address of the local variable at a specific index onto the evaluation stack.
        /// </summary>
        /// <param name="index">The index of the local variable.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address the local variable.</returns>
        /// <seealso cref="OpCodes.Ldloca"/>
        public static CodeInstruction Ldloca(int index) => new(OpCodes.Ldloca, index);

        /// <summary>
        /// Loads the address of a specific local variable based on a LocalBuilder instance.
        /// </summary>
        /// <param name="local">The LocalBuilder that contains the variable's index.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address of local variable.</returns>
        /// <seealso cref="OpCodes.Ldloca"/>
        public static CodeInstruction Ldloca(LocalBuilder local) => Ldloca(local.LocalIndex);

        #endregion

        #region Calls

        /// <summary>
        /// Calls the (virtual) method indicated by the passed method descriptor.
        /// </summary>
        /// <param name="method">The information about the method.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that calls the method.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="method"/> is null.</exception>
        public static CodeInstruction Call(MethodInfo method) {
            if (method == null)
                throw new ArgumentNullException(nameof(method));
            return method.IsVirtual ? new CodeInstruction(OpCodes.Callvirt, method) : new CodeInstruction(OpCodes.Call, method);
        }

        /// <summary>
        /// Calls the (virtual) method indicated by the passed method descriptor.
        /// </summary>
        /// <param name="type">The type of the object containing the method.</param>
        /// <param name="methodName">The name of the method to call.</param>
        /// <param name="parameters">The type parameters of the method.</param>
        /// <param name="generics">The generics used to call the method.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that calls the method.</returns>
        /// <exception cref="ArgumentException">Thrown if the method was not found.</exception>
        public static CodeInstruction Call(Type type, string methodName, Type[] parameters = null, Type[] generics = null) {
            return Call(AccessTools.Method(type, methodName, parameters, generics) ?? throw new ArgumentException($"No method found for type={type.FullName}, name={methodName}, parameters={parameters.Description()}, generics={generics.Description()}"));
        }

        /// <summary>
        /// Calls the (virtual) method indicated by the passed method descriptor.
        /// </summary>
        /// <param name="methodName">The name of the method to call.</param>
        /// <param name="parameters">The type parameters of the method.</param>
        /// <param name="generics">The generics used to call the method.</param>
        /// <typeparam name="T">The type of the object containing the method.</typeparam>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that calls the method.</returns>
        /// <exception cref="ArgumentException">Thrown if the method was not found.</exception>
        /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
        public static CodeInstruction Call<T>(string methodName, Type[] parameters = null, Type[] generics = null) {
            return Call(typeof(T), methodName, parameters, generics);
        }

        /// <summary>
        /// Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.
        /// </summary>
        /// <param name="constructor">The information about the constructor.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that creates a new object.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the constructor was not found.</exception>
        /// <seealso cref="OpCodes.Newobj"/>
        public static CodeInstruction New(ConstructorInfo constructor) {
            if (constructor == null)
                throw new ArgumentNullException(nameof(constructor));
            return new CodeInstruction(OpCodes.Newobj, constructor);
        }

        /// <summary>
        /// Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <param name="parameters">The parameters of the constructor.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that creates a new object.</returns>
        /// <exception cref="ArgumentException">Thrown if the constructor was not found.</exception>
        /// <seealso cref="OpCodes.Newobj"/>
        public static CodeInstruction New(Type type, Type[] parameters = null) {
            return New(AccessTools.Constructor(type, parameters) ?? throw new ArgumentException($"No constructor found for type={type.FullName}, parameters={parameters.Description()}"));
        }

        /// <summary>
        /// Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.
        /// </summary>
        /// <param name="parameters">The parameters of the constructor.</param>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that creates a new object.</returns>
        /// <exception cref="ArgumentException">Thrown if the constructor was not found.</exception>
        /// <seealso cref="OpCodes.Newobj"/>
        public static CodeInstruction New<T>(Type[] parameters = null) {
            return New(typeof(T), parameters);
        }

        #endregion

        #region Fields

        /// <summary>
        /// Finds the value of a non-static field in the object whose reference is currently on the evaluation stack.<br/>
        /// Pushes the value of a static field onto the evaluation stack.
        /// </summary>
        /// <param name="field">The information about the field.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the field.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="field"/> is null.</exception>
        public static CodeInstruction Ldfld(FieldInfo field) {
            if (field == null)
                throw new ArgumentNullException(nameof(field));
            return field.IsStatic ? new CodeInstruction(OpCodes.Ldsfld, field) : new CodeInstruction(OpCodes.Ldfld, field);
        }

        /// <summary>
        /// Finds the value of a non-static field in the object whose reference is currently on the evaluation stack.<br/>
        /// Pushes the value of a static field onto the evaluation stack.
        /// </summary>
        /// <param name="type">The type of the object containing the field.</param>
        /// <param name="fieldName">The name of the field to load.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the field.</returns>
        /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
        public static CodeInstruction Ldfld(Type type, string fieldName) {
            return Ldfld(AccessTools.Field(type, fieldName) ?? throw new ArgumentException($"No field found for type={type.FullName}, name={fieldName}"));
        }

        /// <summary>
        /// Finds the value of a non-static field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        /// <param name="fieldName">The name of the field to load.</param>
        /// <typeparam name="T">The type of the object containing the field.</typeparam>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the field.</returns>
        /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
        /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
        public static CodeInstruction Ldfld<T>(string fieldName) {
            return Ldfld(typeof(T), fieldName);
        }

        /// <summary>
        /// Replaces the value stored in the non-static field of an object reference or pointer with a new value.<br/>
        /// Replaces the value of a static field with a value from the evaluation stack.
        /// </summary>
        /// <param name="field">The information about the field.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the field.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="field"/> is null.</exception>
        public static CodeInstruction Stfld(FieldInfo field) {
            if (field == null)
                throw new ArgumentNullException(nameof(field));
            return field.IsStatic ? new CodeInstruction(OpCodes.Stsfld, field) : new CodeInstruction(OpCodes.Stfld, field);
        }

        /// <summary>
        /// Replaces the value stored in the non-static field of an object reference or pointer with a new value.<br/>
        /// Replaces the value of a static field with a value from the evaluation stack.
        /// </summary>
        /// <param name="type">The type of the object containing the field.</param>
        /// <param name="fieldName">The name of the field to store.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the field.</returns>
        /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
        public static CodeInstruction Stfld(Type type, string fieldName) {
            return Stfld(AccessTools.Field(type, fieldName) ?? throw new ArgumentException($"No field found for type={type.FullName}, name={fieldName}"));
        }

        /// <summary>
        /// Replaces the value stored in the non-static field of an object reference or pointer with a new value.<br/>
        /// </summary>
        /// <param name="fieldName">The name of the field to store.</param>
        /// <typeparam name="T">The type of the object containing the field.</typeparam>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the field.</returns>
        /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
        /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
        public static CodeInstruction Stfld<T>(string fieldName) {
            return Stfld(typeof(T), fieldName);
        }

        /// <summary>
        /// Finds the address of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        /// <param name="field">The information about the field.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address of field.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="field"/> is null.</exception>
        public static CodeInstruction Ldflda(FieldInfo field) {
            if (field == null)
                throw new ArgumentNullException(nameof(field));
            return field.IsStatic ? new CodeInstruction(OpCodes.Ldsflda, field) : new CodeInstruction(OpCodes.Ldflda, field);
        }

        /// <summary>
        /// Finds the address of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        /// <param name="type">The type of the object containing the field.</param>
        /// <param name="fieldName">The name of the field to load.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address of the field.</returns>
        /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
        public static CodeInstruction Ldflda(Type type, string fieldName) {
            return Ldflda(AccessTools.Field(type, fieldName) ?? throw new ArgumentException($"No field found for type={type.FullName}, name={fieldName}"));
        }

        /// <summary>
        /// Finds the address of a field in the object whose reference is currently on the evaluation stack.
        /// </summary>
        /// <param name="fieldName">The name of the field to load.</param>
        /// <typeparam name="T">The type of the object containing the field.</typeparam>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address of field.</returns>
        /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
        /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
        public static CodeInstruction Ldflda<T>(string fieldName) {
            return Ldflda(typeof(T), fieldName);
        }

        #endregion

        #region Get-Set

        /// <summary>
        /// Calls the getter method for the given <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The information about the property.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that gets the value of the property.</returns>
        /// <seealso cref="Call(System.Reflection.MethodInfo)"/>
        /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="property"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the property is write-only.</exception>
        public static CodeInstruction Get(PropertyInfo property) {
            if (property == null)
                throw new ArgumentNullException(nameof(property));
            return property.CanRead ? Call(property.GetMethod) : throw new InvalidOperationException($"Property {property.Name} is write-only.");
        }

        /// <summary>
        /// Calls the getter method for the given property.
        /// </summary>
        /// <param name="type">The type of the object containing the property.</param>
        /// <param name="propertyName">The name of the property to get.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that gets the value of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the property was not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the property is write-only.</exception>
        public static CodeInstruction Get(Type type, string propertyName) {
            return Get(AccessTools.Property(type, propertyName) ?? throw new ArgumentException($"No property found for type={type.FullName}, name={propertyName}"));
        }

        /// <summary>
        /// Calls the getter method for the given property.
        /// </summary>
        /// <param name="propertyName">The name of the property to get.</param>
        /// <typeparam name="T">The type of the object containing the property.</typeparam>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that gets the value of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the property was not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the property is write-only.</exception>
        /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
        public static CodeInstruction Get<T>(string propertyName) {
            return Get(typeof(T), propertyName);
        }

        /// <summary>
        /// Calls the setter method for the given <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The information about the property.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that sets the value of the property.</returns>
        /// <seealso cref="Call(System.Reflection.MethodInfo)"/>
        /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="property"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the property is read-only.</exception>
        public static CodeInstruction Set(PropertyInfo property) {
            if (property == null)
                throw new ArgumentNullException(nameof(property));
            return property.CanWrite ? Call(property.SetMethod) : new CodeInstruction(OpCodes.Stfld, property);
        }

        /// <summary>
        /// Calls the setter method for the given property.
        /// </summary>
        /// <param name="type">The type of the object containing the property.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that sets the value of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the property was not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the property is read-only.</exception>
        public static CodeInstruction Set(Type type, string propertyName) {
            return Set(AccessTools.Property(type, propertyName) ?? throw new ArgumentException($"No property found for type={type.FullName}, name={propertyName}"));
        }

        /// <summary>
        /// Calls the setter method for the given property.
        /// </summary>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <typeparam name="T">The type of the object containing the property.</typeparam>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that sets the value of the property.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the property was not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the property is read-only.</exception>
        /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
        public static CodeInstruction Set<T>(string propertyName) {
            return Set(typeof(T), propertyName);
        }

        #endregion

        #region Extensions

        /// <summary>
        /// Loads the specific local variable based on a LocalBuilder instance.
        /// </summary>
        /// <param name="local">The LocalBuilder that contains the variable's index.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the local variable.</returns>
        /// <seealso cref="OpCodes.Ldloc"/>
        /// <seealso cref="Ldloc(LocalBuilder)"/>
        public static CodeInstruction Load(this LocalBuilder local) => Ldloc(local);

        /// <summary>
        /// Stores the specific local variable based on a LocalBuilder instance.
        /// </summary>
        /// <param name="local">The LocalBuilder that contains the variable's index.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the local variable.</returns>
        /// <seealso cref="OpCodes.Stloc"/>
        /// <seealso cref="Stloc(LocalBuilder)"/>
        public static CodeInstruction Set(this LocalBuilder local) => Stloc(local);

        /// <summary>
        /// Loads the address of a specific local variable based on a LocalBuilder instance.
        /// </summary>
        /// <param name="local">The LocalBuilder that contains the variable's index.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address of local variable.</returns>
        /// <seealso cref="OpCodes.Ldloca"/>
        /// <seealso cref="Ldloca(LocalBuilder)"/>
        public static CodeInstruction LoadAddress(this LocalBuilder local) => Ldloca(local);

        /// <summary>
        /// Loads the integer value of the specified enum.
        /// </summary>
        /// <param name="e">The enum to load.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the enum.</returns>
        /// <seealso cref="Int"/>
        /// <seealso cref="LoadEnum"/>
        /// <seealso cref="OpCodes.Ldc_I4"/>
        public static CodeInstruction Load(this Enum e) => LoadEnum(e);

        /// <summary>
        /// Transfers control to a target instruction if the value is true, not null, or non-zero.
        /// </summary>
        /// <param name="label">The label of the instruction to jump to.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that jumps to the label if the value is truthy.</returns>
        /// <seealso cref="OpCodes.Brtrue"/>
        public static CodeInstruction True(this Label label) => new(OpCodes.Brtrue, label);

        /// <summary>
        /// Transfers control to a target instruction if value is false, a null reference, or zero.
        /// </summary>
        /// <param name="label">The label of the instruction to jump to.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that jumps to the label if the value is falsy.</returns>
        /// <seealso cref="OpCodes.Brfalse"/>
        public static CodeInstruction False(this Label label) => new(OpCodes.Brfalse, label);

        /// <summary>
        /// Unconditionally transfers control to a target instruction.
        /// </summary>
        /// <param name="label">The label of the instruction to jump to.</param>
        /// <returns>An <see cref="CodeInstruction">instruction</see> that jumps to the label.</returns>
        /// <seealso cref="OpCodes.Brfalse"/>
        public static CodeInstruction Jump(this Label label) => new(OpCodes.Br, label);

        /// <summary>
        /// Creates a LocalBuilder of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="generator">The ILGenerator object.</param>
        /// <typeparam name="T">The type of the new local variable.</typeparam>
        /// <returns>A LocalBuilder of type <typeparamref name="T"/>.</returns>
        public static LocalBuilder Local<T>(this ILGenerator generator) => generator.DeclareLocal(typeof(T));

        #endregion

        #region Instruction Searching

        /// <summary>
        /// Finds the index of the instruction in the list which contains the specified label.
        /// </summary>
        /// <param name="instructions">The list of instructions to execute the search in.</param>
        /// <param name="label">The label to find.</param>
        /// <returns>The index of the instruction.</returns>
        /// <seealso cref="CodeInstruction"/>
        public static int IndexOfInstructionWithLabel(this List<CodeInstruction> instructions, Label label) {
            return instructions.FindIndex(i => i.labels.Contains(label));
        }

        /// <summary>
        /// Finds the index of the instruction which calls the specific method.
        /// </summary>
        /// <param name="list">The list of instructions to execute the search in.</param>
        /// <param name="method">The information about the method.</param>
        /// <param name="start">The starting index of the search.</param>
        /// <returns>The index of the instruction.</returns>
        public static int FindCall(this List<CodeInstruction> list, MethodInfo method, int start = 0) {
            return FindCode(list, method.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, i => i.operand as MethodInfo == method, start);
        }

        /// <summary>
        /// Finds the index of the instruction which calls the specific method.
        /// </summary>
        /// <param name="list">The list of instructions to execute the search in.</param>
        /// <param name="type">The type of the object containing the method.</param>
        /// <param name="methodName">The name of the method to call.</param>
        /// <param name="parameters">The type parameters of the method.</param>
        /// <param name="generics">The generics used to call the method.</param>
        /// <param name="start">The starting index of the search.</param>
        /// <returns>The index of the instruction.</returns>
        /// <exception cref="ArgumentException">Thrown if the method was not found.</exception>
        public static int FindCall(this List<CodeInstruction> list, Type type, string methodName, Type[] parameters = null, Type[] generics = null, int start = 0) {
            return FindCall(list, AccessTools.Method(type, methodName, parameters, generics) ?? throw new ArgumentException($"No method found for type={type.FullName}, name={methodName}, parameters={parameters.Description()}, generics={generics.Description()}"), start);
        }

        /// <summary>
        /// Finds the index of the instruction which calls the specific method.
        /// </summary>
        /// <param name="list">The list of instructions to execute the search in.</param>
        /// <param name="methodName">The name of the method to call.</param>
        /// <param name="parameters">The type parameters of the method.</param>
        /// <param name="generics">The generics used to call the method.</param>
        /// <param name="start">The starting index of the search.</param>
        /// <typeparam name="T">The type of the object containing the method.</typeparam>
        /// <returns>The index of the instruction.</returns>
        /// <exception cref="ArgumentException">Thrown if the method was not found.</exception>
        public static int FindCall<T>(this List<CodeInstruction> list, string methodName, Type[] parameters = null, Type[] generics = null, int start = 0) {
            return FindCall(list, typeof(T), methodName, parameters, generics, start);
        }

        /// <summary>
        /// Finds the index of the instruction which loads the specific field.
        /// </summary>
        /// <param name="list">The list of instructions to execute the search in.</param>
        /// <param name="field">The information about the field.</param>
        /// <param name="start">The starting index of the search.</param>
        /// <returns>The index of the instruction.</returns>
        public static int FindField(this List<CodeInstruction> list, FieldInfo field, int start = 0) {
            return FindCode(list, field.IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld, i => i.operand as FieldInfo == field, start);
        }

        /// <summary>
        /// Finds the index of the instruction which loads the specific field.
        /// </summary>
        /// <param name="list">The list of instructions to execute the search in.</param>
        /// <param name="type">The type of the object containing the field.</param>
        /// <param name="fieldName">The name of the field to load.</param>
        /// <param name="start">The starting index of the search.</param>
        /// <returns>The index of the instruction.</returns>
        /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
        public static int FindField(this List<CodeInstruction> list, Type type, string fieldName, int start = 0) {
            return FindField(list, AccessTools.Field(type, fieldName) ?? throw new ArgumentException($"No field found for type={type.FullName}, name={fieldName}"), start);
        }

        /// <summary>
        /// Finds the index of the instruction which loads the specific field.
        /// </summary>
        /// <param name="list">The list of instructions to execute the search in.</param>
        /// <param name="fieldName">The name of the field to load.</param>
        /// <param name="start">The starting index of the search.</param>
        /// <typeparam name="T">The type of the object containing the field.</typeparam>
        /// <returns>The index of the instruction.</returns>
        /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
        /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
        public static int FindField<T>(this List<CodeInstruction> list, string fieldName, int start = 0) {
            return FindField(list, typeof(T), fieldName, start);
        }

        /// <summary>
        /// Finds the index of the instruction with a specific code and an optional check.
        /// </summary>
        /// <param name="list">The list of instructions.</param>
        /// <param name="code">The <see cref="OpCode"/> to find.</param>
        /// <param name="predicate">An additional check.</param>
        /// <param name="start">The starting index of the search.</param>
        /// <returns>The index of the instruction.</returns>
        public static int FindCode(this List<CodeInstruction> list, OpCode code, Predicate<CodeInstruction> predicate = null, int start = 0) {
            return list.FindIndex(start, i => i.opcode == code && (predicate?.Invoke(i) ?? true));
        }

        #endregion

    }

}
