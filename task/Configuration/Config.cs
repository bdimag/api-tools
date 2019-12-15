namespace ApiTools.Codegen.Task.Configuration
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Serializable property")]
        public ProjectSettingsCollection Projects { get; set; } = new ProjectSettingsCollection();
    }
}