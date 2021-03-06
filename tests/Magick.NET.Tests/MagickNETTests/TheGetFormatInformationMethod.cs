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

using System;
using System.Collections.Generic;
using ImageMagick;
using Xunit;
using Xunit.Sdk;
using OperatingSystem = ImageMagick.OperatingSystem;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheGetFormatInformationMethod
        {
            [Fact]
            public void ShouldReturnFormatInfoForAllFormats()
            {
                var missingFormats = new List<string>();

                foreach (MagickFormat format in Enum.GetValues(typeof(MagickFormat)))
                {
                    if (format == MagickFormat.Unknown)
                        continue;

                    var formatInfo = MagickNET.GetFormatInformation(format);
                    if (formatInfo == null)
                    {
                        if (ShouldReport(format))
                            missingFormats.Add(format.ToString());
                    }
                }

                if (missingFormats.Count > 0)
                    throw new XunitException("Cannot find MagickFormatInfo for: " + string.Join(", ", missingFormats.ToArray()));
            }

            private static bool ShouldReport(MagickFormat format)
            {
                if (!OperatingSystem.IsWindows)
                {
                    if (format == MagickFormat.Clipboard || format == MagickFormat.Emf || format == MagickFormat.Wmf)
                        return false;

                    if (format == MagickFormat.Flif)
                        return false;
                }

                if (OperatingSystem.IsMacOS)
                {
                    if (format == MagickFormat.Jxl)
                        return false;
                }

                return true;
            }
        }
    }
}
