// See https://aka.ms/new-console-template for more information


using JceStructBuff.jce;
using JceStructBuff.jce.wup;
using JceStructTest.JceData;
using System.Text;

UserInfo userInfo = new UserInfo()
{
    UserName = "Xiaobai",
    UserState = 200,
    UserToken = new UserToken() { userToken = "344002781@qq.com" } 
};




UniPacket packet = new UniPacket();


packet.Put("_data", userInfo);
byte[] encode = packet.encode();
Console.WriteLine(Encoding.UTF8.GetString(encode));
JceCurrency jceCurrency = new JceCurrency(encode);
var AllJson = jceCurrency.GetAllJsonObj();
var AllJsonStr = AllJson.ToString();

var user_Info = jceCurrency.getByClass<UserInfo>("_data", new UserInfo());

Console.WriteLine(1);