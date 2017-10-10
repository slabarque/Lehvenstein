// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System;
open System.Linq;

[<EntryPoint>]
let main argv = 
    let min3 a b c = 
        let result = min a (min b c)
        //printfn "min %A %A %A = %A" a b c result
        result

    let trim (s:string) = s.Remove (s.Length-1)
    let indicator a b =
        if a=b then 0 else 1
    let last (s:string) = s.Substring (s.Length-1)

    let word1 = "kitten"
    let word2 = "sitting"

    let rec lehvenstein (a:string) (b:string) :int =
        //printfn "evaluating %A <-> %A" a b
        let result = 
            match min a.Length b.Length with
            | 0 ->max a.Length b.Length
            | _ -> min3 (lehvenstein (trim a) b + 1) (lehvenstein a (trim b) + 1) (lehvenstein (trim a) (trim b) + indicator (last a) (last b))
        //printfn "evaluated %A <-> %A => %A" a b result
        result


    let mutable distances:List<int * string * string>=[]
    //= new System.Collections.Generic.List<int * string * string>()
    // = new List<int * string *string>()
    let addDistance (list:List<int * string * string>) a b = 
        ((lehvenstein a b), a ,b)::list
    let words = System.IO.File.ReadAllLines("words.txt").ToArray();
    for i = 0 to words.Length-1 do
        for j = 0 to words.Length-1 do
            if i <> j then
                distances <- addDistance distances words.[i] words.[j]

    distances<-List.sortBy (fun ((d,a,b))->d) distances
    //distances.Sort(new System.Comparison<int*string*string>((d, a, b) (d2,a2, b2)=>d))
    
    for dist in distances do
        printfn "%A" dist

    0 // return an integer exit code
