using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Scriptable Objects/PlayerState")]
public class PlayerState : ScriptableObject
{
	public int PlayerHP;
	public int PlayerMaxHP;
	public int PlayerAttack;

	public Sprite Maid;
	public Sprite Cook;
	public Sprite Gardener;

	public bool Interacting = false;
	public int Sanity;
	public DeathCause deathCause = DeathCause.Alive;

	public bool CookKilled = false;
	public bool MaidKilled = false;
	public bool GardenerKilled = false;

	public HashSet<string> Inventory { get; set; } = new() { "Armor" };
	public Dictionary<string, (Variable, int)> BattleItems { get; set; } = new(){
		{ "Armor", (Variable.PlayerShield, 100) }
	};
	public List<Turn> Abilities { get; set; } = new();
}

public enum DeathCause
{
	Alive,
	Cook,
	Maid,
	Gardener,
}
