open FSharp.Data

type MyVocabularyBook = XmlProvider< @"C:\Users\pgw19\OneDrive\Documents\BingDictionary\1000.xml">

[<EntryPoint>]
let main argv =
    let vocabularyBook = MyVocabularyBook.Load(@"C:\Users\pgw19\OneDrive\Documents\BingDictionary\1000.xml")
    let text =
        vocabularyBook.Words.WordUnits
        |> Array.map ((fun wordUnit -> wordUnit.HeadWord) >> string)
        //|> String.concat System.Environment.NewLine
    let writer = System.IO.File.WriteAllLines(@".\vocabularyBook.csv", text)
    //
    0 // return an integer exit code
