using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyntaxChecked.FluentSimpleTree.Consumer
{
  public class Person
  {
    public string Name { get; set; }
    public int? Age { get; set; }

    public Person(string name)
    {
      Name = name;
    }
  }
}