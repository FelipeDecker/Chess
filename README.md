# Chess

A complete console-based chess game, developed in C# on the **.NET** platform. The project implements the official rules of chess, including movement for all pieces, special moves (castling, en passant and promotion), check and checkmate detection, and a colored text interface to play directly in the terminal.

## Technologies used

- **C#** (main language of the project)
- **.NET 10** (`net10.0`), `Exe` (console) application type
- **System.Console** for the entire input/output interface (board printing, colors and move reading)
- Object-oriented design with inheritance (abstract `Piece` class specialized into `Pawn`, `Rook`, `Knight`, `Bishop`, `Queen` and `King`)
- Error handling with custom exceptions (`BoardException`)

## Project structure

```
Chess/
├── Board/
│   ├── Board.cs           # Board representation (8x8), placing and removing pieces
│   ├── BoardException.cs  # Custom exception for game errors
│   ├── Color.cs           # Enum with piece colors (White / Black)
│   ├── Piece.cs           # Abstract base class for all pieces
│   └── Position.cs        # Represents a position (row, column) in the board matrix
├── Pieces/
│   ├── Bishop.cs          # Bishop
│   ├── ChessPosition.cs   # Conversion between chess notation (e.g., "A1") and matrix positions
│   ├── King.cs            # King (includes castling logic)
│   ├── Knight.cs          # Knight
│   ├── Match.cs           # Match rules: turns, executing/undoing moves, check, checkmate, promotion
│   ├── Pawn.cs            # Pawn (includes en passant logic and two-square first move)
│   ├── Queen.cs           # Queen
│   └── Rook.cs            # Rook
├── Program.cs             # Entry point: main game loop in the console
└── Screen.cs              # Responsible for drawing the board, captured pieces and reading user moves
```

## How to play

1. Build and run the project (`dotnet run` inside the `Chess` folder, or through Visual Studio).
2. The board is displayed in the console, with white pieces shown in white and black pieces shown in yellow.
3. On each turn, type the origin position (e.g., `E2`) and press Enter.
4. The possible target squares for the selected piece are highlighted in gray.
5. Type the target position (e.g., `E4`) and press Enter to complete the move.
6. The game automatically alternates between the white and black players, showing the current turn, whether there is a check, and, at the end, who won by checkmate.

## Rules and moves implemented

The game follows the official rules of chess, including the specific movement of each piece and the following special moves:

### Piece movement
- **Pawn**: moves one square forward (two squares on its first move), captures diagonally, and is blocked by pieces in front of it.
- **Rook**: moves in a straight line horizontally and vertically, any number of squares.
- **Knight**: moves in an "L" shape (two squares in one direction plus one square perpendicular), and can jump over other pieces.
- **Bishop**: moves diagonally, any number of squares.
- **Queen**: combines the movement of the rook and the bishop (straight lines and diagonals).
- **King**: moves one square in any direction (horizontal, vertical or diagonal).

### Special moves
- **Kingside castling** and **queenside castling**: allowed when the king and the involved rook have not moved yet, there are no pieces between them, and the king is not in check nor passes through attacked squares.
- **En passant**: special pawn capture, available only immediately after an opposing pawn advances two squares and ends up beside the capturing pawn.
- **Pawn promotion**: upon reaching the last rank of the board, the player chooses to turn the pawn into a **Queen (Q)**, **Rook (R)**, **Bishop (B)** or **Knight (N)**.

### Match validation rules
- **Check**: the game detects and signals when a king is under attack.
- **Self-check prevention**: it is not allowed to make a move that would leave your own king in check.
- **Checkmate**: the game checks all possible moves of the pieces in play to determine if there is no way to escape check, ending the match and declaring the winner.
- **Captured pieces**: displayed separately by color throughout the match.
- **Turn count**: displayed each round, along with the current player.
