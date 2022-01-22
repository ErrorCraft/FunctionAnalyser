using System;
using System.Diagnostics.CodeAnalysis;

namespace ErrorCraft.Minecraft.Util;

public class Result {
    [MemberNotNullWhen(false, nameof(Message))]
    public bool Successful { get; }
    public Message? Message { get; }

    private Result(bool successful, Message? message) {
        Successful = successful;
        Message = message;
    }

    public static Result Success() {
        return new Result(true, null);
    }

    public static Result Failure(Message message) {
        return new Result(false, message);
    }
}

public class Result<T> {
    [MemberNotNullWhen(true, nameof(Value))]
    [MemberNotNullWhen(false, nameof(Message))]
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
        T? convertedValue = other.Successful ? converter(other.Value) : default;
        return new Result<T>(other.Successful, convertedValue, other.Message);
    }
}
