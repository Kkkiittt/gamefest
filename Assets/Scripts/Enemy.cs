using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
	public int HP;
	public int MaxHP;
	public int Attack;
	public float SightDistance;
	public float Speed;
	public bool fighting = false;
	public PlayerState State;

	public GameObject Fight;

	public GameObject Canva;
	public GameObject Text;
	public GameObject Option1;
	public GameObject Option2;
	public GameObject Option3;
	public GameObject Option4;
	public GameObject Option5;
	public GameObject OptionText1;
	public GameObject OptionText2;
	public GameObject OptionText3;
	public GameObject OptionText4;
	public GameObject OptionText5;

	public DeathCause Type;

	public List<Turn> Turns = new List<Turn>();
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	public virtual void Update()
	{

	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.tag == "Player" && !State.Interacting)
		{
			State.Interacting = true;
			fighting = true;
			Canva.SetActive(true);
			Text.SetActive(true);
			Option1.SetActive(true);
			Option2.SetActive(true);
			Option3.SetActive(true);
			Option4.SetActive(true);
			Option5.SetActive(true);
			OptionText1.SetActive(true);
			OptionText2.SetActive(true);
			OptionText3.SetActive(true);
			OptionText4.SetActive(true);
			OptionText5.SetActive(true);
			var fight = Instantiate(Fight);
			fight.GetComponent<Fight>().enemy = this;
		}
	}
}
