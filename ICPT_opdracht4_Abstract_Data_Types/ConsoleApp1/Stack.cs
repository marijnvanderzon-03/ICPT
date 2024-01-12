namespace ConsoleApp1;

using System;
using System.Collections.Generic;

public struct Stack<T>
{
    public List<T> Items;
    
}

public static class StackFunctions<T>
{

    public static Stack<T> GenerateStack(int capacity)
    {
        Stack<T> stack = new Stack<T>();
        stack.Items = new List<T>(capacity);
        return stack;
    }
    public static void Push(ref Stack<T> stack,T item)
    {
        stack.Items.Add(item);
    }

    public static T Pop(ref Stack<T> stack)
    {
        if (IsEmpty(stack))
        {
            throw new InvalidOperationException("stack is empty, so can't remove object from stack");
        }

        int lastIndex = stack.Items.Count - 1;
        T poppedItem = stack.Items[lastIndex];
        stack.Items.RemoveAt(lastIndex);
        return poppedItem;
    }

    public static T Peek(ref Stack<T> stack)
    {
        if (IsEmpty(stack))
        {
            throw new InvalidOperationException("can't see item in the stack if it is empty");
        }

        return stack.Items[stack.Items.Count - 1];
    }

    public static bool IsEmpty(Stack<T> stack)
    {
        return stack.Items.Count == 0;
    }
}