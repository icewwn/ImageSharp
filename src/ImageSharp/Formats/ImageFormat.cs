// <copyright file="IImageFormat.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Formats
{
    using System.Collections.Generic;

    /// <inheritdoc/>
    internal sealed class ImageFormat : IImageFormat
    {
        /// <inheritdoc/>
        public IEnumerable<string> MimeTypes { get; set; }

        /// <inheritdoc/>
        public IEnumerable<string> SupportedExtensions { get; set; }
    }
}