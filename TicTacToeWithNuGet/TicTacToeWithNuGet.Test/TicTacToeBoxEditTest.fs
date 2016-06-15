module TicTacToeBoxEditTest
open TicTacToeBoxEdit
open TicTacToe.Core.TicTacToeBoxClass
open Xunit 
open FsUnit
open TicTacToe.Core.GameSettings
open TicTacToe.Core.PlayerValues

let gameTestCreate = craftGameSetting (3)("X") ("@") (int playerVals.Human)(false)(false)(false)


[<Fact>]   // test
let Other_User_Input_Is_UnSucessfulWith() =
    let ticTacToeBox = ["X"; "-2-"; "-3-"; "-4-"; "-5-"; "-6-"; "-7-"; "-8-"; "-9-"]
    let editedBox = new TicTacToeBox(ticTacToeBox)
    (fun () -> insertOtherUserOption editedBox 1 gameTestCreate.playerGlyph gameTestCreate.aIGlyph 
                |> ignore) |> should throw typeof<SpotAlreayTaken>