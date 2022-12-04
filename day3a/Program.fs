open System;
open System.IO;
open System.Linq;
open FSharp.Collections;

let scoreLine (line: string): int =
  let pocket1Set: Set<char> =  
    line[..(line.Length / 2 - 1)]    
    |> Set.ofSeq
  let pocket2Chars: string = line[(line.Length / 2)..]

  let duplicate: char = pocket2Chars.First(
    fun c -> pocket1Set.Contains(c))
  let duplicateInt: int = int duplicate

  if Char.IsUpper duplicate then
    duplicateInt - int 'A' + 27
  else
    duplicateInt - int 'a' + 1


let lines: string[] = File.ReadAllLines("./input.txt");
let score: int = 
  lines
  |> Seq.map scoreLine
  |> Seq.sum

printfn $"{score}"
