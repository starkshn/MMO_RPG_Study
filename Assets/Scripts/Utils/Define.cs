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
        // MaxCount�� Sound enum������ �������ؼ� �׳� �߰��Ѱ�

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
