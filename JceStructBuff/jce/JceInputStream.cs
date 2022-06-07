
using JceStructBuff.jce.dynamic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructBuff.jce
{
    public class JceInputStream
    {
        private ByteBuffer bs;
        protected Encoding sServerEncoding = Encoding.UTF8;
        private HeadData hd = new HeadData();

        #region 构造函数
        public JceInputStream()
        {
            this.bs = new ByteBuffer(new MemoryStream());
        }

        public JceInputStream(ByteBuffer byteBuffer)
        {
            this.bs = byteBuffer;
        }

        public JceInputStream(MemoryStream memoryStream)
        {
            this.bs = new ByteBuffer(memoryStream);
        }

        public JceInputStream(byte[] b)
        {
            this.bs = ByteBuffer.Wrap(b);
        }

        public JceInputStream(byte[] b, int index)
        {
            this.bs = ByteBuffer.Wrap(b);
            this.bs.Position = index;
        }

        #endregion 构造函数

        public object read(object var1, int var2, bool var3)
        {
            if (var1 is byte)
            {
                return (byte)this.read((int)0, var2, var3);
            }
            else if (var1 is bool)
            {
                return this.read(false, var2, var3);
            }
            else if (var1 is short)
            {
                return this.read((short)0, var2, var3);
            }
            else if (var1 is int)
            {
                return this.read((int)0, var2, var3);
            }
            else if (var1 is long)
            {
                return this.read(0L, var2, var3);
            }
            else if (var1 is float)
            {
                return this.read(0.0F, var2, var3);
            }
            else if (var1 is double)
            {
                return this.read(0.0D, var2, var3);
            }
            else if (var1 is string)
            {
                return this.readString(var2, var3);
            }
            else if (var1 is IDictionary)
            {
                return this.readMap(new Dictionary<object, object>(), (IDictionary)var1, var2, var3);
            }
            else if (var1 is JceStruct)
            {
                return this.read((JceStruct)var1, var2, var3);
            }
            else if (var1.GetType().IsArray)
            {
                if (var1 is byte[] ByteArray)
                {
                    return this.read(ByteArray, var2, var3);
                }
                else
                {
                    if (var1 is bool[] BoolArray)
                    {
                        return read(BoolArray, var2, var3);
                    }
                    else if (var1 is short[] ShortArray)
                    {
                        return read(ShortArray, var2, var3);
                    }
                    else if (var1 is int[] IntArray)
                    {
                        return read(IntArray, var2, var3);
                    }
                    else if (var1 is long[] LongArray)
                    {
                        return read(LongArray, var2, var3);
                    }
                    else if (var1 is float[] FloatArray)
                    {
                        return read(FloatArray, var2, var3);
                    }
                    else if (var1 is double[] DoubleArray)
                    {
                        return read(DoubleArray, var2, var3);
                    }
                    else
                    {
                        return readArray((IList)var1, var2, var3);
                    }
                }
            }
            else if (var1 is IList)
            {
                return this.readArray((IList)var1, var2, var3);
            }
            else
            {
                throw new Exception("read object error: unsupport type.");
            }
        }

        public double[] read(double[] l, int tag, bool isRequire)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 9:
                    int size = read(0, 0, true);
                    if (size < 0)
                    {
                        throw new Exception("size invalid: " + size);
                    }
                    double[] lr = new double[size];
                    for (int i = 0; i < size; i++)
                    {
                        lr[i] = read(lr[0], 0, true);
                    }
                    return lr;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public float[] read(float[] l, int tag, bool isRequire)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 9:
                    int size = read(0, 0, true);
                    if (size < 0)
                    {
                        throw new Exception("size invalid: " + size);
                    }
                    float[] lr = new float[size];
                    for (int i = 0; i < size; i++)
                    {
                        lr[i] = read(lr[0], 0, true);
                    }
                    return lr;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public long[] read(long[] l, int tag, bool isRequire)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 9:
                    int size = read(0, 0, true);
                    if (size < 0)
                    {
                        throw new Exception("size invalid: " + size);
                    }
                    long[] lr = new long[size];
                    for (int i = 0; i < size; i++)
                    {
                        lr[i] = read(lr[0], 0, true);
                    }
                    return lr;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public int[] read(int[] l, int tag, bool isRequire)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 9:
                    int size = read(0, 0, true);
                    if (size < 0)
                    {
                        throw new Exception("size invalid: " + size);
                    }
                    int[] lr = new int[size];
                    for (int i = 0; i < size; i++)
                    {
                        lr[i] = read(lr[0], 0, true);
                    }
                    return lr;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public short[] read(short[] l, int tag, bool isRequire)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 9:
                    int size = read(0, 0, true);
                    if (size < 0)
                    {
                        throw new Exception("size invalid: " + size);
                    }
                    short[] lr = new short[size];
                    for (int i = 0; i < size; i++)
                    {
                        lr[i] = read(lr[0], 0, true);
                    }
                    return lr;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public bool[] read(bool[] l, int tag, bool isRequire)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 9:
                    int size = read(0, 0, true);
                    if (size < 0)
                    {
                        throw new Exception("size invalid: " + size);
                    }
                    bool[] lr = new bool[size];
                    for (int i = 0; i < size; i++)
                    {
                        lr[i] = read(lr[0], 0, true);
                    }
                    return lr;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public byte[] read(byte[] l, int tag, bool isRequire)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 9:
                    int size = read(0, 0, true);
                    if (size < 0)
                    {
                        throw new Exception("size invalid: " + size);
                    }
                    byte[] lr = new byte[size];
                    for (int i = 0; i < size; i++)
                    {
                        lr[i] = read(lr[0], 0, true);
                    }
                    return lr;

                case 13:
                    readHead(hd);
                    if (hd.type != 0)
                    {
                        throw new Exception("type mismatch, tag: " + tag + ", type: " + ((int)hd.type) + ", " + ((int)hd.type));
                    }
                    int size2 = read(0, 0, true);
                    if (size2 < 0)
                    {
                        throw new Exception("invalid size, tag: " + tag + ", type: " + ((int)hd.type) + ", " + ((int)hd.type) + ", size: " + size2);
                    }
                    byte[] lr2 = this.bs.ReadBytes(size2);
                    return lr2;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public byte read(byte c, int tag, bool isRequire)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 0:
                    byte c2 = this.bs.Get();
                    return c2;

                case 12:
                    return 0;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public float read(float n, int tag, bool isRequire)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 4:
                    float n2 = this.bs.GetFloat();
                    return n2;

                case 12:
                    return 0.0f;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public double read(double n, int tag, bool isRequire)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 4:
                    double n2 = bs.GetFloat();
                    return n2;

                case 5:
                    double n3 = bs.GetDouble();
                    return n3;

                case 12:
                    return 0.0d;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public int read(int var1, int var2, bool var3)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 0:
                    int n2 = this.bs.Get();
                    return n2;

                case 1:
                    int n3 = this.bs.GetShort();
                    return n3;

                case 2:
                    int n4 = this.bs.GetInt();
                    return n4;

                case 12:
                    return 0;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public long read(long var1, int var3, bool var4)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 0:
                    long n2 = bs.Get();
                    return n2;

                case 1:
                    long n3 = bs.GetShort();
                    return n3;

                case 2:
                    long n4 = bs.GetInt();
                    return n4;

                case 3:
                    long n5 = bs.GetLong();
                    return n5;

                case 12:
                    return 0;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public JceStruct read(JceStruct o, int tag, bool isRequire)
        {
            JceStruct jce;
            if (o == null || isRequire)
            {
                object? obj = Activator.CreateInstance(o.GetType());
                if (obj is not null)
                {
                    jce = (JceStruct)obj;
                }
                else
                {
                    throw new Exception("JceStruct type mismatch.");
                }
            }
            else
                jce = o;
            readHead(hd);
            if (hd.type != 10)
            {
                throw new Exception("type mismatch.");
            }
            jce.readFrom(this);
            skipToStructEnd();
            return jce;
        }
        private void skipToStructEnd()
        {
            do
            {
                readHead(hd);
            } while (hd.type != 11);
        }
        public short read(short s, int i, bool z)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 0:
                    short s2 = (short)this.bs.Get();
                    return s2;

                case 1:
                    short s3 = this.bs.GetShort();
                    return s3;

                case 12:
                    return 0;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public bool read(bool var1, int var2, bool var3)
        {
            var1 = false;
            if ((byte)this.read((int)0, var2, var3) != 0)
            {
                var1 = true;
            }

            return var1;
        }

        public String readString(int tag, bool isRequire)
        {
            String s = null;
            readHead(hd);
            switch (hd.type)
            {
                case 6:
                    int len = bs.Get();
                    if (len < 0)
                    {
                        len += 256;
                    }
                    byte[] ss = this.bs.ReadBytes(len);
                    s = sServerEncoding.GetString(ss);
                    break;

                case 7:
                    int len2 = this.bs.GetInt();
                    if (len2 <= 104857600 && len2 >= 0)
                    {
                        byte[] ss2 = this.bs.ReadBytes(len2);

                        s = sServerEncoding.GetString(ss2);
                        break;
                    }
                    else
                    {
                        throw new Exception("String too long: " + len2);
                    }
                default:
                    throw new Exception("type mismatch.");
            }

            return s;
        }

        public Dictionary<K, V> readMap<K, V>(Dictionary<K, V> mr, IDictionary m, int tag, bool isRequire)
        {
            if (m == null)
            {
                return new Dictionary<K, V>();
            }

            IDictionaryEnumerator dictionary = m.GetEnumerator();
            dictionary.MoveNext();
            K k = (K)dictionary.Key;
            V v = (V)dictionary.Value;

            readHead(hd);
            switch (hd.type)
            {
                case 8:
                    int size = read(0, 0, true);
                    if (size < 0)
                    {
                        throw new Exception("size invalid: " + size);
                    }
                    for (int i = 0; i < size; i++)
                    {
                        mr.Add((K)read(k, 0, true), (V)read(v, 1, true));
                    }
                    return mr;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public IList readArray(IList l, int tag, bool isRequire)
        {
            if (l is null || l.Count == 0)
            {
                return new ArrayList();
            }
            object? objArray = l[0];
            if (objArray is null)
            {
                return new ArrayList();
            }
            Object[] _readArrayImpl = readArrayImpl(objArray, tag, isRequire);
            if (_readArrayImpl is null)
            {
                return new ArrayList();
            }
            ArrayList arrayList = new ArrayList();
            foreach (Object obj in _readArrayImpl)
            {
                arrayList.Add(obj);
            }
            return arrayList;
        }

        private object[] readArrayImpl(object var1, int var2, bool var3)
        {
            readHead(hd);
            switch (hd.type)
            {
                case 9:
                    int size = read(0, 0, true);
                    if (size < 0)
                    {
                        throw new Exception("size invalid: " + size);
                    }

                    object[] lr = (object[])Array.CreateInstance(var1.GetType(), size);
                    for (int i = 0; i < size; i++)
                    {
                        lr[i] = read(var1, 0, true);
                    }
                    return lr;

                default:
                    throw new Exception("type mismatch.");
            }
        }

        public int readHead(HeadData paramHeadData)
        {
            byte b = bs.Get();
            paramHeadData.type = (byte)(b & 0xF);
            paramHeadData.tag = (b & 0xF0) >> 4;
            if (paramHeadData.tag == 15)
            {
                paramHeadData.tag = bs.Get();
                return 2;
            }
            return 1;
        }

        public void warp(byte[] var1)
        {
            if (this.bs != null)
            {
                this.bs.Clear();
            }

            this.bs = ByteBuffer.Wrap(var1);
        }


    }
}