using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightManager : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    private Transform highlighted;
    private Transform selected;

    private Outline outline;
    private RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        HoverOutline();
    }

    private void HoverOutline()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.GetMousePosition());

        if(highlighted != null)
        {
            outline.enabled = false;
            highlighted = null;
        }

        if(!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit, layer))
        {
            highlighted = hit.transform;

            if(highlighted.CompareTag("Enemy") && highlighted != selected)
            {
                outline = highlighted.GetComponent<Outline>();
                outline.enabled = true;
            }
            else
            {
                highlighted = null;
            }
        }
    }

    public void SelectOutline()
    {
        if(highlighted == null)
        {
            return;
        }
        if (highlighted.CompareTag("Enemy"))
        {
            if (selected != null)
            {
                selected.gameObject.GetComponent<Outline>().enabled = false;
            }
            selected = hit.transform;
            selected.gameObject.GetComponent<Outline>().enabled = true;

            outline.enabled = true;
            highlighted = null;
        }
    }

    public void DeselectOutline()
    {
        if(selected == null)
        {
            return;
        }
        selected.gameObject.GetComponent<Outline>().enabled = false;
        selected = null;
    }
}
