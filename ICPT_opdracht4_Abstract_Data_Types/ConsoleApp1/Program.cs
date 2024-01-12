using ConsoleApp1;

class Program
{
    static void Main()
    {
        // Stack
        ConsoleApp1.Stack<int> stack = StackFunctions<int>.GenerateStack(3);
        
        StackFunctions<int>.Push(ref stack,1);
        StackFunctions<int>.Push(ref stack,2);
        StackFunctions<int>.Push(ref stack,3);
        
        Console.WriteLine("Current top Item: " + StackFunctions<int>.Peek(ref stack));
        
        int poppedItem = StackFunctions<int>.Pop(ref stack);
        Console.WriteLine("Popped item: " + poppedItem);
        Console.WriteLine("Current stack after pop: " + StackFunctions<int>.Peek(ref stack));

        poppedItem = StackFunctions<int>.Pop(ref stack);
        Console.WriteLine("Popped item: " + poppedItem);
        Console.WriteLine("Current stack after pop: " + StackFunctions<int>.Peek(ref stack) + "\n");
        
        
        // Queue
        ConsoleApp1.Queue<int> queue = QueueFunctions<int>.GenerateQueue(3);
        
        QueueFunctions<int>.Enqueue(ref queue,1);
        QueueFunctions<int>.Enqueue(ref queue,2);
        QueueFunctions<int>.Enqueue(ref queue,3);

        Console.WriteLine("Current top Item: " + QueueFunctions<int>.Peek(ref queue));
        
        poppedItem = QueueFunctions<int>.Dequeue(ref queue);
        Console.WriteLine("Popped item: " + poppedItem);
        Console.WriteLine("Current queue after pop: " + QueueFunctions<int>.Peek(ref queue));

        poppedItem = QueueFunctions<int>.Dequeue(ref queue);
        Console.WriteLine("Popped item: " + poppedItem);
        Console.Write("Current queue after pop: " + QueueFunctions<int>.Peek(ref queue) + "\n");
        
        
        //BST
        BinarySearchTree<int> binarySearchTree = BstFunctions<int>.GenerateTree(15);
        BstFunctions<int>.Add(ref binarySearchTree, 4);
        BstFunctions<int>.Add(ref binarySearchTree, 3);
        BstFunctions<int>.Add(ref binarySearchTree, 2);
        BstFunctions<int>.Add(ref binarySearchTree, 1);
        BstFunctions<int>.PrintTree(ref binarySearchTree);
        BstFunctions<int>.Remove(ref binarySearchTree, 3);
        BstFunctions<int>.PrintTree(ref binarySearchTree);
        Console.WriteLine("the highest value in this tree is: " + BstFunctions<int>.FindGreatestValue(ref binarySearchTree));
        Console.WriteLine("is the value 7 in the tree: "+ BstFunctions<int>.Find(ref binarySearchTree, 7));
        Console.WriteLine("is the value 4 in the tree: "+ BstFunctions<int>.Find(ref binarySearchTree, 4));
    }
}