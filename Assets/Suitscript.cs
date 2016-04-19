using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
public class Suitscript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	GameObject suit;
	int id;
	int player;
	GameSettings gs;
	bool entered;

	// Use this for initialization
	void Start () {
		gs = GameObject.Find("Initialize").GetComponent<GameSettings>();
		string begin;
		if(this.name[0] == 'A'){
			begin = "red";
			player = 0;
		}else{
			begin = "blue";
			player = 1;
		}
		id = int.Parse(this.name.Substring(this.name.Length - 1));
		this.suit = GameObject.Find(begin + "Suit" + id);
		suit.SetActive(false);
		entered = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(gs.playerSpriteIndex[player] == id){
			suit.GetComponent<Image>().color = new Color(1,1,1,1);
			suit.SetActive(true);
		}else if (!entered){
			suit.SetActive(false);
		}
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		suit.SetActive(true);
		entered = true;
		suit.GetComponent<Image>().color = new Color(0.8f,0.8f,0.8f,0.4f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		entered = false;
		suit.SetActive(false);
	}
}

