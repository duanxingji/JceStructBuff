using JceStructBuff.jce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructTest.JceData
{
    public class UserInfo : JceStruct
    {
        public string UserName { get; set; } = string.Empty;
        public int UserState { get; set; }
        public UserToken UserToken { get; set; }
        public override void display(StringBuilder stringBuilder, int index)
        {
            throw new NotImplementedException();
        }

        public override void readFrom(JceInputStream jceInput)
        {
            UserName = (string)jceInput.read(UserName, 0, true);
            UserState = jceInput.read(UserState, 1, true);
            UserToken = (UserToken)jceInput.read(new UserToken(), 2, true);
        }

        public override void writeTo(JceOutputStream jceOut)
        {
            jceOut.write(UserName, 0);
            jceOut.write(UserState, 1);
            jceOut.write(UserToken, 2);
        }
    }
}
