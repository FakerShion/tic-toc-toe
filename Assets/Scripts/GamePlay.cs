using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    private Board board;
    private bool gameOn = false;
    private int currentplayer = 1;
    private string currentsymbol = "O";
    private List<GameObject> chessSet;
    public bool OpenComputer = false;
    public Toggle com;

    void Start()
    {
        board = new Board();
        chessSet = new List<GameObject>();
        com.onValueChanged.AddListener(delegate {
            ToggleValueChanged(com);
        });

    }

    private void ToggleValueChanged(Toggle change)
    {
        OpenComputer = !OpenComputer;
    }

    // Update is called once per frame
    void Update()
    {
        //if (OpenComputer)
        //{
        //    print("open");
        //}
        
        if (gameOn)
        {
            if (OpenComputer && currentplayer == 2)
            {
                Vector2 axis = board.ComputerMove();
                var pos = GameObject.Find(axis.x+""+axis.y);
                GameObject chess = Instantiate(Resources.Load("Prefabs/" + currentsymbol), pos.transform) as GameObject;
                chessSet.Add(chess);
                checkEndGame();
                currentplayer = 1;
                currentsymbol = "O";
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                string re = mouseHit();
                if (re != "")
                {
                    char[] tmp = re.ToCharArray();
                    int i = int.Parse(tmp[0].ToString());
                    int j = int.Parse(tmp[1].ToString());
                   

                        print(i + " " + j);
                        bool success = board.Move(currentplayer, i, j);
                        if (success)
                        {
                            string targetname = i + "" + j;
                            var pos = GameObject.Find(targetname);
                            print(targetname + " " + "Prefabs/" + currentsymbol);
                            GameObject chess = Instantiate(Resources.Load("Prefabs/" + currentsymbol), pos.transform) as GameObject;
                            chessSet.Add(chess);
                            checkEndGame();
                            if (currentplayer == 1)
                            {
                                currentplayer = 2;
                                currentsymbol = "X";
                            }
                            else
                            {
                                currentplayer = 1;
                                currentsymbol = "O";
                            }
                        }

                }
                else
                {

                        print("请点击井中的空位");
                }

            }

        }
    }


    private string mouseHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        print(Input.mousePosition + " hit");
        if (hit.collider != null)
        {
            
            return hit.collider.transform.parent.name;
        }
        else
        {
            return "";
        }
    }

    private int roundVertical(float y)
    {
        if (y <= 436 && y > 307)
        {
            return 0;
        }
        else if (y <= 307 && y > 180)
        {
            return 1;
        }
        else if (y <= 180 && y >= 52)
        {
            return 2;
        }
        else
        {
            return -1;
        }
    }

    private int roundHorizontal(float x)
    {
        if(x>=263&&x < 410)
        {
            return 0;
        }else if (x>= 410 &&x < 554)
        {
            return 1;
        }
        else if (x>=554 &&x<= 696)
        {
            return 2;
        }
        else
        {
            return -1;
        }
 
    }

    private void checkEndGame()
    {
        int winner = board.CheckWinner();
        if (winner > 0)
        {
            print("胜利者为玩家" + winner);
            gameOn = false;
        }
        else if (winner == -2)
        {
            gameOn = false;
            print("平局");
        }
    }

    public void StartGame()
    {
        for(int i=0;i < chessSet.Count; i++)
        {
            Destroy(chessSet[i]);
        }

        board.Refresh();
        gameOn = true;
        currentplayer = 1;
        currentsymbol = "O";
        print("开始游戏");
       
    }

    public void EndGame()
    {
        //chessSet.Clear();
        
        //board.Refresh();
        //gameOn = false;
        //currentplayer = 1;
        //currentsymbol = "X";
    }
}
