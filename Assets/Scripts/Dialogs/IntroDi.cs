using System;
using System.Collections.Generic;

using UnityEngine;

public class Dvor1 : MonoBehaviour
{
	public GameObject ObjectManager;
	private CanvaManager manager;
	public PlayerState state;

	public int phraseCurrent { get; set; } = 1;
	public int phrasePrevious { get; set; } = 0;
	public int optionCurrent { get; set; } = 0;
	public bool choose { get; set; } = false;
	public bool skip { get; set; } = false;
	public bool inc { get; set; } = false;

	public List<string> Phrases = new()
	{
		"доброе утро!",
		"и вам доброе",
		"вам чем то помочь? ",
		"да, не знаешь где здесь выход?",
		"отсюда прямо, как выйдите в сад продолжайте по тропинке до ворот",


	};
	public List<(string opt1, string opt2, int val1, int val2)> Options = new()
	{
		("Thanks good", "Fuck you", 0, 0),
		("Byee", "I hate you", 0, 0)
	};

	void Start()
	{
		Debug.Log(Options.Count);
		state.Interacting = true;
		ObjectManager = GameObject.Find("CanvaManager");
		manager = ObjectManager.GetComponent<CanvaManager>();
		manager.Activate(0);
		manager.Activate(1, false);
		manager.Activate(2, false);
		manager.Activate(3, false);
		manager.Activate(4, false);
		manager.Activate(5, false);
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log(phraseCurrent);
		if(phraseCurrent > Phrases.Count)
		{
			Destroy(gameObject);
			manager.Activate(0, false);
			return;
		}
		if(phraseCurrent != phrasePrevious)
		{
			phrasePrevious = phraseCurrent;
			string txt = Phrases[phraseCurrent - 1];
			if(txt == "|")
			{
				optionCurrent++;
				manager.Activate(1);
				manager.Activate(2);
				manager.SetText(1, "1 " + Options[optionCurrent - 1].opt1);
				manager.SetText(2, "2 " + Options[optionCurrent - 1].opt2);
				choose = true;
			}
			else
			{
				manager.Activate(1, false);
				manager.Activate(2, false);
				if(skip)
				{
					skip = false;
					inc = false;
				}
				else
				{
					manager.SetText(0, txt);
				}
				if(inc)
				{
					phraseCurrent++;
					skip = true;
				}
			}
		}
		else
		{
			if(choose)
			{
				int opt = 0;
				if(Input.GetKey("1"))
					opt = 1;
				if(Input.GetKey("2"))
					opt = 2;
				if(opt != 0)
				{
					choose = false;
					manager.Activate(1, false);
					manager.Activate(2, false);
					phraseCurrent += opt;
					if(opt == 1)
					{
						state.Sanity += Options[optionCurrent - 1].val1;
						inc = true;
					}
					else
						state.Sanity += Options[optionCurrent - 1].val2;
				}
			}
			else
			{
				if(Input.GetKeyDown("space"))
				{
					choose = false;
					phraseCurrent++;
				}
			}
		}
	}
}