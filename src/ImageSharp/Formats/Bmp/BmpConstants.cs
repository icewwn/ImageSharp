// <copyright file="BmpConstants.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Formats
{
    /// <summary>
    /// Defines constants relating to BMPs
    /// </summary>
    internal static class BmpConstants
    {
        /// <summary>
        /// The format that equates to a bmp
        /// </summary>
        public static readonly IImageFormat Format = new ImageFormat()
        {
            MimeTypes = new[] { "image/bmp", "image/x-windows-bmp" },
            SupportedExtensions = new[] { "bm", "bmp", "dip" }
        };
    }
}
