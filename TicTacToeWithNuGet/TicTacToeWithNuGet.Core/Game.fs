module Game
open CleanInput
open TicTacToeBoxEdit
open TicTacToe.Core.Game
open TicTacToe.Core.TicTacToeBoxEdit
open TicTacToe.Core.AI
open TicTacToe.Core.CheckForWinnerOrTie
open ScreenEdit
open TicTacToe.Core.GameSettings
open TicTacToe.Core.GameStatusCodes
open TicTacToe.Core.userInputException
open TicTacToe.Core.PlayerValues
open IInputOutPut
open InputOutPut
open TicTacToe.Core.ITicTacToeBoxClass
open TicTacToe.Core.TicTacToeBoxClass
open TicTacToe.Core.Translate


let gameEndingMessage( board : ITicTacToeBox) 
                     ( playerGlyph : string)
                     ( aIGlyph : string)  : int =
    let endResult = checkForWinnerOrTie(board)(playerGlyph)(aIGlyph)
    if endResult = getWinningAIValue(board.victoryCellCount()) then
        Other_Player_Won
    elif endResult = getWinningHumanValue(board.victoryCellCount()) then
         Player_Won
    else
        Tie

let isGameOver(game : gameSetting)(board : ITicTacToeBox)(io : IInputOut) : bool =
    if checkForWinnerOrTie(board)(game.playerGlyph)(game.aIGlyph) 
            = int GenResult.NoWinner then
        false
    else
        writeToScreen(board)
                     (game.inverted)
                     (gameEndingMessage(board)
                                   (game.playerGlyph)
                                   (game.aIGlyph))
                  (io) |> ignore
        true

let userInputOkToEditTicTacToeBox(game : gameSetting)(board : ITicTacToeBox)(postion : string) : int = 
    let message = isUserInputCorrect(board)
                    (postion)(game.playerGlyph)(game.aIGlyph)
    if( message = Blank ) then
        Blank
    else
        message


let otherUserInputOkToEditTicTacToeBox(game : gameSetting)(board : ITicTacToeBox)(postion : string) : int = 
    let message = isUserInputCorrect(board)
                    (postion)(game.playerGlyph)(game.aIGlyph)
    if( message = Blank ) then
        Blank
    else
        message

let rec humanOneturn(game : gameSetting)(board : ITicTacToeBox)(io : IInputOut)(message : int) : int =
    if not (isGameOver(game)(board)(io)) then
        writeToScreen(board)(game.inverted)(message)(io)
        let userPickedPos = io.getUserInput()
        if userInputOkToEditTicTacToeBox(game)(board)(userPickedPos) = Blank then
            humanTwoTurn(game)
                        (insertUserOption(board)
                                         (int userPickedPos)
                                         (game.playerGlyph)
                                         (game.aIGlyph))
                        (io)
                        (Blank)
        else
            humanOneturn(game)(board)(io)(userInputOkToEditTicTacToeBox(game)(board)(userPickedPos))
    else
        checkForWinnerOrTie(board)(game.playerGlyph)(game.aIGlyph) 

and humanTwoTurn(game : gameSetting)(board : ITicTacToeBox)(io : IInputOut)(message : int) : int =
    if not (isGameOver(game)(board)(io)) then
        writeToScreen(board)(game.inverted)(message)(io)
        let userPickedPos = io.getUserInput()
        if userInputOkToEditTicTacToeBox(game)(board)(userPickedPos) = Blank then
            humanOneturn(game)
                        (insertOtherUserOption(board)
                                         (int userPickedPos)
                                         (game.playerGlyph)
                                         (game.aIGlyph))
                        (io)
                        (Blank)
        else
            humanTwoTurn(game)(board)(io)(userInputOkToEditTicTacToeBox(game)(board)(userPickedPos))
    else
        checkForWinnerOrTie(board)(game.playerGlyph)(game.aIGlyph) 

