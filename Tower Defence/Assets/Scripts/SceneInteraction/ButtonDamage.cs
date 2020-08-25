using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDamage : MonoBehaviour
{

    [SerializeField] Tower tower;
    [SerializeField] Text towerText;

    void Start()
    {
        towerText.text = "Damage:\n" + tower.ReturnDamageDealt().ToString();
    }
}
