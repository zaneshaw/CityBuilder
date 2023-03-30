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

    public void AddMoney(int change) {
        balance += Mathf.Abs(change);
        balanceText.text = $"${balance}";
    }

    public bool SubtractMoney(int change) {
        change = Mathf.Abs(change);
        if (balance - change < 0) {
            return false;
        } else {
            balance -= change;
        }
        balanceText.text = $"${balance}";

        return true;
    }
}
