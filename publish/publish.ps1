# Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
#
# Licensed under the ImageMagick License (the "License"); you may not use this file except in
# compliance with the License. You may obtain a copy of the License at
#
#   https://imagemagick.org/script/license.php
#
# Unless required by applicable law or agreed to in writing, software distributed under the
# License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
# either express or implied. See the License for the specific language governing permissions
# and limitations under the License.

param (
    [string]$quantumName = $env:QuantumName,
    [string]$platformName = $env:PlatformName,
    [string]$pfxPassword = '',
    [string]$version = $env:NuGetVersion,
    [string]$commit = $env:GitCommitId,
    [parameter(mandatory=$true)][string]$destination
)

. $PSScriptRoot\..\tools\windows\utils.ps1
. $PSScriptRoot\publish.shared.ps1

function addMagickNetLibraries($xml, $quantumName, $platform) {
    addLibrary $xml "Magick.NET" $quantumName $platform "net20"
    addLibrary $xml "Magick.NET" $quantumName $platform "net40"
    addLibrary $xml "Magick.NET" $quantumName $platform "netstandard20"
}

function addOpenMPLibrary($xml) {
    $redistFolder = "$($env:VSINSTALLDIR)VC\Redist\MSVC"
    $redistVersion = (ls -Directory $redistFolder -Filter 14.* | sort -Descending | select -First 1 -Property Name).Name
    $source = "$redistFolder\$redistVersion\x64\Microsoft.VC142.OpenMP\vcomp140.dll"
    $target = "runtimes\win-x64\native\vcomp140.dll"
    addFile $xml $source $target
}

function addNativeLibrary($xml, $platform, $runtime, $suffix) {
    $source = fullPath "src\Magick.Native\libraries\$runtime\Magick.Native-$suffix"
    $target = "runtimes\$runtime-$platform\native\Magick.Native-$suffix"
    addFile $xml $source $target
}

function addNativeLibraries($xml, $quantumName, $platform) {
    if ($platform -eq "AnyCPU")
    {
        addNativeLibraries $xml $quantumName "x86"
        addNativeLibraries $xml $quantumName "x64"
        return
    }

    addNativeLibrary $xml $platform "win" "$quantumName-$platform.dll"

    if ($platform -eq "x64") {
        if ($quantumName.EndsWith("-OpenMP")) {
            addOpenMPLibrary $xml
            addNativeLibrary $xml $platform "linux" "$quantumName-$platform.dll.so"
        } else {
            addNativeLibrary $xml $platform "linux" "$quantumName-$platform.dll.so"
            addNativeLibrary $xml $platform "linux-musl" "$quantumName-$platform.dll.so"
            addNativeLibrary $xml $platform "osx" "$quantumName-$platform.dll.dylib"
        }
    }
}

function addNotice($xml, $platform, $runtime) {
    $source = fullPath "src\Magick.Native\libraries\$runtime\Notice.txt"
    $target = "runtimes\$runtime-$platform\native\Notice.txt"
    addFile $xml $source $target
}

function addNotices($xml, $platform) {
    if ($platform -eq "AnyCPU")
    {
        addNotices $xml "x86"
        addNotices $xml "x64"
        return
    }

    addNotice $xml $platform "win"

    if ($platform -eq "x64") {
        if ($quantumName.EndsWith("-OpenMP")) {
            addNotice $xml $platform "linux"
        } else {
            addNotice $xml $platform "linux"
            addNotice $xml $platform "linux-musl"
            addNotice $xml $platform "osx"
        }
    }
}

function createMagickNetNuGetPackage($quantumName, $platform, $version, $commit, $pfxPassword) {
    $xml = loadAndInitNuSpec "Magick.NET" $version $commit

    $name = "Magick.NET-$quantumName-$platform"
    $xml.package.metadata.id = $name
    $xml.package.metadata.title = $name

    $platform = $platformName

    if ($platform -eq "Any CPU") {
        $platform = "AnyCPU"
    }

    addMagickNetLibraries $xml $quantumName $platform
    addNativeLibraries $xml $quantumName $platform
    addNotices $xml $platform

    if ($platform -ne "AnyCPU") {
        addFile $xml "Magick.NET.targets" "build\net20\$name.targets"
        addFile $xml "Magick.NET.targets" "build\net40\$name.targets"
    }

    createAndSignNuGetPackage $xml $name $version $pfxPassword
}

$platform = $platformName

if ($platform -eq "Any CPU") {
    $platform = "AnyCPU"
}

createMagickNetNuGetPackage $quantumName $platform $version $commit $pfxPassword
copyNuGetPackages $destination