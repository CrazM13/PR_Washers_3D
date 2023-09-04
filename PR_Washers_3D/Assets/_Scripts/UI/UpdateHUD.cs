using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHUD : MonoBehaviour {

	[SerializeField] private TMPro.TMP_Text money;

	void Update() {
		money.text = $"MONEY: {GameData.Money}";
	}
}
