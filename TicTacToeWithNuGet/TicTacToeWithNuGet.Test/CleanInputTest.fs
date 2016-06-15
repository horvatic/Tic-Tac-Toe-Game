module CleanInputTest
open Xunit 
open FsUnit
open CleanInput
open userInputException


[<Fact>]   // test
let Sanitize_Player_Picked_Glyph() =
    Assert.Equal("5", SanitizeGlyph "5" )

[<Fact>]   // test
let Sanitize_Player_Picked_Glyph_To_Exception() =
    Assert.Throws<InvaildGlyph>((fun () -> SanitizeGlyph "fe" |> ignore))
//    (fun () -> SanitizeGlyph "fe" |> ignore) |> should throw typeof<InvaildGlyph>

[<Fact>]   // test
let Sanitize_Use_Box_Size_3() =
    Assert.Equal(3, SanitizeBoxSize "3" )

[<Fact>]   // test
let Sanitize_Use_Box_Size_4() =
    Assert.Equal(4, SanitizeBoxSize "4" )

[<Fact>]   // test
let Sanitize_Box_Size_To_Exception() =
    Assert.Throws<NonIntError>((fun () -> SanitizeBoxSize "3m" |> ignore))
//    (fun () -> SanitizeBoxSize "3m" |> ignore) |> should throw typeof<NonIntError>

[<Fact>]   // test
let Sanitize_Box_Size_EmptyString_To_Exception() =
    Assert.Throws<NonIntError>((fun () -> SanitizeBoxSize "" |> ignore))
//    (fun () -> SanitizeBoxSize "" |> ignore) |> should throw typeof<NonIntError>

[<Fact>]   // test
let Sanitize_Box_Size_Empty_To_Exception() =
    Assert.Throws<OutOfBoundsUnderFlow>((fun () -> SanitizeBoxSize "0" |> ignore))
//    (fun () -> SanitizeBoxSize "0" |> ignore) |> should throw typeof<OutOfBoundsUnderFlow>

[<Fact>]   // test
let Sanitize_Box_Size_9_To_Exception() =
    Assert.Throws<OutOfBoundsOverFlow>((fun () -> SanitizeBoxSize "9" |> ignore))
//    (fun () -> SanitizeBoxSize "9" |> ignore) |> should throw typeof<OutOfBoundsOverFlow>

[<Fact>]   // test
let Sanitize_User_Go_First_Or_Not_UpperCase_Y() =
    Assert.Equal("Y", SanitizeYesOrNo "Y" )

[<Fact>]   // test
let Sanitize_User_Go_First_Or_Not_LowerCase_y() =
    Assert.Equal("Y", SanitizeYesOrNo "y" )

[<Fact>]   // test
let Sanitize_User_Go_First_Or_Not_UpperCase_S() =
    Assert.Equal("S", SanitizeYesOrNo "S" )

[<Fact>]   // test
let Sanitize_User_Go_First_Or_Not_LowerCase_s() =
    Assert.Equal("S", SanitizeYesOrNo "s" )

[<Fact>]   // test
let Sanitize_User_Go_First_Or_Not_LowerCase_n() =
    Assert.Equal("N", SanitizeYesOrNo "n" )

[<Fact>]   // test
let Sanitize_User_Go_First_Or_Not_UpperCase_N() =
    Assert.Equal("N", SanitizeYesOrNo "N" )

[<Fact>]   // test
let Sanitize_User_Go_First_Or_Not_9_To_Exception() =
    Assert.Throws<InvaildOption>((fun () -> SanitizeYesOrNo "9" |> ignore))
//    (fun () -> SanitizeYesOrNo "9" |> ignore) |> should throw typeof<InvaildOption>

[<Fact>]   // test
let Sanitize_User_Go_First_Or_Not_rge_To_Exception() =
    Assert.Throws<InvaildOption>((fun () -> SanitizeYesOrNo"rge" |> ignore))
//    (fun () -> SanitizeYesOrNo"rge" |> ignore) |> should throw typeof<InvaildOption>