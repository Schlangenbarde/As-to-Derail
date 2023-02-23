using TMPro;
using UnityEngine;

public class DebugSanityUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text sanityText;

    [SerializeField]
    TMP_Text posSanityText;

    [SerializeField]
    TMP_Text negSanityText;

    private void Update()
    {
        sanityText.text = "Sanity: " + Game.Get().Player.GetComponent<BaseSanity>().GetCurrentSanity.ToString("00.00");
        posSanityText.text = "Positive Multiplicator: " + Game.Get().Player.GetComponent<BaseSanity>().GetPosMultiplicator.ToString("00.00");
        negSanityText.text = "Negative Multiplicator: " + Game.Get().Player.GetComponent<BaseSanity>().GetNegMultiplicator.ToString("00.00");
    }
}
