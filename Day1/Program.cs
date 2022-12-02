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
        // string input = File.ReadAllText("ExampleInput.txt");
         string[] input = File.ReadAllLines("Input.txt");

         List<List<int>> caloriesByElf = new List<List<int>>();
         List<int> totalCaloriesByElf = new List<int>();
         int index = 0; 
         
         foreach (var line in input)
         {
            if (line == String.Empty)
            {
               int totalCalories = 0;
               foreach (int calories in caloriesByElf[index])
               {
                  totalCalories += calories;
               }
               totalCaloriesByElf.Add(totalCalories);
               
               index += 1;
               continue;
            }

            if (caloriesByElf.Count <= index)
            {
               caloriesByElf.Add(new List<int>());
            }
            
            caloriesByElf[index].Add(Int32.Parse(line));
         }

         var max = totalCaloriesByElf.Max();
         totalCaloriesByElf.Sort();
         var c = totalCaloriesByElf.Count;
         
         Console.WriteLine(totalCaloriesByElf[c-1]+totalCaloriesByElf[c-2]+totalCaloriesByElf[c-3]);
      }
   }
}