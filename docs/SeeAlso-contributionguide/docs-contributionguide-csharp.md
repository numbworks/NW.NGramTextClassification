# contribuitionguide-csharp
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2023-05-20 | numbworks | Created. |
| 2027-07-26 | numbworks | Last update. |

## Overview

This document collects all the guidelines to follow for anyone who would like to contribute code to this project.

## Disclaimer

External contributions are welcome, though they must align with the intended purpose and roadmap of this project, and comply with all guidelines outlined in this document.

## Principles and Best Practices

Below a list of principles and best practice that are widely adopted in the codebase:

1. **Maintainability First**. The highest cost in the software industry is the maintanance of a software over time, not its creation.
2. **Software Usability** as part of the **Definition-of-Done**. Usable software is pleasurable for the users to use and for the developers to maintain.
3. **Documentation** as part of the **Definition-of-Done**. Every release should include up-to-date documentation. Actual documentation makes the software faster to learn for the users and easier to debug for the developers. 
4. **DRY Principle**. Write a certain piece of code once, reference it millions of times.
5. **Single Responsability Principle** (SOLID). Every class should have one single responsability, even when you might end up having many very small classes.
6. **Interface Segregation Principle** (SOLID). Better to have a lot of smaller interfaces than a few bigger ones.
7. **Dependency Inversion Principle** (SOLID). Your code should always depend upon abstractions, not upon concrete classes.
8. **Composition over inheritance**. Objects form an opportunistic "has a" relationship with other objects that contain the logic they need, rather than a parent–child relationship that can become difficult to maintain as the codebase grows. In some occasions, inheritance can be used with **abstract classes**.
9. **Design Patterns**. You'll find a heavy use of **Factory Pattern**, **Repository Pattern** and **Object Mother Pattern** (for unit testing).
10. **Strong focus on unit testing**. Software must maintain unit test coverage above 70% and adhere to established testing best practices, including consistent test‑naming conventions (e.g., BDD style), and appropriate use of mocks and fakes.
11. **Naming convention**. Classes, variables and projects must be named consistently according to a defined domain‑specific language and established naming conventions known to improve code readability, such as hierarchical naming.
12. **Templating**. Custom class and method templates should be used consistently across the codebase to standardize structure, enforce uniform patterns and maximize development speed.
13. **Minimal set of external dependencies**. External software packages must be introduced only when they provide clear, measurable value, as each dependency adds long‑term maintenance overhead and requires ongoing lifecycle management.
14. **Minimal set of arguments**. Methods should accept the minimum number of arguments necessary. When appropriate, related arguments should be grouped into dedicated parameter classes to improve method readability and maintainability.

## Clean Architecture

**Clean Architecture** is a software design approach that structures an application into independent, concentric layers, ensuring that business logic stays isolated from frameworks, databases, and UI. The key idea is that all dependencies must point inward, toward the core business rules.

For example, when developing a CLI application, we encapsulate all business logic in a library project and place the delivery mechanism (the CLI) in a separate project:

```
- /src/
	- NW.NGramTextClassification
	- NW.NGramTextClassification.CLI
```

The inner circle corresponds to the library, while the outer circle corresponds to the CLI. In other words, the CLI depends on the library, but the library remains independent of the CLI.

This separation of concerns is also present in other two well‑known architectural styles:

- **Hexagonal Architecture** -  the CLI is an adapter and the library is the domain. The CLI simply forwards requests to the domain layer.
- **Layered Architecture** - the CLI represents the application layer, while the library represents the domain layer.

## The Primary Constructor

Public and private properties of a class should be assigned exclusively through the constructor. This approach ensures:

1. **Centralized validation**, in line with the DRY principle.
2. **Elimination of hidden side effects** caused by scattered validation logic.
3. **Consistent object creation**, improving long‑term maintainability.
4. **Immutability benefits**, including thread safety, reference caching, and other performance and security advantages.

```csharp
...
public uint TruncateTextInLogMessagesAfter { get; }
public double MinimumAccuracySingleLabel { get; }
public double MinimumAccuracyMultipleLabels { get; }
public string FolderPath { get; }

public SettingBag(
	uint truncateTextInLogMessagesAfter,
	double minimumAccuracySingleLabel,
	double minimumAccuracyMultipleLabels,
	string folderPath)
{

    Validator.ValidateStringNullOrWhiteSpace(folderPath, nameof(folderPath));

    TruncateTextInLogMessagesAfter = truncateTextInLogMessagesAfter;
    MinimumAccuracySingleLabel = minimumAccuracySingleLabel;
    MinimumAccuracyMultipleLabels = minimumAccuracyMultipleLabels;
    FolderPath = folderPath;

}
...
```

