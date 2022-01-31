using System.Collections;
using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Json.Validating.Validated;

public class ValidatedJsonArray : IJsonValidated, IEnumerable<IJsonValidated> {
    private readonly List<IJsonValidated> Items;

    public JsonValidatedType ValidatorType {
        get {
            return JsonValidatedType.ARRAY;
        }
    }

    public ValidatedJsonArray() {
        Items = new List<IJsonValidated>();
    }

    public ValidatedJsonArray(IEnumerable<IJsonValidated> other) {
        Items = new List<IJsonValidated>(other);
    }

    public IJsonValidated this[int index] {
        get { return Items[index]; }
        set { Items[index] = value; }
    }

    public IEnumerator<IJsonValidated> GetEnumerator() {
        foreach (IJsonValidated item in Items) {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public void Add(IJsonValidated item) {
        Items.Add(item);
    }
}
