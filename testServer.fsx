#r "nuget: Suave"

open Suave
open Suave.Filters
open Suave.Operators
open Suave.Successful
open System.Text

let receiveNotification (req: HttpRequest) =
    fun (ctx: HttpContext) ->
        async {
            let body = Encoding.UTF8.GetString req.rawForm
            printfn "Received JSON: %s" body
            return! OK body ctx  // Echo back the received JSON
        }

let app =
    choose [
        POST >=> path "/notify" >=> request receiveNotification
    ]

[<EntryPoint>]
startWebServer defaultConfig app