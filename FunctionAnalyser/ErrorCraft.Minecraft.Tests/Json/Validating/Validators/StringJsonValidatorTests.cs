using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating;
using ErrorCraft.Minecraft.Json.Validating.Validators;
using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Validating.Validators;

[TestClass]
public class StringJsonValidatorTests {
    [TestMethod]
    public void Validate_IsSuccessful() {
        StringJsonValidator validator = new StringJsonValidator(false);
        Result<IJsonValidated> result = validator.Validate(new JsonString("text"), "");
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    public void Validate_IsUnsuccessful_BecauseTypeIsIncorrect() {
        StringJsonValidator validator = new StringJsonValidator(false);
        Result<IJsonValidated> result = validator.Validate(JsonNull.INSTANCE, "");
        Assert.IsFalse(result.Successful);
    }
}
