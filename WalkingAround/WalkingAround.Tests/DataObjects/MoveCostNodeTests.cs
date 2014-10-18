using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WalkingAround.DataObjects.Tests
{
    [TestClass()]
    public class MoveCostNodeTests
    {
        [TestMethod()]
        public void MoveCostNodeTest()
        {
            MoveCostNode mcn = new MoveCostNode("1(1)", new Map());
            if (mcn.Status == 0)
                Assert.Fail();
        }
    }
}