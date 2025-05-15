using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomChooseHall : MonoBehaviour
{
	public CanvaManager manager;
	// S
	// tart is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		manager = GameObject.Find("CanvaManager").GetComponent<CanvaManager>();
		manager.Activate(0);
		manager.Activate(1);
		manager.Activate(2);
		manager.Activate(3);
		manager.SetText(0, "Choose Room");
		manager.SetText(1, "1. Kitchen");
		manager.SetText(2, "2. Cabinet");
		manager.SetText(3, "3. Garden");
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown("1"))
		{
			SceneManager.LoadScene("Kitchen");
		}
		if(Input.GetKeyDown("2"))
		{
			SceneManager.LoadScene("Cabinet");
		}
		if(Input.GetKeyDown("3"))
		{
			SceneManager.LoadScene("Garden");
		}
	}
}
