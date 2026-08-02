using System;

namespace Automation.Framework.Dependencies
{
    /// <summary>
    /// Container of information for ordered tests.
    /// </summary>
    public class DependencyTestCase
    {
        /// <summary>
        /// Type of class to be ordered.
        /// </summary>
        public Type ClassType { get; set; }

        /// <summary>
        /// Name of the collection shared by all ordered tests in the collection.
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// Priority of the test in the collection.
        /// </summary>
        public int Priority { get; set; }        
    }
}
