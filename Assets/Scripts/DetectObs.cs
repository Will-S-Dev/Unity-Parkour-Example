using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObs : MonoBehaviour
{
    public string ObjectTagName = "";
    public bool Obstruction;
    public GameObject Object;
    private Collider colnow;
    void OnTriggerStay(Collider col)
    {
        if (ObjectTagName != "" && !Obstruction)
        {
            if (col.GetComponent<CustomTag>()) 
            {
                if (col.GetComponent<CustomTag>().IsEnabled)
                {
                    if (col != null && !col.isTrigger && col.GetComponent<CustomTag>().HasTag(ObjectTagName)) // checks if the object has the right tag
                    {
                        Obstruction = true;
                        Object = col.gameObject;
                        colnow = col;
                    }
                }
            }
            
        }


        if (ObjectTagName == "" && !Obstruction)
        {
            if (col != null && !col.isTrigger)
            {
                Obstruction = true;
                colnow = col;
            }

        }


      
    }

    private void Update()
    {
        
        if(Object == null || !colnow.enabled)
        {
            Obstruction = false;
        }
        if (Object != null)
        {
            if (!Object.activeInHierarchy)
            {
                Obstruction = false;
            }
        }
    }







    void OnTriggerExit(Collider col)
    {
        if (col == colnow)
        {
            Obstruction = false;
        }

    }

}
