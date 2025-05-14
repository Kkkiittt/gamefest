using UnityEngine;

public class Cook : Enemy
{
	public Cook()
	{
		Type = DeathCause.Cook;
		HP = 200;
		MaxHP = 200;
		Attack = 20;
		Turns = new()
		{
			new(Variable.EnemyHP, 50, Variable.None)
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