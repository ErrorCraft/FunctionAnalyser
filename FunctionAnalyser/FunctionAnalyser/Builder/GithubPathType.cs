using System.Runtime.Serialization;

namespace FunctionAnalyser.Builder
{
    public enum GithubPathType
    {
        [EnumMember(Value = "file")]
        File = 0,
        [EnumMember(Value = "dir")]
        Directory = 1
    }
}
