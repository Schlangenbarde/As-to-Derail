using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.PlayerBurning_Event += PlayerStartBurning;
        EventManager.PlayerStopBurning_Event += PlayerStopBurning;
    }

    private void OnDisable()
    {
        EventManager.PlayerBurning_Event -= PlayerStartBurning;
        EventManager.PlayerBurning_Event -= PlayerStopBurning;
    }

    public GameObject fireUI;
    void PlayerStartBurning()
    {
        fireUI.GetComponent<Animator>().SetBool("FireStatus", true);
        StartCoroutine(PlayerBurning());
    }
    IEnumerator PlayerBurning()
    {
        Health player = GetComponent<Health>();
        player.GetDamage(10);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(PlayerBurning());
    }

    void PlayerStopBurning()
    {
        StopAllCoroutines();
        fireUI.GetComponent<Animator>().SetBool("FireStatus", false);
    }
}
