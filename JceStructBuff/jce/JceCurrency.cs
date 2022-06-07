
using JceStructBuff.ForceJSON;
using JceStructBuff.jce.dynamic;
using JceStructBuff.jce.wup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JceStructBuff.jce
{
    public class JceCurrency : UniPacket
    {
        public const int BYTE = 0;
        public const int DOUBLE = 5;
        public const int FLOAT = 4;
        public const int INT = 2;
        public const int JCE_MAX_STRING_LENGTH = 104857600;
        public const int LIST = 9;
        public const int LONG = 3;
        public const int MAP = 8;
        public const int SHORT = 1;
        public const int SIMPLE_LIST = 13;
        public const int STRING1 = 6;
        public const int STRING4 = 7;
        public const int STRUCT_BEGIN = 10;
        public const int STRUCT_END = 11;
        public const int ZERO_TAG = 12;
        private ByteBuffer bs;
        protected Encoding sServerEncoding = Encoding.UTF8;

        public JceCurrency(byte[] bys)
        {
            bs = ByteBuffer.Wrap(bys);
        }

        public JSONObject GetAllJsonObj()
        {
            JSONObject jsonObject = new JSONObject();
            return GetAllJsonObj(jsonObject);
        }

        public JSONObject GetAllJsonObj(JSONObject jsonObject)
        {
            base.decode(bs.ToArray());
            if (this._package.iVersion == 3)
            {
                foreach (var item in _newData.Keys)
                {
                    bs = ByteBuffer.Wrap(this._newData[item]);
                    jsonObject.Put(item, AnalysisBytes());
                }
            }
            else if (this._package.iVersion == 2)
            {
                foreach (var item in _data.Keys)
                {
                    var var4 = _data[item];
                    var var11 = var4.GetEnumerator();
                    if (var11.MoveNext())
                    {
                        bs = ByteBuffer.Wrap(var11.Current.Value);
                        jsonObject.Put(item, AnalysisBytes());
                    }
                }
            }
            return jsonObject;
        }

        public JSONObject AnalysisBytes()
        {
            JSONObject jsonObject = new JSONObject();
            int i = 0;
            while (true)
            {
                HeadData hd = new HeadData();
                if (!readHead(hd))
                {
                    return jsonObject;
                }
                i++;
                switch (hd.type)
                {
                    case BYTE:
                        byte b = bs.Get();
                        jsonObject.Put("byte" + i, b);
                        break;

                    case SHORT:
                        short aShort = bs.GetShort();
                        jsonObject.Put("Short" + i, aShort);
                        break;

                    case INT:
                        int anInt = bs.GetInt();
                        jsonObject.Put("int" + i, anInt);
                        break;

                    case LONG:
                        long aLong = bs.GetLong();
                        jsonObject.Put("Long" + i, aLong);
                        break;

                    case FLOAT:
                        float aFloat = bs.GetFloat();
                        jsonObject.Put("Float" + i, aFloat);
                        break;

                    case DOUBLE:
                        double aDouble = bs.GetDouble();
                        jsonObject.Put("Double" + i, aDouble);
                        break;

                    case STRING1:
                        String readString1 = readString(hd);
                        jsonObject.Put("String1" + i, readString1);
                        break;

                    case STRING4:
                        String readString4 = readString(hd);
                        jsonObject.Put("String4" + i, readString4);
                        break;

                    case MAP:
                        JSONObject json = new JSONObject();
                        jsonObject.Put("Map" + i, json);
                        readMap(hd, json);
                        break;

                    case LIST:
                        JSONArray jsonList = new JSONArray();
                        jsonObject.Put("List" + i, jsonList);
                        readList(jsonList, hd);
                        break;

                    case STRUCT_BEGIN:
                        JSONObject jsonStr = new JSONObject();
                        jsonObject.Put("STRUCT" + i, jsonStr);
                        readJceStruct(jsonStr);
                        bs.Position = bs.Position - 1;
                        break;

                    case STRUCT_END:
                        return jsonObject;

                    case ZERO_TAG:
                        jsonObject.Put("ZERO_TAG" + i, 0);
                        break;

                    case SIMPLE_LIST:
                        readHead(hd);
                        int _read = read(hd);
                        byte[] lr2 = bs.ReadBytes(_read);
                        if (i == 7)
                            jsonObject.Put("SIMPLE_LIST" + i, Encoding.UTF8.GetString(lr2));
                        else
                        {
                            String encodeToString = Convert.ToBase64String(lr2);
                            jsonObject.Put("SIMPLE_LIST" + i, encodeToString);
                        }
                        break;
                }
            }
        }

        private JSONObject readMap(HeadData hd, JSONObject jsonObject)
        {
            JSONObject jsonObject1 = new JSONObject();
            switch (hd.type)
            {
                default:
                    Console.WriteLine("Map提取错误！！");
                    break;

                case 8:
                    int size = read(hd);
                    if (size < 0)
                    {
                        Console.WriteLine("Map提取错误之狗size小于0");
                        return jsonObject;
                    }
                    for (int i = 0; i < size; i++)
                    {
                        jsonObject.Put((String)read(jsonObject, hd, i), read(jsonObject1, hd, i));
                    }
                    break;
            }
            return jsonObject;
        }

        private JSONArray readList(JSONArray jsonObject, HeadData hd)
        {
            JSONObject jsonObject1 = new JSONObject();
            switch (hd.type)
            {
                default:
                    Console.WriteLine("List提取错误！！");
                    break;

                case 9:
                    int size = read(hd);
                    if (size < 0)
                    {
                        Console.WriteLine("List提取错误之狗size小于0");
                    }
                    for (int i = 0; i < size; i++)
                    {
                        jsonObject.Put(read(jsonObject1, hd, i));
                    }
                    break;
            }
            return jsonObject;
        }

        public Object read(JSONObject jsonObject, HeadData hd, int i)
        {
            readHead(hd);
            switch (hd.type)
            {
                case BYTE:
                    byte b = bs.Get();
                    return b;

                case SHORT:
                    short aShort = bs.GetShort();
                    return aShort;

                case INT:
                    int anInt = bs.GetInt();
                    return anInt;

                case LONG:
                    long aLong = bs.GetLong();
                    return aLong;

                case FLOAT:
                    float aFloat = bs.GetFloat();
                    return aFloat;

                case DOUBLE:
                    double aDouble = bs.GetDouble();
                    return aDouble;

                case STRING1:
                    String readString1 = readString(hd);
                    return readString1;

                case STRING4:
                    String readString4 = readString(hd);
                    return readString4;

                case MAP:
                    JSONObject json = new JSONObject();
                    jsonObject.Put("Map1" + i, json);
                    return readMap(hd, json);

                case LIST:
                    JSONArray jsonList = new JSONArray();
                    jsonObject.Put("List" + i, jsonList);
                    return readList(jsonList, hd);

                case STRUCT_BEGIN:
                    JSONObject jsonStr = new JSONObject();
                    jsonObject.Put("STRUCT" + i, jsonStr);
                    return readJceStruct(jsonStr);

                case STRUCT_END:
                    return jsonObject;

                case ZERO_TAG:
                    jsonObject.Put("ZERO_TAG" + i, 0);
                    break;

                case SIMPLE_LIST:
                    readHead(hd);
                    int _read = read(hd);
                    byte[] lr2 = bs.ReadBytes(_read);
                    if (i == 7)
                        jsonObject.Put("SIMPLE_LIST" + i, Encoding.UTF8.GetString(lr2));
                    else
                    {
                        String encodeToString = Convert.ToBase64String(lr2);
                        jsonObject.Put("SIMPLE_LIST" + i, encodeToString);
                    }

                    break;
            }
            return jsonObject;
        }

        private JSONObject readJceStruct(JSONObject jsonObject)
        {
            int i = 0;
            while (true)
            {
                HeadData hd = new HeadData();
                readHead(hd);
                i++;
                switch (hd.type)
                {
                    case BYTE:
                        byte b = bs.Get();
                        jsonObject.Put("byte" + i, b);
                        break;

                    case SHORT:
                        short aShort = bs.GetShort();
                        jsonObject.Put("Short" + i, aShort);
                        break;

                    case INT:
                        int anInt = bs.GetInt();
                        jsonObject.Put("int" + i, anInt);
                        break;

                    case LONG:
                        long aLong = bs.GetLong();
                        jsonObject.Put("Long" + i, aLong);
                        break;

                    case FLOAT:
                        float aFloat = bs.GetFloat();
                        jsonObject.Put("Float" + i, aFloat);
                        break;

                    case DOUBLE:
                        double aDouble = bs.GetDouble();
                        jsonObject.Put("Double" + i, aDouble);
                        break;

                    case STRING1:
                        String readString1 = readString(hd);
                        jsonObject.Put("String1" + i, readString1);
                        break;

                    case STRING4:
                        String readString4 = readString(hd);
                        jsonObject.Put("String4" + i, readString4);
                        break;

                    case MAP:
                        JSONObject json = new JSONObject();
                        jsonObject.Put("Map" + i, json);
                        readMap(hd, json);
                        break;

                    case LIST:
                        JSONArray jsonList = new JSONArray();
                        jsonObject.Put("List", jsonList);
                        readList(jsonList, hd);
                        break;

                    case STRUCT_BEGIN:
                        JSONObject jsonStr = new JSONObject();
                        jsonObject.Put("STRUCT" + i, jsonStr);
                        readJceStruct(jsonStr);
                        break;

                    case STRUCT_END:
                        return jsonObject;

                    case ZERO_TAG:
                        jsonObject.Put("ZERO_TAG" + i, 0);
                        break;

                    case SIMPLE_LIST:
                        readHead(hd);
                        int _read = read(hd);
                        byte[] lr2 = bs.ReadBytes(_read);
                        if (i == 7)
                            jsonObject.Put("SIMPLE_LIST" + i, Encoding.UTF8.GetString(lr2));
                        else
                        {
                            String encodeToString = Convert.ToBase64String(lr2);
                            jsonObject.Put("SIMPLE_LIST" + i, encodeToString);
                        }
                        break;
                }
            }
        }

        public int read(HeadData hd)
        {
            readHead(hd);
            int ret = 0;
            switch (hd.type)
            {
                case 0:
                    ret = this.bs.Get();
                    break;

                case 1:
                    ret = this.bs.GetShort();
                    break;

                case 2:
                    ret = this.bs.GetInt();
                    break;

                case 12:
                    ret = 0;
                    break;

                default:
                    Console.WriteLine("ReadInt默认错误！");
                    break;
            }
            return ret;
        }

        public String readString(HeadData hd)
        {
            String s = string.Empty;

            switch (hd.type)
            {
                case 6:
                    int len = this.bs.Get();
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
                        Console.WriteLine("String too long: " + len2);
                    }
                    break;
            }
            return s;
        }

        private bool readHead(HeadData paramHeadData)
        {
            try
            {
                if (bs.Position == bs.Length)
                    return false;
                byte b = bs.Get();
                paramHeadData.type = (byte)(b & 0xF);
                paramHeadData.tag = (b & 0xF0) >> 4;
                if (paramHeadData.tag == 15)
                {
                    paramHeadData.tag = bs.Get();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}