There may be exceptions when working with **DTOs** (**Data Transfer Objects**) that do not require validation and are intended for internal or private use. In these cases, non‑immutable public properties can be acceptable when immutability would add unnecessary complexity or reduce clarity.

## The Secondary (Default) Constructor

Classes that embed logic (i.e., non‑DTOs) should provide two constructors:

- A **constructor** that requires the consumer to supply all necessary dependencies.
- A **default constructor** that initializes most or all dependencies internally.

First approach:

```csharp
public class SettingBag
{

    public static uint DefaultTruncateTextInLogMessagesAfter { get; } = 20;
    public static double DefaultMinimumAccuracySingleLabel { get; } = 0.5;
    public static double DefaultMinimumAccuracyMultipleLabels { get; } = 0.0;
    public static string DefaultFolderPath { get; } = Directory.GetCurrentDirectory();

    public uint TruncateTextInLogMessagesAfter { get; }
    public double MinimumAccuracySingleLabel { get; }
    public double MinimumAccuracyMultipleLabels { get; }
    public string FolderPath { get; }

    /// <summary>Initializes a <see cref="SettingBag"/> instance.</summary>
    public SettingBag(
                uint truncateTextInLogMessagesAfter,
                double minimumAccuracySingleLabel,
                double minimumAccuracyMultipleLabels,
                string folderPath)
    {

        Validator.ValidateStringNullOrWhiteSpace(folderPath, nameof(folderPath));

        TruncateTextInLogMessagesAfter = truncateTextInLogMessagesAfter;
        MinimumAccuracySingleLabel = minimumAccuracySingleLabel;
        MinimumAccuracyMultipleLabels = minimumAccuracyMultipleLabels;
        FolderPath = folderPath;

    }

    /// <summary>Initializes a <see cref="SettingBag"/> instance using default parameters.</summary>
    public SettingBag()
        : this(
                truncateTextInLogMessagesAfter: DefaultTruncateTextInLogMessagesAfter,
                minimumAccuracySingleLabel: DefaultMinimumAccuracySingleLabel,
                minimumAccuracyMultipleLabels: DefaultMinimumAccuracyMultipleLabels,
                folderPath: DefaultFolderPath
                ) { }

...

}
```

Second approach:

```csharp
...
public ArgumentManager(
	IDoubleManager doubleManager = null,
	IAsciiBannerManager asciiBannerManager = null,
	ITerminalWindowManager terminalWindowManager = null,
	ITextClassifierFactory textClassifierFactory = null,
	IComponentBagFactory componentBagFactory = null,
	ISettingBagFactory settingBagFactory = null)
{
	
	DoubleManager = doubleManager ?? new DoubleManager();
	MinimumAccuracyValidator = new MinimumAccuracyValidator(DoubleManager);
	AsciiBannerManager = asciiBannerManager ?? new AsciiBannerManager();
	TerminalWindowManager = terminalWindowManager ?? new TerminalWindowManager();
	TextClassifierFactory = textClassifierFactory ?? new TextClassifierFactory();
	ComponentBagFactory = componentBagFactory ?? new ComponentBagFactory();
	SettingBagFactory = settingBagFactory ?? new SettingBagFactory();

	_componentBag = ComponentBagFactory.Create();

}
...
```

Using a **default constructor** eliminates unnecessary decisions and reduces the number of steps a developer must take to instantiate the class, resulting in a significant improvement in overall software usability.

Additionally, each class is encouraged to expose its default values or delegates through easily accessible **public static properties**. This practice significantly improves both code readability and class testability by making standard configurations explicit and discoverable. 

Static properties that represent default values should be prefixed with "Default" - e.g. `DefaultTruncateTextInLogMessagesAfter`, `DefaultFolderPath`.

## The Entry-Point Class

Each project has a public **entry-point class**, which conveniently collects and exposes all the available methods to the consumer. The **entry-point class** summarizes how the consumer is allowed to interact with the project or, in other words, it exposes all the use cases that have been considered for the project so far.

To simplify its initialization, the **entry-point class** doesn't exist without two other classes:

- A `SettingBag` class, collecting all the settings that the project requires the user to provide in order to run.
- A `ComponentBag` class, collecting all the dependencies that the project requires in order to run.

