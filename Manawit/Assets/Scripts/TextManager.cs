using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public GameObject manager;
    private Text[] p1Inventory;
    private Text[] p2Inventory;
    private Text p1HP;
    private Text p2HP;
    private Text timeleft;
	// Use this for initialization
	void Start () {
        p1Inventory = new Text[]{
            this.gameObject.transform.Find("P1Light").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P1Fire").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P1Water").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P1Wind").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P1Earth").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P1Dark").gameObject.GetComponent<Text>()
        };
        p2Inventory = new Text[]{
            this.gameObject.transform.Find("P2Light").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P2Fire").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P2Water").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P2Wind").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P2Earth").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P2Dark").gameObject.GetComponent<Text>()
        };
        p1HP = this.gameObject.transform.Find("P1HP").gameObject.GetComponent<Text>();
        p2HP = this.gameObject.transform.Find("P2HP").gameObject.GetComponent<Text>();
        timeleft=this.gameObject.transform.Find("Timeleft").gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 6; i++)
        {
            p1Inventory[i].text = player1.GetComponent<Player1>().inventory[i].ToString();
            p2Inventory[i].text = player2.GetComponent<Player2>().inventory[i].ToString();
            p1HP.text = player1.GetComponent<Player1>().hp.ToString();
            p2HP.text = player2.GetComponent<Player2>().hp.ToString();
            timeleft.text = ((int)(manager.GetComponent<Generating>().time)).ToString();
        }
	}
}
