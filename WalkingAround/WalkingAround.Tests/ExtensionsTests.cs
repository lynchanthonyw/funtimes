using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WalkingAround.Tests
{
    [TestClass()]
    public class ExtensionsTests
    {
        [TestMethod()]
        public void CreateInt()
        {
            byte temp = 0;

            if (Byte.Create(0, 0, 0, 0, 0, 0, 0, 0) != 0)
            { Assert.Fail("0"); }
            if (Byte.Create(0, 0, 0, 0, 0, 0, 0, 1) != 1)
            { Assert.Fail("1"); }
            if (Byte.Create(0, 0, 0, 0, 0, 0, 1, 0) != 2)
            { Assert.Fail("2"); }
            if (Byte.Create(0, 1, 0, 0, 0, 0, 0, 0) != 64)
            { Assert.Fail("64"); }
            if (Byte.Create(1, 0, 0, 0, 0, 0, 0, 0) != 128)
            { Assert.Fail("128"); }
            if (Byte.Create(1, 1, 1, 1, 1, 1, 1, 1) != 255)
            { Assert.Fail("255"); }
        }

        [TestMethod()]
        public void CreateBool()
        {
            byte temp = 0;

            if (Byte.Create(false, false, false, false, false, false, false, false) != 0)
            { Assert.Fail("0"); }
            if (Byte.Create(false, false, false, false, false, false, false, true) != 1)
            { Assert.Fail("1"); }
            if (Byte.Create(false, false, false, false, false, false, true, false) != 2)
            { Assert.Fail("2"); }
            if (Byte.Create(false, true, false, false, false, false, false, false) != 64)
            { Assert.Fail("64"); }
            if (Byte.Create(true, false, false, false, false, false, false, false) != 128)
            { Assert.Fail("128"); }
            if (Byte.Create(true, true, true, true, true, true, true, true) != 255)
            { Assert.Fail("255"); }
        }

        [TestMethod()]
        public void CreateTwoBit()
        {
            byte temp = 0;

            if (Byte.Create(0, 0, 0, 0) != 0)
            { Assert.Fail("0"); }
            if (Byte.Create(0, 0, 0, 1) != 1)
            { Assert.Fail("1"); }
            if (Byte.Create(0, 0, 0, 2) != 2)
            { Assert.Fail("2"); }
            if (Byte.Create(0, 2, 0, 0) != 32)
            { Assert.Fail("32"); }
            if (Byte.Create(1, 0, 0, 0) != 64)
            { Assert.Fail("64"); }
            if (Byte.Create(2, 0, 0, 0) != 128)
            { Assert.Fail("128"); }
            if (Byte.Create(3, 3, 3, 3) != 255)
            { Assert.Fail("255"); }
        }

        [TestMethod()]
        public void CreateNibbles()
        {
            byte temp = 0;

            if (Byte.Create(0, 0) != 0)
            { Assert.Fail("0"); }
            if (Byte.Create(0, 1) != 1)
            { Assert.Fail("1"); }
            if (Byte.Create(0, 2) != 2)
            { Assert.Fail("2"); }
            if (Byte.Create(0, 15) != 15)
            { Assert.Fail("15"); }
            if (Byte.Create(1, 0) != 16)
            { Assert.Fail("16"); }
            if (Byte.Create(2, 0) != 32)
            { Assert.Fail("32"); }
            if (Byte.Create(2, 1) != 33)
            { Assert.Fail("33"); }
            if (Byte.Create(4, 0) != 64)
            { Assert.Fail("64"); }
            if (Byte.Create(8, 0) != 128)
            { Assert.Fail("128"); }
            if (Byte.Create(15, 15) != 255)
            { Assert.Fail("255"); }
        }

        [TestMethod()]
        public void GetBitTest()
        {
            if (((byte)0).GetBit(1))
            {
                Assert.Fail("false>>1=false");
            }

            if (!((byte)255).GetBit(0))
            {
                Assert.Fail("255>>0=1");
            }

            if (!((byte)255).GetBit(1))
            {
                Assert.Fail("255>>1=1");
            }

            if (!((byte)255).GetBit(2))
            {
                Assert.Fail("255>>2=1");
            }

            if (!((byte)255).GetBit(4))
            {
                Assert.Fail("255>>4=1");
            }

            if (!((byte)255).GetBit(7))
            {
                Assert.Fail("255>>8=1");
            }
        }
    }
}