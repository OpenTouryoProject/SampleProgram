//**********************************************************************************
//* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
//**********************************************************************************

#region Apache License
//
//  
// 
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

//**********************************************************************************
//* クラス名        ：GetKeyedHashTest
//* クラス日本語名  ：ハッシュ（キー付き）を取得するクラスのテスト
//*
//* 作成者          ：生技 西野
//* 更新履歴        ：
//* 
//*  日時        更新者            内容
//*  ----------  ----------------  -------------------------------------------------
//*  2014/03/31  西野  大介        新規作成
//*
//**********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// touryo library
using Touryo.Infrastructure.Public.Util;

// testing framework
using NUnit.Framework;

namespace Public.Test
{
    [TestFixture]
    public class GetKeyedHashTest
    {
        // - To create a test case --------------------------------------------------
        // (1) You will develop test code. 
        //     You need to think long and hard about how to assert.
        // (2) You increase the coverage rate by added normal test case.
        //     You increase the coverage rate by added abnormal test case.
        //     You increase the coverage rate by added boundary-value(limit) test case.
        // (3) If an exception occurs, Asserting exception.
        //     To record stack trace of the exception by using cosole.writeline().
        //     And consider whether or not there is a need for additional implementation of check processing from the stack trace.
        // --------------------------------------------------------------------------

        /// <summary>Test pre-processing.</summary>
        [TestFixtureSetUp]
        public void Init()
        {
            // This is a test pre-processing.
            // This is done only once at the beginning.
        }

        /// <summary>Test case pre-processing.</summary>
        [SetUp]
        public void SetUp()
        {
            // This is a test case pre-processing.
            // It runs for each test case.
        }

        #region Test data

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method GetKeyedHashBytesTest.
        /// </summary>
        public IEnumerable<TestCaseData> TestCasesOfGetKeyedHashBytesTest
        {
            get
            {
                // If you need to prepare for the test data below so, you use the TestCaseData.

                // Convert the string to a byte array to be encrypted.
                byte[] abcdeBytes = Encoding.UTF8.GetBytes("abcde");
                byte[] aiueoBytes = Encoding.UTF8.GetBytes("あいうえお");
                byte[] emptyBytes = new Byte[0];
                byte[] nullBytes = null;
                
                // Default
                yield return new TestCaseData(abcdeBytes, EnumKeyedHashAlgorithm.Default, "test@123").SetName("TestID-001N");
                yield return new TestCaseData(aiueoBytes, EnumKeyedHashAlgorithm.Default, "test@123").SetName("TestID-002N");
                yield return new TestCaseData(emptyBytes, EnumKeyedHashAlgorithm.Default, "test@123").SetName("TestID-003L");
                yield return new TestCaseData(nullBytes, EnumKeyedHashAlgorithm.Default, "test@123").Throws(typeof(ArgumentNullException)).SetName("TestID-004A");
                yield return new TestCaseData(abcdeBytes, EnumKeyedHashAlgorithm.Default, "").SetName("TestID-005L");
                yield return new TestCaseData(abcdeBytes, EnumKeyedHashAlgorithm.Default, null).Throws(typeof(ArgumentNullException)).SetName("TestID-006A");

                // HMACSHA1
                yield return new TestCaseData(abcdeBytes, EnumKeyedHashAlgorithm.HMACSHA1, "test@123").SetName("TestID-007N");
                yield return new TestCaseData(aiueoBytes, EnumKeyedHashAlgorithm.HMACSHA1, "test@123").SetName("TestID-008N");
                yield return new TestCaseData(emptyBytes, EnumKeyedHashAlgorithm.HMACSHA1, "test@123").SetName("TestID-009L");
                yield return new TestCaseData(nullBytes, EnumKeyedHashAlgorithm.HMACSHA1, "test@123").Throws(typeof(ArgumentNullException)).SetName("TestID-010A");

                // MACTripleDES
                yield return new TestCaseData(abcdeBytes, EnumKeyedHashAlgorithm.MACTripleDES, "test@123").SetName("TestID-011N");
                yield return new TestCaseData(aiueoBytes, EnumKeyedHashAlgorithm.MACTripleDES, "test@123").SetName("TestID-012N");
                yield return new TestCaseData(emptyBytes, EnumKeyedHashAlgorithm.MACTripleDES, "test@123").SetName("TestID-013L");
                yield return new TestCaseData(nullBytes, EnumKeyedHashAlgorithm.MACTripleDES, "test@123").Throws(typeof(ArgumentNullException)).SetName("TestID-014A");

                // The encryption method that is not defined 
                yield return new TestCaseData(abcdeBytes, 999, "test@123").Throws(typeof(NullReferenceException)).SetName("TestID-015A");
                yield return new TestCaseData(aiueoBytes, 999, "test@123").Throws(typeof(NullReferenceException)).SetName("TestID-016A");
                yield return new TestCaseData(emptyBytes, 999, "test@123").Throws(typeof(NullReferenceException)).SetName("TestID-017A");
                yield return new TestCaseData(nullBytes, 999, "test@123").Throws(typeof(NullReferenceException)).SetName("TestID-018A");
            }
        }

        #endregion

        #region Test Code

