open FSharp.Data
open CommandLine

type MyVocabularyBook = XmlProvider< @"xml-example.xml">

type CommandOptions() = 
    [<Option('d', "directory", Required = true)>]
    member val VocabularyDir : string = ""
    [<Option('o', "output", DefaultValue = ".")>]
    member val OutputDir : string = "."

[<EntryPoint>]
let main argv =
    let args = Parser.Default.ParseArguments<CommandOptions>(argv).Value
    #if DEBUG
    let vocabularyBook = MyVocabularyBook.Load(@"C:\Users\pgw19\OneDrive\Documents\BingDictionary\1000.xml")
    #else
    let vocabularyBook = MyVocabularyBook.Load(args.VocabularyDir)
    #endif
    let text =
        vocabularyBook.Words.WordUnits
        |> Array.map ((fun wordUnit -> wordUnit.HeadWord) >> string)
        //|> String.concat System.Environment.NewLine
    #if DEBUG
    let writer = System.IO.File.WriteAllLines(@".\vocabularyBook.csv", text)
    #else
    let writer = System.IO.File.WriteAllLines(args.OutputDir, text)
    #endif
    0 // return an integer exit code
