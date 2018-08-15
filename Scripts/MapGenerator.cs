using Godot;
using System.Collections;
using System;
namespace MapGenerator
{
    

public class MapGenerator {

	public int width;
	public int height;



	public int randomFillPercent = 40;

	int[,] map;

	public int[,] GenerateMap(int seed) {

		map = new int[width,height];
		RandomFillMap(seed);

		for (int i = 0; i < 5; i ++) {
			SmoothMap();
		}
        return map;
	}

    public MapGenerator(int _width, int _height, int _seed){

     width = _width;
	 height = _height;
     

    }

	void RandomFillMap(int seed) {
		

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (x <= 1 || x >= width-2 || y <= 1 || y >= height-2) {
					map[x,y] = 1;
				}
				else {
					map[x,y] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
				}
			}
		}
	}

	void SmoothMap() {
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				int neighbourWallTiles = GetSurroundingWallCount(x,y);

				if (neighbourWallTiles > 4)
					map[x,y] = 1;
				else if (neighbourWallTiles < 4)
					map[x,y] = 0;

			}
		}
	}

	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height) {
					if (neighbourX != gridX || neighbourY != gridY) {
						wallCount += map[neighbourX,neighbourY];
					}
				}
				else {
					wallCount ++;
				}
			}
		}

		return wallCount;
	}


}
}