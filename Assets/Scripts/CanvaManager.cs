using TMPro;

using UnityEngine;

public class CanvaManager : MonoBehaviour
{
	public GameObject Canva;
	public GameObject Text;
	public GameObject Option1;
	public GameObject Option2;
	public GameObject Option3;
	public GameObject Option4;
	public GameObject Option5;
	public GameObject OptionText1;
	public GameObject OptionText2;
	public GameObject OptionText3;
	public GameObject OptionText4;
	public GameObject OptionText5;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Activate(int index, bool value=true)
	{
		if(index == 0)
			Canva.SetActive(value);
		if(index == 1)
			Option1.SetActive(value);
		if(index == 2)
			Option2.SetActive(value);
		if(index == 3)
			Option3.SetActive(value);
		if(index == 4)
			Option4.SetActive(value);
		if(index == 5)
			Option5.SetActive(value);
	}
	public void SetText(int index, string value)
	{
		if(index == 0)
			Text.GetComponent<TMP_Text>().text = value;
		if(index == 1)
			OptionText1.GetComponent<TMP_Text>().text = value;
		if(index == 2)
			OptionText2.GetComponent<TMP_Text>().text = value;
		if(index == 3)
			OptionText3.GetComponent<TMP_Text>().text = value;
		if(index == 4)
			OptionText4.GetComponent<TMP_Text>().text = value;
		if(index == 5)
			OptionText5.GetComponent<TMP_Text>().text = value;
	}
}
