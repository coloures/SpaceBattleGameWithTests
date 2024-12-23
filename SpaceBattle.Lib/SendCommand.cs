namespace SpaceBattle.Lib;
public class SendCommand : ICommand
{
    private readonly ICommand cmd;
    private readonly ISender sender;
    public SendCommand(ICommand command, ISender _sender)
    {
        cmd = command;
        sender = _sender;
    }
    public void Execute()
    {
        sender.Send(cmd);
    }
}
