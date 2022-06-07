using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructBuff.jce
{
    public class JceOutputStream
    {
        private ByteBuffer bs;
        protected Encoding sServerEncoding = Encoding.UTF8;

        #region 构造函数

        public JceOutputStream() : this(128)
        {
        }

        public JceOutputStream(int var1)
        {
            this.bs = ByteBuffer.Allocate(var1);
        }

        public JceOutputStream(ByteBuffer var1)
        {
            this.bs = var1;
        }

        #endregion 构造函数

        #region write

        public void write(object obj, int index)
        {
            if (obj is byte)
                write((byte)obj, index);
            else if (obj is bool)
                write((bool)obj, index);
            else if (obj is short)
                write((short)obj, index);
            else if (obj is int)
                write((int)obj, index);
            else if (obj is long)
                write((long)obj, index);
            else if (obj is float)
                write((float)obj, index);
            else if (obj is double)
                write((double)obj, index);
            else if (obj is string)
                write((string)obj, index);
            else if (obj is IDictionary)
                write((IDictionary)obj, index);
            else if (obj is IList)
                write((IList)obj, index);
            else if (obj is JceStruct)
                write((JceStruct)obj, index);
            else if (obj is byte[])
                write((byte[])obj, index);
            else if (obj is bool[])
                write((bool[])obj, index);
            else if (obj is short[])
                write((short[])obj, index);
            else if (obj is int[])
                write((int[])obj, index);
            else if (obj is long[])
                write((long[])obj, index);
            else if (obj is float[])
                write((float[])obj, index);
            else if (obj is double[])
                write((double[])obj, index);
            else if (obj.GetType().IsArray)
                writeArray((object[])obj, index);
            else if (obj is ICollection)
                write((ICollection)obj, index);
            else
                new Exception("解析错误:" + obj.GetType().FullName);
        }

        public void write(byte var1, int var2)
        {
            this.reserve(3);
            if (var1 == 0)
            {
                this.writeHead((byte)12, var2);
            }
            else
            {
                this.writeHead((byte)0, var2);
                this.bs.Put(var1);
            }
        }

        public void write(bool var1, int var2)
        {
            this.write((byte)(var1 ? 1 : 0), var2);
        }

        public void write(short var1, int var2)
        {
            this.reserve(4);
            if (var1 >= -128 && var1 <= 127)
            {
                this.write((byte)var1, var2);
            }
            else
            {
                this.writeHead((byte)1, var2);
                this.bs.PutShort(var1);
            }
        }

        public void write(int var1, int var2)
        {
            this.reserve(6);
            if (var1 >= -32768 && var1 <= 32767)
            {
                this.write((short)var1, var2);
            }
            else
            {
                this.writeHead((byte)2, var2);
                this.bs.PutInt(var1);
            }
        }

        public void write(long var1, int var3)
        {
            this.reserve(10);
            if (var1 >= -2147483648L && var1 <= 2147483647L)
            {
                this.write((int)var1, var3);
            }
            else
            {
                this.writeHead(3, var3);
                this.bs.PutLong(var1);
            }
        }

        public void write(float var1, int var2)
        {
            this.reserve(6);
            this.writeHead((byte)4, var2);
            this.bs.PutFloat(var1);
        }

        public void write(double var1, int var3)
        {
            this.reserve(10);
            this.writeHead((byte)5, var3);
            this.bs.PutDouble(var1);
        }

        public void write(string s, int tag)
        {
            byte[] by = sServerEncoding.GetBytes(s);
            reserve(by.Length + 10);
            if (by.Length > 255)
            {
                writeHead((byte)7, tag);
                this.bs.PutInt(by.Length);
                this.bs.Put(by);
                return;
            }
            writeHead((byte)6, tag);
            this.bs.Put((byte)by.Length);
            this.bs.Put(by);
        }

        public void write(IDictionary var1, int var2)
        {
            reserve(8);
            writeHead(8, var2);
            var2 = var1 == null ? 0 : var1.Count;
            write(var2, 0);
            if (var1 != null)
            {
                foreach (var key in var1.Keys)
                {
                    if (key is not null)
                    {
                        object obj = var1[key];
                        if (obj is not null)
                        {
                            write(key, 0);
                            write(obj, 1);
                        }
                    }
                }
            }
        }

        public void write(IList var1, int var2)
        {
            this.reserve(8);
            this.writeHead(9, var2);
            write(var1 == null ? 0 : var1.Count, 0);
            if (var1 != null)
            {
                IEnumerator ie = var1.GetEnumerator();
                while (ie.MoveNext())
                {
                    write(ie.Current, 0);
                }
            }
        }

        public void write(JceStruct var1, int var2)
        {
            this.reserve(2);
            this.writeHead(10, var2);
            var1.writeTo(this);
            this.reserve(2);
            this.writeHead(11, 0);
        }

        public void write(byte[] var1, int var2)
        {
            this.reserve(var1.Length + 8);
            this.writeHead(13, var2);
            this.writeHead(0, 0);
            this.write(var1.Length, 0);
            this.bs.Put(var1);
        }

        public void write(bool[] var1, int var2)
        {
            this.reserve(8);
            this.writeHead(9, var2);
            this.write(var1.Length, 0);
            int var3 = var1.Length;

            for (var2 = 0; var2 < var3; ++var2)
            {
                this.write(var1[var2], 0);
            }
        }

        public void write(short[] var1, int var2)
        {
            this.reserve(8);
            this.writeHead(9, var2);
            this.write(var1.Length, 0);
            int var3 = var1.Length;

            for (var2 = 0; var2 < var3; ++var2)
            {
                this.write(var1[var2], 0);
            }
        }

        public void write(int[] var1, int var2)
        {
            this.reserve(8);
            this.writeHead(9, var2);
            this.write(var1.Length, 0);
            int var3 = var1.Length;

            for (var2 = 0; var2 < var3; ++var2)
            {
                this.write(var1[var2], 0);
            }
        }

        public void write(long[] var1, int var2)
        {
            this.reserve(8);
            this.writeHead(9, var2);
            this.write((int)var1.Length, 0);
            int var3 = var1.Length;
            for (var2 = 0; var2 < var3; ++var2)
            {
                this.write(var1[var2], 0);
            }
        }

        public void write(float[] var1, int var2)
        {
            this.reserve(8);
            this.writeHead(9, var2);
            this.write(var1.Length, 0);
            int var3 = var1.Length;
            for (var2 = 0; var2 < var3; ++var2)
            {
                this.write(var1[var2], 0);
            }
        }

        public void write(double[] var1, int var2)
        {
            this.reserve(8);
            this.writeHead(9, var2);
            this.write(var1.Length, 0);
            int var3 = var1.Length;
            for (var2 = 0; var2 < var3; ++var2)
            {
                this.write(var1[var2], 0);
            }
        }

        public void write(ICollection var1, int var2)
        {
            this.reserve(8);
            this.writeHead((byte)9, var2);
            write(var1 == null ? 0 : var1.Count, 0);
            if (var1 != null)
            {
                IEnumerator ie = var1.GetEnumerator();
                while (ie.MoveNext())
                {
                    write(ie.Current, 0);
                }
            }
        }

        private void writeArray(object[] var1, int var2)
        {
            this.reserve(8);
            this.writeHead(9, var2);
            this.write(var1.Length, 0);
            int var3 = var1.Length;

            for (var2 = 0; var2 < var3; ++var2)
            {
                this.write(var1[var2], 0);
            }
        }

        public void writeHead(byte var1, int var2)
        {
            byte var3;
            if (var2 < 15)
            {
                var3 = (byte)(var2 << 4 | var1);
                this.bs.Put(var3);
            }
            else if (var2 < 256)
            {
                var3 = (byte)(var1 | 240);
                this.bs.Put(var3);
                this.bs.Put((byte)var2);
            }
            else
            {
                //   throw new JceEncodeException("tag is too large: " + var2);
            }
        }

        #endregion write

        public ByteBuffer getByteBuffer()
        {
            return this.bs;
        }

        public void reserve(int var1)
        {
            if (this.bs.Remaining < var1)
            {
                ByteBuffer var2 = ByteBuffer.Allocate((this.bs.Capacity + var1) * 2);
                var2.Put(this.bs.ToArray(), 0, (int)this.bs.Position);
                this.bs = var2;
            }
        }
    }
}