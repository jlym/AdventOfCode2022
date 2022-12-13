open System;
open System.IO;
open FSharp.Collections;
open System.Linq;

type Vector = int * int // x, y

let none = Vector(0, 0)
let up = Vector(0, 1)
let down = Vector(0, -1)
let left = Vector(-1, 0)
let right = Vector(1, 0)

let upLeft = Vector(-1, 1)
let upRight = Vector(1, 1)
let downLeft = Vector(-1, -1)
let downRight = Vector(1, -1)


let add (a: Vector) (b: Vector): Vector =
  let (x1, y1) = a
  let (x2, y2) = b
  (x1 + x2, y1 + y2)


let delta (a: Vector) (b: Vector): Vector =
  let (x1, y1) = a
  let (x2, y2) = b
  (x1 - x2, y1 - y2)


type Move = char * int // direction, count


let moveHead (head: Vector) (direction: char): Vector =
  match direction with
  | 'R' -> add head right
  | 'L' -> add head left
  | 'U' -> add head up
  | 'D' -> add head down
  | _ -> failwith $"unexpected direction, \"{direction}\"" 


let getTailMove (head: Vector) (tail: Vector): Vector =
  let diff = delta head tail
  let diffX, diffY = diff

  if diffX >= -1 && diffX <= 1 && diffY >= -1 && diffY <= 1 then
    none
  else if diff = (2, 0) then
    right
  else if diff = (-2, 0) then
    left
  else if diff = (0, 2) then
    up
  else if diff = (0, -2) then
    down
  else if diff = (2, 2) || diff = (2, 1) || diff = (1, 2) then
    upRight
  else if diff = (-2, 2) || diff = (-2, 1) || diff = (-1, 2) then
    upLeft
  else if diff = (2, -2) || diff = (2, -1) || diff = (1, -2) then
    downRight
  else if diff = (-2, -2) || diff = (-2, -1) || diff = (-1, -2) then
    downLeft
  else
    failwith $"getTailMove failed head: {head}, tail: {tail}"


let rec applyMove (head: Vector) (tail: Vector) (move: Move) (tailMoveSet: Set<Vector>): Vector * Vector * Set<Vector> =
  let direction, count = move
  let tailMoveSet = tailMoveSet.Add(tail)

  if count <= 0 then
    head, tail, tailMoveSet

  else
    let head: Vector = moveHead head direction
    let tailMove = getTailMove head tail
    let tail = add tail tailMove

    let remMove = (direction, count - 1)
    applyMove head tail remMove tailMoveSet


let rec apply (head: Vector) (tail: Vector) (moves: List<Move>) (tailMoveSet: Set<Vector>): Set<Vector> =
  let tailMoveSet = tailMoveSet.Add(tail)

  if moves.IsEmpty then
    tailMoveSet
  else
    let firstMove :: otherMoves = moves
    let head, tail, tailMoveSet = applyMove head tail firstMove tailMoveSet
    apply head tail otherMoves tailMoveSet


let parseLine (line: string): Move =
  let tokens = line.Split(" ")
  let direction = tokens[0][0]
  let count = Int32.Parse(tokens[1])
  (direction, count)


let moves = Seq.toList (File.ReadLines("./input.txt").Select(parseLine))
let head = Vector(0, 0)
let tail = Vector(0, 0)
let s: Set<Vector> = Set.empty
let tailMoveSet = apply head tail moves s
printfn $"{tailMoveSet.Count} "
