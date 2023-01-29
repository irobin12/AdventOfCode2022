using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2022
{
   internal class Program
   {
      public static void Main()
      {
         // Edit Configurations > Working directory to find the files path

         string[] input = File.ReadAllLines("ExampleInput.txt");
         //string[] input = File.ReadAllLines("Input.txt");

         //string input = File.ReadAllText("ExampleInput.txt");
         //string input = File.ReadAllText("Input.txt");

         int[,] trees = new int[input[0].Length,input.Length];
         int[][] trees2 = new int[input[0].Length][];

         for (int i = 0; i < input.Length; i++)
         {
            string line = input[i];
            trees2[i] = new int[input.Length];
            
            for (int j = 0; j < line.Length; j++)
            {
               trees[i,j] = line[j] - '0';
               trees2[i][j] = line[j] - '0';
            }
         }

         int totalVisibleTrees = 0;

         /*for (int i = 0; i < trees.GetLength(0); i++)
         {
            for (int j = 0; j < trees.GetLength(1); j++)
            {
               int tree = trees[i, j];
               
               if (i == 0 || j == 0 || i == trees.GetLength(0) - 1 || j == trees.GetLength(1) - 1) // Side are visible
               {
                  totalVisibleTrees += 1;
                  continue;
               }
               
               if()
            }
         }*/
         
         FindHighestTreesByRow(trees2);
      }

      private static void FindHighestTreesByRow(int[][] trees)
      {
         int[] highestTrees = new int[trees[0].Length];
         
         for (int i = 0; i < trees.GetLength(0); i++)
         {
            int[] currentRow = trees[i];
            int[] sortedRow = trees[i];
            Array.Sort(sortedRow);

            int tallestTree = sortedRow[sortedRow.Length - 1];
            int secondTallestTree = sortedRow[sortedRow.Length - 2];

            int[] rowHighestTrees = new int[trees[0].Length];

            for (int j = 0; j < trees.GetLength(1); j++)
            {
               var tree = currentRow[j];
            }
         }
      }
   }
}