        /// <summary>>Test execution.(CheckListID should be the first argument)</summary>
        /// <param name="testCaseID">test case ID</param> 
        /// <param name="sourceString">The original string to get the hash value.</param>
        /// <param name="eha">>Hash algorithm</param>
        /// <param name="password">password</param>
        [TestCase("abcde", EnumKeyedHashAlgorithm.Default, "test@123", TestName = "TestID-001N")]
        [TestCase("あいうえお", EnumKeyedHashAlgorithm.Default, "test@123", TestName = "TestID-002N")]
        [TestCase("", EnumKeyedHashAlgorithm.Default, "test@123", TestName = "TestID-003L")]
        [TestCase(null, EnumKeyedHashAlgorithm.Default, "test@123", ExpectedException = typeof(ArgumentNullException), TestName = "TestID-004A")]
        [TestCase("abcde", EnumKeyedHashAlgorithm.Default, "", TestName = "TestID-005L")]
        [TestCase("abcde", EnumKeyedHashAlgorithm.Default, null, ExpectedException = typeof(ArgumentNullException), TestName = "TestID-006A")]
        [TestCase("abcde", EnumKeyedHashAlgorithm.HMACSHA1, "test@123", TestName = "TestID-007N")]
        [TestCase("あいうえお", EnumKeyedHashAlgorithm.HMACSHA1, "test@123", TestName = "TestID-008N")]
        [TestCase("", EnumKeyedHashAlgorithm.HMACSHA1, "test@123", TestName = "TestID-009L")]
        [TestCase(null, EnumKeyedHashAlgorithm.HMACSHA1, "test@123", ExpectedException = typeof(ArgumentNullException), TestName = "TestID-010A")]
        [TestCase("abcde", EnumKeyedHashAlgorithm.MACTripleDES, "test@123", TestName = "TestID-011N")]
        [TestCase("あいうえお", EnumKeyedHashAlgorithm.MACTripleDES, "test@123", TestName = "TestID-012N")]
        [TestCase("", EnumKeyedHashAlgorithm.MACTripleDES, "test@123", TestName = "TestID-013L")]
        [TestCase(null, EnumKeyedHashAlgorithm.MACTripleDES, "test@123", ExpectedException = typeof(ArgumentNullException), TestName = "TestID-014A")]
        [TestCase("abcde", 999, "test@123", ExpectedException = typeof(NullReferenceException), TestName = "TestID-015A")]        // The encryption method that is not defined
        [TestCase("あいうえお", 999, "test@123", ExpectedException = typeof(NullReferenceException), TestName = "TestID-016A")]   // The encryption method that is not defined
        [TestCase("", 999, "test@123", ExpectedException = typeof(NullReferenceException), TestName = "TestID-017A")]             // The encryption method that is not defined
        [TestCase(null, 999, "test@123", ExpectedException = typeof(ArgumentNullException), TestName = "TestID-018A")]            // The encryption method that is not defined
        public void GetKeyedHashStringTest(string sourceString, EnumKeyedHashAlgorithm eha, string password)
        {
            try
            {
                // Get the hash value using the components of touryo. 
                string hashString = GetKeyedHash.GetKeyedHashString(sourceString, eha, password);

                // Using the components of touryo, and get the hash value again.
                string hashString2 = GetKeyedHash.GetKeyedHashString(sourceString, eha, password);

                // Check the hash value.
                Assert.AreNotEqual(sourceString, hashString);
                Assert.AreNotEqual(sourceString, hashString2);
                Assert.AreEqual(hashString, hashString2);
            }
            catch (Exception ex)
            {
                // Print a stack trace when an exception occurs.
                Console.WriteLine(":" + ex.StackTrace);
                throw;
            }
        }

        /// <summary>>Test execution.(CheckListID should be the first argument)</summary>
        /// <param name="testCaseID">test case ID</param> 
        /// <param name="asb">The original string to get the hash value.</param>
        /// <param name="eha">Hash algorithm</param>
        /// <param name="password">password</param>
        [TestCaseSource("TestCasesOfGetKeyedHashBytesTest")]
        public void GetKeyedHashBytesTest(byte[] asb, EnumKeyedHashAlgorithm eha, string password)
        {
            try
            {
                // anyWarp 棟梁の部品を使用してハッシュ値を取得
                byte[] hashBytes = GetKeyedHash.GetKeyedHashBytes(asb, eha, password);

                // anyWarp 棟梁の部品を使用して、もう一度ハッシュ値を取得
                byte[] hashBytes2 = GetKeyedHash.GetKeyedHashBytes(asb, eha, password);

                // ハッシュ値が同じかどうかをチェック
                Assert.AreNotEqual(asb, hashBytes);
                Assert.AreNotEqual(asb, hashBytes2);
                Assert.AreEqual(hashBytes, hashBytes2);
            }
            catch (Exception ex)
            {
                // Print a stack trace when an exception occurs.
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        #endregion

        /// <summary>Test case post-processing.</summary>
        [TearDown]
        public void TearDown()
        {
            // This is a test case post-processing.
            // It runs for each test case.
        }

        /// <summary>Test post-processing.</summary>
        [TestFixtureTearDown]
        public void CleanUp()
        {
            // This is a test post-processing.
            // This is done only once at the ending.
        }
    }
}
