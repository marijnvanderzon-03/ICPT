#include "demo.h"
#include <iostream>

int main() {
    for (int i = 0; i < 10; ++i)
    {
        if (i%2 != 0)
        {
            continue;
        }
        std::cout << i << "\n";
    }

    goto label;
    std::cout << "overgeslagen code\n";

    label:
    {
        std::cout << "uitgevoerde code\n";
    }

    for (int i = 0; i< 10; i++){
        std::cout << i << "\n";
    }

    int n = 0;
    do
    {
        std::cout << n << "\n";
        n++;
    }
    while (n < 10);

    while (n < 10)
    {
        std::cout << n << "\n";
        n++;
    }


//selection statements
    switch (n) {
        case 0:
            std::cout << "n is precies 0\n";
            break;
        case 10:
            std::cout << "n is precies 10\n";
            break;
        default:
            std::cout << "n is geen 0 of 10\n";
    }

    if (n < 0)
    {
        std::cout << "n is negatief\n";
    }
    else if (n > 10) {
        std::cout << "n is groter dan 10\n";
    }
    else
    {
        std::cout << "n is tussen 0 en 10\n";
    }

    return 1;
}