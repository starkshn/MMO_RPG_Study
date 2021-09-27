using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{

    enum GameObjects
    {
        GridPanel,

    }

    void Start()
    {
        init();
    }

    public override void init()
    {
        base.init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);


        // ���� �κ��丮 ������ �����ؼ� ������ ä���ִ� �κ� 
        for (int i = 0; i < 4; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item");
            item.transform.SetParent(gridPanel.transform);

        }
    }

    
}
