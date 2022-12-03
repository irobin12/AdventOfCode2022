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

            int GetRoundScore(bool win, bool draw)
            {
               switch (theirMove)
               {
                  case 'A':
                     if (win)
                     {
                        return 2;
                     } else if (draw)
                     {
                        return 1;
                     }
                     else
                     {
                        return 3;
                     }
                  case 'B':
                     if (win)
                     {
                        return 3;
                     } else if (draw)
                     {
                        return 2;
                     }
                     else
                     {
                        return 1;
                     }
                  case 'C':
                     if (win)
                     {
                        return 1;
                     } else if (draw)
                     {
                        return 3;
                     }
                     else
                     {
                        return 2;
                     }
               }

               return 0;
            }
            
            switch (myMove)
            {
               case 'X':
                  // scores[i] = 1 + GetOutcomeScore('A', 'C');
                  scores[i] = 0 + GetRoundScore(false, false);
                  break;
               case 'Y':
                  //scores[i] = 2 + GetOutcomeScore('B', 'A');
                  scores[i] = 3 + GetRoundScore(false, true);
                  break;
               case 'Z':
                  //scores[i] = 3 + GetOutcomeScore('C', 'B');
                  scores[i] = 6 + GetRoundScore(true, false);
                  break;
            }
         }

         int totalScore = scores.Sum();

         Console.WriteLine(totalScore);
      }
   }
}