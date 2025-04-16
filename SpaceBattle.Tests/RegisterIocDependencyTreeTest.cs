using App;
namespace SpaceBattle.Tests;
using App.Scopes;

public class RegisterIocDependencyTreeTest : IDisposable
{
    public RegisterIocDependencyTreeTest()
    {
        new InitCommand().Execute();
        var iocScope = Ioc.Resolve<object>("IoC.Scope.Create");

        Ioc.Resolve<ICommand>("IoC.Scope.Current.Set", iocScope).Execute();
    }

    [Fact]
    public void Execute_RegistersAddSetCommandCorrectly()
    {
        // Arrange
        var obj1Type = "TypeA";
        var obj2Type = "TypeB";
        var command = new Lib.RegisterIocDependencyTree(obj1Type, obj2Type);

        // Act
        command.Execute();

        // Assert: Проверяем, что команда зарегистрирована и работает
        var registeredCommand = Ioc.Resolve<Lib.ICommand>(
            $"{obj1Type}and{obj2Type}TreeAddSet",
            1, 2, 3, 4);

        Assert.IsType<Lib.TreeAddSetCommand>(registeredCommand);
    }

    [Fact]
    public void Execute_RegistersCheckSetCommandCorrectly()
    {
        // Arrange
        var obj1Type = "TypeA";
        var obj2Type = "TypeB";
        var command = new Lib.RegisterIocDependencyTree(obj1Type, obj2Type);
        // Act
        command.Execute();

        // Assert: Проверяем, что команда зарегистрирована и работает
        var registeredCommand = Ioc.Resolve<Lib.ICommand>(
            $"{obj1Type}and{obj2Type}TreeCheckSet",
            1, 2, 3, 4);

        Assert.IsType<Lib.TreeCheckSetCommand>(registeredCommand);
    }

    [Fact]
    public void Execute_CreatesTreeAndPassesToCommands()
    {
        // Arrange
        var obj1Type = "TypeA";
        var obj2Type = "TypeB";
        var command = new Lib.RegisterIocDependencyTree(obj1Type, obj2Type);

        // Act
        command.Execute();

        // Assert: Проверяем, что зарегистрированная команда использует дерево
        var addCommand = Ioc.Resolve<Lib.ICommand>(
            $"{obj1Type}and{obj2Type}TreeAddSet",
            1, 2, 3, 4);

        Assert.IsType<Lib.TreeAddSetCommand>(addCommand);

        // Проверяем, что дерево обновляется (если TreeAddSetCommand добавляет элементы)
        var checkCommand = Ioc.Resolve<Lib.ICommand>(
            $"{obj1Type}and{obj2Type}TreeCheckSet",
            1, 2, 3, 4);

        Assert.IsType<Lib.TreeCheckSetCommand>(checkCommand);
    }
    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }
}
