using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2022
{
   internal class Program
   {
      public static void Main()
      {
         // Edit Configurations > Working directory to find the files path
         
         //string[] input = File.ReadAllLines("ExampleInput.txt");
         string[] input = File.ReadAllLines("Input.txt");

         int[][] pairs = new int[input.Length][];

         int numberOfPairsFullyContained = 0;
         int overlappingPairs = 0;

         for (int i = 0; i < input.Length; i++)
         {
            string line = input[i];
            pairs[i] = new int[4];
            
            int indexOfFirstPairHyphen = line.IndexOf('-');
            int indexOfComma = line.LastIndexOf(',');
            int indexOfSecondPairHyphen = line.LastIndexOf('-');

            pairs[i][0] = Int32.Parse(line.Substring(0, indexOfFirstPairHyphen));
            pairs[i][1] = Int32.Parse(line.Substring(indexOfFirstPairHyphen + 1, indexOfComma - (indexOfFirstPairHyphen + 1)));
            pairs[i][2] = Int32.Parse(line.Substring(indexOfComma + 1, indexOfSecondPairHyphen - (indexOfComma + 1)));
            pairs[i][3] = Int32.Parse(line.Substring(indexOfSecondPairHyphen + 1));

            if (pairs[i][0] >= pairs[i][2] && pairs[i][1] <= pairs[i][3] ||
                pairs[i][2] >= pairs[i][0] && pairs[i][3] <= pairs[i][1])
            {
               numberOfPairsFullyContained++;
            }
            
            if (pairs[i][0] >= pairs[i][2] && pairs[i][0] <= pairs[i][3] ||
                pairs[i][1] >= pairs[i][2] && pairs[i][1] <= pairs[i][3] ||
                pairs[i][2] >= pairs[i][0] && pairs[i][2] <= pairs[i][1] ||
                pairs[i][3] >= pairs[i][0] && pairs[i][3] <= pairs[i][1])
            {
               overlappingPairs++;
            }
         }
         
         Console.WriteLine(numberOfPairsFullyContained);
         Console.WriteLine(overlappingPairs);
      }
   }
}