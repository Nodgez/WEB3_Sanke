using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using Newtonsoft.Json;

namespace evm.net.Models
{
    public class BigIntegerArrayHexConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            StringBuilder writeString = new StringBuilder("0x");

            var values = (BigInteger[])value;

            foreach ( var v in values )
                writeString.Append(v.ToString("X"));
            
            writer.WriteValue(writeString.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                string str = reader.Value?.ToString();
                if (str == null)
                {
                    str = reader.ReadAsString();
                    if (str == null)
                        throw new JsonSerializationException();
                }

                if (str == "0x")
                    return new BigInteger[1] { BigInteger.Zero };

                int subStringStart = 0;
                BigInteger[] values = new BigInteger[(str.Length - 2)/64];
                if (str.StartsWith("0x"))
                {
                    subStringStart = 2;
                    while (subStringStart < str.Length)
                    {
                        var bi = str.Substring(subStringStart, 64);
                        values[subStringStart / 64] = BigInteger.Parse(bi, NumberStyles.AllowHexSpecifier);

                        subStringStart += 64;
                    }
                }
                else
                {
                    while (subStringStart < str.Length)
                    {
                        var bi = str.Substring(subStringStart, 64);
                        values[subStringStart / 64] = BigInteger.Parse(bi, NumberStyles.AllowHexSpecifier);

                        subStringStart += 64;
                    }
                }

                return values;
            } 
            else if (reader.TokenType == JsonToken.Integer)
            {
                var num = reader.ReadAsInt32();
                if (num == null)
                    throw new JsonSerializationException();
                
                return new BigInteger((int)num);
            }

            throw new JsonSerializationException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(BigInteger[]);
        }
    }
}