using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIttable : MonoBehaviour
{
    public void Hit(HitData data)
    {
        Debug.Log(data.id);
    }
}
