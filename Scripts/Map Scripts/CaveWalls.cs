using Godot;
using System;

public class CaveWalls : TileMap
{
    /*
    CENTER - 6
    TOP - 10
    LEFT - 7
    RIGHT - 8
    BOT - 5
    TOPL - 1
    TOPR - 2
    BOTL - 3
    BOTR - 4
    TOPLC - 12
    TOPRC - 13
    BOTLC - 11
    BOTRC - 9
    */


    //Updating All Cells
    public void UpdateCells(int MapBorderX, int MapBorderY){

    GD.Print("Updating Cells...");
    

     for (int y = 0; y <= MapBorderY; y++)
     {
        for (int x = 0; x <= MapBorderX; x++)
        { 
         if(GetCell(x,y) == -1) continue;

         switch (CheckCellAdj(x,y))
         {  
             //center
             case 15 :
              switch (CheckCellDiag(x,y))
              {
                  //TOPRC
                  case 11:
                  SetCell(x,y,13);
                  break;

                  //TOPLC
                  case 13:
                  SetCell(x,y,12);
                  break;

                  //BOTRC
                  case 14:
                  SetCell(x,y,9);
                  break;

                  //BOTLC
                  case 7:
                  SetCell(x,y,11);
                  break;

                  default:
                  SetCell(x,y,6);
                  break;
              }
             
             break;
             //empty
             case 0:
             break;
             // top
             case 14 :
             SetCell(x,y,10);
             break;
             //left
             case 13 :
             SetCell(x,y,7);
             break;
             //top left
             case 12 :
             SetCell(x,y,1);
             break;
             //Right
             case 11 :
             SetCell(x,y,8);
             break;
             //top right
             case 10 :
             SetCell(x,y,2);
             break;
             //bottom
             case 7 :
             SetCell(x,y,5);
             break;
             //bottom left
             case 5 :
             SetCell(x,y,3);
             break;
             //bottom right
             case 3 :
             SetCell(x,y,4);
             break;

             default:
             GD.Print("Woopsie");
             break;
         }


        }

         
     }
        GD.Print("Cells Updated!");
    }

    //Checking 4 joined cells 
    public int CheckCellAdj(int x, int y){

         int LeftN = (GetCell(x - 1, y) !=-1 ? GetCell(x - 1, y):0) > 0 ? 2:0;
         int RightN = (GetCell(x + 1, y) !=-1 ? GetCell(x + 1, y):0) > 0 ? 4:0;
         int BotN = (GetCell(x, y + 1) !=-1 ? GetCell(x, y + 1):0) > 0 ? 8:0;
         int TopN = (GetCell(x, y - 1) !=-1 ? GetCell(x, y - 1):0) > 0 ? 1:0;
         int fin = LeftN + RightN + BotN + TopN;
        return fin;

    }
    //Checking 4 diagonally placed cells
    public int CheckCellDiag(int x, int y){

         int LeftTopN = (GetCell(x - 1, y - 1) !=-1 ? GetCell(x - 1, y):0) > 0 ? 2:0;
         int RightTopN = (GetCell(x + 1, y - 1) !=-1 ? GetCell(x + 1, y):0) > 0 ? 4:0;
         int LeftBotN = (GetCell(x - 1, y + 1) !=-1 ? GetCell(x, y + 1):0) > 0 ? 8:0;
         int RightBotN = (GetCell(x + 1, y + 1) !=-1 ? GetCell(x, y - 1):0) > 0 ? 1:0;
         int fin = LeftTopN + RightTopN + LeftBotN + RightBotN;
        return fin;

    }

}


