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
        for (int i = 0; i < 8; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item");
            item.transform.SetParent(gridPanel.transform);
            // 1.GamObject�� �������ְ� �θ� �������ְ� UI_Inven_Item���� ������Ʈ�����ͼ� �� �ؽ�Ʈ�� ���� �����ϰ�
            // 2. Util.GetOrComponent�� item�������ִ°��� -> �����տ�


            // Util.Get �̷��� �����ϴµ� �׳� item. ���� �����ϴ°��� ���ƺ��̴ϱ� ��������
            UI_Inven_Item invenItem = item.GetOrComponent<UI_Inven_Item>();
            invenItem.SetInfo($"{i}��");
        }
    }

    
}
