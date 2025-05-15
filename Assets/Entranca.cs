using UnityEngine;

public class Entranca : MonoBehaviour
{
	public GameObject Wall;
	public PlayerState state;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.tag == "Player" && state.GardenerKilled && state.MaidKilled && state.CookKilled)
		{
			Wall.SetActive(false);
		}
	}
}
