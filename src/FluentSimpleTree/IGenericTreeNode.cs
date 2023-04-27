using System;
using System.Collections.Generic;

namespace SyntaxChecked.FluentSimpleTree
{
  /// <summary>Tree node interface that holds data of generic type and performs handy operations.</summary>
  /// <typeparam name="T">The type passed to IGenericTreeNode.</typeparam>
  public interface IGenericTreeNode<T>
  {
    #region Data Members
    /// <value>Property <c>Id</c> returns the node unique identifier, which is optional.</value>
    string? Id { get; }

    /// <value>Property <c>Data</c> holds the node data of generic type.</value>
    T Data { get; set; }

    /// <value>Property <c>Parent</c> returns the parent node.</value>
    /// <exception>Thrown when the node is the root node.</exception>
    IGenericTreeNode<T> Parent { get; }

    /// <value>Property <c>PrecedingSibling</c> returns the preceding sibling node.</value>
    /// <exception>Thrown when the node is the first child.</exception>
    IGenericTreeNode<T> PrecedingSibling { get; }

    /// <value>Property <c>NextSibling</c> returns the next sibling node.</value>
    /// <exception>Thrown when the node is the last child.</exception>
    IGenericTreeNode<T> NextSibling { get; }

    /// <value>Property <c>Level</c> returns the node level. Root node has level = 0.</value>
    ushort Level { get; }

    /// <value>Property <c>IsRootNode</c> returns true if the node is the root node; otherwise, false.</value>
    bool IsRootNode { get; }

    /// <value>Property <c>Index</c> returns the position index of the node among its parent's child nodes.</value>
    ushort Index { get; }

    #endregion

    /// <summary>Adds each node of type <typeparamref name="T"/> from <paramref name="nodesData"/> as children of the current node instance.</summary>
    /// <param name="nodesData">The array of generic nodes to add.</param>
    /// <returns>An array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; that contains nodes that were added.</returns>
    IGenericTreeNode<T>[] AddChildren(T[] nodesData);

    /// <summary>Adds each node in <paramref name="nodesData"/> as children of the current node instance.</summary>
    /// <param name="nodesData">An array of tuples (string nodeId, T data) to add.</param>
    /// <returns>An array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; that contains nodes that were added.</returns>
    IGenericTreeNode<T>[] AddChildren((string nodeId, T data)[] nodesData);

    /// <summary>
    /// Inserts each node of type <typeparamref name="T"/> from <paramref name="nodesData"/> as children of the current node instance at the
    /// specified position by <paramref name="position"/>
    /// </summary>
    /// <param name="position">The index of .</param>
    /// <param name="nodesData">The array of generic nodes to insert.</param>
    /// <returns>An array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; that contains nodes that were inserted.</returns>
    IGenericTreeNode<T>[] InsertChildren(ushort position, (string nodeId, T data)[] nodesData);

    /// <summary>
    /// Inserts each node of type <typeparamref name="T"/> from <paramref name="nodesData"/> as children of the current node instance at the
    /// specified position by <paramref name="position"/>
    /// </summary>
    /// <param name="position">The index of .</param>
    /// <param name="nodesData">An array of tuples (string nodeId, T data) to add.</param>
    /// <returns>An array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; that contains nodes that were inserted.</returns>
    IGenericTreeNode<T>[] InsertChildren(ushort position, T[] nodesData);

    /// <summary>Determines whether there exists the child node by <paramref name="index"/> informed.</summary>
    /// <param name="index">An index number.</param>
    /// <returns>true if the current node instance has the child searched by the <paramref name="index"/>; otherwise, false.</returns>
    bool HasChild(uint index);

    /// <summary>Determines whether there exists the child node by <paramref name="nodeId"/> informed.</summary>
    /// <param name="nodeId">An unique identifier.</param>
    /// <returns>true if the current node instance has the child with the <paramref name="nodeId"/>; otherwise, false.</returns>
    bool HasChild(string nodeId);

    /// <summary>Determines whether there exists the descendant node by <paramref name="nodeId"/> informed.</summary>
    /// <param name="nodeId">An unique identifier.</param>
    /// <returns>true if the current node instance has the descendant node; otherwise, false.</returns>
    bool HasDescendant(string nodeId);

    /// <summary>Determines whether there exists a preceding sibling node.</summary>
    /// <returns>true if the current node instance has a preceding sibling one; otherwise, false.</returns>
    bool HasPrecedingSibling();

    /// <summary>Determines whether there exists a next sibling node.</summary>
    /// <returns>true if the current node instance has a next sibling one; otherwise, false.</returns>
    bool HasNextSibling();

    /// <summary>Returns the child node by <paramref name="index"/> informed.</summary>
    /// <param name="index">An index number.</param>
    /// <exception>Thrown when the child node cannot be found by the given index.</exception>
    IGenericTreeNode<T> GetChild(uint index);

