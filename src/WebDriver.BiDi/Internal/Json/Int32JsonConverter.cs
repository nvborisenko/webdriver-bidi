using System;

namespace OpenQA.Selenium.BiDi.Internal.Json
{
    internal class Int32JsonConverter : System.Text.Json.Serialization.JsonConverter<int>
    {
        public override int Read(ref System.Text.Json.Utf8JsonReader reader, Type type, System.Text.Json.JsonSerializerOptions options)
        {
            var valDouble = reader.GetDouble();
            var valInt = (int)valDouble;
            if (valDouble == valInt)
                return valInt;
            return reader.GetInt32();
        }

        public override void Write(System.Text.Json.Utf8JsonWriter writer, int value, System.Text.Json.JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
