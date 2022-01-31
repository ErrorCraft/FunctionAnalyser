using System.Collections;
using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Json.Validating.Validated;

public class ValidatedJsonObject : IJsonValidated, IEnumerable<KeyValuePair<string, IJsonValidated>> {
    private readonly Dictionary<string, IJsonValidated> Items;

    public JsonValidatedType ValidatorType {
        get {
            return JsonValidatedType.OBJECT;
        }
    }

    public ValidatedJsonObject() {
        Items = new Dictionary<string, IJsonValidated>();
    }

    public ValidatedJsonObject(IDictionary<string, IJsonValidated> items) {
        Items = new Dictionary<string, IJsonValidated>(items);
    }

    public IJsonValidated this[string key] {
        get { return Items[key]; }
        set { Items[key] = value; }
    }

    public IEnumerator<KeyValuePair<string, IJsonValidated>> GetEnumerator() {
        foreach (KeyValuePair<string, IJsonValidated> pair in Items) {
            yield return pair;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    public void Add(string key, IJsonValidated value) {
        Items.Add(key, value);
    }
}
