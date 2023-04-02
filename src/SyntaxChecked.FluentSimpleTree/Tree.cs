using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SyntaxChecked.FluentSimpleTree
{
  /**
  <summary>A generic n-ary tree class.</summary>
  <typeparam name="T">The type stored by the tree nodes.</typeparam>
  */
  public class Tree<T>
  {
    #region Data Members
    private readonly Dictionary<string, TreeNode> _nodesWithIds = new Dictionary<string, TreeNode>();

    /// <value>Property <c>RootNode</c> returns the tree root node. It is assigned on class Tree instantiation.</value>
    public IGenericTreeNode<T> RootNode { get; private set; }

    /// <value>Property <c>Height</c> returns the tree height.</value>
    public ushort Height { get; private set; }

    #endregion //Data Members

    #region Methods
    /**
    <summary>
      Initializes the new tree with one root node containing the value passed to
      <paramref name="rootNodeData"/> and whose Id is "root".
    </summary>
    <param name="rootNodeData">The data stored in the node.</param>
    */
    public Tree(T rootNodeData) => RootNode = new TreeNode("root", rootNodeData, null, this);

    /// <summary>Initializes the new tree with one root node whose Id is "root".</summary>
    public Tree() => RootNode = new TreeNode("root", null, this);

    /**
    <summary>
      Determines whether the tree contains one node with <paramref name="nodeId"/> informed.
    </summary>
    <param name="nodeId">The node unique identifier.</param>
    <returns>true if the tree contains one node with the given id; otherwise, false.</returns>
    <example>
      Example:
      <code>
        var myTree = new Tree&lt;int&gt;(1);
        if (myTree.HasNode("root")) //always returns true in this case
            System.Console.WriteLine("True");
      </code>
    </example>
    */
    public bool HasNodeById(string nodeId)
    {
      TreeNode.ValidateNodeId(nodeId);
      return _nodesWithIds.Any(item => item.Key == nodeId);
    }

    /**
    <summary>Returns the node by <paramref name="nodeId"/> informed.</summary>
    <param name="nodeId">The node unique identifier.</param>
    <exception>Thrown when the node cannot be found by the given id.</exception>
    <example>
      Example:
      <code>
        var myTree = new Tree&lt;int&gt;(1);
        var root = myTree.GetNode("root");
        System.Console.WriteLine(root.Data);
      </code>
    </example>
    */
    public IGenericTreeNode<T> GetNodeById(string nodeId)
    {
      try
      {
        TreeNode.ValidateNodeId(nodeId);
        return _nodesWithIds.First(item => item.Key == nodeId).Value;
      }
      catch
      {
        throw new Exception("The node cannot be found by id.");
      }
    }

    /**
    <summary>Returns an array of nodes according to <paramref name="searchCriteria"/> informed.</summary>
    <remarks>if no node is found by the search criteria, returns an empty array.</remarks>
    <param name="searchCriteria">A custom search criteria to find nodes based on a predicate according to data stored in the tree nodes.</param>
    <example>
      Example:
      <code>
        var person1 = new Person("Carlos");
        var person2 = new Person("Bruna");

        var tree = new Tree&lt;Person&gt;();

        tree.RootNode.AddChildren(new[] { person1, person2 });

        var nodes = tree.GetNodes(person => person.Name == "Carlos");

        System.Console.WriteLine($"{nodes.Length}"); //Output: 1
      </code>
    </example>
    */
    public IGenericTreeNode<T>[] GetNodes(Func<T, bool> searchCriteria)
    {
      var nodes = new List<IGenericTreeNode<T>>();

      if (RootNode.Data != null && searchCriteria(RootNode.Data))
        nodes.Add(RootNode);

      nodes.AddRange(RootNode.GetDescendants(searchCriteria));

      return nodes.ToArray();
    }

    /**
    <summary>
      Returns an array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; containing all the tree nodes.
    </summary>
    */
    public IGenericTreeNode<T>[] GetAllNodes() => GetNodes(_ => true);

    #endregion //Methods

    private class TreeNode : IGenericTreeNode<T>
    {
      #region Data Members

      //Private
      private readonly List<TreeNode> _children;
      private readonly Tree<T> _sourceTree;
      private TreeNode? _parent;
      private string? _id;
      private T _data;
      private ushort _level;

      //Public
      public string? Id
      {
        get => _id;
        private set
        {
          if (value != null)
            ValidateNodeId(value);

          _id = value;
        }
      }

      public T Data
      {
        get => _data;
        set => _data = value;
      }

      public IGenericTreeNode<T> Parent
      {
        get => _parent ?? throw new Exception("Root node does not have parent.");
        private set => _parent = (TreeNode?)value;
      }

      public IGenericTreeNode<T> PrecedingSibling
      {
        get
        {
          if (!HasPrecedingSibling())
            throw new Exception("This node does not have previous siblings.");

          var leftSibling = _parent!._children[_parent._children.IndexOf(this) - 1];
          return leftSibling;
        }
      }

      public IGenericTreeNode<T> NextSibling
      {
        get
        {
          if (!HasNextSibling())
            throw new Exception("This node does not have next siblings.");

          var righttSibling = _parent!._children[_parent._children.IndexOf(this) + 1];
          return righttSibling;
        }
      }

      public ushort Level => _level;

      public bool IsRootNode => _parent == null;

      #endregion //Data Members

      #region Methods

      //Public
      public TreeNode(string? id, TreeNode? parent, Tree<T> sourceTree) : this(id, default!, parent!, sourceTree) { }

      public TreeNode(string? id, T data, TreeNode? parent, Tree<T> sourceTree)
      {
        _sourceTree = sourceTree;

        if (id != null)
        {
          if (_sourceTree._nodesWithIds.ContainsKey(id))
            throw new Exception("There already exists a node having this id.");

          Id = id;
          _sourceTree._nodesWithIds.Add(id, this);
        }

        _parent = parent;
        _data = data;
        _children = new List<TreeNode>();

        _level = (ushort)(_parent == null
                          ? 0
                          : (_parent._level + 1));
      }

      public IGenericTreeNode<T>[] AddChildren((string nodeId, T data)[] nodesData) => CreateChildren(nodesData);

      public IGenericTreeNode<T>[] AddChildren(T[] data)
      {
        var nodesData = new (string id, T data)[data.Length];
        int index = 0;
        data.ToList().ForEach(item => nodesData[index++].data = item);
        return CreateChildren(nodesData);
      }

      public bool HasPrecedingSibling()
        => _parent != null && _parent._children.IndexOf(this) > 0;

      public bool HasNextSibling()
        => _parent != null && _parent._children.IndexOf(this) < _parent._children.Count - 1;

      public bool HasChild(uint index) => index >= 0 && index < _children.Count;

      public bool HasChild(string nodeId)
      {
        ValidateNodeId(nodeId);
        return _children.Any(item => item._id == nodeId);
      }

      public bool HasDescendant(string nodeId) => HasDescendant(nodeId, this);

      public IGenericTreeNode<T> GetChild(uint index)
      {
        if (!HasChild(index))
          throw new Exception($"Child node not found by index: {index}.");

        return _children[(int)index];
      }

      public IGenericTreeNode<T> GetChild(string nodeId)
      {
        try
        {
          ValidateNodeId(nodeId);
          return _children.First(item => item.Id == nodeId);
        }
        catch
        {
          throw new Exception($"Child node not found by the id: {nodeId}.");
        }
      }

      public IGenericTreeNode<T>[] GetAllChildren() => _children.Where(_ => true).ToArray();

      public IGenericTreeNode<T>[] GetChildren(Func<T, bool> searchCriteria) => _children.Where(item => searchCriteria(item._data)).ToArray();

      public IGenericTreeNode<T> GetDescendant(string nodeId) => GetDescendant(nodeId, this)!;

      public IGenericTreeNode<T>[] GetDescendants(Func<T, bool> searchCriteria)
      {
        _ = searchCriteria ?? throw new ArgumentException("Argument searchCriteria cannot be null.");

        var descendantsNodes = new List<IGenericTreeNode<T>>();

        SearchForDescendants(searchCriteria, descendantsNodes);

        return descendantsNodes.ToArray();
      }

      public IGenericTreeNode<T>[] GetAllDescendants() => GetDescendants(_ => true);

      public IGenericTreeNode<T> RemoveChild(uint index) => RemoveChild((int)index, null);

      public IGenericTreeNode<T> RemoveChild(string nodeId) => RemoveChild(-1, nodeId);

      public IGenericTreeNode<T>[] RemoveChildren(Func<T, bool> searchCriteria)
      {
        var childrenToRemove = _children.Where(item => searchCriteria(item._data));

        childrenToRemove
          .ToList()
          .ForEach(item =>
            {
              item._parent = null;
              _children.Remove(item);

              if (item._id != null)
                _sourceTree._nodesWithIds.Remove(item._id);
            }
          );

        return childrenToRemove.ToArray();
      }

      public IGenericTreeNode<T>[] RemoveAllChildren() => RemoveChildren(_ => true);

      public IGenericTreeNode<T> RemoveDescendant(string nodeId)
      {
        var descendantNode = (TreeNode)GetDescendant(nodeId);

        descendantNode._parent?.RemoveChild(nodeId);

        return descendantNode;
      }

      public IGenericTreeNode<T>[] RemoveDescendants(Func<T, bool> searchCriteria)
      {
        var descendants = GetDescendants(searchCriteria);

        var removedNodes = new List<IGenericTreeNode<T>>();

        while (descendants.Any())
        {
          var queryParentNodes = descendants
                      .Select(node => new
                      {
                        parentNode = node.Parent
                      })
                      .Distinct()
                      .ToList();

          removedNodes.AddRange(queryParentNodes[0].parentNode.GetChildren(searchCriteria));

          queryParentNodes[0].parentNode.RemoveChildren(searchCriteria);

          descendants = GetDescendants(searchCriteria);
        }

        return removedNodes.ToArray();
      }

      public IGenericTreeNode<T>[] RemoveAllDescendants() => RemoveDescendants(_ => true);

      public IGenericTreeNode<T>[] AppendNodes(IEnumerable<IGenericTreeNode<T>> nodes)
      {
        _ = nodes ?? throw new ArgumentException("Argument [nodes] cannot be null.");

        var nodesNotAppended = new List<IGenericTreeNode<T>>();

        nodes
          .ToList()
          .ForEach(item =>
            {
              var node = (TreeNode)item;

              if (node._parent != null)
              {
                nodesNotAppended.Add(item);
                return;
              }

              if (node._id != null)
              {
                if (_sourceTree._nodesWithIds.ContainsKey(node._id))
                {
                  nodesNotAppended.Add(item);
                  return;
                }

                _sourceTree._nodesWithIds.Add(node._id, (TreeNode)item);
              }

              node._parent = this;
              node._level = (ushort)(node._parent._level + 1);

              _children.Add(node);
            }
          );

        return nodesNotAppended.ToArray();
      }

      public static void ValidateNodeId(string id)
      {
        _ = id ?? throw new ArgumentException("Argument nodeId cannot be null.");

        var regex = new Regex(@"^[a-zA-Z0-9_][a-zA-Z0-9._-]*$");

        if (!regex.IsMatch(id))
          throw new FormatException("Invalid identifier.");
      }

      //Private
      private IGenericTreeNode<T>[] CreateChildren((string id, T data)[] nodesData)
      {
        TreeNode tnode;

        if (nodesData.Any() && _sourceTree.Height == _level) _sourceTree.Height++;

        nodesData.ToList().ForEach(item =>
        {
          tnode = new TreeNode(item.id, item.data, this, _sourceTree);
          _children.Add(tnode);
        });

        return _children.ToArray();
      }

      private bool HasDescendant(string nodeId, TreeNode startingNode)
      {
        try
        {
          GetDescendant(nodeId, startingNode);
          return true;
        }
        catch
        {
          return false;
        }
      }

      private IGenericTreeNode<T> GetDescendant(string nodeId, TreeNode startingNode)
      {
        TreeNode? descendantNode = null;

        if (_sourceTree.HasNodeById(nodeId))
        {
          var targetNode = (TreeNode)_sourceTree.GetNodeById(nodeId);

          if (targetNode._level > startingNode._level)
          {
            TreeNode currentNode;
            ushort currentLevel;

            for (currentNode = targetNode, currentLevel = targetNode._level; currentLevel > startingNode._level; currentLevel--)
            {
              if (currentNode!._parent == startingNode)
              {
                descendantNode = targetNode;
                break;
              }
              currentNode = currentNode._parent!;
            }
          }
        }

        if (descendantNode == null)
          throw new Exception($"Descendant node not found by id. {nodeId}");

        return descendantNode;
      }

      private void SearchForDescendants(Func<T, bool> searchCriteria, List<IGenericTreeNode<T>> descendantsFound)
      {
        var foundNodes = _children.Where(item => searchCriteria(item._data));

        if (foundNodes.Any())
          descendantsFound.AddRange(foundNodes);

        foreach (var child in _children)
        {
          if (child._children.Any())
            child.SearchForDescendants(searchCriteria, descendantsFound);
        }
      }

      private IGenericTreeNode<T> RemoveChild(int index, string? nodeId)
      {
        var childNode = nodeId != null
                        ? (TreeNode)GetChild(nodeId)
                        : (TreeNode)GetChild((uint)index);

        if (childNode._id != null)
          _sourceTree._nodesWithIds.Remove(childNode._id);

        _children.Remove(childNode);
        childNode._parent = null;

        return childNode;
      }

      #endregion //Methods
    }
  }
}