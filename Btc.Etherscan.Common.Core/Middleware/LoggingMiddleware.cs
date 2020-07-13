using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Btc.Etherscan.Common.Core.Middleware
{
    /// <summary>
    /// Middleware providing unhandled exception handling and logging.
    /// Can be setup to be used within an application through: `app.UseMiddleware<LoggingMiddleware>();`
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="next"></param>
        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context.Request);
            await InvokeNextThenLogExceptionsAndResponse(context);
        }

        private static async Task LogRequest(HttpRequest request)
        {
            if (Log.IsEnabled(LogEventLevel.Verbose))
            {
                // copy the request to a new stream
                var streamCopy = new MemoryStream();
                await request.Body.CopyToAsync(streamCopy);
                streamCopy.Seek(0, SeekOrigin.Begin);

                // read the stream copy
                var reader = new StreamReader(streamCopy);
                var body = await reader.ReadToEndAsync();
                Log.Verbose(body);

                // assign request with the reset stream copy
                streamCopy.Seek(0, SeekOrigin.Begin);
                request.Body = streamCopy;
            }
        }

        private async Task InvokeNextThenLogExceptionsAndResponse(HttpContext context)
        {
            using (var buffer = new MemoryStream())
            {
                Stream stream = null;
                if (Log.IsEnabled(LogEventLevel.Verbose))
                {
                    // replace the context response with our buffer
                    stream = context.Response.Body;
                    context.Response.Body = buffer;
                }

                await InvokeNextAndLogExceptions(context);

                if (Log.IsEnabled(LogEventLevel.Verbose))
                {
                    // reset the buffer and read out the contents
                    buffer.Seek(0, SeekOrigin.Begin);
                    using (var bufferReader = new StreamReader(buffer))
                    {
                        string body = await bufferReader.ReadToEndAsync();

                        // reset to start of stream
                        buffer.Seek(0, SeekOrigin.Begin);

                        // copy our content to the original stream and put it back
                        await buffer.CopyToAsync(stream);
                        context.Response.Body = stream;

                        Log.Verbose($"Response: {body}");
                    }
                }
            }
        }

        private async Task InvokeNextAndLogExceptions(HttpContext context)
        {
            try
            {
                // invoke the rest of the pipeline
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                var errorId = Guid.NewGuid();
                Log.ForContext("Type", "Error")
                    .ForContext("Exception", exception, true)
                    .Error(exception, exception.Message + ". {@errorId}", errorId);

                var result = JsonConvert.SerializeObject(new { error = "Oops, an unexpected error has occurred", errorId });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(result);
            }
        }
    }
}
