using UnityEngine;

public class MaidCreator : MonoBehaviour
{
	public PlayerState state;
	public GameObject Maid;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		Maid.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if(state.CookKilled && state.GardenerKilled &&!state.MaidKilled)
		{
			Maid.SetActive(true);
		}
	}
}
