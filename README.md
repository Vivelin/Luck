# Vivelin.Luck
Provides random element selection backed by a thread-safe source of random numbers.

[![Build status](https://ci.appveyor.com/api/projects/status/j2wcsxpkd80maxle/branch/master?svg=true)](https://ci.appveyor.com/project/Vivelin/luck/branch/master) [![NuGet](https://img.shields.io/nuget/v/Vivelin.Luck.svg)](https://www.nuget.org/packages/Vivelin.Luck)

## Usage

Notable functionality provided by this package:

- `Random` extensions that generate a random floating-point number within a range:

  ```c#
  > using Vivelin.Luck;
  > new Random().Next(-100.0d, +100.0d)
  -86.09016276248272
  ```

- A static, thread-safe way to generate numbers, based on the article [Revisiting randomness] by Jon Skeet:

  ```c#
  > using Vivelin.Luck;
  > Rng.Next()
  1284714155
  ```
- An extension method on `IEnumerable<T>` that randomly selects an element, taking weights into account. Currently, this is only implemented for collections of types that implement `IWeighted`, but this could be made optional in future versions.

  ```c#
  var collection = new[]
  {
      new WeightedValue(0.01),
      new WeightedValue(0.1),
      new WeightedValue(10),
      new WeightedValue(100)
  };
  collection.Sample(); // More likely to return WeightedValue(100) than WeightedValue(0.01)
  ```

[Revisiting randomness]: https://codeblog.jonskeet.uk/2009/11/04/revisiting-randomness/
