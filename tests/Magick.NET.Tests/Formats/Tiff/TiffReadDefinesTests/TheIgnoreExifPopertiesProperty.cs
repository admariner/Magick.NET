﻿// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;
using ImageMagick.Formats.Tiff;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TiffReadDefinesTests
    {
        public class TheIgnoreExifPopertiesProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new TiffReadDefines
                    {
                        IgnoreExifPoperties = true,
                    });

                    Assert.Equal("false", image.Settings.GetDefine(MagickFormat.Tiff, "exif-properties"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenTheValueIsFalse()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new TiffReadDefines
                    {
                        IgnoreExifPoperties = false,
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "exif-properties"));
                }
            }

            [Fact]
            public void ShouldIgnoreTheExifProperties()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new TiffReadDefines
                    {
                        IgnoreExifPoperties = true,
                    },
                };

                using (var image = new MagickImage())
                {
                    image.Read(Files.InvitationTIF);
                    Assert.NotNull(image.GetAttribute("exif:PixelXDimension"));

                    image.Read(Files.InvitationTIF, settings);
                    Assert.Null(image.GetAttribute("exif:PixelXDimension"));
                }
            }
        }
    }
}
