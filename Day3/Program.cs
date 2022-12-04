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
         public readonly string contentsFullList;
         private int MiddleIndex => contentsFullList.Length / 2;
         public int ItemPriority => GetItemPriority(contentsFullList.Substring(0, MiddleIndex).Intersect(contentsFullList.Substring(MiddleIndex)).First());

         public Rucksack(string contentsFullList)
         {
            this.contentsFullList = contentsFullList;
         }
      }

      private static int GetItemPriority(char itemType)
      {
         return char.IsUpper(itemType) ? itemType - 64 + 26 : itemType - 64 - 32;
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
         
         Console.WriteLine(rucksacks.Sum(r => r.ItemPriority));// Part 1 answer

         // Part 2 below
         int sum = 0;
         
         for (int i1 = 0; i1 < rucksacks.Length; i1 += 3)
         {
            var commonItems = rucksacks[i1].contentsFullList.Intersect(rucksacks[i1+1].contentsFullList.Intersect(rucksacks[i1+2].contentsFullList));
            sum += GetItemPriority(commonItems.FirstOrDefault());
         }
         
         Console.WriteLine(sum);
      }
   }
}