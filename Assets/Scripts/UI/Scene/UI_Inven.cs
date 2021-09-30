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
            Managers.Resource.Destroy(child.gameObject); // gridPanel 돌면서 child삭제
   
        
        // 실제 인벤토리 정보를 참고해서 뭔가를 채워주는 부분 
        for (int i = 0; i < 8; i++)
        {
            Debug.Log("Hi!");
            Managers.UI.MakeSubItem<UI_Inven_Item>();

            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject; 

            UI_Inven_Item invenItem = item.GetOrComponent<UI_Inven_Item>();
            invenItem.SetInfo($"{i}번");

        }
    }

    
}
