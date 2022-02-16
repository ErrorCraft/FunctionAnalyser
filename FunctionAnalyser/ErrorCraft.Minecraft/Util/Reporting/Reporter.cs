namespace ErrorCraft.Minecraft.Util.Reporting;

public class Reporter {
    public event ReportEvent? OnMessage;

    public void Message(string message) {
        ReportEventArgs args = new ReportEventArgs(message);
        OnMessage?.Invoke(args);
    }
}
