using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WalkingAround.DataObjects.Tests
{
    [TestClass()]
    public class MapTests
    {
        [TestMethod()]
        public void ToFileTest()
        {
            var mp = new Map(50, 50);
            string str = mp.ToFile();
            Assert.Fail();
        }
    }
}