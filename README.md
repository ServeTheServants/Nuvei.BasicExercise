# Nuvei.BasicExercise
Basic Exercise for Developers – Person Collection

Guidelines <br>
 You may implement your solution in C#. <br>
 Your solution should be well-designed according to OO principles. <br>
 You should email only PersonCollection class. <br>
 Your class must pass compilation without any errors (I have IPerson interface). <br>
<br>
Assumptions <br>
The following interface is given:<br>
Public interface IPerson<br>
{<br>
  int Id {get;}<br>
  String FirstName {get;}<br>
  String LastName {get;}<br>
  Date DateOfBirth {get;}<br>
  int Height {get;}<br>
  // etc… there may be more, such as get property methods<br>
  //You cannot rely on the properties in this interface, they can be changed and removed.<br>
}<br>
<br>
Requirements<br>
1. You are asked to implement a new class called PersonCollection.<br>
2. Your implementation should support the following operations:<br>
a. Add - adds the person object which is given as input. This operation may be performed in WC<br>
time complexity of O(n)<br>
b. Remove - removes the person object with the maximum (explanation below) value and returns<br>
it. This operation must be performed in WC time complexity of O(1)<br>
c. Publish - publishes a notification to subscriber objects upon any Add/Remove.<br>
