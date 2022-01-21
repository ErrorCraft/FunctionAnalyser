using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Util.Extensions;

internal static class QueueExtensions {
    public static void Enqueue<T>(this Queue<T> queue, IEnumerable<T> items) {
        foreach (T item in items) {
            queue.Enqueue(item);
        }
    }
}
