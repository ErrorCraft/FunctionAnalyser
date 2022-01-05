using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating.Validators;
using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Validating.Validators;

[TestClass]
public class ObjectJsonValidatorTests {
    [TestMethod]
    public void Validate_IsSuccessful() {
        ObjectJsonValidator validator = new ObjectJsonValidator(false);
        Result result = validator.Validate(new JsonObject(), "");
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    public void Validate_IsUnsuccessful_BecauseTypeIsIncorrect() {
        ObjectJsonValidator validator = new ObjectJsonValidator(false);
        Result result = validator.Validate(JsonNull.INSTANCE, "");
        Assert.IsFalse(result.Successful);
    }
}
