using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour
{
    private int[,] board = new int[3, 3];
    private int left = 9;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 ComputerMove()
    {
        List<Vector2> remain = new List<Vector2>();
        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (board[i,j] == 0)
                {
                    remain.Add(new Vector2(i, j));
                }
            }
        }
        System.Random rnd = new System.Random();
        
        int random = rnd.Next(0, remain.Count);
        //Vector2 re = remain[random];
        Vector2 re = Reward(remain);
        board[(int)re.x, (int)re.y] = 2;
        left -= 1;
        return re;
    }

    public Vector2 Reward(List<Vector2> remain)
    {
        int bestIdx = 0;
        int highest = -20;

        for(int i = 0; i< remain.Count; i++)
        {
            int x = (int)remain[i].x;
            int y = (int)remain[i].y;
            int re = CheckFinish(2,1, x, y);
            if (re > highest)
            {
                highest = re;
                bestIdx = i;
            }

        }
      //print(highest + "highest " + bestIdx);
        return remain[bestIdx];
    }

    private int CheckFinish(int add, int minus, int x, int y)
    {
        int countaddx = 0;
        int countaddy = 0;
        int countaddz = 0;
        int countaddt = 0;
        int countminusx = 0;
        int countminusy = 0;
        int countminusz = 0;
        int countminust = 0;
        int counts = 0;
       
        for (int i=0; i < 3; i++)
        {
            //横
            if (x != i)
            {
                if (board[i, y]== add)
                {
                    countaddx += 1;
                    counts += 1;
                }
                else if (board[i, y] == minus)
                {
                    countminusx += 1;
                    counts -= 1;
                }
                //斜1
                if (x == y)
                {
                    if (board[i, i] == add)
                    {
                        countaddz += 1;
                        counts += 1;
                    }
                    else if (board[i, i] == minus)
                    {
                        countminusz += 1;
                        counts -= 1;
                    }
                   
                }
               
                //斜2
                if (x + y == 2)
                {
                    if (board[i, 2 - i] == add)
                    {
                        countaddt += 1;
                        counts += 1;
                    }
                    else if (board[i, 2 - i] == minus)
                    {
                        countminust += 1;
                        counts -= 1;
                    }
                  
                }
               
            }
            //竖
            if (y != i)
            {
                if (board[x, i] == add)
                {
                    countaddy += 1;
                    counts += 1;
                }
                else if(board[x, i] == minus)
                {
                    countminusy += 1;
                    counts -= 1;
                }
            }
           
        }
        if (countaddx==2 || countaddy==2 || countaddz ==2|| countaddt==2)
        {
            counts += 20;
           
        }
        if (countminusx==2 || countminusy == 2 || countminusz == 2|| countminust == 2)
        {
            counts += 10;
           
        }

        return counts;
    }


        public bool Move(int value,int row,int col)
    {
        if (board[row,col] == 0 )
        {
            board[row,col] = value;
            left -= 1;
            return true;
        }
        else
        {
            return false;
            print("已经下了棋子。");
        }
    }

    public void Refresh()
    {
        board = new int[3, 3];
        left = 9;
    }

    public int CheckWinner()
    {
        //Debug.Log(board);
        for(int i= 0; i<3;i++)
        {
            if (board[i,0] == board[i,1] && board[i,1] == board[i,2])
            {
                return board[i,0];
            }
            if (board[0,i] == board[1,i] && board[1, i]== board[2,i])
            {
                return board[0,i];
            }

        }
        if ((board[0,0] == board[1,1] && board[1,1] == board[2,2]) ||
            (board[0,2] == board[1,1] && board[1,1] == board[2,0]))
        {
            return board[1,1];
        }
        //print("left " + left);
        if(left == 0)
        {
            return -2; 
        }

        return -1; 
      
    }

}
