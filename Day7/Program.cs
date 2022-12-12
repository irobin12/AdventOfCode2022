using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022
{
   public class Node
   {
      public Node parentNode;
      public readonly string name;
      public readonly bool isDirectory;
      public int size;

      public Node(string name, bool isDirectory, Node parentNode, int size = 0)
      {
         this.name = name;
         this.isDirectory = isDirectory;
         this.parentNode = parentNode;
         this.size = size;
      }
   }
   
   internal class Program
   {
      //private static Node root = new Node("/", true);
      private static string[] fileInput;
      private static List<Node> nodes = new List<Node>();

      public static void Main()
      {
         // Edit Configurations > Working directory to find the files path

         string[] input = File.ReadAllLines("ExampleInput.txt");
         //string[] input = File.ReadAllLines("Input.txt");
         
         //string input = File.ReadAllText("ExampleInput.txt");
         //string input = File.ReadAllText("Input.txt");

         fileInput = input;

         nodes.Add(new Node("/", true, null));
         
         ProcessInput();
         AddSizeToParentDirectories();
         //FindAllDirectoriesSizes();
         //Console.WriteLine(GetDirectoriesSum(root));
      }

      private static void ProcessInput()
      {
         for (int i = 0; i < fileInput.Length; i++)
         {
            string line = fileInput[i];
            
            Node GetParentNode()
            {
               for (int j = i; j >= 0; j--)
               {
                  string previousLine = fileInput[j];
                  if (Regex.IsMatch(previousLine, "\\$ cd [A-Za-z/]"))
                  {
                     return nodes.Find(n => n.name == previousLine.Substring(5));
                  }
               }

               return null;
            }

            if (line.Substring(0, 3) == "dir")
            {
               nodes.Add(new Node(line.Substring(4), true, GetParentNode()));
            }
            else if (line[0] != '$')
            {
               int lastDigitIndex = Array.FindLastIndex(line.ToCharArray(), char.IsDigit);
               nodes.Add(new Node(line.Substring(lastDigitIndex + 2), false, GetParentNode(),
                  Int32.Parse(line.Substring(0, lastDigitIndex + 1))));
            }
         }
      }

      private static void AddSizeToParentDirectories()
      {
         foreach (Node node in nodes)
         {
         }
      }

      /*private static void ProcessDirectories(Node parentNode)
      {
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
      }*/
   }
}