# GlobalX code asssesment

## Introduction

The problem domain was to make a console app that would read a list of names from a file, sort said list first by family name and then by given name(s), and finally write the resulting sorted list to a new file.

The aim was to write code that was "caring" towards others (actually myself included) that may need/want to read it/implement it/modify it. 

Also, it needed to adhere to the SOLID principles and contain adequate tests.

## SOLID

As a complete novice to C# and .NET, I started researching about how the language deals with the SOLID principles.

Very quickly I learned that these are first-class citizens within this ecosystem. Writing code that adheres to them is not only possible but well encouraged and facilitated thanks to stuff like:

- Maaany different interfaces available to be implemented or extended
- Support for generics
- Packages to implement automatic dependency injection
- Detailed documentation available online on the subject

## Inversion of control

I tried my hardest at making sure I was doing IoC right: the classes I implemented don't depend on specific class implementations, but rather on interfaces.

This way, classes that implement the same interface can be easily swapped as dependecies of another class.

The benefits of this approach became super apparent when writing tests: `NamesSorter` is injected a `FileReader` and a `FileWriter` in the console application which implement `IReader<T>` and `IWriter<T>` respectively.

In the tests I passed a `MockReader` and a `MockWriter` to `NamesSorter` and everything worked just as well, since they also both implement `IReader<T>` and `IWriter<T>`.

## Dependency injection

While I had used IoC before, I had not used automatic dependency injection before. It took me a little longer to wrap my head around how it works but I think I got it.

It is intimately linked to IoC: basically, instead of passing dependencies directly to the instance of a class when it's created, one registers the dependencies and any class that depends on them as services.

The framework then can do its magic, thanks to the fact that it knows which services implement which classes, and it also knows which classes depend on which implementations.

```cs
// create a new ServiceCollection where we will add the servies that we want the IoC container to handle automatic injection for
ServiceCollection services = new ServiceCollection();

// add an IComparer<string> in the form of our NamesComparator classs
services.AddTransient<IComparer<string>, NamesComparator>();
// add a reader that will return an IEnumerable<string> by reading from a file
services.AddTransient<IReader<IEnumerable<string>>, FileReader>();
// add a write that will write an IEnumerable<string> to a file
services.AddTransient<IWriter<IEnumerable<string>>, FileWriter>();
// add the NamesSorter class. The previous dependencies will be automatically injected into this class that depends on all of them
services.AddTransient<NamesSorter>();

// expose the service provider created by building the services, so we can use it to get references to the instances created and consume them
serviceProvider = services.BuildServiceProvider();
```

I used the same process in the tests with the `MockReader` and `MockWriter` classes.

## Common patterns

My software development background is primarily JavaScript/TypeScript based, with dashes of C/C++ in the mix.

I appreciated C#'s structure: in certain ways it's reminiscent of TypeScript and C++ combined, I felt quite comfortable approaching it.

## What I learned

All the stuff that I read to prepare for this assesment and doing the assesment itself gave me an even stronger appreciation for the principles of SOLID.

Even for something as small as this application you can see why it makes sense to structure code like that. Scaling up to more complex domains, it becomes critical to avoid lots of headaches and sleepless nights for the whole team.

The great thing is also that there's not a whole lot more of prior setting up to do compared with a scenario where you're not using IoC and DI. In this specific case it was just an additional method in the main program.

All in all, SOLID looks to me like a no-brainer at this point.

## Structure

I've decided to only create two namespaces, one called `GlobalxCodingAssesment` for interfaces, classes and program, the other `GlobalxCodingAssesmentTests` for all the tests.

I selected `MSTest` as the test framework.

I'm using the preview version of .NET 6 because it's natively compatible with M1 Macbooks, no other specific reason.

## Limitations

In keeping with the time limitations suggested, I decided to take a couple of shortcuts, mainly around reading/writing the lists.

For example, `FileReader` assumes there's a file named `unsorted-names-list.txt` in directory from where `dotnet run` is executed. If the file's not there though... the program will write an error message and close the application early.

`FileWriter` on the other hand will try to write a file in that same directory. If for some reason it can't the program will fail.

Another thing this assesment doesn't make use of is asynchronous operations. I figured that given the simplicity of the app it was ok to do things like I/O synchronously.

If I was implementing the same thing but for, let's say, a web server API endpoint, I'd definitely prefer using async.

## How to run

In order to run the console app, type `dotnet run --project SorterApp` in the console from inside the project's root directory.

To run tests the command is `dotnet test` once again from the project's root directory.
