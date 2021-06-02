// <copyright file="Payload.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

using System.Text.Json;

namespace Functions
{
    /// <summary>
    /// Class used to define the structure of the messages sent using poco objects.
    /// </summary>
    public class Payload
    {
        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public JsonElement Value { get; set; }
    }
}
