using Moq;
using SpaceBattle.Lib;

public class ReadTxtCommandTests
{
    [Fact]
    public void Execute_SkipsEmptyLines()
    {
        var type1 = "TypeC";
        var type2 = "TypeD";
        var mockAdder = new Mock<ITreeAdderElem>();

        var tempFilePath = $"{type1}and{type2}sets.txt";
        var testData = new[]
        {
        "1 2 3",
        "",
        "4 5 6",
        "   ",
        "7 8 9"
    };

        File.WriteAllLines(tempFilePath, testData);

        var command = new ReadTxtCommand(type1, type2, mockAdder.Object);

        command.Execute();

        mockAdder.Verify(a => a.Add(type1, type2, It.Is<int[]>(x => x.SequenceEqual(new[] { 1, 2, 3 }))), Times.Once);
        mockAdder.Verify(a => a.Add(type1, type2, It.Is<int[]>(x => x.SequenceEqual(new[] { 4, 5, 6 }))), Times.Once);
        mockAdder.Verify(a => a.Add(type1, type2, It.Is<int[]>(x => x.SequenceEqual(new[] { 7, 8, 9 }))), Times.Once);
        mockAdder.Verify(a => a.Add(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int[]>()), Times.Exactly(3));

        File.Delete(tempFilePath);
    }
}
