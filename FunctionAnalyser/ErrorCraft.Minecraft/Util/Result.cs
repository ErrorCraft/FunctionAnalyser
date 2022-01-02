using System;

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

    public static Result<T> Failure<U>(Result<U> other) {
        return new Result<T>(false, default, other.Message);
    }

    public static Result<T> From<U>(Result<U> other, Func<U, T> converter) {
        T? convertedValue = other.Successful ? converter(other.Value!) : default;
        return new Result<T>(other.Successful, convertedValue, other.Message);
    }
}
