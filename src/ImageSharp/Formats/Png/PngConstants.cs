// <copyright file="PngConstants.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>
namespace ImageSharp.Formats
{
    using System.Text;

    /// <summary>
    /// Defines png constants defined in the specification.
    /// </summary>
    internal static class PngConstants
    {
        /// <summary>
        /// The default encoding for text metadata
        /// </summary>
        public static readonly Encoding DefaultEncoding = Encoding.GetEncoding("ASCII");

        /// <summary>
        /// The format that equates to a png
        /// </summary>
        public static readonly IImageFormat Format = new ImageFormat()
        {
            MimeTypes = new[] { "image/png" },
            SupportedExtensions = new[] { "png" }
        };
    }
}
