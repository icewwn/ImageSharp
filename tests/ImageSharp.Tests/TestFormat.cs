﻿// <copyright file="TestImage.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ImageSharp.Formats;
    using ImageSharp.PixelFormats;

    using Xunit;

    /// <summary>
    /// A test image file.
    /// </summary>
    public class TestFormat : IImageFormat
    {
        public static TestFormat GlobalTestFormat { get; } = new TestFormat();

        public static void RegisterGloablTestFormat()
        {
            Configuration.Default.AddImageFormat(GlobalTestFormat.Encoder);
            Configuration.Default.AddImageFormat(GlobalTestFormat.Decoder);
        }

        public TestFormat()
        {
            this.Encoder = new TestEncoder(this); ;
            this.Decoder = new TestDecoder(this); ;
        }

        public List<DecodeOperation> DecodeCalls { get; } = new List<DecodeOperation>();

        public IImageEncoder Encoder { get; }

        public IImageDecoder Decoder { get; }

        private byte[] header = Guid.NewGuid().ToByteArray();

        public MemoryStream CreateStream(byte[] marker = null)
        {
            MemoryStream ms = new MemoryStream();
            byte[] data = this.header;
            ms.Write(data, 0, data.Length);
            if (marker != null)
            {
                ms.Write(marker, 0, marker.Length);
            }
            ms.Position = 0;
            return ms;
        }

        Dictionary<Type, object> _sampleImages = new Dictionary<Type, object>();


        public void VerifyDecodeCall(byte[] marker, Configuration config)
        {
            DecodeOperation[] discovered = this.DecodeCalls.Where(x => x.IsMatch(marker, config)).ToArray();


            Assert.True(discovered.Any(), "No calls to decode on this formate with the proveded options happend");

            foreach (DecodeOperation d in discovered)
            {
                this.DecodeCalls.Remove(d);
            }
        }

        public Image<TPixel> Sample<TPixel>()
            where TPixel : struct, IPixel<TPixel>
        {
            lock (this._sampleImages)
            {
                if (!this._sampleImages.ContainsKey(typeof(TPixel)))
                {
                    this._sampleImages.Add(typeof(TPixel), new Image<TPixel>(1, 1));
                }

                return (Image<TPixel>)this._sampleImages[typeof(TPixel)];
            }
        }

        public IEnumerable<string> MimeTypes => new[] { "img/test" };

        public IEnumerable<string> SupportedExtensions => new[] { "test_ext" };

        public int HeaderSize => this.header.Length;

        public bool IsSupportedFileFormat(Span<byte> header)
        {
            if (header.Length < this.header.Length)
            {
                return false;
            }
            for (int i = 0; i < this.header.Length; i++)
            {
                if (header[i] != this.header[i])
                {
                    return false;
                }
            }
            return true;
        }
        public struct DecodeOperation
        {
            public byte[] marker;
            internal Configuration config;

            public bool IsMatch(byte[] testMarker, Configuration config)
            {

                if (this.config != config)
                {
                    return false;
                }

                if (testMarker.Length != this.marker.Length)
                {
                    return false;
                }

                for (int i = 0; i < this.marker.Length; i++)
                {
                    if (testMarker[i] != this.marker[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public class TestDecoder : IImageDecoder
        {
            private TestFormat testFormat;

            public TestDecoder(TestFormat testFormat)
            {
                this.testFormat = testFormat;
            }

            public IImageFormat Format => this.testFormat;

            public int HeaderSize => testFormat.HeaderSize;

            public Image<TPixel> Decode<TPixel>(Configuration config, Stream stream) where TPixel : struct, IPixel<TPixel>

            {
                var ms = new MemoryStream();
                stream.CopyTo(ms);
                var marker = ms.ToArray().Skip(this.testFormat.header.Length).ToArray();
                this.testFormat.DecodeCalls.Add(new DecodeOperation
                {
                    marker = marker,
                    config = config
                });

                // TODO record this happend so we an verify it.
                return this.testFormat.Sample<TPixel>();
            }

            public bool IsSupportedFileFormat(Span<byte> header) => testFormat.IsSupportedFileFormat(header);
        }

        public class TestEncoder : IImageEncoder
        {
            private TestFormat testFormat;

            public TestEncoder(TestFormat testFormat)
            {
                this.testFormat = testFormat;
            }

            public IImageFormat Format => this.testFormat;

            public IEnumerable<string> FileExtensions => testFormat.SupportedExtensions;

            public void Encode<TPixel>(Image<TPixel> image, Stream stream) where TPixel : struct, IPixel<TPixel>
            {
                // TODO record this happend so we an verify it.
            }
        }
    }
}
