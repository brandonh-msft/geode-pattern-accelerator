// <copyright file="HttpTriggered.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Functions
{
    /// <summary>
    /// Contains functions that respond to http triggered requests.
    /// </summary>
    public static class HttpTriggered
    {
        /// <summary>
        /// Function configured with function level auth thats content is sent as a string.
        /// </summary>
        /// <param name="req">Http request context.</param>
        /// <param name="log">Object used for trace logging.</param>
        /// <returns>The results of the function execution.</returns>
        [FunctionName("HTTP_AsString_FuncAuth")]
        public static async Task<IActionResult> FunctionAuthPostRawRequest(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            ILogger log)
        {
            var requestBody = await req.ReadAsStringAsync().ConfigureAwait(false);

            try
            {
                JsonSerializer.Deserialize<Payload>(requestBody);

                string responseMessage = $@"Deserialized Payload {requestBody.Length} byte(s)";
                log.LogInformation(responseMessage);

                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        /// <summary>
        /// Function configured with function level auth thats content is sent as a stream.
        /// </summary>
        /// <param name="req">Http request context.</param>
        /// <param name="log">Object used for trace logging.</param>
        /// <returns>The results of the function execution.</returns>
        [FunctionName("HTTP_AsStream_FuncAuth")]
        public static async Task<IActionResult> FunctionAuthPostRawRequestStream(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req,
            ILogger log)
        {
            // Add null guard.
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            try
            {
                _ = await JsonSerializer.DeserializeAsync<Payload[]>(req.Body).ConfigureAwait(false);

                string responseMessage = $@"Deserialized Payload {req.Body.Length} byte(s)";
                log.LogInformation(responseMessage);

                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        /// <summary>
        /// Function configured with function level auth thats content is sent as a specific class object.
        /// </summary>
        /// <param name="req">Http request context.</param>
        /// <param name="log">Object used for trace logging.</param>
        /// <returns>The results of the function execution.</returns>
        [FunctionName("HTTP_AsParsedObject_FuncAuth")]
        public static IActionResult FunctionAuthPostPocoRequest(
            [HttpTrigger(AuthorizationLevel.Function, "post")] Payload req,
            ILogger log)
        {
            try
            {
                string responseMessage = $@"Received deserialized payload";
                log.LogInformation(responseMessage);

                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        /// <summary>
        /// Function configured with anonymous access thats content is sent as a string.
        /// </summary>
        /// <param name="req">Http request context.</param>
        /// <param name="log">Object used for trace logging.</param>
        /// <returns>The results of the function execution.</returns>
        [FunctionName("HTTP_AsString_Anonymous")]
        public static async Task<IActionResult> AnonymousPostRawRequest(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            ILogger log)
        {
            var requestBody = await req.ReadAsStringAsync().ConfigureAwait(false);

            try
            {
                // TODO: Do we want to do more with this?
                JsonSerializer.Deserialize<Payload>(requestBody);

                string responseMessage = $@"Deserialized Payload {requestBody.Length} byte(s)";
                log.LogInformation(responseMessage);

                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }

        /// <summary>
        /// Function configured with anonymous access thats content is sent as a specific class object.
        /// </summary>
        /// <param name="req">Http request context.</param>
        /// <param name="log">Object used for trace logging.</param>
        /// <returns>The results of the function execution.</returns>
        [FunctionName("HTTP_AsParsedObject_Anonymous")]
        public static IActionResult AnonymousPostPocoRequest(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] Payload req,
            ILogger log)
        {
            try
            {
                string responseMessage = $@"Received deserialized payload";
                log.LogInformation(responseMessage);

                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
