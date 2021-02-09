// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
// <auto-generated/>

using System;
using System.Security;
using System.Runtime.InteropServices;

namespace ImageMagick
{
    internal static partial class Environment
    {
        [SuppressUnmanagedCodeSecurity]
        private static class NativeMethods
        {
            #if PLATFORM_x64 || PLATFORM_AnyCPU
            public static class X64
            {
                #if PLATFORM_AnyCPU
                static X64() { NativeLibraryLoader.Load(); }
                #endif
                [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
                public static extern void Environment_Initialize();
                [DllImport(NativeLibrary.X64Name, CallingConvention = CallingConvention.Cdecl)]
                public static extern void Environment_SetEnv(IntPtr name, IntPtr value);
            }
            #endif
            #if PLATFORM_x86 || PLATFORM_AnyCPU
            public static class X86
            {
                #if PLATFORM_AnyCPU
                static X86() { NativeLibraryLoader.Load(); }
                #endif
                [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
                public static extern void Environment_Initialize();
                [DllImport(NativeLibrary.X86Name, CallingConvention = CallingConvention.Cdecl)]
                public static extern void Environment_SetEnv(IntPtr name, IntPtr value);
            }
            #endif
        }
        private static class NativeEnvironment
        {
            public static void Initialize()
            {
                #if PLATFORM_AnyCPU
                if (OperatingSystem.Is64Bit)
                #endif
                #if PLATFORM_x64 || PLATFORM_AnyCPU
                NativeMethods.X64.Environment_Initialize();
                #endif
                #if PLATFORM_AnyCPU
                else
                #endif
                #if PLATFORM_x86 || PLATFORM_AnyCPU
                NativeMethods.X86.Environment_Initialize();
                #endif
            }
            public static void SetEnv(string name, string value)
            {
                using (INativeInstance nameNative = UTF8Marshaler.CreateInstance(name))
                {
                    using (INativeInstance valueNative = UTF8Marshaler.CreateInstance(value))
                    {
                        #if PLATFORM_AnyCPU
                        if (OperatingSystem.Is64Bit)
                        #endif
                        #if PLATFORM_x64 || PLATFORM_AnyCPU
                        NativeMethods.X64.Environment_SetEnv(nameNative.Instance, valueNative.Instance);
                        #endif
                        #if PLATFORM_AnyCPU
                        else
                        #endif
                        #if PLATFORM_x86 || PLATFORM_AnyCPU
                        NativeMethods.X86.Environment_SetEnv(nameNative.Instance, valueNative.Instance);
                        #endif
                    }
                }
            }
        }
    }
}
