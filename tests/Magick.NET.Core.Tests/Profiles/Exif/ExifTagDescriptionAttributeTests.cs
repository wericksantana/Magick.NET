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

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public class ExifTagDescriptionAttributeTests
    {
        [Fact]
        public void Test_ExifTag()
        {
            var exifProfile = new ExifProfile();

            exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)1);
            var value = exifProfile.GetValue(ExifTag.ResolutionUnit);
            Assert.Equal("None", value.ToString());

            exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)2);
            value = exifProfile.GetValue(ExifTag.ResolutionUnit);
            Assert.Equal("Inches", value.ToString());

            exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)3);
            value = exifProfile.GetValue(ExifTag.ResolutionUnit);
            Assert.Equal("Centimeter", value.ToString());

            exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)4);
            value = exifProfile.GetValue(ExifTag.ResolutionUnit);
            Assert.Equal("4", value.ToString());

            exifProfile.SetValue(ExifTag.ImageWidth, 123);
            var imageWidth = exifProfile.GetValue(ExifTag.ImageWidth);
            Assert.Equal("123", imageWidth.ToString());
        }
    }
}
