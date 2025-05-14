using System.Collections.Generic;


using UnityEngine;

public class Dialog : MonoBehaviour
{
	public GameObject Canva;
	public GameObject Text;
	public GameObject Option1;
	public GameObject Option2;
	public GameObject Option3;
	public GameObject Option4;
	public GameObject Option5;

	public List<GameObject> Options = new();

	public int CurrentPhrase = 0;
	public int PreviousPhrase = 0;

	void Start()
	{
		Canva.SetActive(true);
		Options.Add(Option1);
		Options.Add(Option2);
		Options.Add(Option3);
		Options.Add(Option4);
		Options.Add(Option5);
	}

	void Update()
	{
		
	}
}
