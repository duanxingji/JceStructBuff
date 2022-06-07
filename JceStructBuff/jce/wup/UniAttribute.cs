using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructBuff.jce.wup
{
    public class UniAttribute : OldUniAttribute
    {
        JceInputStream _is = new JceInputStream();
        protected Dictionary<String, byte[]> _newData = null;
        private Dictionary<String, object> cachedData = new Dictionary<string, object>();

        protected void put(String var1, object var2)
        {
            if (this._newData != null)
            {
                if (var1 == null)
                {
                    throw new Exception("put key can not is null");
                }
                else if (var2 == null)
                {
                    throw new Exception("put value can not is null");
                }
                else if (var2 is ICollection)
                {
                    throw new Exception("can not support ICollection");
                }
                else
                {
                    JceOutputStream var3 = new JceOutputStream();
                    var3.write(var2, 0);
                    byte[] var4 = JceUtil.getJceBufArray(var3.getByteBuffer());
                    this._newData.Add(var1, var4);
                }
            }
            else
            {
                //  super.put(var1, var2);
            }
        }

        public T? getByClass<T>(string str, T t)
        {

            if (this._newData != null)
            {
                if (!this._newData.ContainsKey(str))
                {
                    return default(T);
                }
                if (this.cachedData.ContainsKey(str))
                {
                    return (T)this.cachedData[str];
                }
                try
                {
                    T t2 = (T)decodeData(this._newData[str], t);
                    if (t2 != null)
                    {
                        saveDataCache(str, t2);
                    }
                    return t2;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else if (!_data.ContainsKey(str))
            {
                return default(T);
            }
            else
            {
                if (this.cachedData.ContainsKey(str))
                {
                    return (T)this.cachedData[str];
                }
                byte[] bArr = new byte[0];
                var var4 = _data[str];
                var var11 = var4.GetEnumerator();
                if (var11.MoveNext())
                {
                    bArr = var11.Current.Value;

                }
                this._is.warp(bArr);
                T t2 = (T)this._is.read(t, 0, true);
                saveDataCache(str, t2);
                return t2;

            }
        }
        private void saveDataCache(String str, Object obj)
        {
            this.cachedData.Add(str, obj);
        }

        private Object decodeData(byte[] var1, Object var2)
        {
            this._is.warp(var1);
            return this._is.read(var2, 0, true);
        }
        protected void useVersion3()
        {
            this._newData = new Dictionary<string, byte[]>();
        }
    }
}