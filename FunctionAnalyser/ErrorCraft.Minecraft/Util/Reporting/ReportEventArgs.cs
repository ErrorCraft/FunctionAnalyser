using System;

namespace ErrorCraft.Minecraft.Util.Reporting;

public class ReportEventArgs : EventArgs {
    public string Message { get; }

    public ReportEventArgs(string message) {
        Message = message;
    }
}
