using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void init()
    { 
        base.init();

        SceneType = Define.Scene.Game;

        // Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> dict =  Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();


        GameObject player = Managers.Game.spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        Managers.Game.spawn(Define.WorldObject.Monster, "Knight");
    }

    public override void Clear()
    {
        
    }

     
    
}
