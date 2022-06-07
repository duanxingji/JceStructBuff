using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructBuff.jce.taf
{
    public class RequestPacket : JceStruct
    {
        private static Dictionary<string, string> cache_context = new Dictionary<string, string>();
        private static byte[] cache_sBuffer = new byte[] { 0 };
        public byte cPacketType = 0;
        public Dictionary<string, string> context = new Dictionary<string, string>();
        public int iMessageType = 0;
        public int iRequestId = 0;
        public int iTimeout = 0;
        public short iVersion = 0;
        public byte[] sBuffer = new byte[] { 0 };
        public String sFuncName = "";
        public String sServantName = "";
        public Dictionary<string, string> status = new Dictionary<string, string>();

        public override void display(StringBuilder stringBuilder, int index)
        {
            throw new NotImplementedException();
        }

        public override void readFrom(JceInputStream var1)
        {
            try
            {
                this.iVersion = var1.read(this.iVersion, 1, true);
                this.cPacketType = var1.read(this.cPacketType, 2, true);
                this.iMessageType = var1.read(this.iMessageType, 3, true);
                this.iRequestId = var1.read(this.iRequestId, 4, true);
                this.sServantName = var1.readString(5, true);
                this.sFuncName = var1.readString(6, true);

                this.sBuffer = var1.read(cache_sBuffer, 7, true);
                this.iTimeout = var1.read(this.iTimeout, 8, true);
                cache_context.Add("", "");
                this.context = var1.readMap(new Dictionary<string, string>(), cache_context, 9, true);

                this.status = var1.readMap(new Dictionary<string, string>(), cache_context, 10, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void writeTo(JceOutputStream var1)
        {
            var1.write(this.iVersion, 1);
            var1.write(this.cPacketType, 2);
            var1.write(this.iMessageType, 3);
            var1.write(this.iRequestId, 4);
            var1.write(this.sServantName, 5);
            var1.write(this.sFuncName, 6);
            var1.write(this.sBuffer, 7);
            var1.write(this.iTimeout, 8);
            var1.write(this.context, 9);
            var1.write(this.status, 10);
        }
    }
}