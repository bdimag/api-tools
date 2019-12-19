namespace ApiTools
{
    /// <summary>
    /// This is the first sample class.
    /// </summary>
    [ClientType("19b323ed-b8a4-4b9b-8907-2c37b75fb195")]
    public class Sample_Class1
    {
        /// <summary>
        /// Sample property with a complex-type value.
        /// </summary>
        [ClientProperty]
        public Sample_Class2 Sample_ComplexProp { get; set; }

        /// Sample property with a simple value. This documenation has no xml.
        [ClientProperty]
        public string Sample_ValueProp { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Sample_Class1"/> type.
        /// </summary>
        public Sample_Class1()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Sample_Class1"/> type with the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A sample value that will be set to <see cref="Sample_ValueProp"/>.</param>
        public Sample_Class1(string value)
        {
            Sample_ValueProp = value;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Sample_Class1"/> type with the specified <paramref name="sample"/>.
        /// </summary>
        /// <param name="sample">A sample value that will be set to <see cref="Sample_ComplexProp"/>.</param>
        public Sample_Class1(Sample_Class2 sample)
        {
            Sample_ComplexProp = sample;
        }

        /// <summary>
        /// This does things.
        /// </summary>
        [ClientMethod]
        public void DoThings()
        { }
    }
}