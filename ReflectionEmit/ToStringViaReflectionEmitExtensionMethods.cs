using System;
using System.Collections.Generic;

namespace ReflectionEmit
{
    public static class ToStringViaReflectionEmitExtensionMethods
    {
        private static Lazy<ReflectionEmitMethodGenerator> _generator =
            new Lazy<ReflectionEmitMethodGenerator>();
        private static Dictionary<Type, Delegate> _methods =
            new Dictionary<Type, Delegate>();

        public static string ToStringReflectionEmit<T>(this T @this)
        {
            var targetType = @this.GetType();

            if (!_methods.ContainsKey(targetType))
            {
                _methods.Add(targetType, 
                    _generator.Value.Generate<T>());
            }

            return (_methods[targetType] as Func<T, string>)(@this);
        } 
    }
}