using PnP.Framework.Provisioning.Model.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    /// <summary>
    /// Shared logic to turn the string value of a -Configuration parameter into a configuration object.
    /// The value is either the path to a file holding the JSON, or the JSON itself.
    /// </summary>
    internal static class ConfigurationPipeBindHelper
    {
        /// <summary>
        /// Number of characters of the passed in value echoed back in an error message. Generous enough to
        /// show a file path in full, as that is what the user needs to spot a typo, while still keeping the
        /// message readable when the value turns out to be neither a path nor JSON.
        /// </summary>
        private const int MaxValueInMessage = 200;

        private static readonly JsonSerializerOptions StrictOptions = new()
        {
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
        };

        /// <summary>
        /// Resolves the value of a -Configuration parameter to a configuration object.
        /// </summary>
        /// <param name="value">The path to a file holding the JSON, or the JSON itself</param>
        /// <param name="currentFileSystemLocation">The location to resolve a relative path against</param>
        /// <param name="fromString">Deserializes the JSON into the configuration object</param>
        /// <param name="logWarning">Reports parts of the configuration which are not recognized and are therefore ignored</param>
        /// <exception cref="PSArgumentException">No value was passed in, the file cannot be read, or the JSON cannot be parsed</exception>
        internal static T Resolve<T>(string value, string currentFileSystemLocation, Func<string, T> fromString, Action<string> logWarning = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new PSArgumentException("No configuration was specified. Specify the path to a file holding the JSON configuration, or pass the JSON itself.");
            }

            // Resolve a path first, so that a file whose name starts with a JSON character keeps working.
            var path = ResolvePath(value, currentFileSystemLocation);
            if (path != null && File.Exists(path))
            {
                return Deserialize(ReadFile(path), $"configuration file '{path}'", fromString, logWarning);
            }

            // Not an existing file, so the value itself is meant to be the JSON. A byte order mark does not
            // count as whitespace, so it is removed explicitly before looking at the first character.
            var json = value.TrimStart('\uFEFF').TrimStart();
            if (json.StartsWith('{'))
            {
                return Deserialize(json, "the JSON passed in", fromString, logWarning);
            }
            if (json.StartsWith('[') || json.Equals("null", StringComparison.Ordinal))
            {
                throw new PSArgumentException($"The JSON passed in is not a configuration object: {Abbreviate(json)}");
            }

            if (path != null && Directory.Exists(path))
            {
                throw new PSArgumentException($"'{path}' is a folder. Specify the path to a file holding the JSON configuration, or pass the JSON itself.");
            }
            throw new PSArgumentException($"Configuration file '{Abbreviate(path ?? value)}' does not exist. Specify the path to an existing file holding the JSON configuration, or pass the JSON itself.");
        }

        private static string ResolvePath(string value, string currentFileSystemLocation)
        {
            try
            {
                return Path.IsPathRooted(value) || string.IsNullOrEmpty(currentFileSystemLocation)
                    ? value
                    : Path.Combine(currentFileSystemLocation, value);
            }
            catch (ArgumentException)
            {
                // The value cannot be a path at all, so it can only have been meant as JSON.
                return null;
            }
        }

        private static string ReadFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is NotSupportedException)
            {
                throw new PSArgumentException($"Could not read configuration file '{path}': {ex.Message}", ex);
            }
        }

        private static T Deserialize<T>(string json, string source, Func<string, T> fromString, Action<string> logWarning) where T : class
        {
            T configuration;
            try
            {
                configuration = fromString(json);
            }
            catch (Exception ex) when (ex is JsonException || ex is NotSupportedException)
            {
                throw new PSArgumentException($"Could not parse {source}: {ex.Message}", ex);
            }

            if (configuration == null)
            {
                throw new PSArgumentException($"Could not read a configuration from {source}.");
            }

            WarnOnIgnoredContent<T>(json, source, logWarning);
            return configuration;
        }

        /// <summary>
        /// The configuration is deserialized leniently, so a property whose name is misspelled or misplaced and a
        /// handler which is not recognized are both accepted and then have no effect, which reads as the
        /// configuration being ignored. Both are reported as a warning rather than rejected, so that a
        /// configuration holding something this version does not know about still applies the rest.
        /// </summary>
        private static void WarnOnIgnoredContent<T>(string json, string source, Action<string> logWarning)
        {
            if (logWarning == null)
            {
                return;
            }

            JsonNode root;
            try
            {
                root = JsonNode.Parse(json);
            }
            catch (JsonException)
            {
                return;
            }

            if (root is not JsonObject rootObject)
            {
                return;
            }

            // A $schema reference exists for editor support and is not part of the configuration itself.
            rootObject.Remove("$schema");

            foreach (var handler in UnrecognizedHandlers(rootObject))
            {
                logWarning($"Handler '{handler}' in {source} is not recognized and is ignored. Handler names are case sensitive.");
            }

            try
            {
                JsonSerializer.Deserialize<T>(rootObject.ToJsonString(), StrictOptions);
            }
            catch (JsonException ex)
            {
                logWarning($"A property in {source} is not recognized and is ignored, so the setting it holds has no effect: {ex.Message}");
            }
            catch (NotSupportedException)
            {
                // Reported already by the lenient pass, which threw nothing, so there is nothing to add here.
            }
        }

        /// <summary>
        /// Mirrors ListEnumConverter, which drops any handler it cannot parse, case sensitively and without a word.
        /// </summary>
        private static IEnumerable<string> UnrecognizedHandlers(JsonObject rootObject)
        {
            if (rootObject["handlers"] is not JsonArray handlers)
            {
                yield break;
            }

            foreach (var handler in handlers)
            {
                if (handler is JsonValue value
                    && value.TryGetValue<string>(out var name)
                    && !Enum.TryParse<ConfigurationHandler>(name, out _))
                {
                    yield return name;
                }
            }
        }

        private static string Abbreviate(string value)
        {
            var singleLine = value.Replace("\r", " ").Replace("\n", " ");
            return singleLine.Length <= MaxValueInMessage
                ? singleLine
                : singleLine[..MaxValueInMessage] + "...";
        }
    }
}
