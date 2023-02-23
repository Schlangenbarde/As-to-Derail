using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltips : MonoBehaviour
{
      [SerializeField]
      private TMP_Text tmpText;

      [SerializeField]
      private List<string> toolTipList = new List<string>();

      public Animator animator;

      private void Awake()
      {
            tmpText = GetComponent<TMP_Text>();
      }

      public void changeTip(int index)
      {
            tmpText.text = toolTipList[index];
      }

      public void hit(bool didHit)
      {
            animator.SetBool("activateTooltip", didHit);

      }

}
