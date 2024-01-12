//
// Created by User on 24/11/2023.
//

#ifndef BOARD_H
#define BOARD_H

#include "../tile/tile.h"

int RandomNumberGenerator();

void printBoard(Tile*** board, int boardWith);

Tile*** generateBoard(int boardWith);

void hitEnemy(Tile*** board, int friendlyX, int friendlyY, int enemyX, int enemyY);

Tile ***moveUp(Tile ***board, int xCord, int yCord);

Tile*** moveDown(Tile*** board, int xCord, int yCord);

Tile ***moveLeft(Tile ***board, int xCord, int yCord);

Tile*** moveRight(Tile*** board, int xCord, int yCord);

bool gameFinished(Tile ***board);

#endif
