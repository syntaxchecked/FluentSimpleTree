namespace SyntaxChecked.FluentSimpleTree.Consumer
{
  class Program
  {
    static void Main()
    {
      #region section code 1
      /**
      var tree2 = new Tree<Person>();

      var person1 = new Person
      {
        Name = "Carlos"
      };

      var person2 = new Person
      {
        Name = "Bruna"
      };

      var tree = new Tree<Person>(person1);

      //tree.RootNode.AddChildren(new[] { person1, person2 });
      tree.RootNode.AddChildren(new[] { ("a", person1) });

      var x = tree.RootNode.GetDescendants(p => p.Name == "Carlo");
      //var x = tree.RootNode.GetDescendant(p => p.Name == "Carlo");

      var nodes = tree.GetNodes(person => person.Name == "Carlos");

      var node1 = tree.GetNodeById("a");

      var parent = node1.Parent;

      System.Console.WriteLine($"{nodes.Length}");
      */
      #endregion

      #region section code 2
      /*
      var c = a.GetChild(1);
      var b = c.PreviousSibling;
      var d = c.NextSibling;

      c.AddChildren(new[] { ("node.e", "e") });
      c.AddChildren(new[] { ("node.f", "f") });
      d.AddChildren(new[] { ("node.g", "g") });

      var e = c.GetChild("node.e");
      var f = c.GetDescendant("node.f");
      var g = tree.GetNodeByGuid("node.g");

      var p1 = tree.HasNode("node.e");
      var p2 = tree.HasNode(_ => _.Equals("a"));
      var p3 = a.HasDescendant(_ => _.Equals("node.g"));

      Console.WriteLine($"{p1} {p2} {p3}");

      //d.GetDescendant(x => x.Equals("g"));

      System.Console.WriteLine($"{a.Guid}, {a.Data}");
      System.Console.WriteLine($"{b.Guid}, {b.Data}");
      System.Console.WriteLine($"{c.Guid}, {c.Data}");
      System.Console.WriteLine($"{d.Guid}, {d.Data}");
      System.Console.WriteLine($"{e.Guid}, {e.Data}");
      System.Console.WriteLine($"{f.Guid}, {f.Data}");
      System.Console.WriteLine($"{g.Guid}, {g.Data}");
      */
      #endregion

      #region section code 3
      /*
      //var a = rootNode;
      a.AddChildren(new[] { "b", "c", "d" });
      var b = a.GetChild(0);
      var c = b.NextSibling;
      c.AddChildren(new[] { "e", "f" });
      var e = c.GetChildren(_ => _.Equals("e")).ToList()[0];
      e.AddChildren(new[] { ("node.h", "h"), ("node.i", "i") });
      var f = c.GetChild(1);
      var d = c.NextSibling;
      var h = e.GetChild("node.i").PreviousSibling;

      e.RemoveChild("node.h");

      e.AddChildren(new[] { ("e1", "a"),  //0
                            ("e2", "b"),  //1
                            ("e3", "b"),  //2
                            ("e4", "a"),  //3
      });

      e.RemoveChildren(_ => _.Equals("b"));
      */
      #endregion

      #region section code 4
      /*
      var tree = new Tree<string>("value.a");

      var root = tree.RootNode;

      //root.Data = "banana";

      System.Console.WriteLine(root.Data);

      if (tree.HasNode("root"))
        System.Console.WriteLine("True");

      root
        .AddChildren(new[] { "b", "c", "d" })[1]
          .AddChildren(new[] { "e", "f" })[0]
            .AddChildren(new[] { ("node.h", "value.h"), ("node.i", "value.i") })[0]
          .Parent //e
          .Parent //c
          .GetChild(1) //f
            .AddChildren(new[] { "j" });

      var j = root.GetDescendants(_ => _ == "j")[0];

      j.AddChildren(new[] { "r", "s" })[1]
        .AddChildren(new[] { "t" })[0]
        .AddChildren(new[] { "value.z", "value.k" });

      //var x = tree.GetNodes(_ => _.Contains("value."));
      //var r = j.GetDescendants(_ => _ == "r");

      var e = root.GetDescendants(_ => _ == "e")[0];

      var h = e.RemoveChild(0);

      root.AppendNodes(new[] { h });

      System.Console.WriteLine(j.GetChildren(_ => true).Length);

      //root.RemoveDescendants(_ => _.Contains("value."));
      */
      #endregion

      #region section code 5
      /*
      var p1 = new Person("Lucas") { Age = 40 };
      var p2 = new Person("Mary") { Age = 25 };
      var p3 = new Person("Jason") { Age = 36 };
      var p4 = new Person("Peter") { Age = 14 };
      var p5 = new Person("Fred") { Age = 51 };
      var p6 = new Person("Jane") { Age = 10 };
      var p7 = new Person("Sean") { Age = 22 };
      var p8 = new Person("Jessica") { Age = 31 };
      var p9 = new Person("Hannah") { Age = 60 };
      var p10 = new Person("Joseph") { Age = 42 };
      var p11 = new Person("John") { Age = 25 };
      var p12 = new Person("Laura") { Age = 53 };

      var myTree = new Tree<Person>(p1);

      var root = myTree.RootNode;

      root
        .AddChildren(new Person[] { p2, p3, p4 })[0] //Mary
          .AddChildren(new Person[] { p5, p6 })[0] //Fred
        .Parent //Mary
        .NextSibling //Jason
          .AddChildren(new Person[] { p7, p8, p9 })[1] //Jessica
            .AddChildren(new Person[] { p10, p11, p12 });
      */
      #endregion
    }
  }
}
