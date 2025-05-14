using System.Collections.Generic;

using UnityEngine;

public class Maid : Enemy
{
	public List<Vector3> Places { get; set; } = new()
	{
		new(10,  10, 0),
		new(-10,  10, 0),
		new(10, -10, 0),
		new(-10, -10, 0),
	};
	public Vector3 CurrentPlace { get; set; }

	public Maid()
	{
		Type = DeathCause.Maid;
		HP = 100;
		MaxHP = 100;
		Attack = 10;
		Turns = new()
		{
			new(Variable.PlayerPureHP, -2, Variable.EnemyAttack),
		};
	}

	public override void Update()
	{
		var playerPos = GameObject.FindWithTag("Player").transform.position;
		if((transform.position - playerPos).magnitude < SightDistance && !fighting)
		{
			transform.position = Vector3.MoveTowards(transform.position, playerPos, Speed * Time.deltaTime);
			return;
		}
		if((transform.position - CurrentPlace).magnitude < 0.1f)
		{
			CurrentPlace = Places[Random.Range(0, Places.Count)];
			Debug.Log(CurrentPlace);
		}
		if(!fighting)
			transform.position = Vector3.MoveTowards(transform.position, CurrentPlace, Speed * Time.deltaTime);
	}
}