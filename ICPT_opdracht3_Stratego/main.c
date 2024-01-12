#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "game/game.h"
#include "board/board.h"

int main(int argc, char *argv[]) {
    srand(time(NULL));
    int boardwith = 4;
    Tile*** board = generateBoard(boardwith);

    playGame(board, boardwith);
    cleanup(board, boardwith);
    return 0;
}
