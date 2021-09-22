using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    
   
    public virtual void init()
    {
        // virtual 가상함수를 만든다.
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI() // 내부에서 사용할 수 있도록 만듦.
    {
        Managers.UI.ClosePopupUI(this); // UI_Popup을 상속받은 애들은 ClosePopupUI를 하면 자동으로 Managers.UI.ClosePopupUI(this);이거를 해준다.
    }
    
}
