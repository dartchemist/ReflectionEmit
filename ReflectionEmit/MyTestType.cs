namespace ReflectionEmit
{
    public class MyTestType
    {
        public string TestData { get; set; }
        public int TestAge { get; set; }

        public override string ToString()
        {
            return this.ToStringReflectionEmit();
        }
    }
}