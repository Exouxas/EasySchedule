# EasySchedule
Tired of setting up schedulers? This is probably the easiest alternative, made with Asp.NET in mind.

## How to use
```cs
using EasySchedule;

namespace YourNamespace;

public class ClassYouHaveMethodsIn
{
  [Schedule("0 * * * * *")] // Runs every minute at 0s
  public void MethodToSchedule(){
    Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}: It did the thing");
  }
}
```

## Planned features
- RunOnce property for the attribute
- Logging
- Optional retry configuration