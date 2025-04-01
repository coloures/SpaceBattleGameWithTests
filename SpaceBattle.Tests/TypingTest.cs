namespace SpaceBattle.Tests;
using Moq;
using SpaceBattle.Lib;
public class TypingTest
{
    [Fact]
    public void TestWithoutError()
    {
        var any_dict1 = new Dictionary<string, object>();
        var any_dict2 = new Dictionary<string, object>();
        any_dict2.Add("Type", "Bullet");
        var typing1 = new Typing(any_dict1);
        var typing2 = new Typing(any_dict2);
        typing1.Type = "Torpedo";
        typing2.Type = "Torpedo";

        Assert.Equal("Torpedo", any_dict1["Type"]);
        Assert.Equal("Torpedo", any_dict2["Type"]);
    }
    [Fact]
    public void TestWithError()
    {
        var any_dict1 = new Mock<IDictionary<string, object>>();
        any_dict1.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<object>())).Throws<Exception>().Verifiable();
        any_dict1.Setup(x => x.ContainsKey(It.IsAny<string>())).Returns(false);
        var any_dict2 = new Mock<IDictionary<string, object>>();
        any_dict2.SetupSet(x => x["Type"] = It.IsAny<object>()).Throws<Exception>().Verifiable();
        any_dict2.Setup(x => x.ContainsKey(It.IsAny<string>())).Returns(true);
        var any_dict3 = new Mock<IDictionary<string, object>>();
        any_dict3.Setup(x => x.ContainsKey(It.IsAny<string>())).Throws<Exception>().Verifiable();
        var typing1 = new Typing(any_dict1.Object);
        var typing2 = new Typing(any_dict2.Object);
        var typing3 = new Typing(any_dict3.Object);
        Assert.ThrowsAny<Exception>(() => typing1.Type = "Torpedo");
        Assert.ThrowsAny<Exception>(() => typing2.Type = "Torpedo");
        Assert.ThrowsAny<Exception>(() => typing3.Type = "Torpedo");
        any_dict1.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
        any_dict2.VerifySet(x => x["Type"] = It.IsAny<object>(), Times.Once);
        any_dict3.Verify(x => x.ContainsKey(It.IsAny<string>()), Times.Once);
    }
}
