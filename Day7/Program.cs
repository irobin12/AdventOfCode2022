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
      public readonly bool isDirectory;
      public int size;
      public List<Node> nodes = new List<Node>();

      public Node(string name, bool isDirectory, int size = 0)
      {
         this.name = name;
         this.isDirectory = isDirectory;
         this.size = size;
      }
   }
   
   internal class Program
   {
      private static Node root = new Node("/", true);
      private static string[] fileInput;

      public static void Main()
      {
         // Edit Configurations > Working directory to find the files path

         string[] input = File.ReadAllLines("ExampleInput.txt");
         //string[] input = File.ReadAllLines("Input.txt");
         
         //string input = File.ReadAllText("ExampleInput.txt");
         //string input = File.ReadAllText("Input.txt");

         fileInput = input;
         
         ProcessInput(2, root);
         FindAllDirectoriesSizes();
         Console.WriteLine(GetDirectoriesSum(root));
      }

      private static void ProcessInput(int startIndex, Node parentNode)
      {
         for (int i = startIndex; i < fileInput.Length; i++)
         {
            string line = fileInput[i];

            switch (line[0])
            {
               case '$':
                  goto ProcessDirectories;
               case 'd':
                  parentNode.nodes.Add(new Node(line.Substring(4), true));
                  break;
               default:
                  int lastDigitIndex = Array.FindLastIndex(line.ToCharArray(), char.IsDigit);
                  parentNode.nodes.Add(new Node(line.Substring(lastDigitIndex + 2), false, Int32.Parse(line.Substring(0, lastDigitIndex + 1))));
                  break;
            }
         }

         ProcessDirectories:
         foreach (Node node in parentNode.nodes.Where(node => node.isDirectory))
         {
            string commandLine = "$ cd " + node.name;
            int index = Array.FindIndex(fileInput, s => s == commandLine);
            ProcessInput(index + 2, node);
         }
      }

      private static void FindAllDirectoriesSizes()
      {
         CalculateDirectorySize(root);
      }
      
      private static int CalculateDirectorySize(Node parentNode)
      {
         var size = 0;
         
         foreach (var node in parentNode.nodes)
         {
            if (!node.isDirectory)
            {
               size += node.size;
            }
            else
            {
               size += CalculateDirectorySize(node);
            }
         }

         parentNode.size = size;
         return size;
      }

      private static int GetDirectoriesSum(Node parentNode)
      {
         // Count directory only if its size <= 100000
         var sum = 0;

         if (parentNode.size <= 100000)
         {
            sum += parentNode.size;
         }

         sum += parentNode.nodes.Where(node => node.isDirectory).Sum(GetDirectoriesSum);
         return sum;
      }
   }
}