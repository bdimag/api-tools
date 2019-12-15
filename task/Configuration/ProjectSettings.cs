using System;

namespace ApiTools.Codegen.Task.Configuration
{
    public class ProjectSettings
    {
        /// <summary>
        /// Specifies that files will be generated for this project.
        /// </summary>
        public bool? Enabled { get; set; }

        /// <summary>
        /// The assembly name or module name of this project.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The namespace that will be used for types created in this project.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Directory where generated files will be created.
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// The project type that these settings apply to.
        /// </summary>
        public ProjectType Type { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is ProjectSettings project)
            {
                return project.Type == Type
                       && (string.IsNullOrEmpty(project.Name) || string.IsNullOrEmpty(Name) || project.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -243844509;
            hashCode = hashCode * -1521134295 + StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            return hashCode;
        }
    }
}