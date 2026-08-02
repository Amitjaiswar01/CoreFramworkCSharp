using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Xunit;
using Xunit.Abstractions;

namespace Automation.Framework.Dependencies
{
    /// <summary>
    /// Run the Dependent test classes with same collection name based on given Priority 
    /// </summary>
    public class CollectionRunner
    {
        private static readonly IList<DependencyTestCase> TestCases = new List<DependencyTestCase>();

        private const string TestMethodName = "Test";
        private const string DisposeMethodName = "Dispose";

        /// <summary>
        /// Test ITestOutputHelper instance.
        /// </summary>
        public ITestOutputHelper OutputHelper { get; set; }

        /// <summary>
        /// Initialize the xUnit OutputHelper.
        /// </summary>
        /// <param name="output">xUnit helper for logging and test information.</param>
        public CollectionRunner(ITestOutputHelper output)
        {
            OutputHelper = output;
        }

        /// <summary>
        /// Gets all the test classes with CollectionPriorityAttribute
        /// </summary>
        /// <param name="nameSpace">Namespace of the calling assembly</param>
        /// <returns>IEnumerable of DependencyTestCase</returns>
        protected static IEnumerable<DependencyTestCase> GetClasses(string nameSpace)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(a => a.GetName().Name.StartsWith(nameSpace));

            if (assembly == null) return TestCases;

            var types = assembly.GetTypes().Where(type => type.Namespace != null && type.Namespace.StartsWith(nameSpace));

            foreach (var type in types)
            {
                var collectionPriorityCustomAttribute =
                    type.CustomAttributes.FirstOrDefault(
                        c => c.AttributeType.FullName != null &&
                             c.AttributeType.FullName.Equals(typeof(CollectionPriorityAttribute).FullName));

                if (collectionPriorityCustomAttribute == null) continue;

                var collectionName = Convert.ToString(collectionPriorityCustomAttribute.ConstructorArguments[0].Value);
                var priorityValue = Convert.ToInt32(collectionPriorityCustomAttribute.ConstructorArguments[1].Value);

                var lpTestCase = new DependencyTestCase
                {
                    ClassType = type,
                    CollectionName = collectionName,
                    Priority = priorityValue,
                };
                TestCases.Add(lpTestCase);
            }
            return TestCases;
        }

        /// <summary>
        /// Adds all the missing test classes with CollectionPriorityAttribute
        /// </summary>
        /// <param name="nameSpace">Namespace of the calling assembly</param>
        /// <param name="dependencyTestCases">Original set of test cases</param>
        /// <param name="missingClassPriorities">List of missing priorities</param>
        private static void AddMissingClasses(string nameSpace, List<DependencyTestCase> dependencyTestCases, List<int> missingClassPriorities)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(a => a.GetName().Name.StartsWith(nameSpace));

            if (assembly == null) return;

            var types = assembly.GetTypes().Where(type => type.Namespace != null && type.Namespace.StartsWith(nameSpace));

            foreach (var type in types)
            {
                var collectionPriorityCustomAttribute =
                    type.CustomAttributes.FirstOrDefault(
                        c => c.AttributeType.FullName != null &&
                             c.AttributeType.FullName.Equals(typeof(CollectionPriorityAttribute).FullName));

                if (collectionPriorityCustomAttribute == null) continue;

                var priorityValue = Convert.ToInt32(collectionPriorityCustomAttribute.ConstructorArguments[1].Value);

                if (missingClassPriorities.Any(m => m == priorityValue))
                {
                    var collectionName = Convert.ToString(collectionPriorityCustomAttribute.ConstructorArguments[0].Value);
                    var lpTestCase = new DependencyTestCase
                    {
                        ClassType = type,
                        CollectionName = collectionName,
                        Priority = priorityValue,
                    };
                    dependencyTestCases.Add(lpTestCase);
                }
            }
        }

        /// <summary>
        /// Run a collection 
        /// </summary>
        /// <param name="assemblyName">AssemblyName of the calling assembly</param>
        /// <param name="collectionName">Name of the collection</param>
        public void RunCollection(string assemblyName, string collectionName)
        {
            RunTestsInSequence(assemblyName, GetClasses(assemblyName).Where(t => t.CollectionName == collectionName));
        }

        /// <summary>
        /// Run collection in Sequence
        /// </summary>
        /// <param name="assemblyName">AssemblyName of the calling assembly</param>
        public void RunCollectionsInSequence(string assemblyName)
        {
            var testClasses = GetClasses(assemblyName);

            //When run sequential collection is ordered Alphabetically
            var testGroup = testClasses.GroupBy(t => t.CollectionName)
                                  .Select(group => new
                                  {
                                      Name = group.Key,
                                      TestCases = group.OrderBy(x => x.Priority)
                                  })
                                   .OrderBy(group => group.TestCases.FirstOrDefault()?.CollectionName);

            foreach (var g in testGroup)
            {
                RunTestsInSequence(assemblyName, g.TestCases);
            }
        }

        /// <summary>
        /// Run the given test.
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="testCases">List of test classes to be executed in the collection.</param>
        protected void RunTestsInSequence(string assemblyName, IEnumerable<DependencyTestCase> testCases)
        {
            var testClasses = AddAnyMissingClassUsingPriorities(assemblyName, testCases);

            foreach (var testClass in testClasses)
            {
                var methodInfo = testClass.ClassType.GetMethod(TestMethodName);
                var disposeMethod = testClass.ClassType.GetMethod(DisposeMethodName);

                if (methodInfo == null) continue;
                if (disposeMethod == null) continue;

                var dataArray = methodInfo.GetCustomAttribute<InlineDataAttribute>().GetData(methodInfo);

                foreach (var data in dataArray)
                {
                    var classInstance = Activator.CreateInstance(testClass.ClassType, OutputHelper);
                    methodInfo.Invoke(classInstance, new object[] { Convert.ToString(data.FirstOrDefault()) });
                    disposeMethod.Invoke(classInstance, null);
                }
            }
        }

        /// <summary>
        /// Adds Any Missing lower Priorities Classes.
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="testCases"></param>
        /// <returns>List of test classes to be executed in the collection.</returns>
        private IOrderedEnumerable<DependencyTestCase> AddAnyMissingClassUsingPriorities(string assemblyName, IEnumerable<DependencyTestCase> testCases)
        {
            var dependencyTestCases = testCases.ToList();
            var missingClassPriorities = FindMissingClassPriorities(dependencyTestCases).ToList();

            if (missingClassPriorities.Any()) AddMissingClasses(assemblyName, dependencyTestCases, missingClassPriorities);

            return dependencyTestCases.OrderBy(t => t.Priority);
        }

        /// <summary>
        /// Find missing low priorities classes.
        /// </summary>
        /// <param name="testClasses"></param>
        /// <returns>List of missing priorities</returns>
        private IEnumerable<int> FindMissingClassPriorities(List<DependencyTestCase> testClasses)
        {
            var expectedRange = new HashSet<int>(Enumerable.Range(1, Math.Min(100, testClasses.Max(t => t.Priority))));

            expectedRange.ExceptWith(testClasses.Select(t => t.Priority));
            return expectedRange;
        }
    }
}
