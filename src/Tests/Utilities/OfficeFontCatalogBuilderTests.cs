using Microsoft.VisualStudio.TestTools.UnitTesting;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace PnP.PowerShell.Tests.Utilities
{
    /// <summary>
    /// Tests of the Office font catalog builder (see src/Commands/Utilities/OfficeFontCatalog.md).
    /// No tenant needed. Fonts are built in memory by <see cref="TestFont"/>, so no font file is
    /// committed. The comparison with Microsoft's generator uses Windows fonts and is skipped
    /// (inconclusive) where they are missing or differ from the ones the reference was made from.
    /// </summary>
    [TestClass]
    public class OfficeFontCatalogBuilderTests
    {
        private static readonly Action<string> Ignore = _ => { };

        private static List<OfficeFontCatalogFile> Build(params (string FileName, byte[] Content)[] fonts) =>
            OfficeFontCatalogBuilder.Build(fonts.Select(f => new CustomFontFile { FileName = f.FileName, Content = f.Content }).ToList(), 3, Ignore, Ignore);

        private static JsonElement Catalog(List<OfficeFontCatalogFile> files, int major, string name) =>
            JsonDocument.Parse(files.Single(f => f.MajorVersion == major && f.FileName == name).Content).RootElement;

        private static JsonElement FirstFace(JsonElement catalog) => catalog.GetProperty("Fonts")[0].GetProperty("sf")[0];

        [TestMethod]
        public void Build_WritesFiveFilesInTwoVersions()
        {
            var files = Build(("Contoso.ttf", new TestFont().Bytes()));

            CollectionAssert.AreEquivalent(
                new[] { "1/fontcatalog.fontconfig", "1/fontcatalog_hier.fontconfig", "1/fontcatalog_light.fontconfig", "2/fontcatalog.fontconfig", "2/fontcatalog_hier.fontconfig" },
                files.Select(f => $"{f.MajorVersion}/{f.FileName}").ToArray());
            foreach (var file in files)
            {
                Assert.AreNotEqual(0xEF, file.Content[0], $"{file.FileName} must not start with a BOM");
                var root = JsonDocument.Parse(file.Content).RootElement;
                Assert.AreEqual(file.MajorVersion, root.GetProperty("MajorVersion").GetInt32());
                Assert.AreEqual(3, root.GetProperty("MinorVersion").GetInt32());
                Assert.AreEqual(file.FileName != "fontcatalog_light.fontconfig", root.TryGetProperty("Expiration", out _));
            }
        }

        [TestMethod]
        public void Build_ReadsFaceValuesFromTheFontTables()
        {
            var font = new TestFont
            {
                Family = "Contoso Sans",
                FullName = "Contoso Sans Bold Italic",
                Subfamily = "Bold Italic",
                Weight = 700,
                Width = 5,
                Italic = true,
                Revision = 0x00020000,
                Panose = [2, 11, 7, 4, 2, 2, 2, 9, 2, 4],
                UnicodeRanges = [1, 2, 3, 4],
                CodePages = [5, 6],
            };
            var bytes = font.Bytes();
            var face = FirstFace(Catalog(Build(("ContosoSans-BoldItalic.TTF", bytes)), 2, "fontcatalog_hier.fontconfig"));

            Assert.AreEqual("Contoso Sans Bold Italic", face.GetProperty("dn").GetString());
            Assert.AreEqual("Contoso Sans", face.GetProperty("gn").GetString());
            Assert.AreEqual("ContosoSans-BoldItalic", face.GetProperty("id").GetString());
            Assert.AreEqual("TTF", face.GetProperty("t").GetString(), "the extension keeps its case");
            Assert.AreEqual(bytes.Length, face.GetProperty("fs").GetInt32());
            Assert.AreEqual(0x00020000u, face.GetProperty("v").GetUInt32());
            Assert.AreEqual((700u << 16) | (5u << 8) | 2u, face.GetProperty("w").GetUInt32(), "weight << 16 | width << 8 | italic (2)");
            CollectionAssert.AreEqual(font.Panose.Select(b => (int)b).ToArray(), face.GetProperty("p").EnumerateArray().Select(e => e.GetInt32()).ToArray());
            CollectionAssert.AreEqual(new uint[] { 1, 2, 3, 4 }, face.GetProperty("u").EnumerateArray().Select(e => e.GetUInt32()).ToArray());
            CollectionAssert.AreEqual(new uint[] { 5, 6 }, face.GetProperty("c").EnumerateArray().Select(e => e.GetUInt32()).ToArray());
            Assert.AreEqual("Bold Italic", face.GetProperty("sub")[0].GetProperty("ltx").GetString());
        }

        [TestMethod]
        public void Build_GroupsFacesByTypographicFamily()
        {
            var light = new TestFont { Family = "Contoso Light", TypographicFamily = "Contoso", FullName = "Contoso Light", Subfamily = "Regular", TypographicSubfamily = "Light", Weight = 300 };
            var regular = new TestFont { Family = "Contoso", FullName = "Contoso" };

            var catalog = Catalog(Build(("contosol.ttf", light.Bytes()), ("contoso.ttf", regular.Bytes())), 2, "fontcatalog_hier.fontconfig");
            var family = catalog.GetProperty("Fonts").EnumerateArray().Single();

            Assert.AreEqual("Contoso", family.GetProperty("f").GetString());
            CollectionAssert.AreEqual(new[] { "contoso", "contosol" }, family.GetProperty("sf").EnumerateArray().Select(f => f.GetProperty("id").GetString()).ToArray());
            var lightFace = family.GetProperty("sf")[1];
            Assert.AreEqual("Contoso Light", lightFace.GetProperty("gn").GetString());
            Assert.AreEqual("Light", lightFace.GetProperty("sub")[0].GetProperty("ltx").GetString(), "v2 uses the typographic subfamily");
        }

        [TestMethod]
        public void Build_StripsStyleWordsWhenThereIsNoTypographicFamily()
        {
            var face = new TestFont { Family = "Contoso Rounded Bold", FullName = "Contoso Rounded Bold" };
            var family = Catalog(Build(("cr.ttf", face.Bytes())), 2, "fontcatalog.fontconfig").GetProperty("Fonts")[0];

            Assert.AreEqual("Contoso Rounded", family.GetProperty("f").GetString());
            Assert.AreEqual("Contoso Rounded Bold", family.GetProperty("sf")[0].GetProperty("gn").GetString());
        }

        [TestMethod]
        public void Build_OrdersFamiliesByTheirFirstFileNameIgnoringCase()
        {
            var catalog = Catalog(Build(
                ("zeta.ttf", new TestFont { Family = "Alpha" }.Bytes()),
                ("ALPHA.ttf", new TestFont { Family = "Zeta" }.Bytes()),
                ("beta.ttf", new TestFont { Family = "Beta" }.Bytes())), 1, "fontcatalog.fontconfig");

            CollectionAssert.AreEqual(new[] { "Zeta", "Beta", "Alpha" },
                catalog.GetProperty("Fonts").EnumerateArray().Select(f => f.GetProperty("f").GetString()).ToArray());
        }

        [TestMethod]
        public void Build_MainAndLightCatalogsLeaveOutDetails()
        {
            var files = Build(("Contoso.ttf", new TestFont().Bytes()));

            var main = FirstFace(Catalog(files, 2, "fontcatalog.fontconfig"));
            Assert.AreEqual(0, main.GetProperty("ful").GetArrayLength());
            Assert.AreEqual(0, main.GetProperty("sub").GetArrayLength());

            var lightCatalog = Catalog(files, 1, "fontcatalog_light.fontconfig");
            Assert.IsFalse(lightCatalog.GetProperty("Fonts")[0].TryGetProperty("fam", out _));
            var light = FirstFace(lightCatalog);
            Assert.IsFalse(light.TryGetProperty("fs", out _));
            Assert.IsFalse(light.TryGetProperty("v", out _));

            var v1 = FirstFace(Catalog(files, 1, "fontcatalog_hier.fontconfig"));
            Assert.IsFalse(v1.TryGetProperty("ads", out _), "ads, ldn and lgn are v2 only");
        }

        [TestMethod]
        public void Build_WarnsWhenAnAsciiCharacterHasNoGlyph()
        {
            var warnings = new List<string>();
            var font = new TestFont { LastMappedCharacter = 0x29 }; // no '*' (U+002A) and above
            OfficeFontCatalogBuilder.Build([new CustomFontFile { FileName = "Partial.ttf", Content = font.Bytes() }], 1, warnings.Add, Ignore);

            Assert.AreEqual(1, warnings.Count);
            StringAssert.Contains(warnings[0], "U+002A");
        }

        [TestMethod]
        public void Build_RejectsFilesThatAreNotFonts()
        {
            var good = new TestFont().Bytes();
            var cases = new Dictionary<string, byte[]>
            {
                ["empty"] = [],
                ["random"] = Enumerable.Range(0, 500).Select(i => (byte)(i * 37)).ToArray(),
                ["truncated"] = good.Take(good.Length / 2).ToArray(),
                ["collection"] = Encoding.ASCII.GetBytes("ttcf").Concat(new byte[100]).ToArray(),
            };
            foreach (var (name, bytes) in cases)
            {
                var ex = Assert.ThrowsException<InvalidDataException>(() => Build(($"{name}.ttf", bytes)), name);
                StringAssert.Contains(ex.Message, $"{name}.ttf");
            }
        }

        [TestMethod]
        public void Build_NamesAllInvalidFilesInOneError()
        {
            var ex = Assert.ThrowsException<InvalidDataException>(() => Build(("a.ttf", []), ("ok.ttf", new TestFont().Bytes()), ("b.otf", [1, 2, 3])));
            StringAssert.Contains(ex.Message, "a.ttf");
            StringAssert.Contains(ex.Message, "b.otf");
            Assert.IsFalse(ex.Message.Contains("ok.ttf"));
        }

        /// <summary>
        /// Byte-for-byte comparison with catalogs made by Microsoft's generator (SharePoint Online
        /// Management Shell 16.0.27709) from 13 Windows 11 fonts, saved in Resources.
        /// </summary>
        [TestMethod]
        public void Build_MatchesMicrosoftGeneratorOnWindowsFonts()
        {
            var resources = Path.Combine(AppContext.BaseDirectory, "Resources");
            var reference = JsonDocument.Parse(System.IO.File.ReadAllBytes(Path.Combine(resources, "OfficeFontCatalog_v2_fontcatalog.fontconfig"))).RootElement;
            var fontsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

            var fonts = new List<CustomFontFile>();
            foreach (var face in reference.GetProperty("Fonts").EnumerateArray().SelectMany(f => f.GetProperty("sf").EnumerateArray()))
            {
                var path = Path.Combine(fontsFolder, face.GetProperty("id").GetString() + "." + face.GetProperty("t").GetString());
                if (!System.IO.File.Exists(path) || new FileInfo(path).Length != face.GetProperty("fs").GetInt64())
                {
                    Assert.Inconclusive($"{path} is missing or is not the version the reference was made from.");
                }
                fonts.Add(new CustomFontFile { FileName = Path.GetFileName(path), Content = System.IO.File.ReadAllBytes(path) });
            }

            var files = OfficeFontCatalogBuilder.Build(fonts, reference.GetProperty("MinorVersion").GetUInt32(), Ignore, Ignore);
            foreach (var file in files)
            {
                var expected = System.IO.File.ReadAllText(Path.Combine(resources, $"OfficeFontCatalog_v{file.MajorVersion}_{file.FileName}"));
                Assert.AreEqual(expected, Encoding.UTF8.GetString(file.Content), $"v{file.MajorVersion}/{file.FileName}");
            }
        }
    }

    [TestClass]
    public class FontLibraryReaderTests
    {
        [DataTestMethod]
        [DataRow("https://contoso.sharepoint.com/sites/Brand/Office%20Fonts", "/sites/Brand/Office Fonts")]
        [DataRow("https://contoso.sharepoint.com/sites/Brand/Office Fonts/", "/sites/Brand/Office Fonts")]
        [DataRow("/sites/Brand/Office Fonts", "/sites/Brand/Office Fonts")]
        [DataRow("sites/Brand/Office%20Fonts", "/sites/Brand/Office Fonts")]
        public void ToServerRelativePath_AcceptsEveryUrlForm(string input, string expected) =>
            Assert.AreEqual(expected, FontLibraryReader.ToServerRelativePath(input));

        [DataTestMethod]
        [DataRow("/sites/Brand/100% Fonts", "/sites/Brand/100% Fonts")]
        [DataRow("https://contoso.sharepoint.com/sites/Brand/Fonts%20Archive", "/sites/Brand/Fonts%20Archive")]
        [DataRow("/sites/Brand/Office Fonts", "/sites/Brand/Office Fonts")]
        public void EncodePath_IsDecodedExactlyOnce(string decoded, string expected) =>
            Assert.AreEqual(expected, FontLibraryReader.ToServerRelativePath(FontLibraryReader.EncodePath(decoded)));

        [TestMethod]
        public void ToAbsoluteUrl_DoesNotDoubleTheHost() =>
            Assert.AreEqual("https://contoso.sharepoint.com/sites/Brand/Office Fonts",
                FontLibraryReader.ToAbsoluteUrl(new Uri("https://contoso.sharepoint.com/"), "https://contoso.sharepoint.com/sites/Brand/Office%20Fonts"));

        [DataTestMethod]
        [DataRow("a.ttf", true)]
        [DataRow("a.OTF", true)]
        [DataRow("a.woff2", false)]
        [DataRow("a.ttc", false)]
        [DataRow("fontcatalog.fontconfig", false)]
        public void IsOfficeFont_AcceptsOnlyTtfAndOtf(string name, bool expected) =>
            Assert.AreEqual(expected, CustomFontsCatalogPublisher.IsOfficeFont(name));
    }

    /// <summary>
    /// Writes a minimal TrueType font (name, OS/2, head, cmap tables) with the given values. Enough for
    /// the catalog builder; not a font anything could render.
    /// </summary>
    internal sealed class TestFont
    {
        public string Family { get; init; } = "Contoso";
        public string TypographicFamily { get; init; }
        public string FullName { get; init; }
        public string Subfamily { get; init; } = "Regular";
        public string TypographicSubfamily { get; init; }
        public ushort Weight { get; init; } = 400;
        public ushort Width { get; init; } = 5;
        public bool Italic { get; init; }
        public uint Revision { get; init; } = 0x00010000;
        public byte[] Panose { get; init; } = [2, 11, 6, 4, 2, 2, 2, 2, 2, 4];
        public uint[] UnicodeRanges { get; init; } = [1, 0, 0, 0];
        public uint[] CodePages { get; init; } = [1, 0];
        /// <summary>Characters U+0020 to this one have a glyph.</summary>
        public ushort LastMappedCharacter { get; init; } = 0x7E;

        public byte[] Bytes()
        {
            var tables = new SortedDictionary<string, byte[]>(StringComparer.Ordinal)
            {
                ["OS/2"] = Os2(),
                ["cmap"] = Cmap(),
                ["head"] = Head(),
                ["name"] = Name(),
            };

            var output = new List<byte>();
            Put32(output, 0x00010000);
            Put16(output, (ushort)tables.Count);
            Put16(output, 0); Put16(output, 0); Put16(output, 0); // searchRange etc.: not read
            var offset = 12 + 16 * tables.Count;
            foreach (var (tag, data) in tables)
            {
                output.AddRange(Encoding.ASCII.GetBytes(tag));
                Put32(output, 0); // checksum: not read
                Put32(output, (uint)offset);
                Put32(output, (uint)data.Length);
                offset += (data.Length + 3) & ~3;
            }
            foreach (var data in tables.Values)
            {
                output.AddRange(data);
                while (output.Count % 4 != 0) output.Add(0);
            }
            return output.ToArray();
        }

        private byte[] Name()
        {
            var names = new List<(ushort Id, string Text)> { (1, Family), (2, Subfamily), (4, FullName ?? Family) };
            if (TypographicFamily != null) names.Add((16, TypographicFamily));
            if (TypographicSubfamily != null) names.Add((17, TypographicSubfamily));

            var storage = new List<byte>();
            var table = new List<byte>();
            Put16(table, 0);
            Put16(table, (ushort)names.Count);
            Put16(table, (ushort)(6 + 12 * names.Count));
            foreach (var (id, text) in names)
            {
                var bytes = Encoding.BigEndianUnicode.GetBytes(text);
                Put16(table, 3); Put16(table, 1); Put16(table, 0x0409); Put16(table, id);
                Put16(table, (ushort)bytes.Length); Put16(table, (ushort)storage.Count);
                storage.AddRange(bytes);
            }
            table.AddRange(storage);
            return table.ToArray();
        }

        private byte[] Os2()
        {
            var t = new byte[96];
            Set16(t, 0, 4);              // version
            Set16(t, 4, Weight);
            Set16(t, 6, Width);
            Panose.CopyTo(t, 32);
            for (var i = 0; i < 4; i++) Set32(t, 42 + i * 4, UnicodeRanges[i]);
            Set16(t, 62, (ushort)(Italic ? 0x0001 : 0x0040)); // fsSelection: ITALIC or REGULAR
            Set32(t, 78, CodePages[0]);
            Set32(t, 82, CodePages[1]);
            return t;
        }

        private byte[] Head()
        {
            var t = new byte[54];
            Set32(t, 0, 0x00010000);
            Set32(t, 4, Revision);
            return t;
        }

        /// <summary>cmap with one format 4 subtable (3,1): U+0020..LastMappedCharacter, then the 0xFFFF end segment.</summary>
        private byte[] Cmap()
        {
            var sub = new List<byte>();
            Put16(sub, 4); Put16(sub, 32); Put16(sub, 0); // format, length (2 segments: 16 + 8 * 2), language
            Put16(sub, 4); Put16(sub, 4); Put16(sub, 1); Put16(sub, 0); // segCountX2, searchRange, entrySelector, rangeShift
            Put16(sub, LastMappedCharacter); Put16(sub, 0xFFFF);        // endCode
            Put16(sub, 0);                                               // reservedPad
            Put16(sub, 0x20); Put16(sub, 0xFFFF);                        // startCode
            Put16(sub, unchecked((ushort)(1 - 0x20))); Put16(sub, 1);   // idDelta: glyph ids from 1
            Put16(sub, 0); Put16(sub, 0);                                // idRangeOffset

            var t = new List<byte>();
            Put16(t, 0); Put16(t, 1);                 // version, numTables
            Put16(t, 3); Put16(t, 1); Put32(t, 12);   // Windows, Unicode BMP, offset
            t.AddRange(sub);
            return t.ToArray();
        }

        private static void Put16(List<byte> b, ushort v) { b.Add((byte)(v >> 8)); b.Add((byte)v); }
        private static void Put32(List<byte> b, uint v) { Put16(b, (ushort)(v >> 16)); Put16(b, (ushort)v); }
        private static void Set16(byte[] b, int o, ushort v) { b[o] = (byte)(v >> 8); b[o + 1] = (byte)v; }
        private static void Set32(byte[] b, int o, uint v) { Set16(b, o, (ushort)(v >> 16)); Set16(b, o + 2, (ushort)v); }
    }
}
