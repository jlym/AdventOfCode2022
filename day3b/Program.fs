open System;
open System.IO;
open System.Linq;
open FSharp.Collections;


let scoreChar (c: char): int =
  let n = int c
  if Char.IsUpper c then
    n - int 'A' + 27
  else
    n - int 'a' + 1


let findDuplicate (lines: string[]): char =
  let firstLine = lines[0]

  let otherLines: seq<Set<char>> = 
    lines[1..]
    |> Seq.map Set.ofSeq

  let isCharInAllOtherLines (c: char): bool =
    otherLines.All (fun (line: Set<char>) -> line.Contains c)

  firstLine.First isCharInAllOtherLines


let scoreGroup (lines: string[]): int =
  let duplicate: char = findDuplicate lines
  scoreChar duplicate


let rec findTotalScore (lines: string[]): int =
  if lines.Length = 0 then
    0
  else
    let currentScore: int = scoreGroup lines[..2]
    currentScore + findTotalScore lines[3..] 


// Main
let lines: string[] = File.ReadAllLines("./input.txt");
let score: int = findTotalScore lines
printfn $"{score}"

