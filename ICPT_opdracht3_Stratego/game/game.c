#include "../board/board.h"
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

void cleanup(Tile ***board, int boardWith){
    for (int i = 0; i< boardWith; i++){
        free(board[i]);
    }
    free(board);
}

void playGame(Tile ***board, int boardWith){
    printBoard(board, boardWith);
    printf("\n");
    while (!gameFinished(board)){
        int xCoord;
        int yCoord;
        char direction[100];
        printf("\nInput the coordinates you want to move.\n X:");
        scanf("%d", &xCoord);
        printf(" Y:");
        scanf("%d", &yCoord);
        if (xCoord >3 || xCoord <0 || yCoord>3 || yCoord<0){
            printf("This coordinate doesn't exist, try again.");
        } else if (board[yCoord][xCoord]!=NULL &&! board[yCoord][xCoord]->enemy){
            printf("Enter direction (up/down/left/right):");
            scanf("%s", direction);
            if (strcmp(direction, "up") == 0){
                printf("Moving up\n");
                board = moveUp(board, xCoord, yCoord);
                printBoard(board, boardWith);
            } else if (strcmp(direction, "down") == 0){
                printf("Moving down\n");
                board = moveDown(board, xCoord, yCoord);
                printBoard(board, boardWith);
            } else if (strcmp(direction, "left") == 0){
                printf("Moving left\n");
                board = moveLeft(board, xCoord, yCoord);
                printBoard(board, boardWith);
            } else if (strcmp(direction, "right") == 0){
                printf("Moving right\n");
                board = moveRight(board, xCoord, yCoord);
                printBoard(board, boardWith);
            } else {
                printf("This isn't a valid direction, try again.");
            }
        } else {
            printf("This piece doesn't belong to you, try again.");
        }
    }
}
