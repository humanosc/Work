// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System

// rekursive berechnung der fakultät
let rec fac x =
    if x > 0 then
       x * fac (x - 1)
    else
       1

// rekursive berechnung der eulerschen zahl
// x: genauigkeit
let rec euler x : float =
    if x >= 0 then
        1.0 / float ( fac x ) + euler( x - 1 ) 
    else
        0.0

// rekursive permutation
//let rec permutation x =
//    x |> List.append x 

// rekursive berechnung der fibonacci zahl
let rec fib x =
    let rec intern (x0, x1, xi) = 
        let x2 = x0 + x1
        if xi > 0 then
            intern(x1, x2, xi - 1)
        else
            x2
    if x > 0 then
        intern( 0, 1, x - 2)
    else
        0
  
let rec fibC n =
    printfn "n in: %d" n
    let result = match n with
        | 1 | 2 -> 1
        | n -> fibC(n-1) + fibC(n-2)
    printfn "n out: %d result: %d" n result

    result

[<EntryPoint>]
let main argv = 
    let list = [ fac( 3 ); fac( 2 ); fac( 1 ) ]
    list |> List.iter( fun item -> printfn "Fakultäten %d" item )
    
    let eul = euler( 10 )
    printfn "Euler (mit 10 als Genauigkeitsfaktor) %f" eul
    
    let f = fibC( 10 )
    printfn "Euler (mit 10) %d" f
    

    ignore Console.Read  
    0 // return an integer exit code
