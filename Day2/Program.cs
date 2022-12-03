using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2022
{
   internal class Program
   {
      public static void Main(string[] args)
      {
         // Edit Configurations > Working directory to find the files path
         
         //string[] input = File.ReadAllLines("ExampleInput.txt");
         string[] input = File.ReadAllLines("Input.txt");

         int[] scores = new int[input.Length];

         for (int i = 0; i < input.Length; i++)
         {
            string line = input[i];
            char myMove = line[line.Length - 1];
            char theirMove = line[0];

            int GetOutcomeScore(char drawCharacter, char winCharacter)
            {
               if (theirMove == drawCharacter)
               {
                  return 3;
               }

               return theirMove == winCharacter ? 6 : 0;
            }
            
            switch (myMove)
            {
               case 'X':
                  scores[i] = 1 + GetOutcomeScore('A', 'C');
                  break;
               case 'Y':
                  scores[i] = 2 + GetOutcomeScore('B', 'A');
                  break;
               case 'Z':
                  scores[i] = 3 + GetOutcomeScore('C', 'B');
                  break;
            }
         }

         int totalScore = scores.Sum();

         Console.WriteLine(totalScore);
      }
   }
}