using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,

    }

    public enum Sound
    {
        Bgm,
        Effect,
        Jump,
        MaxCount,
        // MaxCount는 Sound enum갯수를 세기위해서 그냥 추가한것

    }

    public enum UIEvent
    {
        Click,
        Drag,

    }

    public enum MouseEvent
    {
        Press,
        Click,
    }

    public enum KeyBoardEvent
    {
        P_Space,
    }
    
   public enum CameraMode
    {
        QuarterView
    }
}
