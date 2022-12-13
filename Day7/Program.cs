using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022
{
   public class Node : IComparable<Node>
   {
      public Node parentNode;
      public readonly string name;
      public readonly bool isDirectory;
      public int size;
      public int treeSize;
      public List<Node> children;
      public int indexInInput;

      public Node(string name, bool isDirectory, int indexInInput = -1, List<Node> children = null, int size = 0)
      {
         this.name = name;
         this.isDirectory = isDirectory;
         this.indexInInput = indexInInput;
         this.children = children;
         this.parentNode = parentNode;
         this.size = size;
      }

      public int CompareTo(Node other)
      {
         if (other.treeSize < treeSize)
            return 1;
         else
            return 0;
      }
   }
   
   internal class Program
   {
      //private static Node root = new Node("/", true);
      private static string[] fileInput;
      private static List<Node> nodes = new List<Node>();
      private static List<Node> directories = new List<Node>();

      public static void Main()
      {
         // Edit Configurations > Working directory to find the files path

         //string[] input = File.ReadAllLines("ExampleInput.txt");
         string[] input = File.ReadAllLines("Input.txt");
         
         //string input = File.ReadAllText("ExampleInput.txt");
         //string input = File.ReadAllText("Input.txt");

         fileInput = input;

         RegisterAllDirectories();
         PopulateAllDirectories();
         CalculateDirectoriesSizes();
      }

      private static void RegisterAllDirectories()
      {
         for (int i = 0; i < fileInput.Length; i++)
         {
            string line = fileInput[i];
            if (line.Substring(0, 3) == "$ c" && line[line.Length - 1] != '.')
            {
               string name = line.Substring(5);
               directories.Add(new Node(name, true, indexInInput:i));
            }
         }
      }

      private static void PopulateAllDirectories()
      {
         foreach (Node directory in directories)
         {
            int lastIndex = directory.indexInInput + 2;
            for (int i = directory.indexInInput + 2; i < fileInput.Length; i++)
            {
               if (fileInput[i][0] == '$')
               {
                  lastIndex = i - 1;
                  break;
               }

               if (i == fileInput.Length - 1)
               {
                  lastIndex = i;
                  break;
               }
            }

            directory.children = new List<Node>();
            for (int i = directory.indexInInput + 2; i <= lastIndex; i++)
            {
               string line = fileInput[i];

               switch (line[0])
               {
                  case 'd':
                     string directoryName = line.Substring(4);
                     Node childDirectory = directories.Find(d => d.name == directoryName); // cause recursion
                     directory.children.Add(childDirectory);
                     break;
                  default:
                     int lastDigitIndex = Array.FindLastIndex(line.ToCharArray(), char.IsDigit);
                     directory.children.Add(new Node(line.Substring(lastDigitIndex + 2), false, size:Int32.Parse(line.Substring(0, lastDigitIndex + 1))));
                     break;
               }
            }
         }
      }

      private static void CalculateDirectoriesSizes()
      {
         for (int index = 0; index < directories.Count; index++)
         {
            Node directory = directories[index];
            Console.WriteLine($"Calculating size of directory {directory.name} at index {index}.");
            if (directory.size != 0) continue;
            CalculateDirectorySize(directory);
         }
      }

      private static void CalculateDirectorySize(Node directory)
      {
         foreach (Node child in directory.children.Where(c => c.isDirectory && c.size == 0))
         {
            CalculateDirectorySize(child);
         }

         directory.size = directory.children.Sum(c => c.size);
      }
   }
}
