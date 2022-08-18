using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    [SerializeField] private Color _baseColorTrue, _offSetColorTrue, _baseColorWrong, _offSetColorWrong;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private bool isEmpty = true, mouseOut = true, tryBuilding = false;
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offSetColorTrue : _baseColorTrue;

        _highlight.SetActive(false);
    }

    void OnMouseEnter()
    {
        Debug.Log(1);
        _highlight.SetActive(true);

        mouseOut = false;
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);

        mouseOut = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogWarning(1);

        if (other.CompareTag("Block"))
        {
            if (isEmpty)
            {
                tryBuilding = true;

            }
            else
            {
                tryBuilding = false;
                
                if (_renderer.color == _offSetColorTrue)
                {
                    _renderer.color = _offSetColorWrong;
                }
                else if (_renderer.color == _baseColorTrue)
                {
                    _renderer.color = _baseColorWrong;
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            if ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, -10) - transform.position).magnitude > 5)
            {
                isEmpty = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
