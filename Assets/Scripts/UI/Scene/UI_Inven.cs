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
   
        
        // 실제 인벤토리 정보를 참고해서 뭔가를 채워주는 부분 
        for (int i = 0; i < 8; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inven_Item");
            item.transform.SetParent(gridPanel.transform);
            // 1.GamObject를 생성해주고 부모 설정해주고 UI_Inven_Item에서 컴포넌트가져와서 그 텍스트의 글을 설정하고
            // 2. Util.GetOrComponent로 item연결해주는거임 -> 프리팹에


            // Util.Get 이렇게 접근하는데 그냥 item. 으로 접근하는것이 좋아보이니까 수정하자
            UI_Inven_Item invenItem = item.GetOrComponent<UI_Inven_Item>();
            invenItem.SetInfo($"{i}번");
        }
    }

    
}
