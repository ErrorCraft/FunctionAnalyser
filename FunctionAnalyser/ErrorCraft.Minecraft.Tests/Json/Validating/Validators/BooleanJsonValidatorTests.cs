using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating;
using ErrorCraft.Minecraft.Json.Validating.Validators;
using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Validating.Validators;

[TestClass]
public class BooleanJsonValidatorTests {
    [TestMethod]
    public void Validate_IsSuccessful() {
        BooleanJsonValidator validator = new BooleanJsonValidator(false);
        Result<IJsonValidated> result = validator.Validate(new JsonBoolean(true), "");
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    public void Validate_IsUnsuccessful_BecauseTypeIsIncorrect() {
        BooleanJsonValidator validator = new BooleanJsonValidator(false);
        Result<IJsonValidated> result = validator.Validate(JsonNull.INSTANCE, "");
        Assert.IsFalse(result.Successful);
    }
}
