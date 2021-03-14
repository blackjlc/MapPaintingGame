using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.RegisterTick(Process);
    }

    void Process()
    {
        // Military - attack neighbor if I'm strong enough
    }
}
