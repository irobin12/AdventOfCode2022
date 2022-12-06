using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022
{
   internal class Program
   {
      private class Instruction
      {
         public readonly int amountToMove;
         public readonly int origin;
         public readonly int destination;

         public Instruction(int amountToMove, int origin, int destination)
         {
            this.amountToMove = amountToMove;
            this.origin = origin;
            this.destination = destination;
         }
      }
      
      public static void Main()
      {
         // Edit Configurations > Working directory to find the files path
         
         //string[] input = File.ReadAllLines("ExampleInput.txt");
         string[] input = File.ReadAllLines("Input.txt");

         //int indexOfEmptyLine = -1;
         int indexOfEmptyLine = Array.FindIndex(input, s => s == string.Empty);
         
         /*for (int i = 0; i < input.Length; i++)
         {
            if (input[i] == String.Empty)
            {
               indexOfEmptyLine = i;
               break;
            }
         }*/

         int startingLineIndex = indexOfEmptyLine - 2;
         string startingLine = input[startingLineIndex];
         Dictionary<int, Stack<char>> stacks = new Dictionary<int, Stack<char>>();

         for (int i = 0; i < startingLine.Length; i++)
         {
            char item = startingLine[i];
            if (item == '[' || item == ']' || item == ' ') continue;

            Stack<char> stack = new Stack<char>();
            stack.Push(item);
            for (int index = startingLineIndex - 1; index >= 0; index--)
            {
               char lineItem = input[index][i];
               if (lineItem == ' ') break;
               stack.Push(lineItem);
            }

            stacks.Add(stacks.Count + 1, stack);
         }

         // Parse instructions
         Queue<Instruction> instructions = new Queue<Instruction>();

         for (int i = indexOfEmptyLine + 1; i < input.Length; i++)
         {
            var digits = Regex.Split(input[i], "\\D+").Where(s => s != String.Empty).ToArray();
            instructions.Enqueue(new Instruction(int.Parse(digits[0]), int.Parse(digits[1]), int.Parse(digits[2])));
         }

         // Execute instructions
         while (instructions.Count > 0)
         {
            var instruction = instructions.Dequeue();

            for (int i = 0; i < instruction.amountToMove; i++)
            {
               stacks[instruction.destination].Push(stacks[instruction.origin].Pop());
            }
         }

         // Get Part 1 Answer
         foreach (KeyValuePair<int,Stack<char>> keyValuePair in stacks)
         {
            Console.Write(keyValuePair.Value.Peek());
         }
      }
   }
}