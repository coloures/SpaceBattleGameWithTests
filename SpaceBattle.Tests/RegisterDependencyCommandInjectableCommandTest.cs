using App;
using App.Scopes;
namespace SpaceBattle.Tests;
public class RegisterDependencyCommandInjectableCommandTest
{
    public RegisterDependencyCommandInjectableCommandTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }
}
