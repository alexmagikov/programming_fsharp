module MiniCrawler.Test

open System
open System.Net
open System.Threading
open NUnit.Framework
open FsUnit

let startTestServer () =
    let listener = new HttpListener()
    listener.Prefixes.Add("http://localhost:12345/")
    listener.Start()
    let cts = new CancellationTokenSource()
    let serverTask = async {
        while not cts.Token.IsCancellationRequested do
            try
                let! context = listener.GetContextAsync() |> Async.AwaitTask
                let url = context.Request.Url.ToString()
                let content =
                    match url with
                    | "http://localhost:12345/start" ->
                        """<a href="http://localhost:12345/page1">Page1</a>
                           <a href="http://localhost:12345/page2">Page2</a>"""
                    | "http://localhost:12345/page1" -> "Hello"
                    | "http://localhost:12345/page2" -> "World!"
                    | _ -> ""
                let buffer = System.Text.Encoding.UTF8.GetBytes(content)
                context.Response.ContentLength64 <- buffer.LongLength
                context.Response.OutputStream.Write(buffer, 0, buffer.Length)
                context.Response.OutputStream.Close()
            with _ -> ()
    }
    Async.Start(serverTask, cts.Token)
    listener, cts

[<Test>]
let ``PrintPageSizes downloads pages and return correct value`` () =
    let listener, cts = startTestServer()
    use sw = new System.IO.StringWriter()
    let originalOut = Console.Out
    Console.SetOut(sw)
    try
        let expected = 
            [ "http://localhost:12345/page1", 5
              "http://localhost:12345/page2", 6 ]

        let result = Crawler.printPageSizes "http://localhost:12345/start"
        result |> should equal expected
    finally
        Console.SetOut(originalOut)
        cts.Cancel()
        listener.Stop()