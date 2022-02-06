﻿using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating;
using ErrorCraft.Minecraft.Json.Validating.Validators;
using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Validating.Validators;

[TestClass]
public class NumberJsonValidatorTests {
    [TestMethod]
    public void Validate_IsSuccessful() {
        NumberJsonValidator validator = new NumberJsonValidator(false);
        Result<IJsonValidated> result = validator.Validate(new JsonNumber(10.0), "");
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    public void Validate_IsUnsuccessful_BecauseTypeIsIncorrect() {
        NumberJsonValidator validator = new NumberJsonValidator(false);
        Result<IJsonValidated> result = validator.Validate(JsonNull.INSTANCE, "");
        Assert.IsFalse(result.Successful);
    }
}
