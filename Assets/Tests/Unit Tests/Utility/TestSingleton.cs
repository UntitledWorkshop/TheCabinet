using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Utility;

namespace Tests.Unit_Tests.Utility {
    public class TestSingletonA: ISingleton<TestSingletonA> {
        public TestSingletonA()
        {
            ISingleton<TestSingletonA>.Instance = this;
        }
        
        ~TestSingletonA()
        {
            ISingleton<TestSingletonA>.Instance = null;
        }
    }
    
    public class TestSingletonB: ISingleton<TestSingletonB> {
        public TestSingletonB()
        {
            ISingleton<TestSingletonB>.Instance = this;
        }
        
        ~TestSingletonB()
        {
            ISingleton<TestSingletonB>.Instance = null;
        }
    }
    
    public class TestSingleton
    {
        [Test]
        public void TestSingletonSimplePasses()
        {
            var singletonA = new TestSingletonA();
            var singletonB = new TestSingletonB();
            Assert.AreSame(singletonA, ISingleton<TestSingletonA>.Instance);
            Assert.AreSame(singletonB, ISingleton<TestSingletonB>.Instance);
            Assert.AreNotSame(singletonA, ISingleton<TestSingletonB>.Instance);
        }
    }
}