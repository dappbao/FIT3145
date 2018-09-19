using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleManager : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    private bool p1WindFlag;
    private bool p2WindFlag;
	// Use this for initialization
	void Start () {
        this.p1WindFlag = false;
        this.p2WindFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 6; i++)
        {
            if (player1.GetComponent<Player1>().inventory[i] >= 10)
            {
                player1.GetComponent<Player1>().inventory[i] -= 10;
                switch (i)
                {
                    case 0:
                        player1.GetComponent<Player1>().hp *= 2;
                        break;
                    case 1:
                        player2.GetComponent<Player2>().hp -= 6;
                        break;
                    case 2:
                        player1.GetComponent<Player1>().hp += 6;
                        break;
                    case 3:
                        p1WindFlag = true;
                        break;
                    case 4:
                        player1.GetComponent<Player1>().hp += 3;
                        player2.GetComponent<Player2>().hp -= 3;
                        break;
                    case 5:
                        player2.GetComponent<Player2>().hp = (int)(player2.GetComponent<Player2>().hp / 2);
                        break;
                }
            }
                
            if (player2.GetComponent<Player2>().inventory[i] >= 10)
            {
                player2.GetComponent<Player2>().inventory[i] -= 10;
                switch (i)
                {
                    case 0:
                        player2.GetComponent<Player2>().hp *= 2;
                        break;
                    case 1:
                        player1.GetComponent<Player1>().hp -= 6;
                        break;
                    case 2:
                        player2.GetComponent<Player2>().hp += 6;
                        break;
                    case 3:
                        p2WindFlag = true;
                        break;
                    case 4:
                        player1.GetComponent<Player1>().hp -= 3;
                        player2.GetComponent<Player2>().hp += 3;
                        break;
                    case 5:
                        player1.GetComponent<Player1>().hp = (int)(player1.GetComponent<Player1>().hp / 2);
                        break;
                }
            }
        }

        if (p1WindFlag)
        {
            for (int i = 0; i < 6; i++)
            {
                player2.GetComponent<Player2>().inventory[i] = (int)(player2.GetComponent<Player2>().inventory[i] / 2);
            }
        }

        if (p2WindFlag)
        {
            for (int i = 0; i < 6; i++)
            {
                player1.GetComponent<Player1>().inventory[i] = (int)(player1.GetComponent<Player1>().inventory[i] / 2);
            }
        }

        p1WindFlag = false;
        p2WindFlag = false;
	}
}
