# JceStructBuff

#### 介绍
基于Net实现的JceStruct数据结构序列化、反序列化

#### 使用说明
序列化的实体类需继承 JceStruct 接口

```
    public abstract class JceStruct
    {
        public abstract void display(StringBuilder stringBuilder, int index);

        public abstract void readFrom(JceInputStream jceInput);

        public abstract void writeTo(JceOutputStream jceOut);
    }
```
序列化对象

```
UniPacket packet = new UniPacket();

packet.Put("_data", userInfo);
byte[] encode = packet.encode();
```
反序列化数据取实体对象

```
JceCurrency jceCurrency = new JceCurrency(encode);
var user_Info = jceCurrency.getByClass<UserInfo>("_data", new UserInfo());
```
可将全部数据转为json格式展示

```
JceCurrency jceCurrency = new JceCurrency(encode);
var AllJson = jceCurrency.GetAllJsonObj();
var AllJsonStr = AllJson.ToString();
```

