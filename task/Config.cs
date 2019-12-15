using System.Collections.Generic;

namespace ApiTools.Codegen.Task
{
    /// <summary>
    /// Specifies build configuration and project type output settings.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Default namespace that will be used in generated files, when not specified in the individual <see cref="ProjectSettings"/>.
        /// </summary>
        public string DefaultNamespace { get; set; }

        /// <summary>
        /// Default directory where generated files will be created, when not specified in the individual <see cref="ProjectSettings"/>.
        /// </summary>
        public string DefaultOutputPath { get; set; }

        /// <summary>
        /// Configuration for individual project type outputs.
        /// </summary>
        public ProjectSettings[] Projects { get; set; }
    }
}