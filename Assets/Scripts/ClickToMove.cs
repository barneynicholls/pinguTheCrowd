using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && objectToMove != null)
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                objectToMove.transform.position = hit.point;
            }
        }
    }
}
