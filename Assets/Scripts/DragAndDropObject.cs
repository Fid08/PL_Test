using UnityEngine;

public class DragAndDropObject : MonoBehaviour
{
    [SerializeField] private LayerMask _hardLayers;

    private Vector2 _defaultScale;
    void Awake()
    {
        _defaultScale = transform.localScale;

         EventBus.DragObjectStart.AddListener(ReturnScaleToDefault);
         EventBus.DragObjectStop.AddListener(RaycastScale);
    }

    void RaycastScale()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, _hardLayers);
        SceneObjectInfo _scaleInfo = hit.transform.GetComponent<SceneObjectInfo>();

        if (_scaleInfo!=null)
        {
           transform.localScale = transform.localScale* _scaleInfo.ScaleFactor;
           
        }       
        
    }
    void ReturnScaleToDefault()
    {
        transform.localScale = _defaultScale;
        Debug.Log("Return scale to Default");
    }
    
}
