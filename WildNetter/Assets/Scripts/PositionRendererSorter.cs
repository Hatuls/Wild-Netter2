
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    [SerializeField] int sortingOrderBase = 5000;
    [SerializeField] int offset = 0;


    Renderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();

    }
    private void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(sortingOrderBase -transform.position.z - offset);
    }


  

}



