using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CommandParser.Parsers.JsonParser
{
    public enum JsonArgumentType
    {
        [EnumMember(Value = "null"), Display(Name = "Null")]
        Null = 1,
        [EnumMember(Value = "boolean"), Display(Name = "Boolean")]
        Boolean = 2,
        [EnumMember(Value = "number"), Display(Name = "Number")]
        Number = 3,
        [EnumMember(Value = "string"), Display(Name = "String")]
        String = 4,
        [EnumMember(Value = "object"), Display(Name = "Object")]
        Object = 5,
        [EnumMember(Value = "array"), Display(Name = "Array")]
        Array = 6
    }
}
