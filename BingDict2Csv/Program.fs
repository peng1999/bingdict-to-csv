open FSharp.Data
open CommandLine

type MyVocabularyBook = XmlProvider< @"xml-example.xml">

type CommandOptions() = 
    [<Option('d', "directory", Required = true)>]
    member val VocabularyDir : string = "" with get, set
    [<Option('o', "output", DefaultValue = "vocabularyBook.csv")>]
    member val OutputDir : string = "vocabularyBook.csv" with get, set

[<EntryPoint>]
let main argv =
    let args = try Parser.Default.ParseArguments<CommandOptions>(argv)
               with ex -> null
    let args = args.Value
//    let vocabularyBook = MyVocabularyBook.Load(@"C:\Users\pgw19\OneDrive\Documents\BingDictionary\1000.xml")
    let vocabularyBook = MyVocabularyBook.Load(args.VocabularyDir)
    let text =
        vocabularyBook.Words.WordUnits
        |> Array.map ((fun wordUnit -> wordUnit.HeadWord) >> string)
        //|> String.concat System.Environment.NewLine
    let writer = System.IO.File.WriteAllLines(args.OutputDir, text)
    0 // return an integer exit code
