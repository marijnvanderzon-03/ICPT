// board.c
#include "board.h"
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

int RandomNumberGenerator() {
    int nMin = 1;
    int nMax = 10;
    int randonNumber;
    randonNumber = rand()%(nMax-nMin) + nMin;
    return randonNumber;
}

void printBoard(Tile*** board, int boardWith) {
    printf("=======================================\n");
    printf("   ");
    for (int i = 0; i< boardWith; i++){
        printf("%d ",i);
    }
    printf("X\n");
    printf("   ");
    for (int i = 0; i< boardWith; i++){
        printf("- ");
    }
    printf("\n");
    for (int i = 0; i < boardWith; i++) {
        printf("%d |", i);
        Tile** row = board[i];
        for (int j = 0; j < boardWith; j++) {
            Tile* tile = row[j];
            if (tile == NULL){
                printf("%d ", 0);
            } else {
                if (tile->enemy){
                    printf("%s ", "*");
                } else {
                    printf("%d ", tile->value);
                }
            }
        }
        printf("\n");
    }
    printf("Y\n");
    printf("=======================================");
}

Tile*** generateBoard(int boardWidth) {
    Tile *tile1, *tile2, *tile3, *tile4;
    tile1 = malloc(sizeof(Tile));
    tile2 = malloc(sizeof(Tile));
    tile3 = malloc(sizeof(Tile));
    tile4 = malloc(sizeof(Tile));
    tile1->value = RandomNumberGenerator();
    tile1->enemy = true;
    tile2->value = RandomNumberGenerator();
    tile2->enemy = true;
    tile3->value = RandomNumberGenerator();
    tile3->enemy = false;
    tile4->value = RandomNumberGenerator();
    tile4->enemy = false;

    Tile*** board = malloc(boardWidth * sizeof(Tile**));
    for (int i = 0; i < boardWidth; ++i) {
        board[i] = malloc(boardWidth * sizeof(Tile*));
        for (int j = 0; j < boardWidth; ++j) {
            board[i][j] = NULL;
        }
    }

    board[0][1] = tile1;
    board[0][2] = tile2;
    board[3][1] = tile3;
    board[3][2] = tile4;
    return board;
}


void hitEnemy(Tile*** board, int friendlyX, int friendlyY, int enemyX, int enemyY){
    int enemyStrength = board[enemyY][enemyX]->value;
    int friendlyStrength = board[friendlyY][friendlyX]->value;
    Tile *friendlyPiece = board[friendlyY][friendlyX];
    Tile *enemyPiece = board[enemyY][enemyX];
    int val = enemyPiece->value;
    int val2 = friendlyPiece->value;
    printf("%d enemy\n", val);
    printf("%d friendly\n", val2);
    if (friendlyStrength > enemyStrength) {
        free(enemyPiece);
        printf("Attacker(%d) won from defender(%d)\n", friendlyStrength, enemyStrength);
        board[enemyY][enemyX] = board[friendlyY][friendlyX];
        board[friendlyY][friendlyX] = NULL;
    } else if (friendlyStrength < enemyStrength) {
        free(friendlyPiece);
        printf("Attacker(%d) lost from defender(%d)\n", friendlyStrength, enemyStrength);
        board[friendlyY][friendlyX] = NULL;
    } else {
        free(friendlyPiece);
        free(enemyPiece);
        printf("Both attacker(%d) and defender(%d) died in combat\n", friendlyStrength, enemyStrength);
        board[enemyY][enemyX] = NULL;
        board[friendlyY][friendlyX] = NULL;

    }

}

Tile ***moveUp(Tile ***board, int xCord, int yCord) {
    if (yCord-1 <0){
        printf("You are trying to go outside of the board, think again.\n");
    } else {
        if (board[yCord-1][xCord] != NULL) {
            if (board[yCord -1][xCord]->enemy) {
                hitEnemy(board, xCord, yCord, xCord, yCord-1);
            } else if (!board[yCord-1][xCord ]->enemy) {
                printf("You are trying to attack a friendly force. Don't!\n");
            }
        } else {
            Tile *pawn = board[yCord][xCord];
            board[yCord][xCord] = NULL;
            board[yCord-1][xCord] = pawn;
        }
    }
    return board;
}

Tile*** moveDown(Tile*** board, int xCord, int yCord){
    if (yCord+1 >3){
        printf("You are trying to go outside of the board, think again.\n");
    } else {
        if (board[yCord+1][xCord] != NULL) {
            if (board[yCord +1][xCord]->enemy) {
                hitEnemy(board, xCord, yCord, xCord, yCord+1);
            } else if (!board[yCord+1][xCord ]->enemy) {
                printf("You are trying to attack a friendly force. Don't!\n");
            }
        } else {
            Tile *pawn = board[yCord][xCord];
            board[yCord][xCord] = NULL;
            board[yCord+1][xCord] = pawn;
        }
    }
    return board;
}

Tile ***moveLeft(Tile ***board, int xCord, int yCord) {
    if (xCord-1 <0){
        printf("You are trying to go outside of the board, think again.\n");
    } else {
        if (board[yCord][xCord - 1] != NULL) {
            if (board[yCord][xCord - 1]->enemy) {
                hitEnemy(board, xCord, yCord, xCord - 1, yCord);
            } else if (!board[yCord][xCord - 1]->enemy) {
                printf("You are trying to attack a friendly force. Don't!\n");
            }
        } else {
            Tile *pawn = board[yCord][xCord];
            board[yCord][xCord] = NULL;
            board[yCord][xCord - 1] = pawn;
        }
    }
    return board;
}

Tile*** moveRight(Tile*** board, int xCord, int yCord){
    if (xCord+1 >3){
        printf("You are trying to go outside of the board, think again.\n");
    } else {
        if (board[yCord][xCord + 1] != NULL) {
            if (board[yCord][xCord + 1]->enemy) {
                hitEnemy(board, xCord, yCord, xCord + 1, yCord);
            } else if (!board[yCord][xCord + 1]->enemy) {
                printf("You are trying to attack a friendly force. Don't!\n");
            }
        } else {
            Tile *pawn = board[yCord][xCord];
            board[yCord][xCord] = NULL;
            board[yCord][xCord + 1] = pawn;
        }
    }
    return board;
}

bool gameFinished(Tile ***board){
    int enemyPieceCount = 0;
    int friendlyPieceCount = 0;
    for (int i = 0; i < 4; ++i) {
        for (int j = 0; j < 4; ++j) {
            if(board[i][j] != NULL){
                Tile *piece = board[i][j];
                if (piece->enemy){
                    enemyPieceCount+=1;
                } else {
                    friendlyPieceCount +=1;
                }
            }
        }
    }
    if (enemyPieceCount == 0 && friendlyPieceCount ==0){
        printf("\nIt is a draw!\n");
        return true;
    }
    else if(enemyPieceCount == 0 ) {
        printf("\nCongratulations you won!\n");
        return true;
    } else if(friendlyPieceCount == 0){
        printf("\nYou lost!\n");
        return true;
    } else {
        return false;
    }
}

