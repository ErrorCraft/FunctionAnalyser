using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public interface IBuilder<T, U>
    {
        U Build(Dictionary<string, T> resources);
    }
}
