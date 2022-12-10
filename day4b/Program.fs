open System.IO
open System.Collections.Generic
open System
open FSharp.Collections

type Range = (int * int)

let parseRange (rangeStr: string): Range =
  let rangeTokens = rangeStr.Split("-")
  let first = Int32.Parse(rangeTokens[0])
  let last = Int32.Parse(rangeTokens[1])
  (first, last)


let hasOverlap ((range1, range2): Range * Range): bool =
  // Order the ranges so that the first range starts before the second range
  let firstRange, secondRage =
    if fst range1 <= fst range2 then
      range1, range2
    else
      range2, range1

  let _, last1 = firstRange
  let first2, _ = secondRage

  first2 <= last1


let parseLine (line: string): Range * Range =
  let tokens = line.Split(",")
  let range1 = parseRange tokens[0]
  let range2 = parseRange tokens[1]
  range1, range2


let lines: IEnumerable<string> = File.ReadLines("./input.txt")
let rangePairs: seq<Range * Range> = lines |> Seq.map parseLine
let numOverlaps: int = 
  rangePairs 
  |> Seq.map hasOverlap 
  |> Seq.sumBy (fun (overlap: bool) -> if overlap then 1 else 0) 


printfn $"{numOverlaps}"
