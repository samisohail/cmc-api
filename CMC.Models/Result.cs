using System;

namespace CMC.Models
{
    public class Result
    {
        public readonly bool Success;
        public readonly string Error;
        public readonly bool? NotFoundToModify;

        protected Result(bool success, string error, bool? notFoundToModify = null)
        {
            Success = success;
            Error = error;
            NotFoundToModify = notFoundToModify; // if an object asked to update/delete, not found. used to determine Http Status 404
        }

        public static Result Fail(string message) =>
            new Result(false, message);

        public static Result<T> Fail<T>(string message) =>
            new Result<T>(default, false, message);

        public static Result<T> Fail<T>(string message, bool? notFound) =>
            new Result<T>(default, false, message, notFound);

        // public static Result OK() => new Result(true, null);

        public static Result<T> OK<T>(T value) =>
            new Result<T>(value, true, null);
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (!Success) throw new InvalidOperationException();

                return _value;
            }
        }

        protected internal Result(T value, bool success, string error, bool? notFound = null)
            : base(success, error, notFound) => this._value = value;
    }
}
