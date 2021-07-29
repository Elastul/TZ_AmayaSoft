using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class onClickEvent : UnityEvent<int>
{
}

public class Player_input : MonoBehaviour
{
    public onClickEvent onClick;

    void Update()
    {        
        Grid_cell _cellTemp = null;
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 _worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(_worldPoint, Vector2.zero);

            if (hit.collider != null) 
            {
                _cellTemp = hit.collider.gameObject.GetComponent<Grid_cell>();
            }
        }
        
        if(_cellTemp != null)
        {
            onClick.Invoke(_cellTemp.ID);
        }
    }
}
