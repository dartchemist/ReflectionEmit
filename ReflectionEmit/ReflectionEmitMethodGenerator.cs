using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;

namespace ReflectionEmit
{
    public sealed class ReflectionEmitMethodGenerator
    {
        private AssemblyBuilder Assembly { get; set; }
        private ModuleBuilder Module { get; set; }
        private AssemblyName AssemblyName { get; set; }

        public ReflectionEmitMethodGenerator()
        {
            AssemblyName = new AssemblyName
            {
                Name = Guid.NewGuid().ToString("N")
            };
            Assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(AssemblyName, AssemblyBuilderAccess.Run);
            Module = Assembly.DefineDynamicModule(AssemblyName.Name);
        }

        public Func<T, string> Generate<T>()
        {
            var target = typeof (T);
            var targetType = Module.DefineType(
                string.Concat(target.Namespace, ".", target.Name));
            var method = targetType.DefineMethod("MyToString",
                MethodAttributes.Static | MethodAttributes.Public, typeof (string), new[] {target});

            method.GetILGenerator().Generate(target);

            var createdType = targetType.CreateType();

            var createdMethod = createdType.GetMethod("MyToString");
            return (Func<T, string>)Delegate.CreateDelegate(
                typeof (Func<T, string>), createdMethod);
        } 
    }
}