The concept behind these two classes can be defined as **The Bag Pattern**, and both classes are provided to the **entry-point class** thru its constructor:

```csharp
...
public TextClassifier(ComponentBag componentBag, SettingBag settingBag)
{

    Validator.ValidateObject(componentBag, nameof(componentBag));
    Validator.ValidateObject(settingBag, nameof(settingBag));

    _componentBag = componentBag;
    _settingBag = settingBag;

}

public TextClassifier()
    : this(
        DefaultComponentBag, 
        DefaultSettingBag) { }

...
```

## Internal And External Business Logic

Each class has two kinds of business logic: 

- **Internal logic**, when the class provides the behavior itself.
- **External logic**, when the behavior is delegated to another class.

Additionally, according to the **Single Responsibility Principle**, every class should have one clear responsibility. Non‑specific or cross‑cutting logic should be moved into its own dedicated class. For example, all serialization‑related logic should be collected in a `Serializer` class (or placed under a `Serialization` namespace).

The entry‑point class is a good example of this pattern: most of its behavior comes from its dependencies, and the entry‑point class acts primarily as a “junction” between those dependencies and the library user.

```csharp
...
public class TextClassifier : ITextClassifier
{

    private ComponentBag _componentBag;

    ...

    public void LogAsciiBanner()
        => _componentBag.LoggingActionAsciiBanner(AsciiBanner);
    public IFileInfoAdapter Convert(string filePath)
        => _componentBag.FileManager.Create(filePath);

    ...
    
}
```

## Private Methods

The use of `private` methods is absolutely incentivized, because: 

- They massively help to break the code in smaller parts within the same class, minimizing duplication and maximizing re-use
- They maximize the readibility of the `public` methods

```csharp
...
public class ArgumentManager : IArgumentManager
{

	...
	public CommandLineApplication Create()
	{

		CommandLineApplication app = new CommandLineApplication
		{

			Name = CommandLineString.APPLICATION_NAME,
			Description = CommandLineString.APPLICATION_DESCRIPTION

		};

		app = AddRoot(app);
		app = AddSession(app);

		app.HelpOption(inherited: true);

		AddCustomHelpTextGenerator(app);
		AddValidationErrorHandling(app);

		return app;

	}

	...
	private CommandLineApplication AddRoot(CommandLineApplication app) { ... }
	private CommandLineApplication AddSession(CommandLineApplication app) { ... }
	private void AddCustomHelpTextGenerator(CommandLineApplication app) { ... }
	private void AddValidationErrorHandling(CommandLineApplication app) { ... }
}
```

## The Session Object

It's good practice to define a `Session` object, to collect all the possible outcome objects generated by the library's entry-point class. 

This practice allows to post-pone the decision about what to use to a later stage.

```csharp
...
public class TextClassifierSession
{

	public double MinimumAccuracySingleLabel { get; }
	public double MinimumAccuracyMultipleLabels { get; }
	public List<TextClassifierResult> Results { get; }
	public string Version { get; }

	...
}
```

```csharp
...
public class TextClassifier : ITextClassifier
{
	...
	public TextClassifierSession ClassifyOrDefault(
		TextSnippet textSnippet, 
		INGramTokenizerRuleSet tokenizerRuleSet, 
		List<LabeledExample> labeledExamples) { ... }
	...
}
```

Having a dedicated session object aligns well with **Clean Architecture** principles because:

- It keeps the domain results cohesive (**session encapsulation**).
- It allow the delivery mechanism (CLI, API, GUI) to decide what to display or persist (**deferred decisions**).
- It ensures that the library's entry-point class doesn't return presentation-specific constructs.

## The ComponentBag Object

The `ComponentBag` object collects all the dependencies into a single place, including any default logic, and is passed to the library's entry-point class. This object has the responsibility of providing all the necessary dependencies to the entry-point.

```csharp
...
public class ComponentBag
{
	...
	public static Func<double, double> DefaultRoundingFunction { get; }
		= x => Math.Round(x, 6, MidpointRounding.AwayFromZero);
	...

	public INGramTokenizer NGramsTokenizer { get; }
	public ISimilarityIndexCalculator SimilarityIndexCalculator { get; }
	public Func<double, double> RoundingFunction { get; }
	
	public ComponentBag(
			INGramTokenizer nGramsTokenizer = null,
			ISimilarityIndexCalculator similarityIndexCalculator = null,
			Func<double, double> roundingFunction = null)
	{
	
            NGramsTokenizer = nGramsTokenizer ?? new NGramsTokenizer();
            SimilarityIndexCalculator = similarityIndexCalculator ?? new SimilarityIndexCalculator();
            RoundingFunction = roundingFunction ?? DefaultRoundingFunction;
			
	}
}
```

