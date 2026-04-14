module MiniCrawler.Crawler

open System.Net.Http
open System.Text.RegularExpressions

let downloadContent (client: HttpClient) (url: string) = async {
    try
        let! html = client.GetStringAsync(url) |> Async.AwaitTask
        return Some (url, html.Length)
    with ex ->
        printf $"Download error %s{url}: %s{ex.Message}"
        return None    
}

let extractUrls html =
    Regex.Matches(html, @"<a\s+href=""(http://[^""]+)""")
    |> Seq.cast<Match>
    |> Seq.map (fun m -> m.Groups[1].Value)
    |> Seq.distinct
    |> Seq.toList
   
let printPageSizes (url: string) =
    async {
        use client = new HttpClient()
        let! startHtml = client.GetStringAsync(url) |> Async.AwaitTask
        let urls = extractUrls startHtml
        let! results =
            urls
            |> List.map (downloadContent client)
            |> Async.Parallel
        let pages =
            results
            |> Array.choose id
            |> Array.toList
        pages
        |> List.iter (fun (url, size) -> printfn $"%s{url} -- %d{size}")
        return pages
    }
    |> Async.RunSynchronously
    
printPageSizes "http://hwproj.ru"