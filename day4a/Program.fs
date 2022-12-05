open System.IO
open System.Collections.Generic
open System.Linq
open System
open FSharp.Collections

type Range = (int * int)

let parseRange (rangeStr: string): Range =
  let rangeTokens = rangeStr.Split("-")
  let first = Int32.Parse(rangeTokens[0])
  let last = Int32.Parse(rangeTokens[1])
  (first, last)


let hasOverlap ((range1, range2): Range * Range): bool =
  let first1, last1 = range1
  let first2, last2 = range2
  (first1 <= first2 && last1 >= last2) || 
  (first2 <= first1 && last2 >= last1)


let parseLine (line: string) =
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
