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
using Xunit.Sdk;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheFontNamesProperty
        {
            [Fact]
            public void ContainsArial()
            {
                var fontNames = MagickNET.FontNames.ToArray();
                var fontName = fontNames.FirstOrDefault(f => f == "Arial");
                if (fontName == null)
                    throw new XunitException($"Unable to find Arial in font families: {string.Join(",", fontNames)}");
            }
        }
    }
}
