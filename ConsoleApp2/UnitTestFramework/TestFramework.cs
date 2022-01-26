using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Loggers;

namespace UnitTestFramework
{
    public class TestFramework
    {
        private static int _testCount; 
        public static int TestCount
        {
            get
            {
                _testCount++;
                return _testCount;
            }
            private set { }
        }

        static TestFramework()
        {
            _testCount = 0;
        }

        public static void IsEqual (object obj1, object obj2)
        {
            Logger logger = new Logger();
            bool equalty = obj1.Equals(obj2);
            logger.Log(equalty, TestCount);            
        }

        public void StartTest()
        {
            Assembly assembly = Assembly.LoadFrom("PresentationLayer.dll");
            IEnumerable<Type> types = assembly.GetTypes().Where(type => Attribute.IsDefined(type, typeof(TestClass))).ToArray();
            foreach (Type type in types)
            {
                var testClass = Activator.CreateInstance(type);
                IEnumerable<MethodInfo> firstMethods = type.GetMethods().Where(method => Attribute.IsDefined(method, typeof(FirstMethodToTest))).ToArray();
                IEnumerable<MethodInfo> methods = type.GetMethods().Where(method => Attribute.IsDefined(method, typeof(TestMethod))).ToArray();
                IEnumerable<MethodInfo> lastMethods = type.GetMethods().Where(method => Attribute.IsDefined(method, typeof(LastMethodToTest))).ToArray();
                foreach (MethodInfo method in firstMethods)
                {
                    method.Invoke(testClass, null);
                }

                foreach (MethodInfo method in methods)
                {
                    method.Invoke(testClass, null);
                }

                foreach (MethodInfo method in lastMethods)
                {
                    method.Invoke(testClass, null);
                }
            }
        }
    }
}
