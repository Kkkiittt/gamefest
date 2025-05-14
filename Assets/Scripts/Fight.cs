using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEditor.SearchService;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
	private GameObject Canva;
	private GameObject Text;
	private GameObject Option1;
	private GameObject Option2;
	private GameObject Option3;
	private GameObject Option4;
	private GameObject Option5;
	private GameObject OptionText1;
	private GameObject OptionText2;
	private GameObject OptionText3;
	private GameObject OptionText4;
	private GameObject image;
	private GameObject OptionText5;
	public List<GameObject> Options = new();
	public List<GameObject> OptionTexts = new();

	public PlayerState state;
	public DeathCause deathCause;
	public Enemy enemy;

	public int PlayerHP;
	public int EnemyHP;
	public int PlayerMaxHP;
	public int EnemyMaxHP;
	public int PlayerAttack;
	public int EnemyAttack;
	public int PlayerShield;
	public int EnemyShield;

	public bool PlayerTurn = true;
	public bool PlayerTurnPrevious = false;

	public List<Turn> PlayerTurns = new() { new(Variable.EnemyHP, -1, Variable.PlayerAttack), new(Variable.PlayerShield, 10) };
	public List<Turn> EnemyTurns = new() { new(Variable.PlayerHP, -1, Variable.EnemyAttack) };

	private void Start()
	{
		Option1 = GameObject.Find("Option 1");
		OptionText1 = GameObject.Find("OptionText1");
		Option2 = GameObject.Find("Option 2");
		OptionText2 = GameObject.Find("OptionText2");
		Option3 = GameObject.Find("Option 3");
		OptionText3 = GameObject.Find("OptionText3");
		Option4 = GameObject.Find("Option 4");
		OptionText4 = GameObject.Find("OptionText4");
		Option5 = GameObject.Find("Option 5");
		OptionText5 = GameObject.Find("OptionText5");
		Canva = GameObject.Find("Canvas");
		Text = GameObject.Find("MainText");
		image = GameObject.Find("Image");

		Options.Add(Option1);
		Options.Add(Option2);
		Options.Add(Option3);
		Options.Add(Option4);
		Options.Add(Option5);
		OptionTexts.Add(OptionText1);
		OptionTexts.Add(OptionText2);
		OptionTexts.Add(OptionText3);
		OptionTexts.Add(OptionText4);
		OptionTexts.Add(OptionText5);
		foreach(var opt in Options)
			opt.SetActive(false);
		switch(enemy.Type)
		{
			case DeathCause.Maid:
				image.GetComponent<Image>().sprite = state.Maid;
				break;
			case DeathCause.Cook:
				image.GetComponent<Image>().sprite = state.Cook;
				break;
			case DeathCause.Gardener:
				image.GetComponent<Image>().sprite = state.Gardener;
				break;
		}

		EnemyHP = enemy.HP;
		EnemyMaxHP = enemy.MaxHP;
		EnemyAttack = enemy.Attack;
		EnemyShield = 0;
		deathCause = enemy.Type;
		EnemyTurns.AddRange(enemy.Turns);
		PlayerHP = state.PlayerHP;
		PlayerMaxHP = state.PlayerMaxHP;
		PlayerAttack = state.PlayerAttack;
		PlayerShield = 0;
		PlayerTurns.AddRange(state.Abilities);
		foreach(var item in state.BattleItems)
		{
			if(state.Inventory.Contains(item.Key))
			{
				switch(item.Value.Item1)
				{
					case Variable.PlayerAttack:
						PlayerAttack += item.Value.Item2;
						break;
					case Variable.EnemyAttack:
						EnemyAttack += item.Value.Item2;
						break;
					case Variable.PlayerShield:
						PlayerShield += item.Value.Item2;
						break;
					case Variable.EnemyShield:
						EnemyShield += item.Value.Item2;
						break;
					case Variable.PlayerHP:
						PlayerHP += item.Value.Item2;
						break;
					case Variable.EnemyHP:
						EnemyHP += item.Value.Item2;
						break;
					case Variable.PlayerMaxHP:
						PlayerMaxHP += item.Value.Item2;
						break;
					case Variable.EnemyMaxHP:
						EnemyMaxHP += item.Value.Item2;
						break;
				}
			}
		}
	}

	private void Update()
	{
		EnemyHP = Mathf.Min(EnemyHP, EnemyMaxHP);
		PlayerHP = Mathf.Min(PlayerHP, PlayerMaxHP);
		if(EnemyHP <= 0)
		{
			Canva.SetActive(false);
			switch(deathCause)
			{
				case DeathCause.Cook:
					state.CookKilled = true;
					break;
				case DeathCause.Maid:
					state.MaidKilled = true;
					break;
				case DeathCause.Gardener:
					state.GardenerKilled = true;
					break;
			}
			state.PlayerHP = PlayerHP;
			state.Interacting = false;
			Destroy(enemy.gameObject);
			Destroy(gameObject);
		}
		if(PlayerTurn != PlayerTurnPrevious)
		{
			PlayerTurnPrevious = PlayerTurn;

			if(PlayerTurn)
			{
				Text.GetComponent<TMP_Text>().SetText($"Enemy: {EnemyHP}/{EnemyMaxHP}[{EnemyShield}]\nPlayer: {PlayerHP}/{PlayerMaxHP}[{PlayerShield}]");
				for(int i = 0; i < Mathf.Min(PlayerTurns.Count, 5); i++)
				{
					int mod = 1;
					var turn = PlayerTurns[i];
					Options[i].SetActive(true);
					float value = turn.Value;
					switch(turn.Scaling)
					{
						case Variable.PlayerHP:
							value *= PlayerHP;
							break;
						case Variable.EnemyHP:
							mod = -1;
							value *= EnemyHP;
							break;
						case Variable.PlayerAttack:
							value *= PlayerAttack;
							break;
						case Variable.EnemyAttack:
							mod = -1;
							value *= EnemyAttack;
							break;
						case Variable.PlayerShield:
							value *= PlayerShield;
							break;
						case Variable.EnemyShield:
							mod = -1;
							value *= EnemyShield;
							break;
					}
					OptionTexts[i].GetComponent<TMP_Text>().text = $"{turn.Variable}: {value * mod}".Replace("PlayerHP", "Heal").Replace("PlayerAttack", "Increase attack by").Replace("PlayerShield", "Gain shield").Replace("EnemyHP", "Attack").Replace("EnemyAttack", "Decrease enemy attack").Replace("EnemyShield", "Reduce enemy shield of");
				}

			}
			else
			{
				foreach(GameObject option in Options)
				{
					option.SetActive(false);
				}
				Turn turn = EnemyTurns[Random.Range(0, EnemyTurns.Count)];
				float value = turn.Value;
				switch(turn.Scaling)
				{
					case Variable.PlayerHP:
						value *= PlayerHP;
						break;
					case Variable.EnemyHP:
						value *= EnemyHP;
						break;
					case Variable.PlayerAttack:
						value *= PlayerAttack;
						break;
					case Variable.EnemyAttack:
						value *= EnemyAttack;
						break;
					case Variable.PlayerShield:
						value *= PlayerShield;
						break;
					case Variable.EnemyShield:
						value *= EnemyShield;
						break;
				}
				int mod = 1;
				switch(turn.Variable)
				{
					case Variable.PlayerHP:
						mod = -1;
						PlayerHP += (int)value;
						if(PlayerShield > 0 && value < 0)
						{
							var val = Mathf.Min(-value, PlayerShield);
							PlayerShield -= (int)val;
							PlayerHP += (int)val;
						}
						break;
					case Variable.EnemyHP:
						mod = 1;
						EnemyHP += (int)value;
						break;
					case Variable.PlayerAttack:
						mod = -1;
						PlayerAttack += (int)value;
						break;
					case Variable.EnemyAttack:
						mod = 1;
						EnemyAttack += (int)value;
						break;
					case Variable.PlayerShield:
						mod = -1;
						PlayerShield += (int)value;
						break;
					case Variable.EnemyShield:
						mod = 1;
						EnemyShield += (int)value;
						break;
					case Variable.PlayerPureHP:
						mod = -1;
						PlayerHP += (int)value;
						break;
				}
				Text.GetComponent<TMP_Text>().text = $"Enemy: {turn.Variable} {mod * value}".Replace("PlayerPureHP", "Pure damage, ignoring shield").Replace("PlayerHP", "Attack by").Replace("PlayerAttack", "Reduced player attack by").Replace("PlayerShield", "Reduced player shield by").Replace("EnemyHP", "Healed").Replace("EnemyAttack", "Increased attack by").Replace("EnemyShield", "Gained a shield of");
			}
		}
		else
		{
			if(PlayerTurn)
			{
				if(PlayerHP <= 0)
				{
					state.deathCause = deathCause;
					state.Interacting = false;
					Canva.SetActive(false);
					Destroy(gameObject);
					switch(deathCause)
					{
						case DeathCause.Maid:
							SceneManager.LoadScene("MaidDead");
							break;
						case DeathCause.Gardener:
							SceneManager.LoadScene("GardenerDead");
							break;
						case DeathCause.Cook:
							SceneManager.LoadScene("CookDead");
							break;
					}
				}
				int opt = 0;
				if(Input.GetKey("1"))
				{
					opt = 1;
				}
				else if(Input.GetKey("2"))
				{
					opt = 2;
				}
				else if(Input.GetKey("3"))
				{
					opt = 3;
				}
				else if(Input.GetKey("4"))
				{
					opt = 4;
				}
				else if(Input.GetKey("5"))
				{
					opt = 5;
				}
				if(opt != 0 && PlayerTurns.Count >= opt)
				{
					var turn = PlayerTurns[opt - 1];
					float value = turn.Value;
					switch(turn.Scaling)
					{
						case Variable.PlayerHP:
							value *= PlayerHP;
							break;
						case Variable.EnemyHP:
							value *= EnemyHP;
							break;
						case Variable.PlayerAttack:
							value *= PlayerAttack;
							break;
						case Variable.EnemyAttack:
							value *= EnemyAttack;
							break;
						case Variable.PlayerShield:
							value *= PlayerShield;
							break;
						case Variable.EnemyShield:
							value *= EnemyShield;
							break;
					}
					switch(turn.Variable)
					{
						case Variable.PlayerHP:
							PlayerHP += (int)value;
							break;
						case Variable.EnemyHP:
							EnemyHP += (int)value;
							if(value < 0 && EnemyShield > 0)
							{
								var val = Mathf.Min(-value, EnemyShield);
								EnemyShield -= (int)val;
								EnemyHP += (int)val;
							}
							break;
						case Variable.PlayerAttack:
							PlayerAttack += (int)value;
							break;
						case Variable.EnemyAttack:
							EnemyAttack += (int)value;
							break;
						case Variable.PlayerShield:
							PlayerShield += (int)value;
							break;
						case Variable.EnemyShield:
							EnemyShield += (int)value;
							break;
					}
					PlayerTurn = false;
				}
			}
			else if(Input.GetKey("space"))
			{
				PlayerTurn = true;
			}
		}
	}
}

public class Turn
{
	public Variable Variable;
	public float Value;
	public Variable Scaling;

	public Turn(Variable variable, float value, Variable scaling = Variable.None)
	{
		Variable = variable;
		Value = value;
		Scaling = scaling;
	}
}

public enum Variable
{
	None,
	PlayerHP,
	EnemyHP,
	PlayerAttack,
	EnemyAttack,
	PlayerShield,
	EnemyShield,
	PlayerMaxHP,
	EnemyMaxHP,
	PlayerPureHP
}