```csharp
...
public TextClassifier(ComponentBag componentBag, SettingBag settingBag)
{

    Validator.ValidateObject(componentBag, nameof(componentBag));
    Validator.ValidateObject(settingBag, nameof(settingBag));

    _componentBag = componentBag;
    _settingBag = settingBag;

}
```

On **Clean Architecture**'s terms, the `ComponentBag` object acts as a **composition root for the domain**. 

The **composition root for the domain** is the place where all domain dependencies are assembled and configured before being passed into the domain’s entry-point. It is the single, centralized location responsible for constructing the domain with the exact set of collaborators it needs, while keeping the domain itself free of creation logic.

## The SettingBag Object

The `SettingBag` object collects all the relevant settings, including their eventual default values, into a single place, and is passed to the library’s entry-point. This object has the responsibility of providing all the settings that the user may want to customize to the entry-point.

```csharp
public class SettingBag
{

    public static uint DefaultTruncateTextInLogMessagesAfter { get; } = 20;
    public static double DefaultMinimumAccuracySingleLabel { get; } = 0.5;
    public static double DefaultMinimumAccuracyMultipleLabels { get; } = 0.0;
    public static string DefaultFolderPath { get; } = Directory.GetCurrentDirectory();

    public uint TruncateTextInLogMessagesAfter { get; }
    public double MinimumAccuracySingleLabel { get; }
    public double MinimumAccuracyMultipleLabels { get; }
    public string FolderPath { get; }

    /// <summary>Initializes a <see cref="SettingBag"/> instance.</summary>
    public SettingBag(
                uint truncateTextInLogMessagesAfter,
                double minimumAccuracySingleLabel,
                double minimumAccuracyMultipleLabels,
                string folderPath)
    {

        Validator.ValidateStringNullOrWhiteSpace(folderPath, nameof(folderPath));

        TruncateTextInLogMessagesAfter = truncateTextInLogMessagesAfter;
        MinimumAccuracySingleLabel = minimumAccuracySingleLabel;
        MinimumAccuracyMultipleLabels = minimumAccuracyMultipleLabels;
        FolderPath = folderPath;

    }

    /// <summary>Initializes a <see cref="SettingBag"/> instance using default parameters.</summary>
    public SettingBag()
        : this(
                truncateTextInLogMessagesAfter: DefaultTruncateTextInLogMessagesAfter,
                minimumAccuracySingleLabel: DefaultMinimumAccuracySingleLabel,
                minimumAccuracyMultipleLabels: DefaultMinimumAccuracyMultipleLabels,
                folderPath: DefaultFolderPath
                ) { }

...

}
```

```csharp
...
public TextClassifier(ComponentBag componentBag, SettingBag settingBag)
{

    Validator.ValidateObject(componentBag, nameof(componentBag));
    Validator.ValidateObject(settingBag, nameof(settingBag));

    _componentBag = componentBag;
    _settingBag = settingBag;

}
```

On **Clean Architecture**'s terms, the `SettingBag` object acts as a **configuration provider  for the domain**. 

The **configuration provider for the domain** is a component that supplies the domain with all runtime settings and customizable parameters it needs, without embedding configuration logic inside the domain itself. It centralizes defaults and user‑defined values so the domain can operate with stable, validated settings.

## The MessageCollection Class

The `MessageCollection` class is static and centralizes all messages in a single place, including both logging messages and exception messages. This centralization helps developers to use a cohesive and consistent language throughout the entire application.

```csharp
namespace NW.NGramTextClassification.TextClassifications
{
    public static class MessageCollection
    {

        public static Func<int, string> ProvidedSnippetsAre = (count) => $"The provided snippets are: '{count}'.";
        public static string AttemptingToClassifyProvidedSnippet = "Attempting to classify the provided snippet of text...";
        ...
    }
}
```

It's a more future-proof practice to have one `MessageCollection` class for each namespace.

## Inline Documentation

Inline documentation (**XML Docs**) is mandatory for public members, such as classes, interfaces and methods. It's optional for public properties and private members.

