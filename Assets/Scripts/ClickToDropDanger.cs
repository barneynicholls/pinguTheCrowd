using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToDropDanger : MonoBehaviour
{
    [SerializeField]
    private GameObject dangerPrefab;

    private GameObject[] agents;

    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                var danger = Instantiate(dangerPrefab, hit.point, Quaternion.identity);

                Destroy(danger, 3f);

                foreach(var agent in agents)
                {
                    var agentScript = agent.GetComponent<AIFlee>();
                    if(agentScript is not null)
                    {
                        agentScript.flee(hit.point);
                    }
                }
            }
        }
    }
}
