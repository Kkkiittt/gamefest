using System.Collections.Generic;

using NUnit.Framework;

using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public PlayerState State;

	void Start()
	{
		State.Interacting = false;
		State.PlayerHP = 100;
		State.PlayerMaxHP = 100;
		State.PlayerAttack = 15;
		State.MaidKilled = false;
		State.GardenerKilled = false;
		State.CookKilled = false;
	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log(State.Interacting);
		if(Input.GetKey("w") && !State.Interacting)
		{
			transform.position += new Vector3(0, 5, 0) * Time.deltaTime;
		}
		if(Input.GetKey("s") && !State.Interacting)
		{
			transform.position += new Vector3(0, -5, 0) * Time.deltaTime;
		}
		if(Input.GetKey("a") && !State.Interacting)
		{
			transform.position += new Vector3(-5, 0, 0) * Time.deltaTime;
		}
		if(Input.GetKey("d") && !State.Interacting)
		{
			transform.position += new Vector3(5, 0, 0) * Time.deltaTime;
		}
	}

	public void OnCollisionStay2D(Collision2D other)
	{
		//Debug.Log(other.tag);
		if(other.collider.tag == "NPC" && Input.GetKey("f") && !State.Interacting)
		{
			Debug.Log("Interact");
			State.Interacting = true;
			var npc = other.gameObject.GetComponent<NPC>();
			npc.Interact();

		}
		if(other.collider.tag == "ITEM" && Input.GetKey("f"))
		{
			Debug.Log("Pickup");
			//Pickup
			var item = other.gameObject.GetComponent<Item>();
			State.Inventory.Add(item.Name);
			Destroy(other.gameObject);
		}
		if(other.collider.tag == "ENEMY")
		{
			Debug.Log("Enemy");

			if(Input.GetKey("f"))
			{
				Debug.Log("Attack");

			}
		}
	}
}
