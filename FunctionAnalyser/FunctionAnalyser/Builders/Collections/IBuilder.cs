using System.Collections.Generic;

namespace ErrorCraft.PackAnalyser.Builders.Collections {
    public interface IBuilder<T, U> {
        U Build(Dictionary<string, T> resources);
    }
}
