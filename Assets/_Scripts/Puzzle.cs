// NULLcode Studio © 2015
// null-code.ru

using UnityEngine;
using System.Collections;

public class Puzzle : MonoBehaviour {

	public int ID; // номер должен соответствовать данной "костяшки"
    [SerializeField] AudioSource audioRock;
	// текущая и пустая клетка, меняются местами
	void ReplaceBlocks(int x, int y, int XX, int YY)
	{
		GameControl.grid[x,y].transform.position = GameControl.position[XX,YY];
		GameControl.grid[XX,YY] = GameControl.grid[x,y];
		GameControl.grid[x,y] = null;
		GameControl.click++;
		GameControl.GameFinish();
	}

	// определение пустой клетки, рядом с текущей, по горизонтали или вертикали
	void OnMouseDown()
	{
        if (!audioRock.isPlaying)
        {
            audioRock.Play();
        }
        else
        {
            audioRock.Stop();
            audioRock.Play();
        }

		for(int y = 0; y < 3; y++)
		{
			for(int x = 0; x < 3; x++)
			{
				if(GameControl.grid[x,y])
				{
					if(GameControl.grid[x,y].GetComponent<Puzzle>().ID == ID)
					{
						if(x > 0 && GameControl.grid[x-1,y] == null)
						{
							ReplaceBlocks(x,y,x-1,y);
							return;
						}
						else if(x < 2 && GameControl.grid[x+1,y] == null)
						{
							ReplaceBlocks(x,y,x+1,y);
							return;
						}
					}
				}
				if(GameControl.grid[x,y])
				{
					if(GameControl.grid[x,y].GetComponent<Puzzle>().ID == ID)
					{
						if(y > 0 && GameControl.grid[x,y-1] == null)
						{
							ReplaceBlocks(x,y,x,y-1);
							return;
						}
						else if(y < 2 && GameControl.grid[x,y+1] == null)
						{
							ReplaceBlocks(x,y,x,y+1);
							return;
						}
					}
				}
			}
		}
	}
}
