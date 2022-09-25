using System.Collections.Generic;
using NW.NGramTextClassification.NGrams;

namespace NW.NGramTextClassification.UnitTests.NGrams
{
    public static class ObjectMother
    {

        #region Properties

        public static ushort FakeGram01_N = 1;
        public static ushort FakeGram02_N = 1;

        public static FakeGram FakeGram01
            = new FakeGram(
                    n: FakeGram01_N, 
                    strategy: UnitTests.ObjectMother.Shared_TokenizationStrategyDefault, 
                    value: LabeledExamples.ObjectMother.LabeledExample01_Monograms[0].Value
                );
        public static FakeGram FakeGram02
            = new FakeGram(
                    n: FakeGram02_N,
                    strategy: UnitTests.ObjectMother.Shared_TokenizationStrategyDefault,
                    value: LabeledExamples.ObjectMother.LabeledExample01_Monograms[1].Value
                );
        
        public static int FakeGram01_HashCode
            = (FakeGram01_N, UnitTests.ObjectMother.Shared_TokenizationStrategyDefault, LabeledExamples.ObjectMother.LabeledExample01_Monograms[0].Value).GetHashCode();

        #endregion

        #region Methods

        public static bool AreEqual(List<Monogram> list1, List<Monogram> list2)
            => UnitTests.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));
        public static bool AreEqual(List<Bigram> list1, List<Bigram> list2)
            => UnitTests.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));
        public static bool AreEqual(List<Trigram> list1, List<Trigram> list2)
            => UnitTests.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));
        public static bool AreEqual(List<Fourgram> list1, List<Fourgram> list2)
            => UnitTests.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));
        public static bool AreEqual(List<Fivegram> list1, List<Fivegram> list2)
            => UnitTests.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));
        public static bool AreEqual(List<INGram> list1, List<INGram> list2)
            => UnitTests.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => obj1.Equals(obj2));

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 25.09.2022
*/