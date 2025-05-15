using UnityEngine;

public class NPC : MonoBehaviour
{

	public PlayerState State;
	public GameObject Dialog;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void Interact()
	{
		State.Interacting = true;
		Instantiate(Dialog);
	}
}
