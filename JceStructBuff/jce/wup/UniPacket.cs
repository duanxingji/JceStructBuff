
using JceStructBuff.jce.taf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructBuff.jce.wup
{
    public class UniPacket : UniAttribute
    {
        private static Dictionary<String, Dictionary<String, byte[]>>? cache__tempdata = null;
        private static Dictionary<String, byte[]>? newCache__tempdata = null;
        protected Dictionary<String, Object> cachedClassName = new Dictionary<String, Object>();
        protected RequestPacket _package = new RequestPacket();
        private int oldRespIret = 0;

        public UniPacket()
        {
           // this._package.iVersion = 2;
            base.useVersion3();
            this._package.iVersion = 3;
        }

        public void Put(String var1, object var2)
        {
            if (var1.StartsWith("."))
            {
                throw new Exception("put name can not startwith . , now is " + var1);
            }
            else
            {
                base.put(var1, var2);
            }
        }

      /*  public T getByClass<T>(string str, T t)
        {
            return base.getByClass(str, t);


        }*/
        public void readFrom(JceInputStream var1)
        {
            this._package.readFrom(var1);
        }

        public void decode(byte[] var1)
        {
            if (var1.Length < 4)
            {
                throw new Exception("decode package must include size head");
            }
            else
            {
                try
                {
                    JceInputStream var4 = new JceInputStream(var1, 4);
                    this.readFrom(var4);
                    if (this._package.iVersion == 3)
                    {
                        var4 = new JceInputStream(this._package.sBuffer);
                        if (newCache__tempdata == null)
                        {
                            newCache__tempdata = new Dictionary<String, byte[]>();
                            newCache__tempdata.Add("", new byte[0]);
                        }

                        this._newData = var4.readMap(new Dictionary<String, byte[]>(), newCache__tempdata, 0, false);
                    }
                    else
                    {
                       this._newData = null;
                        var4 = new JceInputStream(this._package.sBuffer);
                        if (cache__tempdata == null)
                        {
                            cache__tempdata = new Dictionary<String, Dictionary<String, byte[]>>();
                            Dictionary<String, byte[]> var2 = new Dictionary<String, byte[]>();
                            var2.Add("", new byte[0]);
                            cache__tempdata.Add("", var2);
                        }

                        this._data = var4.readMap(new Dictionary<String, Dictionary<String, byte[]>>(), cache__tempdata, 0, false);
                        this.cachedClassName = new Dictionary<string, object>();
                    }
                }
                catch (Exception var3)
                {
                    Exception var31 = var3;
                    throw var31;
                }
            }
        }

        public byte[] encode()
        {
            if (this._package.iVersion == 2)
            {
                if (this._package.sServantName == null || this._package.sServantName.Equals(""))
                {
                    throw new Exception("servantName can not is null");
                }

                if (this._package.sFuncName == null || this._package.sFuncName.Equals(""))
                {
                    throw new Exception("funcName can not is null");
                }
            }
            else
            {
                if (this._package.sServantName == null)
                {
                    this._package.sServantName = "";
                }

                if (this._package.sFuncName == null)
                {
                    this._package.sFuncName = "";
                }
            }

            JceOutputStream var2 = new JceOutputStream(0);
            if (this._package.iVersion == 2)
            {
                var2.write(this._data, 0);
            }
            else
            {
                var2.write(this._newData, 0);
            }
            this._package.sBuffer = JceUtil.getJceBufArray(var2.getByteBuffer());
            var2 = new JceOutputStream(0);
            this.writeTo(var2);
            byte[] var4 = JceUtil.getJceBufArray(var2.getByteBuffer());
            int var1 = var4.Length;
            ByteBuffer var3 = ByteBuffer.Allocate(var1 + 4);
            var3.PutInt(var1 + 4);
            var3.Put(var4);
            var3.Flip();
            return var3.ToArray();
        }

        public void setFuncName(String var1)
        {
            this._package.sFuncName = var1;
        }

        public void setOldRespIret(int var1)
        {
            this.oldRespIret = var1;
        }

        public void setRequestId(int var1)
        {
            this._package.iRequestId = var1;
        }

        public void setServantName(String var1)
        {
            this._package.sServantName = var1;
        }

        public void UseVersion3()
        {
            base.useVersion3();
            this._package.iVersion = 3;
        }

        public void SetiRequestId(int id)
        {
            this._package.iRequestId = id;
        }

        public void writeTo(JceOutputStream var1)
        {
            this._package.writeTo(var1);
        }
    }
}