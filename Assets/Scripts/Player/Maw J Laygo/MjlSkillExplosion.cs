using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MjlSkillExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", 1f);
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
