using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // 10*10のint型2次元配列を定義
    public int[,] Board = new int[10, 10];

    // EMPTY=0, WHITE=1, BLACK=2で定義
    public const int EMPTY = 0;
    public const int WHITE = 1;
    public const int BLACK = 2;

    private int BoardMax = 10;
    public int VictoryCount = 5;

    //プレハブ達
    public GameObject WhiteStone;
    public GameObject BlackStone;

    // カメラ情報
    private Camera camera_object;
    private RaycastHit hit;

    // 現在のプレイヤー(初期プレイヤーは白)
    private int CurrentPlayer = WHITE;

    // どっちの色が勝ったか
    private bool VictorColor;

    // ターン表示テキスト
    public Text trunWhite; //白ターン表示
    public Text trunBlack; //黒ターン表示
    public Text win; //勝利者表示

    // ターン表示の定型文
    private string WhiteTrun = "白のターン";
    private string BlackTrun = "黒のターン";

    private string WhiteWin = "白の勝ち";
    private string BlackWin = "黒の勝ち";

    void Start()
    {
        trunWhite.text = WhiteTrun; //ターン表示テキストの初期値を代入
        trunBlack.GetComponent<Text>().enabled = false;

        InitializeArray(); //配列を初期化

        DebugArray(); //デバック用

        // カメラの情報を取得
        camera_object = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        // 石が揃っているか確認
        if(CheckStone(WHITE) || CheckStone(BLACK))
        {
            return;
        }

        if (Input.GetMouseButtonDown(0)) //マウス押したら
        {
            MouseClick(); //マウスをクリックしたら
        }
    }

    void InitializeArray() //配列を初期化
    {
        for (int i = 0; i < BoardMax; i++) //for文を利用して配列にアクセス
        {
            for (int j = 0; j < BoardMax; j++) 
            {
                Board[i,j] = EMPTY; //配列を空(値を0)にする
            }
        }
    }

    void DebugArray() //デバック用
    {

        if(Input.GetKey(KeyCode.Escape)) //エスケープで終了
        {
            Application.Quit();
        }

        // 配列情報を確認する
        for (int i = 0; i < BoardMax; i++) //for文を利用して配列にアクセス
        {
            for (int j = 0; j < BoardMax; j++)
            {
               // Debug.Log("(i,j) = (" + i + "," + j + ") = " + Board[i, j]);
            }
        }
    }
    
    void MouseClick() //マウスをクリックしたら
    {
        // マウスのポジションを取得してRayに代入
        Ray ray = camera_object.ScreenPointToRay(Input.mousePosition);

        // マウスのポジションからRayを投げて何かに当たったらhitに入れる
        if (Physics.Raycast(ray, out hit))
        {
            // x,Zの値を取得
            int x = (int)hit.collider.gameObject.transform.position.x;
            int z = (int)hit.collider.gameObject.transform.position.z;

            // マスが空の時
            if (Board[z, x] == EMPTY)
            {
                // 白のターンの時
                if (CurrentPlayer == WHITE)
                {
                    // Boardの値を更新
                    Board[z, x] = WHITE;

                    // Stoneを出力
                    GameObject stone = Instantiate(WhiteStone);
                    stone.transform.position = hit.collider.gameObject.transform.position;

                    // 黒プレイヤーに交代
                    CurrentPlayer = BLACK;

                    // ターンの交代
                    trunWhite.GetComponent<Text>().enabled = false;
                    trunBlack.GetComponent<Text>().enabled = true;

                    trunBlack.text = BlackTrun;
                }
                else if (CurrentPlayer == BLACK)
                {
                    Board[z, x] = BLACK;

                    GameObject stone = Instantiate(BlackStone);
                    stone.transform.position = hit.collider.gameObject.transform.position;

                    CurrentPlayer = WHITE;

                    // ターンの交代
                    trunWhite.GetComponent<Text>().enabled = true;
                    trunBlack.GetComponent<Text>().enabled = false;

                    
                    trunWhite.text = WhiteTrun;

                }
            }
        }
    }

    private bool CheckStone(int color) //縦横ななめの判定
    {
        // どっちかが勝ったら
        if (VictorColor == true)
        {
            win.GetComponent<Text>().enabled = true; //どっちかが勝ったら表示

            //白のとき
            if (color == WHITE)
            {
                Debug.Log("白の勝ち！！！");
                win.text = WhiteWin;
            }
            //黒のとき
            else
            {
                Debug.Log("黒の勝ち！！！");
                win.text = BlackWin;
            }
        }
        else
        {
            win.GetComponent<Text>().enabled = false;　//普通は非表示
        }
        

        //碁石の数をカウントする
        int count = 0;

        #region 横向き
        for (int i = 0; i < BoardMax; i++)
        {
            for (int j = 0; j < BoardMax; j++)
            {
                //squaresの値が空のとき
                if (Board[i, j] == EMPTY || Board[i, j] != color)
                {
                    //countを初期化する
                    count = 0;
                }
                else
                {
                    //countにsquaresの値を格納する
                    count++;
                }

                //countの値が勝利数になったとき
                if (count == VictoryCount)
                {
                    VictorColor = true;

                    return true;
                }
            }
        }
        #endregion

        //countの値を初期化
        count = 0;

        #region 縦向き
        for (int i = 0; i < BoardMax; i++)
        {
            for (int j = 0; j < BoardMax; j++)
            {
                //squaresの値が空のとき
                if (Board[j, i] == EMPTY || Board[j, i] != color)
                {
                    //countを初期化する
                    count = 0;
                }
                else
                {
                    //countにsquaresの値を格納する
                    count++;
                }

                //countの値が勝利数になったとき
                if (count == VictoryCount)
                {
                    VictorColor = true;

                    return true;
                }
            }
        }

        #endregion

        //countの値を初期化
        count = 0;

        #region 斜め(右上がり)
        for (int i = 0; i < BoardMax; i++)
        {
            //上移動用
            int up = 0;

            for (int j = i; j < BoardMax; j++)
            {
                //squaresの値が空のとき
                if (Board[j, up] == EMPTY || Board[j, up] != color)
                {
                    //countを初期化する
                    count = 0;
                }
                else
                {
                    count++;
                }

                //countの値が勝利数になったとき
                if (count == VictoryCount)
                {
                    VictorColor = true;

                    return true;
                }

                up++;
            }
        }

        #endregion

        //countの値を初期化
        count = 0;

        #region 斜め(右下がり)
        for (int i = 0; i < BoardMax; i++)
        {
            //下移動用
            int down = 9;

            for (int j = i; j < BoardMax; j++)
            {
                //squaresの値が空のとき
                if (Board[j, down] == EMPTY || Board[j, down] != color)
                {
                    //countを初期化する
                    count = 0;
                }
                else
                {
                    count++;
                }

                //countの値が勝利数になったとき
                if (count == VictoryCount)
                {
                    VictorColor = true;

                    return true;
                }

                down--;
            }
        }
        #endregion

        //countの値を初期化
        count = 0;

        #region 斜め(左上がり)

        for (int i = 0; i < BoardMax; i++)
        {
            // 上移動用
            int up = 0;

            for (int j = i; j < BoardMax; j++)
            {
                if (Board[up, j] == EMPTY || Board[up, j] != color)
                {
                    //countを初期化する
                    count = 0;
                }
                else
                {
                    count++;
                }

                //countの値が勝利数になったとき
                if (count == VictoryCount)
                {
                    VictorColor = true;

                    return true;
                }

                up++;
            }
        }

        #endregion

        //countの値を初期化
        count = 0;

        #region 斜め(左下がり)
        for (int i = 0; i < BoardMax; i++)
        {
            // 上移動用
            int down = 9;

            for (int j = i; j < BoardMax; j++)
            {
                if (Board[down, j] == EMPTY || Board[down, j] != color)
                {
                    //countを初期化する
                    count = 0;
                }
                else
                {
                    count++;
                }

                //countの値が勝利数になったとき
                if (count == VictoryCount)
                {
                    VictorColor = true;

                    return true;
                }

                down--;
            }
        }
        #endregion

        //まだ判定がついていないとき
        return false;
    }  
}
