using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField] private int startingBalance;
    [SerializeField] private TMP_Text balanceText;

    public int balance { get; private set; }

    private void Start() {
        balance = startingBalance;
        balanceText.text = $"${balance}";
    }

    /// <returns>
    /// If the adjustment was successful
    /// </returns>
    public bool AdjustBalance(int adjustment) {
        if (balance + adjustment < 0) {
            return false;
        } else {
            balance += adjustment;
        }

        balanceText.text = $"${balance}";
        return true;
    }
}
