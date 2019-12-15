using System;
using System.Collections.ObjectModel;

namespace ApiTools.Codegen.Task.Configuration
{
    [Serializable]
    public class ProjectSettingsCollection : Collection<ProjectSettings>
    {
        /// <summary>
        /// Retrievs a specific element from the collection.
        /// </summary>
        /// <param name="project">The project that will be used to find an element.</param>
        /// <returns>A matching <see cref="ProjectSettings"/> from this collection, otherwise <see langword="null"/>.</returns>
        public ProjectSettings Find(ProjectSettings project)
        {
            foreach (var p in this)
            {
                if (project.Equals(p))
                {
                    return p;
                }
            }

            return null;
        }
    }
}