// <copyright file="IImageFormat.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Formats
{
    using System.Collections.Generic;

    /// <summary>
    /// Encapsulates a supported image format, providing means to encode and decode an image.
    /// Individual formats implements in this interface must be registered in the <see cref="Configuration"/>
    /// </summary>
    public interface IImageFormat
    {
        /// <summary>
        /// Gets the collection of mime types that this decoder supports encoding for.
        /// </summary>
        IEnumerable<string> MimeTypes { get; }

        /// <summary>
        /// Gets the supported file extensions for this format.
        /// </summary>
        /// <returns>
        /// The supported file extension.
        /// </returns>
        IEnumerable<string> SupportedExtensions { get; }
    }
}