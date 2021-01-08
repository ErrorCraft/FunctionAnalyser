using System.Runtime.Serialization;

namespace FunctionAnalyser.Builders
{
    public enum PathType
    {
        [EnumMember(Value = "file")]
        File = 0,
        [EnumMember(Value = "dir")]
        Directory = 1
    }
}
