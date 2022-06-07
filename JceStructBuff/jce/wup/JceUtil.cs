using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructBuff.jce.wup
{
    public class JceUtil
    {
        public static byte[] getJceBufArray(ByteBuffer var0)
        {
            byte[] var1 = new byte[var0.Position];
            Buffer.BlockCopy(var0.ToArray(), 0, var1, 0, var1.Length);
            return var1;
        }
    }
}