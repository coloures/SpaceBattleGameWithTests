namespace SpaceBattle.Lib;
using System.IO;
using System.Linq;

public class ReadTxtCommand : ICommand
{
    public readonly string type1;
    public readonly string type2;
    public readonly ITreeAdderElem adder;
    public ReadTxtCommand(string type1, string type2, ITreeAdderElem adder)
    {
        this.type1 = type1;
        this.type2 = type2;
        this.adder = adder;
    }
    public void Execute()
    {
        var filepath = $"{type1}and{type2}sets.txt";
        var data = File.ReadAllLines(filepath)
                       .Where(line => !string.IsNullOrWhiteSpace(line))
                       .Select(line => line.Split(' ')
                                            .Select(int.Parse)
                                            .ToArray())
                       .ToList();
        data.ForEach(Set => adder.Add(type1, type2, Set));
    }
}
