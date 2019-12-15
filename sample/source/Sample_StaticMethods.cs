using System.Collections.Generic;
using System.Linq;

namespace ApiTools
{
    [ClientType("1d2338ca-bd57-409d-b4a1-d22f4a54f04b")]
    public static class Sample_StaticMethods
    {
        /// <summary>
        /// This does things.
        /// </summary>
        [ClientMethod]
        public static void DoThings()
        { }

        /// <summary>
        /// This gets a sample.
        /// </summary>
        /// <returns>Sample which was got.</returns>
        [ClientMethod]
        public static Sample_Class1 GetSample()
            => new Sample_Class1();
            
        /// <summary>
        /// This gets a collection of sample.
        /// </summary>
        /// <returns>Samples which were got.</returns>
        [ClientMethod]
        public static IEnumerable<Sample_Class1> GetSamples()
            => Enumerable.Empty<Sample_Class1>();

        /// <summary>
        /// This gets a thing.
        /// </summary>
        /// <returns>Thing that was got.</returns>
        [ClientMethod]
        public static string GetThing()
            => string.Empty;
    }
}