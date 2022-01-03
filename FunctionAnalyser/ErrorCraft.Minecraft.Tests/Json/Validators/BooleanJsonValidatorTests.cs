using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validators;
using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Validators;

[TestClass]
public class BooleanJsonValidatorTests {
    [TestMethod]
    public void Validate_IsSuccessful() {
        BooleanJsonValidator validator = new BooleanJsonValidator(false);
        Result result = validator.Validate(new JsonBoolean(true), "");
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    public void Validate_IsUnsuccessful_BecauseTypeIsIncorrect() {
        BooleanJsonValidator validator = new BooleanJsonValidator(false);
        Result result = validator.Validate(JsonNull.INSTANCE, "");
        Assert.IsFalse(result.Successful);
    }
}
