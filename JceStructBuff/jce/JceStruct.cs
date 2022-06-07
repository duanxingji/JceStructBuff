using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructBuff.jce
{
    [Serializable]
    public abstract class JceStruct
    {
        public abstract void display(StringBuilder stringBuilder, int index);

        public abstract void readFrom(JceInputStream jceInput);

        public abstract void writeTo(JceOutputStream jceOut);
    }
}