module TicTacToeBoxEdit
open TicTacToe.Core.ITicTacToeBoxClass
open TicTacToe.Core.GameSettings
exception SpotAlreayTaken of unit

let insertOtherUserOption(board : ITicTacToeBox) 
                         (userInput: int)(playerGlyph : string )
                         (aIGlyph : string ) : ITicTacToeBox =
    if board.getGlyphAtLocation(userInput-1) = playerGlyph || board.getGlyphAtLocation(userInput-1) = aIGlyph then
        raise(SpotAlreayTaken())
    board.getTicTacToeBoxEdited(userInput - 1)(aIGlyph)(playerGlyph)(aIGlyph)