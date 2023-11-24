using Krosoft.Extensions.Core.Models.Exceptions;

namespace Krosoft.Extensions.Core.Models;

public readonly struct Result<T>
{
    public T? Value { get; }
    public Exception? Exception { get; }

    public Result(T value)
    {
        Value = value;
        Exception = null;
    }

    public Result(Exception e)
    {
        Exception = e;
        Value = default;
    }

    public T Validate()
    {
        if (IsFaulted)
        {
            throw new KrosoftMetierException(Exception!.Message);
        }

        return Value!;
    }

    public bool IsSuccess => Exception == null;
    public bool IsFaulted => !IsSuccess;
}











namespace MyProject.Common
{
    public class QueryResult
    {
        private static readonly QueryResult success = new QueryResult { Succeeded = true };
        private readonly List<QueryError> errors = new List<QueryError>();

        public static QueryResult Success { get { return success; } }

        public bool Succeeded { get; protected set; }
        public IEnumerable<QueryError> Errors { get { return errors; } }

        public static QueryResult Failed(params QueryError[] errors)
        {
            var result = new QueryResult { Succeeded = false };
            if (errors != null)
            {
                result.errors.AddRange(errors);
            }

            return result;
        }
    }

    public class QueryResult<T> : QueryResult where T : class
    {
        public T Result { get; protected set; }

        public static QueryResult<T> Suceeded(T result)
        {
            var queryResult = new QueryResult<T>
            {
                Succeeded = true,
                Result = result
            };

            return queryResult;
        }
    }
}


public static class QueryResultExtensions
{
    public IActionResult ToActionResult(this QueryResult result)
    {
        if(result.Success)
        {
            return new OkResult();
        }

        if(result.Errors != null)
        {
            // ToModelStateDictionary needs to be implemented by you ;)
            var errors = result.Errors.ToModelStateDictionary();
            return BadRequestResult(errors);
        }

        return BadRequestResult();
    }

    public IActionResult ToActionResult<T>(this QueryResult<T> result)
    {
        if(result.Success)
        {
            if(result.Result == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result.Result);
        }

        if(result.Errors != null)
        {
            // ToModelStateDictionary needs to be implemented by you ;)
            var errors = result.Errors.ToModelStateDictionary();
            return BadRequestResult(errors);
        }

        return BadRequestResult();
    }
}