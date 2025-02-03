using UnityEngine;

public class Container : MonoBehaviour
{
    private ObjectToSpawn _element;
    
    public bool IsHasElement => _element != null;

    public void SetElement(ObjectToSpawn element)
    {
        _element = element;
        _element.LifeTimeFinished += OnElementLeaved;
    }

    private void OnElementLeaved(ObjectToSpawn element)
    {
        _element = null;
        element.LifeTimeFinished -= OnElementLeaved;
    }
}