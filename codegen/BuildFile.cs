using System.IO;

namespace ApiTools.Codegen
{
    public class BuildFile
    {
        /// <summary>
        /// The path and filename of this file, relative to the root of the project.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The generated content of this file.
        /// </summary>
        internal string Content { get; set; }

        /// <summary>
        /// Write the contents of this file to the specified <see cref="TextWriter"/>.
        /// </summary>
        public void Write(TextWriter writer)
        {
            writer.Write(Content);
        }
    }
}