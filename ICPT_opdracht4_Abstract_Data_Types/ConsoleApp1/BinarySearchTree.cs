namespace ConsoleApp1;

public struct BinarySearchTree<T> where T : IComparable<T>
{
    public (bool exists, T value)[] Tree;
}

public static class BstFunctions<T> where T : IComparable<T>
{
    public static BinarySearchTree<T> GenerateTree(int amountOfElementsInTree)
    {
        BinarySearchTree<T> tree = new BinarySearchTree<T>();
        tree.Tree = new (bool exists, T value)[amountOfElementsInTree];
        for (int i = 0; i < tree.Tree.Length; i++)
        {
            tree.Tree[i] = (false, default);
        }
        return tree;
    }
    
    public static void Add(ref BinarySearchTree<T> tree, T value, int root = 0)
    {
        if (tree.Tree[root].exists == false)
        {
            tree.Tree[root].exists = true;
            tree.Tree[root].value = value;
        }
        else if (tree.Tree[root].value.CompareTo(value) < 0)
        {
            Add(ref tree, value, root * 2 + 2);
        }
        else if (tree.Tree[root].value.CompareTo(value) > 0)
        {
            Add(ref tree, value, root * 2 + 1);
        }
    }

    private static (bool, T) GetNextBiggerValue(ref BinarySearchTree<T> tree, T value)
    {
        List<T> values = new List<T>();
        for (int i = 0; i < tree.Tree.Length; i++)
        {
            if (tree.Tree[i].exists)
            {
                    values.Add(tree.Tree[i].value);
            }
        }
        values.Sort();
        int index = values.IndexOf(value);
        if (index == -1)
        {
            return (false, default);
        }
        return (true, values[index + 1]);
    }
    
    private static T GetNextSmallerValue(ref BinarySearchTree<T> tree, T value)
    {
        List<T> values = new List<T>();
        for (int i = 0; i < tree.Tree.Length; i++)
        {
            if (tree.Tree[i].exists)
            {
                values.Add(tree.Tree[i].value);
            }
        }
        values.Sort();
        int index = values.IndexOf(value);
        return values[index - 1];
    }

    private static int FindIndex(ref BinarySearchTree<T> tree, T value, int root)
    {
        if (root >= 0 && root < tree.Tree.Length && tree.Tree[root].exists)
        {
            int compareResult = value.CompareTo(tree.Tree[root].value);

            if (compareResult == 0)
            {
                return root;
            }
            else if (compareResult < 0)
            {
                return FindIndex(ref tree,  value, 2 * root + 1);
            }
            else
            {
                return FindIndex(ref tree,  value, 2 * root + 2);
            }
        }
        return -1;
    }

    private static bool NodeHasNoChildren(ref BinarySearchTree<T> tree, int root)
    {
        if ((root*2+1 >= tree.Tree.Length ||!tree.Tree[root * 2 + 1].exists) && (root*2+2 >= tree.Tree.Length || !tree.Tree[root * 2 + 2].exists)) 
        {
            return true;
        }
        return false;
    }

    private static bool NodeHasLeftChild(ref BinarySearchTree<T> tree, int root)
    {
        if (root * 2 + 1 <= tree.Tree.Length && tree.Tree[root * 2 + 1].exists)
        {
            return true;
        }

        return false;
    }
    
    private static bool NodeHasRightChild(ref BinarySearchTree<T> tree, int root)
    {
        if (root*2+2 <= tree.Tree.Length || tree.Tree[root * 2 + 2].exists)
        {
            return true;
        }

        return false;
    }

    private static bool NodeHasBothChildren(ref BinarySearchTree<T> tree, int root)
    {
        if (NodeHasLeftChild(ref tree, root) && NodeHasRightChild(ref tree, root))
        {
            return true;
        }

        return false;
    }


    public static void Remove(ref BinarySearchTree<T> tree, T value, int root = 0)
    {
        if (tree.Tree[root].value.CompareTo(value) == 0)
        {
            if (NodeHasNoChildren(ref tree, root))
            {
                tree.Tree[root].value = default;
                tree.Tree[root].exists = false;
            }
            else if (NodeHasBothChildren(ref tree, root))
            {
                T nextValue;
                (bool succes, T value) nextBigger = GetNextBiggerValue(ref tree, tree.Tree[root].value);
                if (nextBigger.succes)
                {
                    nextValue = nextBigger.value;
                }
                else
                {
                    nextValue = GetNextSmallerValue(ref tree, tree.Tree[root].value);
                }
                int indexNextValue = FindIndex(ref tree, nextValue, root);
                tree.Tree[root].value = nextValue;
                tree.Tree[indexNextValue].value = value;
                Remove(ref tree, value, indexNextValue);
            }
            else if (NodeHasLeftChild(ref tree, root))
            {
                T rootValue = tree.Tree[root].value;
                if (NodeHasNoChildren(ref tree, root * 2 + 1) && rootValue.CompareTo(tree.Tree[root * 2 + 1].value) < 0)
                {
                    tree.Tree[root-1].value = tree.Tree[root * 2 + 1].value;
                    tree.Tree[root-1].exists = true;
                    tree.Tree[root].value = default;
                    tree.Tree[root].exists = false;
                }
                else
                {
                    tree.Tree[root].value = tree.Tree[root * 2 + 1].value;
                }
                tree.Tree[root * 2 + 1].value = rootValue;
                Remove(ref tree, value, root*2+1);
            }
            else if (NodeHasRightChild(ref tree, root))
            {
                T rootValue = tree.Tree[root].value;
                if (NodeHasNoChildren(ref tree, root * 2 + 1) && rootValue.CompareTo(tree.Tree[root * 2 + 2].value) > 0)
                {
                    tree.Tree[root-2].value = tree.Tree[root * 2 + 2].value;
                    tree.Tree[root-2].exists = true;
                }
                else
                {
                    tree.Tree[root].value = tree.Tree[root * 2 + 2].value;
                }
                tree.Tree[root * 2 + 2].value = rootValue;
                Remove(ref tree, value, root*2+2);
            }
        } else if (tree.Tree[root].value.CompareTo(value) > 0)
        {
            Remove(ref tree, value, root * 2 + 1);
        }
        else
        {
            Remove(ref tree, value, root * 2 + 2);
        }
    }
    
    public static Boolean Find(ref BinarySearchTree<T> tree, T value)
    {
        for (int i = 0; i < tree.Tree.Length; i++)
        {
            if (tree.Tree[i].exists && tree.Tree[i].value.CompareTo(value) == 0)
            {
                return true;
            }
        }
        return false;
    }

    public static T FindGreatestValue(ref BinarySearchTree<T> tree)
    {
        T highestValue = tree.Tree[0].value;
        for (int i = 0; i < tree.Tree.Length; i++)
        {
            if (tree.Tree[i].exists && tree.Tree[i].value.CompareTo(highestValue) > 0)
            {
                highestValue = tree.Tree[i].value;
            }
        }
        return highestValue;
    }
    
    public static void PrintTree(ref BinarySearchTree<T> tree)
    {
        int currentLevel = 0;
        int elementsInCurrentLevel = 1;

        for (int i = 0; i < tree.Tree.Length; i++)
        {
            if (i == elementsInCurrentLevel - 1)
            {
                Console.WriteLine();
                currentLevel++;
                elementsInCurrentLevel = (int)Math.Pow(2, currentLevel);
            }

            if (tree.Tree[i].exists)
                Console.Write(tree.Tree[i].value);
            else
                Console.Write("-");
            Console.Write(" ");
        }
        Console.WriteLine();
    }
}