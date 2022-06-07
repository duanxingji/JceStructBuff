using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructBuff.jce.wup
{
    public class OldUniAttribute
    {
        protected Dictionary<String, Dictionary<String, byte[]>> _data = new Dictionary<string, Dictionary<string, byte[]>>();

        /* public void put(String var1, object var2)
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
                 throw new Exception("can not support Set");
             }
             else
             {
                 JceOutputStream var3 = new JceOutputStream();
                 var3.write(var2, 0);
                 byte[] var6 = JceUtil.getJceBufArray(var3.getByteBuffer());
                 Dictionary<String, byte[]> var4 = new Dictionary<String, byte[]>(1);
                 ArrayList var5 = new ArrayList(1);
                 this.checkObjectType(var5, var2);
                 var4.put(BasicClassTypeUtil.transTypeList(var5), var6);
                 this.cachedData.remove(var1);
                 this._data.put(var1, var4);
             }
         }*/
    }
}