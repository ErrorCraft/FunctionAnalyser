using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating;
using ErrorCraft.Minecraft.Json.Validating.Validators;
using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ErrorCraft.Minecraft.Tests.Json.Validating.Validators;

[TestClass]
public class ArrayJsonValidatorTests {
    [TestMethod]
    public void Validate_IsSuccessful() {
        ArrayJsonValidator validator = new ArrayJsonValidator(false, new NumberJsonValidator(false));
        List<JsonNumber> items = new List<JsonNumber>() { new JsonNumber(1.0d), new JsonNumber(-1.5d) };
        Result<IJsonValidated> result = validator.Validate(new JsonArray(items), "");
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    public void Validate_IsSuccessful_WithoutItems() {
        ArrayJsonValidator validator = new ArrayJsonValidator(false, new NumberJsonValidator(false));
        Result<IJsonValidated> result = validator.Validate(new JsonArray(), "");
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    public void Validate_IsUnsuccessful_BecauseTypeIsIncorrect() {
        ArrayJsonValidator validator = new ArrayJsonValidator(false, new NumberJsonValidator(false));
        Result<IJsonValidated> result = validator.Validate(JsonNull.INSTANCE, "");
        Assert.IsFalse(result.Successful);
    }

    [TestMethod]
    public void Validate_IsUnsuccessful_BecauseItemTypeIsIncorrect() {
        ArrayJsonValidator validator = new ArrayJsonValidator(false, new NumberJsonValidator(false));
        List<IJsonElement> items = new List<IJsonElement>() { new JsonString("text") };
        Result<IJsonValidated> result = validator.Validate(new JsonArray(items), "");
        Assert.IsFalse(result.Successful);
    }
}
