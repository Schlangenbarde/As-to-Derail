using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public virtual void StartState() { }
    public virtual void Do() { }
    public virtual void Stop() { }
}
