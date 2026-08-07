using System;
using System.IO;
using System.Management.Automation;
using System.Text.Json;

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

        /// <summary>
        /// Resolves the value of a -Configuration parameter to a configuration object.
        /// </summary>
        /// <param name="value">The path to a file holding the JSON, or the JSON itself</param>
        /// <param name="currentFileSystemLocation">The location to resolve a relative path against</param>
        /// <param name="fromString">Deserializes the JSON into the configuration object</param>
        /// <exception cref="PSArgumentException">No value was passed in, the file cannot be read, or the JSON cannot be parsed</exception>
        internal static T Resolve<T>(string value, string currentFileSystemLocation, Func<string, T> fromString) where T : class
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new PSArgumentException("No configuration was specified. Specify the path to a file holding the JSON configuration, or pass the JSON itself.");
            }

            // Resolve a path first, so that a file whose name starts with a JSON character keeps working.
            var path = ResolvePath(value, currentFileSystemLocation);
            if (path != null && File.Exists(path))
            {
                return Deserialize(ReadFile(path), $"configuration file '{path}'", fromString);
            }

            // Not an existing file, so the value itself is meant to be the JSON. A byte order mark does not
            // count as whitespace, so it is removed explicitly before looking at the first character.
            var json = value.TrimStart('\uFEFF').TrimStart();
            if (json.StartsWith('{'))
            {
                return Deserialize(json, "the JSON passed in", fromString);
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

        private static T Deserialize<T>(string json, string source, Func<string, T> fromString) where T : class
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

            return configuration ?? throw new PSArgumentException($"Could not read a configuration from {source}.");
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
