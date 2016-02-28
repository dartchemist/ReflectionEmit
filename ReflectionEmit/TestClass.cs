using System;

namespace ReflectionEmit
{
    public static class TestClass
    {
        static void Main()
        {
            var testType = new MyTestType
            {
                TestAge = 24,
                TestData = "My Test Data"
            };
            Console.WriteLine(testType);
        }
    }
}