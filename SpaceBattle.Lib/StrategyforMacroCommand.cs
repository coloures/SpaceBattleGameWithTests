namespace SpaceBattle.Lib;
using App;

public class CreateMacroCommandStrategy(string commandSpec)
{
    public ICommand Resolve(params object [] args)
    {
        var names_of_cmds = Ioc.Resolve<string[]>($"Specs.{commandSpec}");
        var cmds = names_of_cmds.Select((x) => Ioc.Resolve<SpaceBattle.Lib.ICommand>((string)x, args[0])).ToArray();
        return Ioc.Resolve<SpaceBattle.Lib.MacroCommand>("Command.Macro", cmds);
    }
}