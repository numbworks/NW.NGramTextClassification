using System;
using System.Collections.Generic;
using NW.NGramTextClassification.LabeledExamples;
using NW.NGramTextClassification.NGrams;

namespace NW.NGramTextClassification.UnitTests.LabeledExamples
{
    public static class ObjectMother
    {

        #region Properties

        public static LabeledExample ShortLabeledExample01 
            = new LabeledExample(label: "en", text: "We are looking for several skilled and driven developers to join our team.");
        public static List<Monogram> ShortLabeledExample01_Monograms = new List<Monogram>()
        {

            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "we"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "are"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "looking"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "for"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "several"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "skilled"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "and"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "driven"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "developers"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "to"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "join"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "our"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "team")

        };
        public static List<Bigram> ShortLabeledExample01_Bigrams = new List<Bigram>()
        {

            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "we are"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "are looking"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "looking for"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "for several"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "several skilled"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "skilled and"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "and driven"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "driven developers"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "developers to"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "to join"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "join our"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "our team"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "team")

        };
        public static List<Trigram> ShortLabeledExample01_Trigrams = new List<Trigram>()
        {

            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "we are looking"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "are looking for"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "looking for several"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "for several skilled"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "several skilled and"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "skilled and driven"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "and driven developers"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "driven developers to"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "developers to join"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "to join our"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "join our team"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "our team"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "team")

        };
        public static List<Fourgram> ShortLabeledExample01_Fourgrams = new List<Fourgram>()
        {

            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "we are looking for"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "are looking for several"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "looking for several skilled"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "for several skilled and"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "several skilled and driven"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "skilled and driven developers"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "and driven developers to"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "driven developers to join"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "developers to join our"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "to join our team"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "join our team"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "our team"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "team")

        };
        public static List<Fivegram> ShortLabeledExample01_Fivegrams = new List<Fivegram>()
        {

            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "we are looking for several"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "are looking for several skilled"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "looking for several skilled and"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "for several skilled and driven"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "several skilled and driven developers"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "skilled and driven developers to"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "and driven developers to join"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "driven developers to join our"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "developers to join our team"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "to join our team"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "join our team"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "our team"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "team")

        };
        public static List<INGram> ShortLabeledExample01_NGrams = new List<INGram>() 
        {

            ShortLabeledExample01_Monograms[0],
            ShortLabeledExample01_Monograms[1],
            ShortLabeledExample01_Monograms[2],
            ShortLabeledExample01_Monograms[3],
            ShortLabeledExample01_Monograms[4],
            ShortLabeledExample01_Monograms[5],
            ShortLabeledExample01_Monograms[6],
            ShortLabeledExample01_Monograms[7],
            ShortLabeledExample01_Monograms[8],
            ShortLabeledExample01_Monograms[9],
            ShortLabeledExample01_Monograms[10],
            ShortLabeledExample01_Monograms[11],
            ShortLabeledExample01_Monograms[12],

            ShortLabeledExample01_Bigrams[0],
            ShortLabeledExample01_Bigrams[1],
            ShortLabeledExample01_Bigrams[2],
            ShortLabeledExample01_Bigrams[3],
            ShortLabeledExample01_Bigrams[4],
            ShortLabeledExample01_Bigrams[5],
            ShortLabeledExample01_Bigrams[6],
            ShortLabeledExample01_Bigrams[7],
            ShortLabeledExample01_Bigrams[8],
            ShortLabeledExample01_Bigrams[9],
            ShortLabeledExample01_Bigrams[10],
            ShortLabeledExample01_Bigrams[11],
            ShortLabeledExample01_Bigrams[12],

            ShortLabeledExample01_Trigrams[0],
            ShortLabeledExample01_Trigrams[1],
            ShortLabeledExample01_Trigrams[2],
            ShortLabeledExample01_Trigrams[3],
            ShortLabeledExample01_Trigrams[4],
            ShortLabeledExample01_Trigrams[5],
            ShortLabeledExample01_Trigrams[6],
            ShortLabeledExample01_Trigrams[7],
            ShortLabeledExample01_Trigrams[8],
            ShortLabeledExample01_Trigrams[9],
            ShortLabeledExample01_Trigrams[10],
            ShortLabeledExample01_Trigrams[11],
            ShortLabeledExample01_Trigrams[12],

            ShortLabeledExample01_Fourgrams[0],
            ShortLabeledExample01_Fourgrams[1],
            ShortLabeledExample01_Fourgrams[2],
            ShortLabeledExample01_Fourgrams[3],
            ShortLabeledExample01_Fourgrams[4],
            ShortLabeledExample01_Fourgrams[5],
            ShortLabeledExample01_Fourgrams[6],
            ShortLabeledExample01_Fourgrams[7],
            ShortLabeledExample01_Fourgrams[8],
            ShortLabeledExample01_Fourgrams[9],
            ShortLabeledExample01_Fourgrams[10],
            ShortLabeledExample01_Fourgrams[11],
            ShortLabeledExample01_Fourgrams[12],

            ShortLabeledExample01_Fivegrams[0],
            ShortLabeledExample01_Fivegrams[1],
            ShortLabeledExample01_Fivegrams[2],
            ShortLabeledExample01_Fivegrams[3],
            ShortLabeledExample01_Fivegrams[4],
            ShortLabeledExample01_Fivegrams[5],
            ShortLabeledExample01_Fivegrams[6],
            ShortLabeledExample01_Fivegrams[7],
            ShortLabeledExample01_Fivegrams[8],
            ShortLabeledExample01_Fivegrams[9],
            ShortLabeledExample01_Fivegrams[10],
            ShortLabeledExample01_Fivegrams[11],
            ShortLabeledExample01_Fivegrams[12]

        };
        public static TokenizedExample ShortTokenizedExample01
            = new TokenizedExample(labeledExample: ShortLabeledExample01, nGrams: ShortLabeledExample01_NGrams);
        public static string ShortLabeledExample01_AsString
            = string.Concat(
                        "[ ",
                        $"Label: '{ShortLabeledExample01.Label}', ",
                        $"Text: '{ShortLabeledExample01.Text}' ",
                        "]"
                    );
        public static string ShortTokenizedExample01_AsString
            = string.Concat(
                        "[ ",
                        $"Label: '{ShortTokenizedExample01.LabeledExample.Label}', ",
                        $"Text: '{ShortTokenizedExample01.LabeledExample.Text}', ",
                        $"NGrams: '{ShortTokenizedExample01.NGrams.Count}' ",
                        "]"
                    );

        public static LabeledExample ShortLabeledExample02
            = new LabeledExample(label: "sv", text: "Vår kund erbjuder trivsel, arbetsglädje och en trygg arbetsmiljö.");
        public static List<Monogram> ShortLabeledExample02_Monograms = new List<Monogram>()
        {

            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "vår"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "kund"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "erbjuder"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "trivsel"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "arbetsglädje"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "och"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "en"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "trygg"),
            new Monogram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "arbetsmiljö")

        };
        public static List<Bigram> ShortLabeledExample02_Bigrams = new List<Bigram>()
        {

            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "vår kund"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "kund erbjuder"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "erbjuder trivsel"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "trivsel arbetsglädje"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "arbetsglädje och"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "och en"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "en trygg"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "trygg arbetsmiljö"),
            new Bigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "arbetsmiljö")

        };
        public static List<Trigram> ShortLabeledExample02_Trigrams = new List<Trigram>()
        {

            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "vår kund erbjuder"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "kund erbjuder trivsel"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "erbjuder trivsel arbetsglädje"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "trivsel arbetsglädje och"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "arbetsglädje och en"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "och en trygg"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "en trygg arbetsmiljö"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "trygg arbetsmiljö"),
            new Trigram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "arbetsmiljö")

        };
        public static List<Fourgram> ShortLabeledExample02_Fourgrams = new List<Fourgram>()
        {

            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "vår kund erbjuder trivsel"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "kund erbjuder trivsel arbetsglädje"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "erbjuder trivsel arbetsglädje och"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "trivsel arbetsglädje och en"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "arbetsglädje och en trygg"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "och en trygg arbetsmiljö"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "en trygg arbetsmiljö"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "trygg arbetsmiljö"),
            new Fourgram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "arbetsmiljö")

        };
        public static List<Fivegram> ShortLabeledExample02_Fivegrams = new List<Fivegram>()
        {

            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "vår kund erbjuder trivsel arbetsglädje"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "kund erbjuder trivsel arbetsglädje och"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "erbjuder trivsel arbetsglädje och en"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "trivsel arbetsglädje och en trygg"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "arbetsglädje och en trygg arbetsmiljö"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "och en trygg arbetsmiljö"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "en trygg arbetsmiljö"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "trygg arbetsmiljö"),
            new Fivegram(NGramTokenization.ObjectMother.TokenizationStrategy_Default, "arbetsmiljö")

        };
        public static List<INGram> ShortLabeledExample02_NGrams = new List<INGram>()
        {

            ShortLabeledExample02_Monograms[0],
            ShortLabeledExample02_Monograms[1],
            ShortLabeledExample02_Monograms[2],
            ShortLabeledExample02_Monograms[3],
            ShortLabeledExample02_Monograms[4],
            ShortLabeledExample02_Monograms[5],
            ShortLabeledExample02_Monograms[6],
            ShortLabeledExample02_Monograms[7],
            ShortLabeledExample02_Monograms[8],

            ShortLabeledExample02_Bigrams[0],
            ShortLabeledExample02_Bigrams[1],
            ShortLabeledExample02_Bigrams[2],
            ShortLabeledExample02_Bigrams[3],
            ShortLabeledExample02_Bigrams[4],
            ShortLabeledExample02_Bigrams[5],
            ShortLabeledExample02_Bigrams[6],
            ShortLabeledExample02_Bigrams[7],
            ShortLabeledExample02_Bigrams[8],

            ShortLabeledExample02_Trigrams[0],
            ShortLabeledExample02_Trigrams[1],
            ShortLabeledExample02_Trigrams[2],
            ShortLabeledExample02_Trigrams[3],
            ShortLabeledExample02_Trigrams[4],
            ShortLabeledExample02_Trigrams[5],
            ShortLabeledExample02_Trigrams[6],
            ShortLabeledExample02_Trigrams[7],
            ShortLabeledExample02_Trigrams[8],

            ShortLabeledExample02_Fourgrams[0],
            ShortLabeledExample02_Fourgrams[1],
            ShortLabeledExample02_Fourgrams[2],
            ShortLabeledExample02_Fourgrams[3],
            ShortLabeledExample02_Fourgrams[4],
            ShortLabeledExample02_Fourgrams[5],
            ShortLabeledExample02_Fourgrams[6],
            ShortLabeledExample02_Fourgrams[7],
            ShortLabeledExample02_Fourgrams[8],

            ShortLabeledExample02_Fivegrams[0],
            ShortLabeledExample02_Fivegrams[1],
            ShortLabeledExample02_Fivegrams[2],
            ShortLabeledExample02_Fivegrams[3],
            ShortLabeledExample02_Fivegrams[4],
            ShortLabeledExample02_Fivegrams[5],
            ShortLabeledExample02_Fivegrams[6],
            ShortLabeledExample02_Fivegrams[7],
            ShortLabeledExample02_Fivegrams[8]

        };
        public static TokenizedExample ShortTokenizedExample02
            = new TokenizedExample(labeledExample: ShortLabeledExample02, nGrams: ShortLabeledExample02_NGrams);

        public static List<LabeledExample> ShortLabeledExamples = new List<LabeledExample>()
        {

            ShortLabeledExample01,
            ShortLabeledExample02

        };
        public static List<TokenizedExample> ShortTokenizedExamples = new List<TokenizedExample>()
        {

            ShortTokenizedExample01,
            ShortTokenizedExample02

        };

        public static TokenizedExample ShortTokenizedExample01_Mono
            = new TokenizedExample(
                    labeledExample: ShortLabeledExample01,
                    nGrams: CreateNGrams(
                                monograms: ShortLabeledExample01_Monograms
                        ));
        public static TokenizedExample ShortTokenizedExample01_MonoBi
            = new TokenizedExample(
                    labeledExample: ShortLabeledExample01,
                    nGrams: CreateNGrams(
                                monograms: ShortLabeledExample01_Monograms,
                                bigrams: ShortLabeledExample01_Bigrams
                        ));
        public static TokenizedExample ShortTokenizedExample01_MonoBiTri
            = new TokenizedExample(
                    labeledExample: ShortLabeledExample01,
                    nGrams: CreateNGrams(
                                monograms: ShortLabeledExample01_Monograms,
                                bigrams: ShortLabeledExample01_Bigrams,
                                trigrams: ShortLabeledExample01_Trigrams
                        ));
        public static TokenizedExample ShortTokenizedExample01_MonoBiTriFour
            = new TokenizedExample(
                    labeledExample: ShortLabeledExample01,
                    nGrams: CreateNGrams(
                                monograms: ShortLabeledExample01_Monograms,
                                bigrams: ShortLabeledExample01_Bigrams,
                                trigrams: ShortLabeledExample01_Trigrams,
                                fourgrams: ShortLabeledExample01_Fourgrams
                        ));
        public static TokenizedExample ShortTokenizedExample01_MonoBiTriFourFive
            = new TokenizedExample(
                        labeledExample: ShortLabeledExample01,
                        nGrams: CreateNGrams(
                                    monograms: ShortLabeledExample01_Monograms,
                                    bigrams: ShortLabeledExample01_Bigrams,
                                    trigrams: ShortLabeledExample01_Trigrams,
                                    fourgrams: ShortLabeledExample01_Fourgrams,
                                    fivegrams: ShortLabeledExample01_Fivegrams
                            ));

        public static LabeledExample ShortLabeledExample03_Untokenizable
            = new LabeledExample(label: "en", text: "We");
        public static List<LabeledExample> ShortLabeledExamples_Untokenizable = new List<LabeledExample>()
        {

            ShortLabeledExample01,
            ShortLabeledExample02,
            ShortLabeledExample03_Untokenizable

        };

        #endregion

        #region Methods

        public static bool AreEqual(LabeledExample obj1, LabeledExample obj2)
        {

            return string.Equals(obj1.Text, obj2.Text, StringComparison.InvariantCulture)
                    && string.Equals(obj1.Label, obj2.Label, StringComparison.InvariantCulture);

        }
        public static bool AreEqual(TokenizedExample obj1, TokenizedExample obj2)
        {

            return AreEqual(obj1.LabeledExample, obj2.LabeledExample)
                    && NGrams.ObjectMother.AreEqual(obj1.NGrams, obj2.NGrams);

        }
        public static bool AreEqual(List<TokenizedExample> list1, List<TokenizedExample> list2)
            => Utilities.ObjectMother.AreEqual(list1, list2, (obj1, obj2) => AreEqual(obj1, obj2));

        public static List<LabeledExample> CreateThirtyCompleteLabeledExamples()
        {

            List<LabeledExample> labeledExamples = new List<LabeledExample>()
            {

                new LabeledExample("en", "VerksamhetsbeskrivningGoGift is a company which focuses on innovative gifts and gift cards solutions. GoGift has activities in every Nordic country and addresses both private as well as corporate customers. GoGift distributes gift cards for thousands of shops, brands and experiences delivered with post, email or SMS. The Super Gift Card, one of the most popular gifts, makes it possible for the gift recipient to choose their own gift at GoGift.com. With more than 7000 business to business customers worldwide GoGift is also a preferred supplier of corporate gifts.ArbetsuppgifterYou will play a very important role developing and maintaining our core platform as well as numerous APIs, applications and websites. You will be a key player in our dev team with highly skilled and experienced software developers (public and external), UX/UI designers, dev ops specialists and online content manager."),
                new LabeledExample("en", "You will report to the Team Manager. You will get the opportunity to define your own tasks as well influence planning and decisions regarding technologies and architecture going forward. After a focused introduction, you will be involved in developing our core engine and existing platforms as well as numerous new projects; front-end, back-end, public and external API, native and web apps depending on your skill set and expertise. Your main tasks will be to design and develop new features, maintain the existing products and optimise workflows based on feedback from different stake holders. You will be working with a fully automated test and deploy set up where code quality and smart scalable solutions are of the essence. Collaboration and team work is at the core of this position and you will be expected to be able to handle several projects or tasks simultaneously."),
                new LabeledExample("en", "You will become part of a very exciting and international company, with big ambitions, yet informal work environment. Utbildning/Erfarenhet' Excellent written and verbal communication skills (English) ' Comfortable contributing in a group and able to work independently ' Experience with .NET development ' Experience developing with a least a couple or more languages and technologies such as: o C#, JavaScript, o Html/CSS o SQL, Maria DB o ASP .Net MVC, A plus if you are familiar with one or more of the following technologies and tools: Xamarin CI/CD Docker Kubernetes React SASS GitHub Angular Experience with RESTful API development."),
                new LabeledExample("en", "E.ON is one of the world's largest investor-owned power and gas companies. At facilities across Europe, Russia, and North America, our nearly 62,000 employees generated just under EUR 122.5 billion in sales. We have an ambitious objective: to make energy cleaner and better wherever we operate.E.ON is looking for a Controller Business IT Everyone at E.ON is unique and there’s a common goal that pulls us all together. It’s our dedication to convert to 100% renewable and recycled energy by 2025. That’s no small ambition. To make it happen, we need ground-breaking people brave enough to make a difference.  As a Controller Business IT you will give professional guidance to accountable IT Manager in international environment to ensure cost efficiency as well as cost effectiveness. You will be partnering with the assigned business controlling to ensure transparency and influenceability of IT costs including recommendations for consumption improvements."),
                new LabeledExample("en", "The role also includes to ensure integration of commercial IT-processes and business processes for the assigned business units, especially provide information relevant for planning.  You’ll be part of our international team and working close to business to active support of Business IT managers, IT board members and controllers of business units in all IT topics, especially regarding charging if IT cost.  This includes tasks such as:  ' Create effective reports by KPI &amp; trend based commentary including actions for managers. ' Support Global and Local Portfolio Management in CAPEX allocation as well as fund approval and reporting of actual consumptions. ' Perform international Business/IT reviews including Consumption and technology KPIs, active Cost Management for FTE-based services as well as IT-Project Budget consumption for Business IT Regional. ' Create efficiency potentials and attractive value propositions based on benchmarks, business specific consumption KPIs and market proven technology KPIs."),
                new LabeledExample("en", "'High NPS from E.ON´s business excecutives for high transparency of IT cost and IT processes.' Supporting in special tasks set by CIO or business unit CFOs.   The role is located in Malmö.  For this position, we’re looking for a person that’s is business focused, has a high developed team spirit and very good social competencies. You have the ability to work under pressure, willingness for continuous learning and the ability to collaborate actively across cultural and organizational boundaries. Our team work close together with the software and product development to ensure that Axis product portfolio is up-to-date and meet our customers’ requirements."),
                new LabeledExample("en", "We offer a pleasant workplace with opportunities to develop.  A suitable candidate needs:  ' Academic degree in Economics ' Profound experience in finance and controlling area ' Experience in drafting decision proposals on executive level ' Experience in driving financial review meetings on management level ' Excellent language skill in English and Swedish(verbal and written)   In order to be successful in the role, it’s important that you identify with the values of E.ON:  ”We put our customers first, we work together, we improve and innovate, we win together and act responsible with an open mind”.   The selection process will take place continuously, so please send in your application in English as soon as possible.   If you have questions related to the role, please contact the recruiting manager. If you have questions related to the unions, please contact. Apply today and join us on our journey towards a sustainable society!"),
                new LabeledExample("en", "About Axis Communications Axis offers intelligent security solutions that enable a smarter, safer world. As the market leader in network video, Axis is driving the industry by continually launching innovative network products based on an open platform - delivering high value to customers through a global partner network. Axis has long-term relationships with partners and provides them with knowledge and ground-breaking network products in existing and new markets.Axis has more than 2,700 dedicated employees in more than 50 countries around the world, supported by a global network of over 80,000 partners. For more information about Axis, please visit our website.Reference number: 2182OUR TEAMWe are a team within Axis Research &amp; Development organization, overall   responsible for Axis Common Firmware PLatform and upgrading products with new firmware."),
                new LabeledExample("en", "WHAT WILL YOU DO? You will be a part of a team working with our common firmware at a system level. The team’s main responsibilities are to keep track of the firmware platform system resources and handle system wide activities such as open source upgrades.WHO ARE WE LOOKING FOR?We believe you are a curious person with a holistic and open source mindset. It is important that you have a genuine interest in software and tools at a system level.Suitable background for the position:Master’s/Bachelor’s degree in engineering, system science or similarExperience of general software developmentExperience of Linux environmentExperience of Embedded systemExperience of programing languages such as C and PythonExperience of Agile methods such as Scrum &amp; KanbanKnowledge of Docker, Jenkins, git, Elasticsearch and Kibana is an advantage.WHAT CAN WE DO FOR YOU?"),
                new LabeledExample("en", "Working with the Platform Management team will give you possibility to influence Axis firmware at a system level. You will have the opportunity to get broad knowledge of Axis’ products &amp; solutions, working closely with different parts of the company. Axis is an organization that values creativity and promotes teamwork and openness. This is a great opportunity to use and develop your skills as part of an exciting, successful organization that is already the world leader in network video. In exchange for your dedication Axis can offer you an innovative and global environment where you can develop both as a professional and as an individual.READY TO ACT?Axis is a company realizing the benefits of a diverse workforce. We know that diversity in groups creates a better working environment and promotes creativity, something that is fundamental for our success. Would you like to grow with us? Find out more from our recruiting manager."),

                new LabeledExample("sv", "Conic Restaurants AB äger och driver idag SUBWAY restauranger i Göteborg, Uddevalla, Skövde, Halmstad, Helsingborg, Malmö, Lund, Kristianstad och Kalmar. Subway är en av världens största restaurangkedjor med över 45 000 restauranger i över 100 länder och ett självklart val för alla som vill ha ett nyttigare och fräschare snabbmatsalternativ.Conic äger också varumärket “Togogo” och driver Togogos första restaurang på Centralstationen i Göteborg. Vi är ett expansivt företag och vår målsättning är att under de kommande åren öppna eller förvärva fler restauranger och att utveckla vår verksamhet. Vår expansion bygger på att vi lyckas attrahera de allra bästa inom vår bransch.Vi vill vara en modern arbetsgivare där medarbetare trivs på jobbet och får en bra balans mellan arbete och fritid. Vi behåller duktiga medarbetare genom att erbjuda intressanta utvecklingsmöjligheter.Vi behöver nu förstärka vårt team och söker dig som är glad, flexibel och som får både kunder och kollegor att trivas."),
                new LabeledExample("sv", "Du ska vara noggrann, van vid att ta eget ansvar och gilla att arbeta i ett högt tempo. Dina arbetsuppgifter kommer att bestå av försäljning och beredning av våra produkter samt att hålla rent och snyggt i restaurangen. Vi jobbar ständigt med att hålla hög produktkvalitet, god hygien och genuin kundservice. Nyckeln till detta tror vi är teamkänsla och söker därför dig som gillar att vara en del av en grupp.Erfarenhet från restaurang, café, livsmedel, kassa eller liknande är meriterande men inte ett krav. Som nyanställd hos oss på Subway kommer du under vår introduktion att utbildas i alla olika delar och moment du förväntas behärska. Vi lägger därför stor vikt vid vilja och iniativförmåga. Vi söker dig som inspirerar dina kollegor, är ambitiös och vill visa vad du går för.Hos oss avgör alltid dina ambitioner hur långt du vill utvecklas. För rätt person finns det stora chanser till att inom vår organisation utvecklas och få mer ansvar."),
                new LabeledExample("sv", "Vi söker nu dig som vill arbeta deltid och då våra restauranger har öppet både dag, kväll och helg söker vi dig som kan arbeta alla tider på dygnet. Vi tillämpar alltid en provanställning på sex månader och följer kollektivavtal. Tjänsten är på deltid 6 timmar per vecka.Vi kommer tillsätta tjänsterna så snart som möjligt och kommer löpande hålla intervjuer, så vänta inte med din ansökan!"),
                new LabeledExample("sv", "I Malmö stad arbetar vi med respekt, engagemang och kreativitet för att utveckla Malmö. Vi har Sveriges viktigaste jobb. Hos Malmö stad finns framtidens arbetsplats. Här gör vi skillnad. Vi skapar en ekologisk, ekonomisk och socialt hållbar stad. Vill du vara med och påverka? När du arbetar inom hemtjänst Dammfri och Fågelbacken cyklar du till våra brukare i den lugna innerstaden med närheten till Malmös vackra parker, restauranger och butiker. Du kommer att få arbeta med kollegor som ställer upp för dig, bemöter dig med respekt och med ett närvarande och tillgängligt ledarskap. Vi ser fram emot att ha dig i vårt team på Dammfri eller Fågelbacken! ARBETSUPPGIFTERDu kommer att tillhöra område Dammfri eller Fågelbacken. Tillsammans med dina kollegor i servicegruppen kommer du att ansvara för utförandet av beviljade bistånd städ och inköp."),
                new LabeledExample("sv", "KVALIFIKATIONERVi söker dig som har ett brinnande intresse av att underlätta vardagen för Malmöbon. Du är en person som;•är ansvarstagande•har lätt för att samarbeta•är initiativtagande•är kommunikativ, gärna med erfarenhet av dokumentation samt rapportering•är lugn och kan inge trygghet till våra brukare•har cykelvanaDet är meriterande om du har erfarenhet av städning. I ditt personliga brev vill vi att du tydligt beskriver vad det är som lockar dig att söka just denna tjänst. ÖVRIGTMalmö stad strävar efter att medarbetarna ska avspegla den mångfald som finns bland befolkningen. Vi välkomnar därför sökande som kan bidra till att vi som arbetsgivare kan tillgodose Malmöbornas behov.För att skicka in din ansökan, klicka på ansökningslänken i annonsen. Frågor om hur du ansöker och lägger in ditt CV ställer du direkt till Visma Recruit 0771- 10 10 29. Frågor om specifika jobb besvaras av den rekryterare eller kontaktperson som anges i annonsen."),
                new LabeledExample("sv", "Vill du arbeta på ett av Sveriges största byggbolag? Då är detta jobbet för dig!Om kundföretagetTill vår kund inom byggbranschen söker vi nu flitiga elmontörer. Kunden förser företag runt om i Sverige med rätt utrustning genom hela byggprojektet. De erbjuder hjälp med allt från etablering till färdigställande av projekt samt att skapa effektiva, säkra och gröna arbetsplatser.Dina arbetsuppgifterDin uppgift är att se till att elförsörjningen på olika byggarbetsplatser fungerar. Tjänsten innefattar arbete som exempelvis att bygga och serva elcentraler, undercentraler och fördelningscentraler till byggarbetsplatser. Arbetet är förlagt mestadels inomhus men även utomhusinstallationer kan förekomma.Din profilVi söker dig med avslutad gymnasieutbildning från elprogrammet, yrkesutbildning eller motsvarande utbildning. Meriterande om du arbetat som elektriker tidigare. Som person uppskattar du att arbeta i ett högt tempo och för att uppfylla kravprofilen för tjänsten är viktigt att du har ett högt säkerhetstänk."),
                new LabeledExample("sv", "Om vårt företagOnePartnerGroup är en svenskägd koncern. Vare sig du behöver hjälp med rekrytering, bemanning, funktionslösningar, omställning, utbildning eller HR kan du som kund hos oss räkna med att vi gör allt för att vara snabba och lätta att arbeta med, utan att tumma på vår professionalitet. Vi är helt sonika en komplett leverantör av kompetens inom både yrkesarbetar- och tjänstemannasektorn.Tillsammans med våra 2500 medarbetare är vi verksamma på ett 40-tal orter i Sverige. Med hjälp av vår stora lokala kännedom, erfarenhet och vårt engagemang strävar vi alltid efter att vara den lokalaste samarbetspartnern.Besök oss gärna på <a href='http://www.onepartnergroup.se' target='_blank' >www.onepartnergroup.se</a>"),
                new LabeledExample("sv", "Söker du ett spännande jobb inom gaming-världen? Vill du jobba centralt på ett coolt företag som är lite crazy och nytänkande? Fortsätt då läsa, vi har jobbet för dig!Dina arbetsuppgifterSom frontend-utvecklare hos vår kund kommer du framför allt att jobba med kodning. Du jobbar projektbaserat och i team för det aktuella projektet. Hos vår kund får du möjlighet att arbeta både med befintliga spel samt nyutveckling och lansering av spel.  Här får du möjlighet att jobba med erfarna och mycket komptetenta programmerare i ett företag som växer, det finns med andra ord stora möjligheter för att utvecklas! De språk du kommer att arbeta i är JavaScript, CSS, HTML5, Redux och React. Om företagetVår kund arbetar med spelutveckling av casinospel och gamblingspel. Genom att kombinera funktioner inom sina spel, kan du vinna pengar. Spelen riktar sig till de personer som vill tjäna pengar på att spela spel på mobilen. Bolaget grundades 2014 och växer snabbt."),
                new LabeledExample("sv", "I dagsläget är de ca 30 personer i Malmö där produktionen sköts, både förbättring av buggar och nyskapande. Resten av företaget finns på Malta. Företaget är väldigt måna om sin personal och dess välmående. Varje år skickas man på seminarier, kurser, kick-offer och de har återkommande AW:s. Frukost och energidryck serveras dagligen. Här jobba personer som är drivna, vill ha kul tillsammans och göra ett så  bra jobb sm möjligt.Din profilFör att trivas hos vår kund ska du vara en lagspelare. Du ska tycka om att ha ett utbyte av kunskap med dina kollegor, och det är inte fel om du gillar att stanna en stund och umgås efter jobb under lättsamma former.Skallkrav: 3 års erfarenhet inom kodning, behöver inte ha utbildning men kunskap! Språken; JavaScript, CSS, Redux, React och HTML5EngelskaMeriterande: 'Erfarenhet av iGamingJobbat i team tidigareOm ossFramtiden AB är ett rekryterings- och bemanningsföretag och vår specialitet är Unga Talanger."),
                new LabeledExample("sv", "Det innebär att vi hjälper våra kunder att hitta de bästa medarbetarna som fortfarande är i början av sin karriär. Just nu har vi hundratals lediga jobb. Framtiden AB arbetar med både bemanning och rekrytering och vår idé är att minska avståndet mellan utbildningen och arbetslivet. Vi gör vad vi heter – hjälper våra kunder till en bättre framtid genom den personal som vi förmedlar. För denna tjänst kommer du vara anställd av oss på Framtiden AB och arbeta som konsult hos ett av våra kundföretag.VillkorStart: omgående Arbetstider: Kontorstid med flexVi behandlar ansökningar löpande, skicka in din redan idag!"),

                new LabeledExample("dk", "Har du lyst til et nært samarbejde med kolleger i en klinik, der sætter patienten i centrum? Hos Vitanova får du rig mulighed for at udvikle dig, når du i en ledende funktion arbejder ud fra to af vores kerneværdier: støtte og nærvær. Som lægesekretær hos Vitanova bliver du en vigtig del af sekretariatet, når du er med til at opdatere og forny vores processer i samarbejde med den øvrige ledelse, ligesom du også har ansvar for at oplære personale, fortæller personalechef og jordemoder Rie Koldorf Jørgensen. Vi arbejder tværfagligt sammen med fokus på individuel behandling af vores patienter. Derfor vil du møde både glade patienter, men også nogle der trænger til trøstende ord. Som frontmedarbejder har du daglig patientkontakt både på mail og telefon, ligesom du også tager del i praktiske opgaver for vores behandlerteam, når de trænger til en hjælpende hånd."),
                new LabeledExample("dk", "Lægesekretær/SOSU-assistent 20-25 timer ugentligt til almen lægepraksis, Amager Speciallæger i Almen Medicin Boiesen, Binderup, Møller og Svendsen 4-lægepraksis med ca. 6500 patienter søger stabil, fleksibel og serviceminded sekretær. Du skal være indstillet på en travl og udfordrende hverdag og være vant til at have flere bolde i luften ad gangen. Dit job vil bestå i patientkontakt telefonisk og ved personlig henvendelse samt forefaldende sekretariatsopgaver. Du vil komme til at samarbejde tæt med 1 sekretær, 2 sygeplejersker og 4 læger. Erfaring fra almen praksis er en klar fordel. Vi kan tilbyde søde patienter, velordnede arbejdsforhold og gode samarbejdsrelationer."),
                new LabeledExample("dk", "MENY søger flere MADarbejdere – er du vores nye delikatesseassistent? MENY Nærum er byens og hele Danmarks førende fødevaremarked, og vi søger en passioneret og engageret delikatesseassistent. Så ka' du flække en hummer og skære hovedet af en grøn asparges? Er du kreativ, elsker du at arbejde med mad og synes MENYs delikatessekoncept er spændende? Ja, så er jobbet måske lige dig. Meny Du skal blandt andet: Være en del af den daglige produktion og sikre en fantastisk kundebetjening Sikre at vores stærke koncepter og faste rutiner overholdes Være med til at udvikle nye retter og måltidsløsninger Du får et spændende og varieret job i en dynamisk virksomhed, hvor du kan være med til at præge din egen arbejdssituation og udvikling."),
                new LabeledExample("dk", "Gram Equipment A/S, Kolding Vil du være med til at udvikle og definere Gram Equipments fremtidige ERP-løsning? Har du en god projekt- og procesforståelse? Brænder du for at analysere og optimere arbejdsgange? Vil du være en del af en global virksomhed, der udvikler og sælger verdens mest innovative og fascinerende udstyr til industriel isproduktion? Gram Equipment Med reference til vores Head of IT søger vi en ERP-konsulent, der skal være med til at sikre en stabil drift af den nuværende NAV-løsning samt udvikle og definere Gram Equipments fremtidige forretningsløsning i et nyt ERP-system. Dine arbejdsopgaver vil bl.a. omfatte at: Konfigure MS Dynamics Nav i relation til ændringer i forretningen Tilsikre, at NAV-brugerflader samt andre applikationer modsvarer forretningens behov Fungere som projektleder på udvalgte IT-projekter"),
                new LabeledExample("dk", "Solrød Bibliotek søger en studerende til udlånsvagt og formidling af materialer og kulturaktiviteter Solrød Kommune Opgaverne består primært af biblioteksopgaver i udlånet og håndtering af forespørgsler fra borgere, som vi servicerer gennem nærvær, smil og litterært engagement. Arbejdet kræver, at du kan opsøge og gå i dialog med vores brugere og appellere til deres nysgerrighed og pirre deres læselyst og vidensbegær. I forbindelse med arbejdet, vil der også være forskellige praktiske opgaver i biblioteksrummet, så det altid fremstår ordentligt og indbydende. Vi kan tilbyde en arbejdsplads med mulighed for personlig og faglig udvikling, udfordringer og selvstændighed en spændende, lærerig og sjov arbejdsdag i et godt kollegialt miljø engagerede og kreative kollegaer, der arbejder tæt sammen på tværs af fagligheder"),
                new LabeledExample("dk", "Salgsassistent Fendt, Glostrup Vi søger en salgsassistent til at varetage vores ordreproces for Fendt i Danmark – primært ordrebehandling og salgssupport. Jobbet indbefatter daglig kontakt til vores sælgere og forhandlere i Danmark samt jævnlig kontakt med vores fabrikker i Tyskland og Italien. Opgaver: Fakturering/kreditering Opfølgning og vedligeholdelse af ordrer Opdatering i vores ordresystemer Vi tilbyder: Et spændende og udfordrende job Fast placering hos AGCO Danmark A/S i Glostrup Gode fremtidige muligheder inden for AGCO-koncernen"),
                new LabeledExample("dk", "Lead Buyer VVS Bravida Danmark A/S, Brøndby Vil du bistå Bravidas afdelinger med at skabe synergi og besparelser på indkøb af VVS-komponenter? Brænder du for at vise, hvad en god indkøbsstrategi kan gøre for virksomheden? Du vil som lead buyer for Vvs-komponenter arbejde på tværs af organisationen i Danmark. Din fornemmeste opgave bliver at støtte vores driftsafdelinger med at skabe synergi og besparelser inden for projektindkøb. Dine primære opgaver bliver at: Gøre en proaktiv indsats med at bundle volumener på tværs af afdelinger i Bravida, og herefter udbyde disse til leverandører både nationalt og internationalt Udfordre vores afdelinger til at tænke i nye løsninger inden for produkt- og leverandørvalg Sikre kvalitet i leverancer"),
                new LabeledExample("dk", "Freight Forwarding Assistent Blue Water Shipping A/S, Aalborg Hos Blue Water Shipping A/S tror vi på at kundefokus, teamwork og købmandskab er nogle af de vigtigste elementer til succes. Til vores speditionsafdeling i Aalborg søger vi nu en serviceminded kollega, der brænder for struktur og detaljer. Blue Water Shipping A/S Du vil blive en del af at team med 4 kolleger hvor høj faglighed, en uformel teamkultur og et stærkt sammenhold er de vigtigste drivkræfter for at sikre fælles succes. Vores nye kollega vil håndtere administrative opgaver, såsom: Oprette og behandle indkomne bookinger Specialistrolle i bookingsystemet Afregning og fakturering"),
                new LabeledExample("dk", "Montør Hoyer A/S, Hadsten Har du lyst til at indgå i et montageteam af velkvalificerede medarbejdere hos Hoyer, hvor der ofte er travlt, og opgaverne varierer? Så er dette en oplagt mulighed for dig. Vi er på jagt efter en ny topmotiveret montør, der ønsker at gøre en forskel for både kunder og kollegaer. Du er ikke bange for at tage fat og trives i en uformel hverdag med en direkte omgangstone. Af montageopgaver kan bl.a. nævnes udskiftning af flanger og lejer, isætning af PTC, varmebånd og klixon samt andre kundespecifikke montageopgaver. Erfaring i arbejde med elektromotorer er et klart plus, men er ikke et must, da oplæring vil finde sted."),
                new LabeledExample("dk", "Business Development Manager CCCCCCC ApS, Frederiksberg ccccccc er et kreativt studio, som specialiserer sig i motion design og animation. Fra vores kontor på Frederiksberg skaber vi visuelle fortællinger for en bred palet af kunder fra hele verden. Vi insisterer på, at både indhold og visuelt udtryk skal leve op til de højeste standarder inden for feltet. Vi søger en forretningsudvikler, der tænker formål over profit - og værdiskabelse over kold kanvas. Vi søger dig, som er optaget af at gøre visioner til virkelighed. Det betyder, at du er strategisk stærk, og at du gennem salg af højkvalitets motion design og animation kan bidrage til at føre ccccccc i nye og spændende retninger. Jobets opgaver vil bl.a. bestå af Oversætte virksomhedens visioner til konkrete markedsføringsstrategier og handleplaner. Kortlægning og udvikling af value propositions. Flytte mindset fra lokalt til globalt.")

            };


            return labeledExamples;

        }
        public static List<TokenizedExample> CreateThirtyCompleteTokenizedExamples()
        {

            List<LabeledExample> labeledExamples = CreateThirtyCompleteLabeledExamples();
            List<TokenizedExample> tokenizedExamples = new LabeledExampleManager().CreateOrDefault(labeledExamples);

            return tokenizedExamples;

        }

        public static List<INGram> CreateNGrams
            (List<Monogram> monograms, List<Bigram> bigrams = null, List<Trigram> trigrams = null, List<Fourgram> fourgrams = null, List<Fivegram> fivegrams = null)
        {

            List<INGram> ngrams = new List<INGram>();
            ngrams.AddRange(monograms);

            if (bigrams != null)
                ngrams.AddRange(bigrams);

            if (trigrams != null)
                ngrams.AddRange(trigrams);

            if (fourgrams != null)
                ngrams.AddRange(fourgrams);

            if (fivegrams != null)
                ngrams.AddRange(fivegrams);

            return ngrams;

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 29.09.2022
*/