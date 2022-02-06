using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating;
using ErrorCraft.Minecraft.Json.Validating.Validated;
using ErrorCraft.Minecraft.Json.Validating.Validators;
using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Tests.Json.Validating.Validators;

[TestClass]
public class ObjectJsonValidatorTests {
    [TestMethod]
    public void Validate_WithJsonElement_IsSuccessful() {
        ObjectJsonValidator validator = GetValidator();
        Dictionary<string, IJsonElement> items = new Dictionary<string, IJsonElement>() {
            { "key", new JsonNumber(-1.5d) }
        };
        IJsonElement json = new JsonObject(items);
        Result<IJsonValidated> result = validator.Validate(json, "");
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    public void Validate_WithJsonObject_IsSuccessful() {
        ObjectJsonValidator validator = GetValidator();
        Dictionary<string, IJsonElement> items = new Dictionary<string, IJsonElement>() {
            { "key", new JsonNumber(-1.5d) }
        };
        JsonObject json = new JsonObject(items);
        Result<ValidatedJsonObject> result = validator.Validate(json, "");
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    public void Validate_IsUnsuccessful_BecauseTypeIsIncorrect() {
        ObjectJsonValidator validator = GetValidator();
        Result<IJsonValidated> result = validator.Validate(JsonNull.INSTANCE, "");
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Validate_IsUnsuccessful_BecauseItemIsNotPresent() {
        ObjectJsonValidator validator = GetValidator();
        JsonObject json = new JsonObject();
        Result<ValidatedJsonObject> result = validator.Validate(json, "");
        Assert.IsFalse(result.Successful);
    }

    private static ObjectJsonValidator GetValidator() {
        Dictionary<string, JsonValidator> children = new Dictionary<string, JsonValidator>() {
            { "key", new NumberJsonValidator(false) }
        };
        return new ObjectJsonValidator(false, children);
    }
}
