using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{ 

    enum GameObjects
    {
        HPBar,
    }

    Stat _stat;

    public override void init()
    {
        Bind<GameObject>(typeof(GameObjects));

        _stat = transform.parent.GetComponent<Stat>();
        
    }

    //public Vector3 height = new Vector3(0, 2, 0);

    private void Update()
    {
        Transform parent = gameObject.transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y + 0.3f);
        

        //transform.LookAt(Camera.main.transform);
        transform.rotation = Camera.main.transform.rotation;

        //transform.position = parent.position + height;
        //transform.eulerAngles = new Vector3(transform.rotation.x, 0.0f, transform.rotation.z);


        float ratio = _stat.Hp / (float)_stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
