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
         public int amountToMove;
         public int origin;
         public int destination;

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
         
         string[] input = File.ReadAllLines("ExampleInput.txt");
         //string[] input = File.ReadAllLines("Input.txt");

         /*List<string> rowsOfCrates = new List<string>();
         
         foreach (string line in input)
         {
            if(Regex.IsMatch(line, "\\s\\d")) break;

            string newLine = Regex.Replace(line, "\\[|\\]|\\s", "");
            rowsOfCrates.Add(newLine);
            
            Console.WriteLine(newLine);
         }

         int numberOfStacks = rowsOfCrates.OrderByDescending(r => r.Length).First().Length;*/

         int indexOfEmptyLine = -1;
         
         for (int i = 0; i < input.Length; i++)
         {
            if (input[i] == String.Empty)
            {
               indexOfEmptyLine = i;
               break;
            }
         }

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

         Queue<Instruction> instructions = new Queue<Instruction>();

         for (int i = indexOfEmptyLine + 1; i < input.Length - 1; i++)
         {
            var digits = Regex.Split(input[i], "\\D+").Where(s => s != String.Empty).ToArray();
            instructions.Enqueue(new Instruction(int.Parse(digits[0]), int.Parse(digits[1]), int.Parse(digits[2])));
         }

         var t = 0;
         /*string stacksNumbers = input[indexOfEmptyLine - 1];
         int numberOfStacks = stacksNumbers[stacksNumbers.Length - 2];
         
         char[,] cargo = new char[indexOfEmptyLine - 2, numberOfStacks];*/

         /*List<Stack<char>> stacks = new List<Stack<char>>();
         
         for (int index = startingLine; index >= 0; index--)
         {
            string line = input[index];
            int currentStack = 0;
            
            for (int i = 0; i < line.Length - 1; i++)
            {
               char item = line[i];
               if (item == ' ' || item == '[' || item == ']') continue;
               if (index == startingLine) 
               {
                  //First line from the bottom will always be at max capacity with crates.
                  //Therefore it's safe to set up the stacks only from here.
                  stacks.Add(new Stack<char>());
               }
               
               stacks[currentStack].Push(item);
               currentStack++;
            }
         }

         Console.WriteLine("dic of size "+stacks.Count);

         for (int index = 0; index < stacks.Count; index++)
         {
            var stack = stacks[index];
            foreach (var VARIABLE in stack)
            {
               Console.WriteLine("in column " + index + " there is " + VARIABLE);
            }
         }*/
      }
   }
}