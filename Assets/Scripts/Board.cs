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
        Vector2 re = remain[random];
        board[(int)re.x, (int)re.y] = 2;
        left -= 1;
        return re;
    }

    public Vector2 Reward(List<Vector2> remain)
    {
        int best = 0;

        for(int i = 0; i< remain.Count; i++)
        {
            cost = remain[i].x
        }
        return remain[best];
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
        print("left " + left);
        if(left == 0)
        {
            return -2; 
        }

        return -1; 
      
    }

}
