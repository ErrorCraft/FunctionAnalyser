namespace ErrorCraft.Minecraft.Util;

public class Result<T> {
    public bool Successful { get; }
    public T? Value { get; }
    public Message? Message { get; }

    private Result(bool successful, T? value, Message? message) {
        Successful = successful;
        Value = value;
        Message = message;
    }

    public static Result<T> Success(T value) {
        return new Result<T>(true, value, null);
    }

    public static Result<T> Failure(Message message) {
        return new Result<T>(false, default, message);
    }
}
