using NUnit.Framework;

namespace ProjectMarsAutomationAdvanceTask.AssertHelpers
{
    public static class ProfileAssertHelper
    {
        public static void AssertSuccessToast(string expectedMessage, string actualMessage)
        {
            Assert.That(actualMessage, Is.EqualTo(expectedMessage),
                $"Success toast message is correct. Expected: '{expectedMessage}', Actual: '{actualMessage}'");
        }
    }
}
