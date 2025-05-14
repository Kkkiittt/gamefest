using UnityEngine;

public class NPC : MonoBehaviour
{

	public GameObject Canva;
	public bool Interacting = true;

	public PlayerState State;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if(Interacting && Input.GetKey("escape") && State.Inventory.Contains("Key"))
		{
			Interacting = false;
			State.Interacting = false;
			Canva.SetActive(false);
		}
	}

	public void Interact()
	{
		Interacting = true;
		Canva.SetActive(true);
	}
}
