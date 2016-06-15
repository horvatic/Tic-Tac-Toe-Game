module CleanInput
open TicTacToe.Core.userInputException
exception NonIntError of unit
exception InvaildOption of unit

let SanitizeGlyph( input : string) : string =
    if input.Length = 1 then
        input
    else
        raise(InvaildGlyph())
 
let boxSizeWithinBound(userInput : int ) : int =
    if userInput > 4 then
        raise(OutOfBoundsOverFlow())
    elif userInput < 3 then
        raise(OutOfBoundsUnderFlow())
    userInput

let SanitizeBoxSize(input : string) : int =
    try
        let userInput = int input
        boxSizeWithinBound(userInput)
    with
        | :? System.FormatException -> raise(NonIntError())


let SanitizeYesOrNo(input : string) : string =
    let userInput = input
    if not( userInput.ToUpper() = "Y") && not( userInput.ToUpper() = "N" ) 
        && not( userInput.ToUpper() = "S") then
        raise(InvaildOption())
    userInput.ToUpper()