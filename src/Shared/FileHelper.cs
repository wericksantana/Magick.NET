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
using System.IO;

namespace ImageMagick
{
    internal static partial class FileHelper
    {
        public static string CheckForBaseDirectory(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return fileName;

            if (fileName.Length < 2 || fileName[0] != '~')
                return fileName;

            return AppDomain.CurrentDomain.BaseDirectory + fileName.Substring(1);
        }

        public static string GetFullPath(string path)
        {
            Throw.IfNullOrEmpty(nameof(path), path);

            path = CheckForBaseDirectory(path);
            path = Path.GetFullPath(path);
            Throw.IfFalse(nameof(path), Directory.Exists(path), $"Unable to find directory: {path}");
            return path;
        }
    }
}