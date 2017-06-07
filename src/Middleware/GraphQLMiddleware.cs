using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQLSample.Models.GraphQLQueries;
using GraphQLSample.Repository;
using GraphQLSample.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLSample.Middleware
{
    public class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IBookService _bookService;

        public GraphQLMiddleware(RequestDelegate next, IBookService bookService)
        {
            _next = next;
            _bookService = bookService;
        }

        public async Task Invoke(HttpContext context)
        {
            var sent = false;
            if (context.Request.Path.StartsWithSegments("/graph"))
            {
                using (var sr = new StreamReader(context.Request.Body))
                {
                    var query = await sr.ReadToEndAsync();
                    if (!string.IsNullOrWhiteSpace(query))
                    {
                        var schema = new Schema { Query = new BooksQuery(_bookService) };

                        var result = await new DocumentExecuter()
                            .ExecuteAsync(o =>
                            {
                                o.Schema = schema;
                                o.Query = query;
                            }).ConfigureAwait(false);

                        CheckForErrors(result);

                        await WriteResult(context, result);
                        sent = true;
                    }
                }
            }

            if (!sent)
            {
                await _next(context);
            }
        }

        private void CheckForErrors(ExecutionResult result)
        {
            if (result.Errors?.Count > 0)
            {
                var errors = new List<Exception>();
                foreach (var error in result.Errors)
                {
                    var ex = new Exception(error.Message);
                    if (error.InnerException != null)
                    {
                        ex = new Exception(error.Message, error.InnerException);
                    }
                    errors.Add(ex);
                }

                throw new AggregateException(errors);
            }
        }

        private async Task WriteResult(HttpContext context, ExecutionResult result)
        {
            var json = new DocumentWriter(indent: true).Write(result);

            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
