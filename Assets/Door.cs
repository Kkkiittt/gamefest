using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
	public string Scene;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	private void OnTriggerStay2D(Collider2D collision)
	{
		if(Input.GetKeyDown("f") && collision.tag == "Player")
		{
			SceneManager.LoadScene(Scene);
		}
	}
}
