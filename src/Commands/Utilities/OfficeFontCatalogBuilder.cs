using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>A catalog file for the Office font library: &lt;library&gt;/&lt;MajorVersion&gt;/&lt;FileName&gt;.</summary>
    internal sealed class OfficeFontCatalogFile
    {
        public int MajorVersion { get; init; }
        public string FileName { get; init; }
        public byte[] Content { get; init; }
    }

    /// <summary>
    /// Builds the Office font catalog (fontcatalog*.fontconfig) in managed code, with no Microsoft
    /// binaries, so it runs wherever .NET runs.
    ///
    /// The format is not documented by Microsoft. It was reproduced from the output of Microsoft's
    /// generator (CustomFontDataGenerator, SharePoint Online Management Shell 16.0.27709) and checked
    /// byte for byte against it. OfficeFontCatalog.md, next to this file, describes every field, how
    /// it was verified, and what is known not to match. Read it before changing this file.
    ///
    /// Output: catalog v1 (fontcatalog, fontcatalog_hier, fontcatalog_light) and catalog v2
    /// (fontcatalog, fontcatalog_hier). Key order and value forms must stay exactly as written here.
    /// </summary>
    internal static class OfficeFontCatalogBuilder
    {
        public static readonly int[] MajorVersions = [1, 2];

        /// <summary>
        /// "a": the same value (0xFFFFFE7F) for every font in every catalog Microsoft's generator
        /// produced. Its meaning is unknown (it looks like a bit mask with bits 7 and 8 off).
        /// </summary>
        private const uint UnknownFamilyMask = 0xFFFFFE7F;

        /// <summary>"Expiration": always 14 in Microsoft's output (days, probably).</summary>
        private const int Expiration = 14;

        private static readonly JsonWriterOptions JsonOptions = new()
        {
            // Microsoft's files keep non-ASCII names as raw UTF-8 ("맑은 고딕"), not \u escapes, and have no BOM.
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            Indented = false,
        };

        private enum CatalogKind
        {
            /// <summary>fontcatalog.fontconfig: names without "ful"/"sub" details.</summary>
            Main,
            /// <summary>fontcatalog_hier.fontconfig: with full names and subfamilies.</summary>
            Hierarchy,
            /// <summary>fontcatalog_light.fontconfig (v1 only): no Expiration, fam, fs or v.</summary>
            Light,
        }

        /// <summary>
        /// Reads every font and builds the catalog files of every supported version.
        /// Throws <see cref="InvalidDataException"/> naming every file that is not a usable font.
        /// </summary>
        public static List<OfficeFontCatalogFile> Build(IReadOnlyCollection<CustomFontFile> fonts, uint minorVersion, Action<string> warn, Action<string> verbose)
        {
            var faces = new List<OfficeFontFace>(fonts.Count);
            var failed = new List<string>();
            foreach (var font in fonts)
            {
                try
                {
                    var face = OfficeFontFace.Read(font.FileName, font.Content);
                    faces.Add(face);
                    if (face.IsVariable) verbose($"Variable font: {face.Family} ({font.FileName})");
                    if (face.IsPostScript) verbose($"PostScript (CFF) outlines: {face.Family} ({font.FileName})");
                    if (face.FirstMissingAscii is int missing)
                    {
                        // Same check as Microsoft's generator (AsciiSupportFailure); a warning, not an error.
                        warn($"Font catalog: {face.Family} ({font.FileName}) has no glyph for U+{missing:X4} '{(char)missing}'");
                    }
                }
                catch (InvalidDataException ex)
                {
                    failed.Add($"{font.FileName} ({ex.Message})");
                }
            }
            if (failed.Count > 0)
            {
                throw new InvalidDataException($"These files are not usable TrueType/OpenType fonts: {string.Join(", ", failed)}");
            }

            // Families are ordered by their first file name, case-insensitively: Book Antiqua (ANTQUAB)
            // comes before Arial (arial). Faces in a family keep that order too.
            var families = faces
                .OrderBy(f => f.Id, StringComparer.OrdinalIgnoreCase)
                .GroupBy(f => f.Family, StringComparer.OrdinalIgnoreCase)
                .Select(g => g.ToList())
                .ToList();

            var files = new List<OfficeFontCatalogFile>(5);
            foreach (var major in MajorVersions)
            {
                files.Add(Write(major, "fontcatalog.fontconfig", families, minorVersion, CatalogKind.Main));
                files.Add(Write(major, "fontcatalog_hier.fontconfig", families, minorVersion, CatalogKind.Hierarchy));
                if (major == 1) files.Add(Write(major, "fontcatalog_light.fontconfig", families, minorVersion, CatalogKind.Light));
            }
            return files;
        }

        private static OfficeFontCatalogFile Write(int major, string fileName, List<List<OfficeFontFace>> families, uint minor, CatalogKind kind)
        {
            var v2 = major >= 2;
            using var buffer = new MemoryStream();
            using (var json = new Utf8JsonWriter(buffer, JsonOptions))
            {
                json.WriteStartObject();
                json.WriteNumber("MajorVersion", major);
                json.WriteNumber("MinorVersion", minor);
                if (kind != CatalogKind.Light) json.WriteNumber("Expiration", Expiration);
                json.WriteStartArray("Fonts");
                foreach (var family in families)
                {
                    var first = family[0];
                    json.WriteStartObject();
                    json.WriteStartArray("a"); json.WriteNumberValue(UnknownFamilyMask); json.WriteEndArray();
                    json.WriteString("f", first.Family);
                    if (kind != CatalogKind.Light)
                    {
                        WriteScripted(json, "fam", [("Latn", first.Family)]);
                        if (v2) WriteLocalized(json, "ldf", first.LocalizedFamily);
                    }
                    json.WriteStartArray("sf");
                    foreach (var face in family) WriteFace(json, face, v2, kind);
                    json.WriteEndArray();
                    json.WriteEndObject();
                }
                json.WriteEndArray();
                json.WriteEndObject();
            }
            return new OfficeFontCatalogFile { MajorVersion = major, FileName = fileName, Content = buffer.ToArray() };
        }

        private static void WriteFace(Utf8JsonWriter json, OfficeFontFace face, bool v2, CatalogKind kind)
        {
            var hier = kind == CatalogKind.Hierarchy;
            json.WriteStartObject();
            if (v2) { json.WriteStartArray("ads"); json.WriteEndArray(); } // always empty in Microsoft's output
            json.WriteStartArray("c");
            json.WriteNumberValue(face.CodePageRange1);
            json.WriteNumberValue(face.CodePageRange2);
            json.WriteEndArray();
            json.WriteString("dn", face.FullName);
            if (kind != CatalogKind.Light) json.WriteNumber("fs", face.FileSize);
            // v1 has only the English full name; v2 adds the localized ones.
            WriteScripted(json, "ful", !hier ? [] : v2 ? face.ScriptedFullNames : face.ScriptedFullNames.Take(1));
            json.WriteString("gn", face.GdiFamily);
            json.WriteString("id", face.Id);
            if (v2)
            {
                WriteLocalized(json, "ldn", face.LocalizedFullNames);
                WriteLocalized(json, "lgn", face.LocalizedGdiFamily);
            }
            json.WriteStartArray("p");
            foreach (var b in face.Panose) json.WriteNumberValue(b);
            json.WriteEndArray();
            WriteScripted(json, "sub", hier ? [("Latn", v2 ? face.Subfamily : face.SubfamilyV1)] : []);
            json.WriteString("t", face.Type);
            json.WriteStartArray("u");
            foreach (var range in face.UnicodeRanges) json.WriteNumberValue(range);
            json.WriteEndArray();
            if (kind != CatalogKind.Light) json.WriteNumber("v", face.Revision);
            json.WriteNumber("w", face.WeightWidthStyle);
            json.WriteEndObject();
        }

        /// <summary>[{"lcp":0,"lsc":script,"ltx":text}...]. "lcp" is 0 in every sample.</summary>
        private static void WriteScripted(Utf8JsonWriter json, string name, IEnumerable<(string Script, string Text)> values)
        {
            json.WriteStartArray(name);
            foreach (var (script, text) in values)
            {
                json.WriteStartObject();
                json.WriteNumber("lcp", 0);
                json.WriteString("lsc", script);
                json.WriteString("ltx", text);
                json.WriteEndObject();
            }
            json.WriteEndArray();
        }

        /// <summary>[{"lng":culture,"lnv":text}...].</summary>
        private static void WriteLocalized(Utf8JsonWriter json, string name, IEnumerable<(string Culture, string Text)> values)
        {
            json.WriteStartArray(name);
            foreach (var (culture, text) in values)
            {
                json.WriteStartObject();
                json.WriteString("lng", culture);
                json.WriteString("lnv", text);
                json.WriteEndObject();
            }
            json.WriteEndArray();
        }
    }
}
