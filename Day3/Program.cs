using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2022
{
   internal class Program
   {
      private class Rucksack
      {
         private readonly string contentsFullList;
         private int middleIndex => contentsFullList.Length / 2;
         private char specialItem => contentsFullList.Substring(0, middleIndex).Intersect(contentsFullList.Substring(middleIndex)).First();
         public int itemPriority => char.IsUpper(specialItem) ? specialItem - 64 + 26 : specialItem - 64 - 32;

         public Rucksack(string contentsFullList)
         {
            this.contentsFullList = contentsFullList;
         }
      }
      
      public static void Main()
      {
         // Edit Configurations > Working directory to find the files path
         
         // string[] input = File.ReadAllLines("ExampleInput.txt");
         string[] input = File.ReadAllLines("Input.txt");

         Rucksack[] rucksacks = new Rucksack[input.Length];

         for (int i = 0; i < input.Length; i++)
         {
            rucksacks[i] = new Rucksack(input[i]);
         }
         
         Console.WriteLine(rucksacks.Sum(r => r.itemPriority));
      }
   }
}