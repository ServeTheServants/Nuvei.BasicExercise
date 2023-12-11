# Nuvei.BasicExercise
Basic Exercise for Developers – Person Collection

## Guidelines
- You may implement your solution in C#.
- Your solution should be well-designed according to OO principles.
- You should email only `PersonCollection` class.
- Your class must pass compilation without any errors (I have `IPerson` interface).
- If you have any questions you can reply to this email.

## Assumptions
The following interface is given:
```csharp
public interface IPerson
{
    int Id { get; }
    string FirstName { get; }
    string LastName { get; }
    DateTime DateOfBirth { get; }
    int Height { get; }
    // etc… there may be more, such as get property methods
    // You cannot rely on the properties in this interface, they can be changed and removed.
}
```
# Requirements

1. You are asked to implement a new class called `PersonCollection`.

2. Your implementation should support the following operations:
    a. **Add** - adds the person object which is given as input. This operation may be performed in worst-case time complexity of O(n).
    b. **Remove** - removes the person object with the maximum (explanation below) value and returns it. This operation must be performed in worst-case time complexity of O(1).
    c. **Publish** - publishes a notification to subscriber objects upon any Add/Remove.
