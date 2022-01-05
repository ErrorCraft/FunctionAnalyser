using ErrorCraft.Minecraft.Json.Types;
using ErrorCraft.Minecraft.Json.Validating.Validators;
using ErrorCraft.Minecraft.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.Minecraft.Tests.Json.Validating.Validators;

[TestClass]
public class ArrayJsonValidatorTests {
    [TestMethod]
    public void Validate_IsSuccessful() {
        ArrayJsonValidator validator = new ArrayJsonValidator(false);
        Result result = validator.Validate(new JsonArray(), "");
        Assert.IsTrue(result.Successful);
    }

    [TestMethod]
    public void Validate_IsUnsuccessful_BecauseTypeIsIncorrect() {
        ArrayJsonValidator validator = new ArrayJsonValidator(false);
        Result result = validator.Validate(JsonNull.INSTANCE, "");
        Assert.IsFalse(result.Successful);
    }
}
