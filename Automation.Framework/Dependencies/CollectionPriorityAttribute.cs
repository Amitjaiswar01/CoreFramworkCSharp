using System;

namespace Automation.Framework.Dependencies
{
    /// <summary>
    /// Prioritize class in a collection
    /// </summary>
    public class CollectionPriorityAttribute : Attribute
    {
        /// <summary>
        /// Name of the collection
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// Priority of Collection
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Attribute to control the priority of test execution.
        /// </summary>
        /// <param name="collectionName">Name of the collection to be ordered. All ordered tests need the same string here.</param>
        /// <param name="priority">Priority number 1 being the highest priority.</param>
        public CollectionPriorityAttribute(string collectionName, int priority = 99999)
        {
            CollectionName = collectionName;
            Priority = priority;
        }
    }
}
