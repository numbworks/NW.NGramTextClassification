using System;
using System.Collections.Generic;
using System.Reflection;
using NW.Shared.Files;
using NUnit.Framework;

namespace NW.NGramTextClassification.UnitTests.Utilities
{
    public static class ObjectMother
    {

        #region Properties

        public static string FileInfoAdapterFullName = @"C:\somefile.txt";
        public static IFileInfoAdapter FileInfoAdapterDoesntExist
            => new FakeFileInfoAdapter(false, FileInfoAdapterFullName);

        #endregion

        #region Methods

        public static TReturn CallPrivateMethod<TClass, TReturn>(TClass obj, string methodName, object[] args)
        {

            Type type = typeof(TClass);

            return (TReturn)type.GetTypeInfo().GetDeclaredMethod(methodName).Invoke(obj, args);

        }
        public static TReturn CallPrivateGenericMethod<TClass, TReturn>(TClass obj, string methodName, object[] args, Type methodType)
        {

            MethodInfo methodInfo = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            var genericMethod = methodInfo.MakeGenericMethod(methodType);

            return (TReturn)genericMethod.Invoke(obj, args);

        }

        public static void Method_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.That(expectedMessage, Is.EqualTo(actual.Message));

        }
        public static bool AreEqual<T>(List<T> list1, List<T> list2, Func<T, T, bool> comparer)
        {

            if (list1 == null && list2 == null)
                return true;

            if (list1 == null || list2 == null)
                return false;

            if (list1.Count != list2.Count)
                return false;

            for (int i = 0; i < list1.Count; i++)
                if (comparer(list1[i], list2[i]) == false)
                    return false;

            return true;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 12.02.2024
*/