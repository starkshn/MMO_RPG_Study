using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HPBar : UI_Base
{ 

    enum GameObjects
    {
        HPBar,
    }
    public override void init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    //public Vector3 height = new Vector3(0, 2, 0);

    private void Update()
    {
        Transform parent = gameObject.transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);

             


        //transform.position = parent.position + height;
        //transform.eulerAngles = new Vector3(transform.rotation.x, 0.0f, transform.rotation.z);
    }
}