let rec humanChoseMove(game : gameSetting)(board : ITicTacToeBox)(io : IInputOut)(message : int) : int =
    if not (isGameOver(game)(board)(io)) then
        writeToScreen(board)(game.inverted)(message)(io)
        let userPickedPos = io.getUserInput()
        if userInputOkToEditTicTacToeBox(game)(board)(userPickedPos) = Blank then
            aiChoseMove(game)
                   (insertUserOption(board)
                                    (int userPickedPos)
                                    (game.playerGlyph)
                                    (game.aIGlyph))
                        (io)
        else
            humanChoseMove(game)(board)(io)(userInputOkToEditTicTacToeBox(game)(board)(userPickedPos))
    else
        checkForWinnerOrTie(board)(game.playerGlyph)(game.aIGlyph) 

and aiChoseMove(game : gameSetting)(board : ITicTacToeBox)(io : IInputOut) : int =
    if not (isGameOver(game)(board)(io)) then
        humanChoseMove(game)(aIMove(game)(board))(io)(Blank)
    else
        checkForWinnerOrTie(board)(game.playerGlyph)(game.aIGlyph) 

let humanVsHuman(game : gameSetting)(board : ITicTacToeBox)(io : IInputOut) : int =
    if int playerVals.Human = game.firstPlayer then
        humanOneturn(game)(board)(io)(Blank)
    else
        humanTwoTurn(game)(board)(io)(Blank)

let humanFirstVsAiGame(game : gameSetting)(ticTacToeBox : ITicTacToeBox)(io : IInputOut) : int =
    humanChoseMove(game)(ticTacToeBox)(io)(Blank)

let humanVsAiFirstGame(game : gameSetting)(ticTacToeBox : ITicTacToeBox)(io : IInputOut) : int =
    aiChoseMove(game)(ticTacToeBox)(io)

let rec aIPlayerOneTurn(playerOneSettings : gameSetting)(playerTwoSettings : gameSetting)
                       (board : ITicTacToeBox)(io : IInputOut) : int =
    
    if not (isGameOver(playerOneSettings)(board)(io)) then
        aIPlayerTwoTurn(playerOneSettings)(playerTwoSettings)
                   (aIMove(playerOneSettings)(board))(io)
    else
        checkForWinnerOrTie(board)(playerOneSettings.playerGlyph)(playerOneSettings.aIGlyph) 

and aIPlayerTwoTurn(playerOneSettings : gameSetting)(playerTwoSettings : gameSetting)
                   (board : ITicTacToeBox)(io : IInputOut) : int =
    
    if not (isGameOver(playerOneSettings)(board)(io)) then
        aIPlayerOneTurn(playerOneSettings)(playerTwoSettings)
                   (aIMove(playerTwoSettings)(board))(io)
    else
        checkForWinnerOrTie(board)(playerOneSettings.playerGlyph)(playerOneSettings.aIGlyph) 

let aIvsAI(game : gameSetting)(board : ITicTacToeBox)(io : IInputOut) : int =
    let aIPlayerTwoGame = craftGameSetting(game.ticTacToeBoxSize)(game.aIGlyph)
                                          (game.playerGlyph)(game.firstPlayer)
                                          (game.inverted)(false)(true)
    aIPlayerOneTurn(game)(aIPlayerTwoGame)(board)(io)

let gameRunner(game : gameSetting)(io : IInputOut) : int =
    let ticTacToeBoard = new TicTacToeBox(makeTicTacToeBox(game.ticTacToeBoxSize))
    if game.firstPlayer = int playerVals.Human && not game.humanVsHuman && not game.aIvAI then
        humanFirstVsAiGame(game)(ticTacToeBoard)(io)
    elif game.humanVsHuman then
        humanVsHuman(game)(ticTacToeBoard)(io)
    elif game.aIvAI then
        aIvsAI(game)(ticTacToeBoard)(io)
    else
        humanVsAiFirstGame(game)(ticTacToeBoard)(io) 

let startGame (game : gameSetting)(io : IInputOut) : int =
    let result = gameRunner(game)(io)
    result