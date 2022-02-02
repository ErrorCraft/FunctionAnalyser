﻿using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Util;
using ErrorCraft.Minecraft.Util.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ErrorCraft.Minecraft.Json.Validating;

public abstract class JsonValidator {
    protected readonly bool Optional;

    protected JsonValidator(bool optional) {
        Optional = optional;
    }

    public abstract Result<IJsonValidated> Validate(IJsonElement json, string name);

    public abstract class Serialiser<T> : IJsonSerialiser<T> where T : JsonValidator {
        public T FromJson(JObject json, JsonSerializer serialiser) {
            bool optional = json.GetBoolean("optional", false);
            return FromJson(json, serialiser, optional);
        }

        public abstract T FromJson(JObject json, JsonSerializer serialiser, bool optional);
    }
}
