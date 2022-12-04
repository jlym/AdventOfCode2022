open System;
open System.IO;
open System.Linq;
open FSharp.Collections;

let scoreChar (c: char): int =
  let n: int = int c
  if Char.IsUpper c then
    n - int 'A' + 27
  else
    n - int 'a' + 1


let findDuplicate (lines: string[]): char =
  let duplicates: Set<char> = 
    lines 
    |> Seq.map Set.ofSeq
    |> Set.intersectMany

  duplicates.ToArray().First()


let scoreGroup (lines: string[]): int =
  let duplicate: char = findDuplicate lines
  scoreChar duplicate


// Main
let lines: string[] = File.ReadAllLines("./input.txt");
let groups: string[][] = Array.splitInto (lines.Length / 3) lines  
let score: int = 
  groups
  |> Seq.map scoreGroup
  |> Seq.sum

printfn $"{score}"
