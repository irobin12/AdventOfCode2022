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
            
            List<Node> GetChildren()
            {
               List<Node> children = new List<Node>();
               
               for (int j = i + 2; j < fileInput.Length; j++)
               {
                  string nextLine = fileInput[j];

                  switch (nextLine[0])
                  {
                     case '$':
                        goto End;
                     case 'd':
                        string directoryName = nextLine.Substring(5);
                        Node childDirectory = directories.Find(d => d.name == directoryName);
                        if(childDirectory == null)
                        {
                           //childDirectory = new Node(directoryName, true, GetChildren());
                           directories.Add(childDirectory);
                        }
                        children.Add(childDirectory);
                        break;
                     default:
                        int lastDigitIndex = Array.FindLastIndex(line.ToCharArray(), char.IsDigit);
                        children.Add(new Node(line.Substring(lastDigitIndex + 2), false, size:Int32.Parse(line.Substring(0, lastDigitIndex + 1))));
                        break;
                  }
               }
               
               End:
               return children;
            }

            if (line.Substring(0, 3) == "$ c" && line[line.Length - 1] != '.')
            {
               string name = line.Substring(5);
               if(directories.Find(d => d.name == name) == null)
               {
                  directories.Add(new Node(name, true, indexInInput:i));
               }
            }
         }
      }

      private static void PopulateAllDirectories()
      {
         foreach (Node directory in directories)
         {
            directory.children = new List<Node>();
            for (int i = directory.indexInInput + 2; i < fileInput.Length; i++)
            {
               string line = fileInput[i];

               switch (line[0])
               {
                  case '$':
                     goto End;
                  case 'd':
                     string directoryName = line.Substring(4);
                     Node childDirectory = directories.Find(d => d.name == directoryName);
                     directory.children.Add(childDirectory);
                     break;
                  default:
                     int lastDigitIndex = Array.FindLastIndex(line.ToCharArray(), char.IsDigit);
                     directory.children.Add(new Node(line.Substring(lastDigitIndex + 2), false, size:Int32.Parse(line.Substring(0, lastDigitIndex + 1))));
                     break;
               }
            }
            
            End: ;
         }
      }

      private static void CalculateDirectoriesSizes()
      {
         foreach (Node directory in directories)
         {
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
