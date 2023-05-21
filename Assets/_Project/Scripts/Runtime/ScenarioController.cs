using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScenarioController : MonoBehaviour
{
    [SerializeField] GameObject HouseFrontSide;

    void Start()
    {
        DOVirtual.DelayedCall(0.5f, () =>
        {
            HouseFrontSide.SetActive(true);
        });
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HouseFrontSide.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        HouseFrontSide.SetActive(true);
    }
}
