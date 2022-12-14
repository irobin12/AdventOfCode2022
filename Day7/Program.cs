using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022
{
   public class Node
   {
      public readonly string name;
      public int size;
      public readonly List<Node> children;

      public Node(string name, List<Node> children = null, int size = 0)
      {
         this.name = name;
         this.children = children;
         this.size = size;
      }
   }
   
   internal class Program
   {
      public static void Main()
      {
         // Edit Configurations > Working directory to find the files path

         //string[] input = File.ReadAllLines("ExampleInput.txt");
         string[] input = File.ReadAllLines("Input.txt");
         
         //string input = File.ReadAllText("ExampleInput.txt");
         //string input = File.ReadAllText("Input.txt");
         
         int totalSizeOfSmallishDirectories = 0;
         Node root = new Node("/", new List<Node>());
         
         Stack<Node> hierarchy = new Stack<Node>();
         hierarchy.Push(root);

         void GetDirectorySizeAndPop()
         {
            var node = hierarchy.Pop();
            foreach (Node child in node.children)
            {
               node.size += child.size;
            }

            if (node.size < 100000) 
               totalSizeOfSmallishDirectories += node.size;
         }

         for (int i = 2; i < input.Length; i++)
         {
            string line = input[i];
            
            switch (line)
            {
               case "$ ls":
                  continue;
               case "$ cd ..":
                  GetDirectorySizeAndPop();
                  break;
               default:
               {
                  if (Regex.IsMatch(line, "\\$ cd [A-Za-z]"))
                  {
                     hierarchy.Push(hierarchy.Peek().children.Find(c => c.name == line.Substring(5)));
                  }
                  else
                  {
                     Node child;
                     if (line[0] == 'd')
                     {
                        child = new Node(line.Substring(4), new List<Node>());
                     }
                     else
                     {
                        int lastDigitIndex = Array.FindLastIndex(line.ToCharArray(), char.IsDigit);
                        child = new Node(line.Substring(lastDigitIndex + 2), size:Int32.Parse(line.Substring(0, lastDigitIndex + 1)));
                     }
                     hierarchy.Peek().children.Add(child);
                  }
                  break;
               }
            }
         }

         while (hierarchy.Count > 0)
         {
            GetDirectorySizeAndPop();
         }

         Console.WriteLine(totalSizeOfSmallishDirectories);
         
         // Part 2 below

         int spaceNeeded = 30000000 - (70000000 - root.size);
         List<int> directorySizesBigEnough = new List<int>();
         FindDirectoriesBigEnough(root);

         void FindDirectoriesBigEnough(Node node)
         {
            if(node.size >= spaceNeeded) 
               directorySizesBigEnough.Add(node.size);

            foreach (Node child in node.children.Where(c => c.children != null))
            {
               FindDirectoriesBigEnough(child);
            }
         }

         Console.WriteLine(directorySizesBigEnough.Min());
      }
   }
}
