using UnityEngine;

public class Gardener : Enemy
{
	public Gardener()
	{
		Type = DeathCause.Gardener;
		HP = 100;
		MaxHP = 100;
		Attack = 15;
		Turns = new()
		{
			new(Variable.PlayerHP, -30, Variable.None)
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
	}
}