using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashTest : MonoBehaviour
{
    public List<Slash> Slashes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SlashAttack());
        }
    }
    IEnumerator SlashAttack()
    {
        for (int i = 0; i < Slashes.Count; i++)
        {
            yield return new WaitForSeconds(Slashes[i].delay);
            Slashes[i].SlashObt.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        onDisableSlash();
    }
    void onDisableSlash()
    {
        for (int i = 0; i < Slashes.Count; i++)
        {
            Slashes[i].SlashObt.SetActive(false);
        }
    }
}
[System.Serializable]
public class Slash
{
    public GameObject SlashObt;
    public float delay;
}

