namespace WalkingAround
{
    public static partial class Byte
    {
        public static byte Create(bool bit1, bool bit2, bool bit3, bool bit4, bool bit5, bool bit6, bool bit7, bool bit8)
        {
            int val = 0;
            val += bit1 ? 128 : 0;
            val += bit2 ? 64 : 0;
            val += bit3 ? 32 : 0;
            val += bit4 ? 16 : 0;
            val += bit5 ? 8 : 0;
            val += bit6 ? 4 : 0;
            val += bit7 ? 2 : 0;
            val += bit8 ? 1 : 0;
            return (byte)val;
        }

        public static byte Create(int bit1, int bit2, int bit3, int bit4, int bit5, int bit6, int bit7, int bit8)
        {
            return Create(bit1 > 0, bit2 > 0, bit3 > 0, bit4 > 0, bit5 > 0, bit6 > 0, bit7 > 0, bit8 > 0);
        }

        public static byte Create(int bits1, int bits2, int bits3, int bits4)
        {
            int val = 0;
            byte temp = (byte)bits1;
            val += temp << 6;
            temp = (byte)bits2;
            val += temp << 4;
            temp = (byte)bits3;
            val += temp << 2;
            temp = (byte)bits4;
            val += temp;

            return (byte)val;
        }

        public static byte Create(int nibble1, int nibble2)
        {
            int val = 0;
            byte temp = (byte)nibble1;

            val += temp << 4;
            temp = (byte)nibble2;
            val += temp;

            return (byte)val;
        }

        public static bool GetBit(this byte current, int index)
        {
            return ((int)current >> index) % 2 == 1;
        }
    }
}