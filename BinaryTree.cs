using System;

namespace BinaryTrees;

public class TreeNode<T> where T: IComparable  
{
    public T Value;
    public TreeNode<T> Left;
    public TreeNode<T> Right;
    public TreeNode(T value) { Value = value; }

}
public class BinaryTree<T>
    where T: IComparable
{
    private TreeNode<T> rootNode;
    public bool Contains(T value)
    {
        var newNode = rootNode;
        while (newNode != null)
        {
            if(value.Equals(newNode.Value)) return true;
            newNode = value.CompareTo(newNode.Value)>0?newNode.Right:newNode.Left;
        }
        return false;
    }
    public void Add(T value)
    {
        var newTree = rootNode;
        if(rootNode == null) rootNode = new TreeNode<T>(value);
        else
        {
            while (true)
            {
                if(value.CompareTo(newTree.Value)>0) {
                    if (newTree.Right != null) newTree = newTree.Right;
                    else
                    {
                        newTree.Right = new TreeNode<T>(value);
                        break;
                    }
                }
                else
                {
                    if (newTree.Left != null) newTree = newTree.Left;
                    else
                    {
                        newTree.Left = new TreeNode<T>(value);
                        break;
                    }
                }
            }
        }
    }
}