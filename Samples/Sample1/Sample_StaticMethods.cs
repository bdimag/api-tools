namespace ApiTools
{
    [ClientType("1d2338ca-bd57-409d-b4a1-d22f4a54f04b")]
    public class Sample_StaticMethods : ClientObject
    {
        [ClientMethod]
        private static void DoThings()
        { }

        [ClientMethod]
        private static Sample_StaticMethods GetExample()
        => new Sample_StaticMethods();

        [ClientMethod]
        private static string GetThings()
        => string.Empty;
    }
}