using JceStructBuff.jce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructTest.JceData
{
    public class UserToken : JceStruct
    {
        public string userToken { get; set; } = string.Empty;
        public override void display(StringBuilder stringBuilder, int index)
        {
            throw new NotImplementedException();
        }

        public override void readFrom(JceInputStream jceInput)
        {
            userToken = (string)jceInput.read(userToken, 0, true);
        }

        public override void writeTo(JceOutputStream jceOut)
        {
            jceOut.write(userToken, 0);
        }
    }
}
