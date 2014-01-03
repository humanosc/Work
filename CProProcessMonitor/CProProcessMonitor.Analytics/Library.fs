namespace CProProcessMonitor.Analytics

open System.IO 


    



type Processor() = 
    member this.X = "F#"

    
    member this.loadAndMap path =
        let split (x:string) =
            x.Split([|'\t'|])
        
        let lines = File.ReadAllLines path
        let list = Array.toList lines
        List.map split list 

   // member this.Average path =
   //     let values = this.loadAndMap path
       // List.average values
         