using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022
{
   public class Node
   {
      public string name;
      public bool isDirectory;
      public int size = 0;
      public List<Node> nodes = new List<Node>();

      public Node(string name, bool isDirectory)
      {
         this.name = name;
         this.isDirectory = isDirectory;
      }
   }
   
   internal class Program
   {
      private static Node root = new Node("/", true);
      //private static List<Node> directories = new List<Node>();

      public static void Main()
      {
         // Edit Configurations > Working directory to find the files path

         string[] input = File.ReadAllLines("ExampleInput.txt");
         //string[] input = File.ReadAllLines("Input.txt");
         
         //string input = File.ReadAllText("ExampleInput.txt");
         //string input = File.ReadAllText("Input.txt");

         //directories.Add(root);
         ProcessInput(input, root);
      }

      public static void ProcessInput(string[] input, Node parentNode)
      {
         for (int i = 2; i < input.Length; i++)
         {
            string line = input[i];
            char firstChar = line[0];

            switch (firstChar)
            {
               case '$':
                  if(line.Length < 6) break;
                  var newDirectory = line.Substring(5);
                  if(newDirectory == "..") return;
                  var newParentNode = parentNode.nodes.Find(n => n.name == newDirectory);
                  ProcessInput(input.Skip(i).ToArray(), newParentNode);
                  break;
               case 'd':
                  var directoryNode = new Node(line.Substring(4), true);
                  //if(directories.Contains<Node>(n => directoryNode.name != n.name)directories.Add(directoryNode));
                  parentNode.nodes.Add(directoryNode);
                  break;
               default:
                  int lastDigitIndex = Array.FindLastIndex(line.ToCharArray(), char.IsDigit);
                  var node = new Node(line.Substring(lastDigitIndex + 2), false);
                  node.size = Int32.Parse(line.Substring(0, lastDigitIndex + 1));
                  parentNode.nodes.Add(node);
                  break;
            }
            
            /*if(firstChar == '$') break;
            if (firstChar == 'd')
            {
               var node = new Node(line.Substring(4), true);
            }
            else
            {
               //int lastDigitIndex = FindLastDigitIndex(line);
               int lastDigitIndex = Array.FindLastIndex(line.ToCharArray(), char.IsDigit);

               var node = new Node(line.Substring(lastDigitIndex + 2), false);
               node.size = Int32.Parse(line.Substring(0, lastDigitIndex + 1));
            }*/
         }
      }

      private static int FindLastDigitIndex(string line)
      {
         //int lastDigitIndex = line.LastIndexOf(c => Char.IsDigit(c));
         //int index = Array.FindLastIndex(line.ToCharArray(), char.IsDigit);

         int lastDigitIndex = -1;
         foreach (char c in line)
         {
            if (Char.IsDigit(c)) lastDigitIndex++;
            else break;
         }

         return lastDigitIndex;
      }

      public int GetTotalDirectoriesSum()
      {
         int sum = 0;

         GetTotalDirectoriesSum();
         return sum;
      }
   }
}