using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// What the Office font catalog needs from one font file, read from its OpenType tables
    /// (https://learn.microsoft.com/typography/opentype/spec/). Names follow DirectWrite's rules,
    /// because Office groups fonts the way DirectWrite does. See OfficeFontCatalog.md.
    /// </summary>
    internal sealed class OfficeFontFace
    {
        /// <summary>File name without extension ("arialbd"): the face id in the catalog.</summary>
        public string Id { get; private init; }
        /// <summary>File extension without the dot, case kept ("ttf", "TTF", "otf").</summary>
        public string Type { get; private init; }
        public long FileSize { get; private init; }

        /// <summary>Family that groups faces in the catalog ("Calibri" for Calibri Light).</summary>
        public string Family { get; private set; }
        /// <summary>GDI (legacy) family, name ID 1 ("Calibri Light").</summary>
        public string GdiFamily { get; private set; }
        /// <summary>Full name, name ID 4 ("Calibri Light").</summary>
        public string FullName { get; private set; }
        /// <summary>English subfamily, for catalog v2 ("Light").</summary>
        public string Subfamily { get; private set; }
        /// <summary>Subfamily for catalog v1, which uses the record with the highest language id.</summary>
        public string SubfamilyV1 { get; private set; }

        public IReadOnlyList<(string Culture, string Text)> LocalizedFamily { get; private set; } = [];
        public IReadOnlyList<(string Culture, string Text)> LocalizedGdiFamily { get; private set; } = [];
        public IReadOnlyList<(string Culture, string Text)> LocalizedFullNames { get; private set; } = [];
        /// <summary>English full name first, then the localized ones, with their ISO 15924 script.</summary>
        public IReadOnlyList<(string Script, string Text)> ScriptedFullNames { get; private set; }

        public byte[] Panose { get; private init; }
        public uint[] UnicodeRanges { get; private init; }
        public uint CodePageRange1 { get; private init; }
        public uint CodePageRange2 { get; private init; }
        /// <summary>head.fontRevision as stored (16.16 fixed): 65536 is version 1.0.</summary>
        public uint Revision { get; private init; }
        /// <summary>weight &lt;&lt; 16 | width &lt;&lt; 8 | style (0 normal, 1 oblique, 2 italic).</summary>
        public uint WeightWidthStyle { get; private init; }

        public bool IsVariable { get; private init; }
        public bool IsPostScript { get; private init; }
        /// <summary>First printable ASCII character (U+0020-U+007E) without a glyph, or null.</summary>
        public int? FirstMissingAscii { get; private init; }

        // OpenType name IDs used here.
        private const ushort NameFamily = 1, NameSubfamily = 2, NameFullName = 4,
            NameTypographicFamily = 16, NameTypographicSubfamily = 17, NameWwsFamily = 21;

        // sfnt version tags.
        private const uint TagTrueType = 0x00010000, TagOpenType = 0x4F54544F /* OTTO */, TagApple = 0x74727565 /* true */,
            TagCollection = 0x74746366 /* ttcf */;

        private const int Os2MinimumLength = 64;   // through fsSelection
        private const int Os2CodePagesEnd = 86;    // ulCodePageRange1/2, OS/2 version 1 and later

        /// <summary>Reads a .ttf/.otf file. Throws <see cref="InvalidDataException"/> when it is not a usable font.</summary>
        public static OfficeFontFace Read(string fileName, byte[] data)
        {
            var font = new SfntReader(data);
            var names = NameTable.Read(font);

            var os2 = font.Table("OS/2");
            if (os2.Length < Os2MinimumLength) throw new InvalidDataException("the OS/2 table is too short");
            var os2Version = font.U16(os2.Offset);
            var selection = font.U16(os2.Offset + 62);
            // DirectWrite style: italic bit first, then the OBLIQUE bit (OS/2 version 4).
            var style = (selection & 0x0001) != 0 ? 2u : (selection & 0x0200) != 0 ? 1u : 0u;
            var hasCodePages = os2Version >= 1 && os2.Length >= Os2CodePagesEnd;

            var face = new OfficeFontFace
            {
                Id = Path.GetFileNameWithoutExtension(fileName),
                Type = Path.GetExtension(fileName).TrimStart('.'),
                FileSize = data.Length,
                IsVariable = font.Has("fvar"),
                IsPostScript = font.Has("CFF ") || font.Has("CFF2"),
                Panose = font.Bytes(os2.Offset + 32, 10),
                UnicodeRanges = [font.U32(os2.Offset + 42), font.U32(os2.Offset + 46), font.U32(os2.Offset + 50), font.U32(os2.Offset + 54)],
                CodePageRange1 = hasCodePages ? font.U32(os2.Offset + 78) : 0,
                CodePageRange2 = hasCodePages ? font.U32(os2.Offset + 82) : 0,
                WeightWidthStyle = ((uint)font.U16(os2.Offset + 4) << 16) | ((uint)font.U16(os2.Offset + 6) << 8) | style,
                Revision = font.U32(font.Table("head").Offset + 4),
                FirstMissingAscii = Cmap.FirstMissing(font, 0x20, 0x7E),
            };

            face.GdiFamily = names.English(NameFamily) ?? throw new InvalidDataException("the font has no family name");
            // DirectWrite's family: WWS family, else typographic family, else the GDI family without
            // trailing style words ("Arial Rounded MT Bold" -> "Arial Rounded MT").
            face.Family = names.English(NameWwsFamily) ?? names.English(NameTypographicFamily) ?? StyleWords.Strip(face.GdiFamily);
            face.FullName = names.English(NameFullName) ?? face.GdiFamily;
            face.Subfamily = names.English(NameTypographicSubfamily) ?? names.English(NameSubfamily) ?? "Regular";
            // v1 takes the record with the highest language id, so Arial Bold is "Gras" (fr-CA) there.
            face.SubfamilyV1 = names.Last(NameTypographicSubfamily) ?? names.Last(NameSubfamily) ?? face.Subfamily;

            // Localized names are only written for fonts whose family name is localized (CJK fonts...).
            face.LocalizedGdiFamily = names.Localized(NameFamily);
            if (face.LocalizedGdiFamily.Count > 0)
            {
                var typographic = names.Localized(NameTypographicFamily);
                face.LocalizedFamily = typographic.Count > 0 ? typographic : face.LocalizedGdiFamily;
                face.LocalizedFullNames = names.Localized(NameFullName);
            }

            if (face.IsVariable && font.Has("STAT"))
            {
                face.ApplyDefaultInstanceNames(font, names);
            }

            face.ScriptedFullNames = [("Latn", face.FullName), .. face.LocalizedFullNames.Select(l => (Scripts.Of(l.Culture), l.Text))];
            return face;
        }

        /// <summary>
        /// Variable fonts are named after their default instance, as DirectWrite does: the STAT names of
        /// the default axis values that are not elidable. Weight/width/slope names go into the face name,
        /// the others (optical size...) into the family name. Segoe UI Variable, whose default optical size
        /// is "Text", becomes family "Segoe UI Variable Text". A STAT table that cannot be read keeps the
        /// plain names: naming must not reject a font Office can use.
        /// </summary>
        private void ApplyDefaultInstanceNames(SfntReader font, NameTable names)
        {
            List<(int Ordering, string Text)> styleNames = [], familyNames = [];
            try
            {
                var fvar = font.Table("fvar").Offset;
                var defaults = new Dictionary<string, uint>(StringComparer.Ordinal);
                var axesOffset = font.U16(fvar + 4);
                var axisCount = font.U16(fvar + 8);
                var axisSize = font.U16(fvar + 10);
                for (var a = 0; a < axisCount; a++)
                {
                    var record = fvar + axesOffset + a * axisSize;
                    defaults[font.Tag(record)] = font.U32(record + 8);
                }
                // Axes STAT describes but fvar does not vary take the font's fixed value (16.16):
                // an italic file of a variable family has "ital" = 1 ("Italic"), not 0.
                const uint FixedOne = 0x00010000;
                defaults.TryAdd("ital", (WeightWidthStyle & 0xFF) == 2 ? FixedOne : 0);
                defaults.TryAdd("wght", (WeightWidthStyle >> 16) * FixedOne);

                var stat = font.Table("STAT").Offset;
                var designAxisSize = font.U16(stat + 4);
                var designAxisCount = font.U16(stat + 6);
                var designAxes = stat + (int)font.U32(stat + 8);
                var valueCount = font.U16(stat + 12);
                var valueOffsets = stat + (int)font.U32(stat + 14);

                for (var v = 0; v < valueCount; v++)
                {
                    var value = valueOffsets + font.U16(valueOffsets + v * 2);
                    var format = font.U16(value);
                    if (format is < 1 or > 3) continue; // format 4 combines several axes
                    var axisIndex = font.U16(value + 2);
                    var flags = font.U16(value + 4);
                    const ushort ElidableAxisValueName = 0x0002;
                    if ((flags & ElidableAxisValueName) != 0 || axisIndex >= designAxisCount) continue;

                    var axis = designAxes + axisIndex * designAxisSize;
                    var tag = font.Tag(axis);
                    if (!defaults.TryGetValue(tag, out var defaultValue)) continue;
                    var matches = format == 2
                        ? (int)defaultValue >= (int)font.U32(value + 12) && (int)defaultValue <= (int)font.U32(value + 16)
                        : font.U32(value + 8) == defaultValue;
                    if (!matches) continue;

                    var text = names.English(font.U16(value + 6));
                    // DirectWrite also drops the "normal" names, even when the font does not flag them
                    // as elidable: Inter's default instance is "Inter", not "Inter Regular".
                    if (string.IsNullOrEmpty(text) || NormalStyleNames.Contains(text)) continue;
                    (tag is "wght" or "wdth" or "ital" or "slnt" ? styleNames : familyNames).Add((font.U16(axis + 6), text));
                }
            }
            catch (InvalidDataException)
            {
                return;
            }
            if (styleNames.Count == 0 && familyNames.Count == 0)
            {
                // The default instance is the plain family: its full name is the family name, not
                // name ID 4 ("Inter", not "Inter Regular").
                FullName = Family;
                return;
            }

            static string Join(IEnumerable<(int Ordering, string Text)> parts) => string.Join(" ", parts.OrderBy(p => p.Ordering).Select(p => p.Text));
            if (familyNames.Count > 0)
            {
                Family = Family + " " + Join(familyNames);
                GdiFamily = Family;
            }
            FullName = styleNames.Count > 0 ? Family + " " + Join(styleNames) : Family;
            Subfamily = Join(familyNames.Concat(styleNames));
        }

        /// <summary>STAT axis value names that mean "the normal style" and never appear in a name.</summary>
        private static readonly HashSet<string> NormalStyleNames = new(StringComparer.OrdinalIgnoreCase)
        {
            "Regular", "Normal", "Roman", "Upright", "Standard",
        };

        /// <summary>Bounds-checked big-endian reads over an sfnt file and its table directory.</summary>
        private sealed class SfntReader
        {
            private readonly byte[] _data;
            private readonly Dictionary<string, (int Offset, int Length)> _tables = new(StringComparer.Ordinal);

            public SfntReader(byte[] data)
            {
                _data = data ?? throw new InvalidDataException("no data");
                var version = U32(0);
                if (version == TagCollection) throw new InvalidDataException("font collections (.ttc) are not supported");
                if (version != TagTrueType && version != TagOpenType && version != TagApple) throw new InvalidDataException("not a TrueType/OpenType font");

                var count = U16(4);
                for (var i = 0; i < count; i++)
                {
                    var record = 12 + i * 16;
                    long offset = U32(record + 8), length = U32(record + 12);
                    if (offset + length > data.Length) throw new InvalidDataException($"the '{Tag(record)}' table is out of range");
                    _tables[Tag(record)] = ((int)offset, (int)length);
                }
                foreach (var required in new[] { "name", "OS/2", "head", "cmap" })
                {
                    if (!Has(required)) throw new InvalidDataException($"the '{required}' table is missing");
                }
            }

            public bool Has(string tag) => _tables.ContainsKey(tag);
            public (int Offset, int Length) Table(string tag) =>
                _tables.TryGetValue(tag, out var table) ? table : throw new InvalidDataException($"the '{tag}' table is missing");

            public ushort U16(int offset) => BinaryPrimitives.ReadUInt16BigEndian(Span(offset, 2));
            public uint U32(int offset) => BinaryPrimitives.ReadUInt32BigEndian(Span(offset, 4));
            public string Tag(int offset) => Encoding.ASCII.GetString(Span(offset, 4));
            public byte[] Bytes(int offset, int length) => Span(offset, length).ToArray();
            public string Text(int offset, int length, Encoding encoding) => encoding.GetString(Span(offset, length));

            private ReadOnlySpan<byte> Span(int offset, int length) =>
                offset >= 0 && length >= 0 && (long)offset + length <= _data.Length
                    ? _data.AsSpan(offset, length)
                    : throw new InvalidDataException("the font data ends too early");
        }

        /// <summary>The 'name' table: Windows (platform 3) records, Mac Roman as a fallback.</summary>
        private sealed class NameTable
        {
            private const ushort EnglishUs = 0x0409;
            private const ushort PlatformUnicode = 0, PlatformMac = 1, PlatformWindows = 3;

            private readonly List<(ushort Platform, ushort Language, ushort NameId, string Text)> _records;

            private NameTable(List<(ushort, ushort, ushort, string)> records) => _records = records;

            public static NameTable Read(SfntReader font)
            {
                var table = font.Table("name").Offset;
                var count = font.U16(table + 2);
                var storage = table + font.U16(table + 4);
                var records = new List<(ushort, ushort, ushort, string)>(count);
                for (var i = 0; i < count; i++)
                {
                    var r = table + 6 + i * 12;
                    var platform = font.U16(r);
                    var encoding = font.U16(r + 2);
                    Encoding textEncoding = platform switch
                    {
                        PlatformWindows or PlatformUnicode => Encoding.BigEndianUnicode,
                        PlatformMac when encoding == 0 => Encoding.Latin1,
                        _ => null,
                    };
                    if (textEncoding == null) continue;
                    try
                    {
                        var text = font.Text(storage + font.U16(r + 10), font.U16(r + 8), textEncoding);
                        if (!string.IsNullOrEmpty(text)) records.Add((platform, font.U16(r + 4), font.U16(r + 6), text));
                    }
                    catch (InvalidDataException)
                    {
                        // A name record pointing past the end: skip it, the font may still have the names we need.
                    }
                }
                return new NameTable(records);
            }

            private IEnumerable<(ushort Platform, ushort Language, ushort NameId, string Text)> Windows(ushort id) =>
                _records.Where(n => n.Platform == PlatformWindows && n.NameId == id);

            /// <summary>English (US) name, else the first Windows one, else the Mac Roman one.</summary>
            public string English(ushort id) =>
                Windows(id).FirstOrDefault(n => n.Language == EnglishUs).Text
                ?? Windows(id).FirstOrDefault().Text
                ?? _records.FirstOrDefault(n => n.Platform == PlatformMac && n.NameId == id && n.Language == 0).Text;

            /// <summary>The Windows name with the highest language id.</summary>
            public string Last(ushort id) => Windows(id).OrderBy(n => n.Language).LastOrDefault().Text;

            /// <summary>Non-English names that differ from the English one, as (culture, text), one per culture.</summary>
            public List<(string Culture, string Text)> Localized(ushort id)
            {
                var english = English(id);
                var result = new List<(string Culture, string Text)>();
                foreach (var n in Windows(id).Where(n => n.Language != EnglishUs && n.Text != english).OrderBy(n => n.Language))
                {
                    var culture = Scripts.CultureName(n.Language);
                    if (culture != null && !result.Any(r => r.Culture == culture)) result.Add((culture, n.Text));
                }
                return result;
            }
        }

        private static class Scripts
        {
            public static string CultureName(ushort lcid)
            {
                try { return CultureInfo.GetCultureInfo(lcid).Name; }
                catch (CultureNotFoundException) { return null; }
            }

            /// <summary>ISO 15924 script of a culture ("ko-KR" -> "Hang"); Latin when not listed.</summary>
            public static string Of(string culture) => culture switch
            {
                "zh-CN" or "zh-SG" => "Hans",
                "zh-TW" or "zh-HK" or "zh-MO" => "Hant",
                _ => culture.Split('-')[0] switch
                {
                    "ko" => "Hang",
                    "ja" => "Jpan",
                    "zh" => "Hans",
                    "ar" or "fa" or "ur" => "Arab",
                    "he" or "yi" => "Hebr",
                    "ru" or "uk" or "bg" or "be" or "sr" or "mk" or "kk" => "Cyrl",
                    "el" => "Grek",
                    "th" => "Thai",
                    "hi" or "mr" or "ne" => "Deva",
                    "bn" => "Beng",
                    "ta" => "Taml",
                    "hy" => "Armn",
                    "ka" => "Geor",
                    _ => "Latn",
                },
            };
        }

        /// <summary>Style words DirectWrite removes from the end of a family name that has no typographic family.</summary>
        private static class StyleWords
        {
            private static readonly HashSet<string> Words = new(StringComparer.OrdinalIgnoreCase)
            {
                "Thin", "Hairline", "ExtraLight", "UltraLight", "Light", "SemiLight", "DemiLight",
                "Regular", "Normal", "Medium", "SemiBold", "DemiBold", "Demi", "Bold", "ExtraBold", "UltraBold",
                "Black", "Heavy", "ExtraBlack", "UltraBlack", "Italic", "Oblique",
                "Condensed", "SemiCondensed", "ExtraCondensed", "UltraCondensed", "Narrow", "Compressed",
                "Expanded", "SemiExpanded", "ExtraExpanded", "UltraExpanded", "Extended", "Wide",
            };

            public static string Strip(string family)
            {
                var words = family.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                while (words.Count > 1 && Words.Contains(words[^1])) words.RemoveAt(words.Count - 1);
                return string.Join(" ", words);
            }
        }

        /// <summary>Character-to-glyph lookups in the 'cmap' table (formats 4 and 12).</summary>
        private static class Cmap
        {
            public static int? FirstMissing(SfntReader font, int first, int last)
            {
                var table = font.Table("cmap").Offset;
                int format4 = -1, format12 = -1;
                try
                {
                    var count = font.U16(table + 2);
                    for (var i = 0; i < count; i++)
                    {
                        var r = table + 4 + i * 8;
                        var platform = font.U16(r);
                        var encoding = font.U16(r + 2);
                        var sub = table + (int)font.U32(r + 4);
                        var format = font.U16(sub);
                        // Unicode full repertoire (3,10) or Unicode platform; BMP (3,1), symbol (3,0).
                        if (format == 12 && (platform == 3 && encoding == 10 || platform == 0)) format12 = sub;
                        if (format == 4 && format4 < 0 && (platform == 3 && encoding is 0 or 1 || platform == 0)) format4 = sub;
                    }
                    if (format12 < 0 && format4 < 0) return first;

                    for (var cp = first; cp <= last; cp++)
                    {
                        if ((format12 >= 0 ? Glyph12(font, format12, cp) : Glyph4(font, format4, cp)) == 0) return cp;
                    }
                    return null;
                }
                catch (InvalidDataException)
                {
                    return first; // unreadable cmap: report it as a warning, do not reject the font
                }
            }

            private static int Glyph4(SfntReader font, int sub, int cp)
            {
                var segCount = font.U16(sub + 6) / 2;
                var ends = sub + 14;
                var starts = ends + segCount * 2 + 2;
                var deltas = starts + segCount * 2;
                var ranges = deltas + segCount * 2;
                for (var s = 0; s < segCount; s++)
                {
                    if (cp > font.U16(ends + s * 2)) continue;
                    var start = font.U16(starts + s * 2);
                    if (cp < start) return 0;
                    var delta = (short)font.U16(deltas + s * 2);
                    var rangeOffset = font.U16(ranges + s * 2);
                    if (rangeOffset == 0) return (cp + delta) & 0xFFFF;
                    var glyph = font.U16(ranges + s * 2 + rangeOffset + (cp - start) * 2);
                    return glyph == 0 ? 0 : (glyph + delta) & 0xFFFF;
                }
                return 0;
            }

            private static int Glyph12(SfntReader font, int sub, int cp)
            {
                var groups = font.U32(sub + 12);
                for (var g = 0; g < groups; g++)
                {
                    var r = sub + 16 + g * 12;
                    var start = font.U32(r);
                    if (cp < start) continue;
                    if (cp <= font.U32(r + 4)) return (int)(font.U32(r + 8) + (cp - start));
                }
                return 0;
            }
        }
    }
}