    /// <summary>Returns the child node by <paramref name="nodeId"/> informed.</summary>
    /// <param name="nodeId">An unique identifier.</param>
    /// <exception>Thrown when the child node cannot be found by the given id.</exception>
    IGenericTreeNode<T> GetChild(string nodeId);

    /// <summary>
    /// Returns an array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; according to <paramref name="searchCriteria"/>.
    /// </summary>
    /// <remarks>If no node is found according to <paramref name="searchCriteria"/>, an empty array is returned.</remarks>
    /// <param name="searchCriteria">A custom search criteria based on a predicate according to the data type of the tree nodes.</param>
    IGenericTreeNode<T>[] GetChildren(Func<T, bool> searchCriteria);

    /// <summary>
    /// Returns an array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; containing all the children of the current node instance.
    /// </summary>
    IGenericTreeNode<T>[] GetAllChildren();

    /// <summary>Returns the descendant node by <paramref name="nodeId"/> informed.</summary>
    /// <param name="nodeId">An unique identifier.</param>
    /// <exception>Thrown when the descendant node is not found by <paramref name="nodeId"/>.</exception>
    IGenericTreeNode<T> GetDescendant(string nodeId);

    /// <summary>
    /// Returns an array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; according to <paramref name="searchCriteria"/>.
    /// </summary>
    /// <remarks>If no node is found according to <paramref name="searchCriteria"/>, an empty array is returned.</remarks>
    /// <param name="searchCriteria">A custom search criteria based on a predicate according to the data type of the tree nodes.</param>
    IGenericTreeNode<T>[] GetDescendants(Func<T, bool> searchCriteria);

    /// <summary>
    /// Returns an array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; containing all the descendants of the current node instance.
    /// </summary>
    IGenericTreeNode<T>[] GetAllDescendants();

    /// <summary>
    /// Removes the current node instance from the tree and returns it.
    /// </summary>
    IGenericTreeNode<T> Remove();

    /// <summary>Removes the child node by <paramref name="index"/> from the current instance node.</summary>
    /// <param name="index">An index number.</param>
    /// <returns>The IGenericTreeNode&lt;<typeparamref name="T"/>&gt; that was removed.</returns>
    /// <exception>Thrown when the child node cannot be found by the given index.</exception>
    IGenericTreeNode<T> RemoveChild(uint index);

    /// <summary>Removes the child node by <paramref name="nodeId"/> from the current instance node.</summary>
    /// <param name="nodeId">An unique identifier.</param>
    /// <returns>The IGenericTreeNode&lt;<typeparamref name="T"/>&gt; that was removed.</returns>
    /// <exception>Thrown when the child node cannot be found by the given id.</exception>
    IGenericTreeNode<T> RemoveChild(string nodeId);

    /// <summary>Removes the children nodes according to <paramref name="searchCriteria"/> from the current instance node.</summary>
    /// <param name="searchCriteria">A custom search criteria based on a predicate according to the data type of the tree nodes.</param>
    /// <returns>An array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; that contains child nodes that were removed.</returns>
    IGenericTreeNode<T>[] RemoveChildren(Func<T, bool> searchCriteria);

    /// <summary>
    /// Removes and returns an array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; containing all the children nodes removed from the current node instance.
    /// </summary>
    IGenericTreeNode<T>[] RemoveAllChildren();

    /// <summary>Removes the descendant node by <paramref name="nodeId"/> from the current instance node.</summary>
    /// <param name="nodeId">An unique identifier.</param>
    /// <returns>The IGenericTreeNode&lt;<typeparamref name="T"/>&gt; that was removed.</returns>
    /// <exception>Thrown when the descendant node cannot be found by the given id.</exception>
    IGenericTreeNode<T> RemoveDescendant(string nodeId);

    /// <summary>Removes the descendants nodes according to <paramref name="searchCriteria"/> from the current instance node.</summary>
    /// <param name="searchCriteria">A custom search criteria based on a predicate according to the data type of the tree nodes.</param>
    /// <returns>An array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; that contains descendant nodes that were removed.</returns>
    IGenericTreeNode<T>[] RemoveDescendants(Func<T, bool> searchCriteria);

    /// <summary>
    /// Removes and returns an array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; containing all the descendant nodes removed from the current node instance.
    /// </summary>
    IGenericTreeNode<T>[] RemoveAllDescendants();

    /// <summary>Appends a collection of nodes of type <typeparamref name="T"/> to the current node instance.</summary>
    /// <remarks>This method is intended to be used with a collection of previously removed nodes.</remarks>
    /// <param name="nodes">An collection of IGenericTreeNode&lt;<typeparamref name="T"/>&gt;</param>
    /// <returns>An array of IGenericTreeNode&lt;<typeparamref name="T"/>&gt; that contains nodes that were not appended.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    IGenericTreeNode<T>[] AppendNodes(IEnumerable<IGenericTreeNode<T>> nodes);
  }
}