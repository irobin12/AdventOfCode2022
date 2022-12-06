﻿using System;
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

         //string[] input = File.ReadAllLines("ExampleInput.txt");
         //string[] input = File.ReadAllLines("Input.txt");
         
         //string input = File.ReadAllText("ExampleInput.txt");
         string input = File.ReadAllText("Input.txt");

         for (int i = 0; i < input.Length; i++)
         {
            if (input.Substring(i, 4).Distinct().ToArray().Length == 4)
            {
               Console.WriteLine(i + 4);
               break;
            }
         }
      }
   }
}