```csharp
...
/// <summary>The entry point of this library.</summary>
public interface ITextClassifier
{

    /// <summary>
    /// Attempts to assign a label to <paramref name="textSnippet"/> by learning from <paramref name="labeledExamples"/> and by using a default <see cref="INGramTokenizerRuleSet"/>.
    /// <para>If one rule in the default <see cref="INGramTokenizerRuleSet"/> fails, no exception will be thrown and the method will continue processing the other rules.</para>
    /// <para>If all rules will fail, <see cref="TextClassifier.DefaultTextClassifierResult"/> will be added to the <see cref="TextClassifierSession"/>.</para>
    /// </summary>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentException"/>       
    TextClassifierSession ClassifyOrDefault(TextSnippet textSnippet, List<LabeledExample> labeledExamples);

    ...
}
```

```csharp
...
/// <inheritdoc cref="ITextClassifier"/>
public class TextClassifier : ITextClassifier
{
    ...

    /// <summary>Initializes a <see cref="TextClassifier"/> instance.</summary>
    public TextClassifier(ComponentBag componentBag, SettingBag settingBag) { ... }

    /// <summary>Initializes a <see cref="TextClassifier"/> instance using default parameters.</summary>
    public TextClassifier() { ... }

    ...

    public TextClassifierSession ClassifyOrDefault(TextSnippet textSnippet, List<LabeledExample> labeledExamples) { ... }

    ...
}
```

## Unit Test Structure

The unit tests are grouped per project:

```
- /src/
	- NW.NGramTextClassification
	- NW.NGramTextClassification.CLI
- /tests/
	- NW.NGramTextClassification.UnitTests
	- NW.NGramTextClassification.CLI.UnitTests
```

## Unit Test Framework

We adopt `NUnit` as the testing framework, augmented with `NSubstitute` when mocking functionalities are needed:

```csharp
using NUnit.Framework;
using NSubstitute;
...
```

## Unit Test Naming Convention

The unit tests:

- Can be standard or parametric (when required).
- Must follow the **Arrange-Act-Assert (AAA)** pattern.
- Must use test names that follow the **Behavior-Driven Development (BDD)** naming convention (Should...When).
- Must adopt the **Object Mother** pattern when the same object is required by multiple test classes.

```csharp
...
[TestFixture]
public class TextClassifierResultTests
{

    private static TestCaseData[] toStringTestCases =
    {

        new TestCaseData(
                new TextClassifierResult(
                    textSnippet: TextSnippets.ObjectMother.TextSnippet,
                    label: LabeledExamples.ObjectMother.ShortLabeledExample01.Label,
                    indexes: Similarity.ObjectMother.SimilarityIndexes,
                    indexAverages: Similarity.ObjectMother.SimilarityIndexAverages
                    ),
                ObjectMother.TextClassifierResult_AsString
            ).SetArgDisplayNames($"{nameof(toStringTestCases)}_01"),
            ...
    }
    ...

    [TestCaseSource(nameof(toStringTestCases))]
    public void ToString_ShouldReturnTheExpectedString_WhenInvoked(
        TextClassifierResult textClassifierResult, 
        string expected)
    {

        // Arrange
        // Act
        string actual = textClassifierResult.ToString();

        // Assert
        Assert.That(
            string.Equals(
                expected,
                actual,
                StringComparison.InvariantCulture),
            Is.True);

    }
    
    ...

    [Test]
    public void TextClassifierResult_ShouldCreateAnInstanceOfThisType_WhenProperArgument()
    {

        // Arrange
        // Act
        TextClassifierResult actual
            = new TextClassifierResult(
                    textSnippet: TextSnippets.ObjectMother.TextSnippet,
                    label: LabeledExamples.ObjectMother.ShortLabeledExample01.Label,
                    indexes: Similarity.ObjectMother.SimilarityIndexes,
                    indexAverages: Similarity.ObjectMother.SimilarityIndexAverages
                );

        // Assert
        Assert.That(actual, Is.InstanceOf<TextClassifierResult>());

        Assert.That(actual.TextSnippet, Is.InstanceOf<TextSnippet>());
        Assert.That(actual.Label, Is.InstanceOf<string>());
        Assert.That(actual.SimilarityIndexes, Is.InstanceOf<List<SimilarityIndex>>());
        Assert.That(actual.SimilarityIndexAverages, Is.InstanceOf<List<SimilarityIndexAverage>>());

    }
}
```

## Unit Test Coverage

We aim for <u>at least 70% code coverage</u> for each project.

## Source

Most of the code used in the example blocks comes from the following project:

- [NW.NGramTextClassification](https://github.com/numbworks/NW.NGramTextClassification)

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)
