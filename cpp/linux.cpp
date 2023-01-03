#include <iostream>
#include <list>
#include <stdio.h>
#include <stdlib.h>
#include <fstream>
#include <string>
using namespace std;

list<string> splashscreen = {
    "FFF000FFF00FFF",
    "F0F00F000000F0",
    "FFF00F000000F0",
    "F0000F000000F0",
    "F00000FFF00FFF"
};

string CEND = "\u001b[0m";

string CBLACK = "\u001b[40m";
string CRED = "\u001b[41m";
string CGREEN = "\u001b[42m";
string CYELLOW = "\u001b[43m";
string CBLUE = "\u001b[44m";
string CMAGENTA = "\u001b[45m";
string CCYAN = "\u001b[46m";
string CWHITE = "\u001b[47m";
string CBBLACK = "\u001b[40;1m";
string CBRED = "\u001b[41;1m";
string CBGREEN = "\u001b[42;1m";
string CBYELLOW = "\u001b[43;1m";
string CBBLUE = "\u001b[44;1m";
string CBMAGENTA = "\u001b[45;1m";
string CBCYAN = "\u001b[46;1m";
string CBWHITE = "\u001b[47;1m";

list<string> processLines(string filepath) {
    list<string> data;
    string rawData;
    ifstream file;

    file.open(filepath);
    if (file.is_open()) {
        char character;
        while (file) {
            character = file.get();
            //rawData += character;
            cout << character;
        }
    }
}

int keypress() {
  system ("/bin/stty raw");
  int c;
  system("/bin/stty -echo");
  c = getc(stdin);
  system("/bin/stty echo");
  system ("/bin/stty cooked");
  return c;
}

int main() {
    // TODO: Print splashscreen

    while(true){
        cout << "1 | Renderer\n2 | Editor\ne | exit" << endl;
        int key = keypress();
        system("clear");
        if(key == 49) {
            string filepath;
            cout << "Filepath: ";
            cin >> filepath;
            processLines(filepath);
        } else if(key == 50) {

        } else if(key == 69 || key == 101) {
            break;
        }
    }

    return 0;
}