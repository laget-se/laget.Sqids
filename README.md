# laget.Sqids
> **Note**
> The IDs that Sqids generates are unique and always decode into the same numbers. You can also make them unique to your application (so that they're not the same as everyone else who uses Sqids) by providing a shuffled alphabet.
>
> Default alphabet: "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"

Simple library for using hashed ids in DTOs

Based on https://www.sqids.org/

## Configuration
> This example is shown using Autofac since this is the go-to IoC for us.

```c#
await Host.CreateDefaultBuilder()
    .ConfigureContainer<ContainerBuilder>((context, builder) =>
    {
        builder.RegisterSqids();
    })
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .Build()
    .RunAsync();
```

Salts for the hashes will be loaded from the config file expecting the following format 
```json
{
    /* ..., */
    "Sqids": {
        "DefaultAlphabetVersion": "xx",
        "Alphabets": {
            "xx": "yyyy..."
        }
    }
}
```
Where 'xx' is a 2 character version code and 'yyyy' is the alphabet (of unlimited size but must be larger than 5) used by that version.

> **Warning**
> Sqids needs an alphabet that contains at least 5 unique characters.

## Usage
> **Note**
> You can use any [integral numeric type](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types) (e.g. `long`, `byte`, `short`, etc.) as the type argument. `int` is just the most common one, but if you need to encode/decode larger numbers, for example, you could use `long`/`ulong` instead.

If you're targeting an older framework than .NET 7, SqidsEncoder only supports int, and there is no generic type parameter you need to supply, so just:

### Basic usage
```c#
    var sqid = Sqid.FromInt(42);
    var id = sqid.ToInt();
```

```c#
    var sqid = Sqid.FromLong(42);
    var id = sqid.ToLong();
```

> If you have multiple alphabets you can specify what alphabet to use when converting an int or long to a Sqid.
```c#
    var sqid = Sqid.FromInt(42, "a2");
    var id = sqid.ToInt();
```

#### Extensions
```c#
    var sqid = (int)1.ToSqid();
    var sqid = (int)1.ToSqid("a2");
    var sqid = (int?)1.ToSqid();
    var sqid = (int?)1.ToSqid("a2");

    var sqid = (long)1.ToSqid();
    var sqid = (long)1.ToSqid("a2");
    var sqid = (long?)1.ToSqid();
    var sqid = (long?)1.ToSqid("a2");

    var sqid = "0xR4reL0zL3Xgq8".ToSqid();
```

### Usage in a class
```c#
    public class Dto 
    {
        public Sqid Id { get; set; }
    }

    // Serializes to { Id: "somehash" }
```


### Example when used with the laget.Mapper nuget package
```c#
    public class ModelMapper : IMapper
    {
        [MapperMethod]
        public Dto ModelToDto(Model model) => new() { Id = model.Id.ToSqid() };

        [MapperMethod]
        public Model DtoToModel(Dto dto) => new() { Id = dto.Id.ToInt() };
    }
```