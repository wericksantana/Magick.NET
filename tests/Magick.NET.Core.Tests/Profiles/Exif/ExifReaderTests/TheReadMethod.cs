﻿// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ExifReaderTests
    {
        public class TheReadMethod
        {
            [Fact]
            public void ShouldReturnEmptyCollectionWhenDataIsEmpty()
            {
                var reader = new ExifReader();
                var data = new byte[] { };

                reader.Read(data);

                Assert.Empty(reader.Values);
            }

            [Fact]
            public void ShouldReturnEmptyCollectionWhenDataHasNoValues()
            {
                var reader = new ExifReader();
                var data = new byte[] { 69, 120, 105, 102, 0, 0 };

                reader.Read(data);

                Assert.Empty(reader.Values);
            }

            [Fact]
            public void ShouldCheckArraySize()
            {
                var reader = new ExifReader();
                var data = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 148, 1, 1, 0, 255, 255, 255, 255, 26, 0, 0, 0, 31, 0, 0, 0, 42 };

                reader.Read(data);

                Assert.Empty(reader.Values);
                Assert.Single(reader.InvalidTags);
            }

            [Fact]
            public void ShouldCheckTypeOfJPEGInterchangeFormat()
            {
                var reader = new ExifReader();
                var data = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 1, 2, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0 };

                reader.Read(data);

                Assert.Empty(reader.Values);
                Assert.Equal(2, reader.InvalidTags.Count);
            }

            [Fact]
            public void ShouldCheckTypeOfJPEGInterchangeFormatLength()
            {
                var reader = new ExifReader();
                var data = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 2, 2, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0 };

                reader.Read(data);

                Assert.Empty(reader.Values);
                Assert.Equal(2, reader.InvalidTags.Count);
            }

            [Fact]
            public void ShouldBeAbleToReadEmptyStrings()
            {
                var reader = new ExifReader();
                var data = new byte[] { 69, 120, 105, 102, 0, 0, 73, 73, 42, 0, 8, 0, 0, 0, 1, 0, 14, 1, 2, 0, 0, 0, 0, 0, 32, 0, 0, 0, 26, 0, 0, 0, 0, 0 };

                reader.Read(data);

                Assert.Single(reader.Values);
                Assert.Equal(string.Empty, reader.Values.First().GetValue());
            }
        }
    }
}
