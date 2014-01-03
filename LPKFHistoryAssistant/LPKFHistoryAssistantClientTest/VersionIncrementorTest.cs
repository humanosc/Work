using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LPKFHistoryAssistantClient;
using System.Reflection;
using System.IO;


namespace LPKFHistoryAssistantClientTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class VersionIncrementorText
    {
        private string _csPath;
        private string _csPathInvalidVersion;
        private string _csPathInvalidAssemblyName;

        private string _cppPath;
        private string _cppPathInvalidVersion;
        private string _cppPathInvalidAssemblyName;

        private string _rcPath;
        private string _rcPathInvalidVersion;
        private string _rcPathInvalidAssemblyName;


        public VersionIncrementorText ()
        {
            //
            // TODO: Add constructor logic here
            //
        }

       
       

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() 
        {
            _csPath = Path.Combine( AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "AssemblyInfo.cs");
            _csPathInvalidAssemblyName = Path.Combine( AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "AssemblyInfoInvalidAssemblyName.cs" );
            _csPathInvalidVersion = Path.Combine( AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "AssemblyInfoInvalidVersion.cs" );

            _cppPath = Path.Combine( AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "AssemblyInfo.cpp" );
            _cppPathInvalidAssemblyName = Path.Combine( AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "AssemblyInfoInvalidAssemblyName.cpp" );
            _cppPathInvalidVersion = Path.Combine( AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "AssemblyInfoInvalidVersion.cpp" );

            _rcPath = Path.Combine( AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "AssemblyInfo.rc" );
            _rcPathInvalidAssemblyName = Path.Combine( AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "AssemblyInfoInvalidAssemblyName.rc" );
            _rcPathInvalidVersion = Path.Combine( AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "AssemblyInfoInvalidVersion.rc" );
        }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion



        [TestMethod]
        public void TestCsVersionIncrementor ()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ARRANGE
            var csIncrementor = LPKFHistoryAssistantClient.VersionIncrementorFactory.Instance.Create( _csPath );

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ACT
            var result = csIncrementor.Increment();
            
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ASSERT
            Assert.IsNotNull( result );
            Assert.AreEqual( result.OldVersion, "02.070.00520" );
            Assert.AreEqual( result.NewVersion, "02.070.00521" );
            Assert.AreEqual( result.AssemblyName, "LPKF.Fusion.UI" );
        }

        [TestMethod]
        public void TestCsVersionIncrementorInvalidVersion ()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ARRANGE
            var csIncrementor = LPKFHistoryAssistantClient.VersionIncrementorFactory.Instance.Create( _csPathInvalidVersion  );
    
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ACT
            var result = csIncrementor.Increment();

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ASSERT
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestCsVersionIncrementorInvalidAssemblyName ()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ARRANGE
            var csIncrementor = LPKFHistoryAssistantClient.VersionIncrementorFactory.Instance.Create( _csPathInvalidAssemblyName );
    
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ACT
            var result = csIncrementor.Increment();

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ASSERT
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestCppVersionIncrementor ()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ARRANGE
            var cppIncrementor = LPKFHistoryAssistantClient.VersionIncrementorFactory.Instance.Create( _cppPath );
    
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ACT
            var result = cppIncrementor.Increment();

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ASSERT
            Assert.IsNotNull( result );
            Assert.AreEqual( result.OldVersion, "03.010.00055" );
            Assert.AreEqual( result.NewVersion, "03.010.00056" );
            Assert.AreEqual( result.AssemblyName, "LPKF.Fusion.OCC.Algo" );
        }

        [TestMethod]
        public void TestCppVersionIncrementorInvalidVersion ()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ARRANGE
            var cppIncrementor = LPKFHistoryAssistantClient.VersionIncrementorFactory.Instance.Create( _cppPathInvalidVersion );
    
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ACT
            var result = cppIncrementor.Increment();

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ASSERT
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestCppVersionIncrementorInvalidAssemblyName ()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ARRANGE
            var cppIncrementor = LPKFHistoryAssistantClient.VersionIncrementorFactory.Instance.Create( _cppPathInvalidAssemblyName );
    
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ACT
            var result = cppIncrementor.Increment();

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ASSERT
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestRcVersionIncrementor ()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ARRANGE
            var rcIncrementor = LPKFHistoryAssistantClient.VersionIncrementorFactory.Instance.Create( _rcPath );
    
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ACT
            var result = rcIncrementor.Increment();

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ASSERT
            Assert.IsNotNull( result );
            Assert.AreEqual( result.OldVersion, "03,010,00055" );
            Assert.AreEqual( result.NewVersion, "03,010,00056" );
            Assert.AreEqual( result.AssemblyName, "OCC.Algo" );
        }

        [TestMethod]
        public void TestRcVersionIncrementorInvalidVersion ()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ARRANGE
            var rcIncrementor = LPKFHistoryAssistantClient.VersionIncrementorFactory.Instance.Create( _rcPathInvalidVersion );
    
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ACT
            var result = rcIncrementor.Increment();

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ASSERT
            Assert.IsNull( result );
        }

        [TestMethod]
        public void TestRcVersionIncrementorInvalidAssemblyName ()
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ARRANGE
            var rcIncrementor = LPKFHistoryAssistantClient.VersionIncrementorFactory.Instance.Create( _rcPathInvalidAssemblyName );
    
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ACT
            var result = rcIncrementor.Increment();

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            // ASSERT
            Assert.IsNull( result );
        }
    }
}
