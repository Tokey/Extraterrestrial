using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CrystalCounterUI : MonoBehaviour {

	private Text crystalText;

	void Awake()
	{
		crystalText = GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		crystalText.text = "x" + GameMaster.Crystals.ToString();
	}
}
