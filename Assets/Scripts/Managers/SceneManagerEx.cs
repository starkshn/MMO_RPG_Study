using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{

    public BaseScene CurrenScene { get { return GameObject.FindObjectOfType<BaseScene>(); } } // property�� �����.
    // �̷������� BaseScene Component�� ��� �ִ� �ָ� ã���ּ����̴�.

    public void  LoadScene(Define.Scene type)
    {
        CurrenScene.Clear(); // ���� Scene�� ã�ư����� Clear�� �����ش����� ���� ������ �̵�(SceneManager.LoadScene() �̺κ�.)
        SceneManager.LoadScene(GetSceneName(type));
        // LoadScene�� string�� �־���µ� Define�� Define������ string�� �ƴϴ�. �׷��� Define.Scene�� string���� �޾��ִ� �Լ� ������
    }

    public string GetSceneName(Define.Scene type)
    {
        // ���� �� �Լ��� C++������ ������ �� ���� ����! Reflection�� �������� �ʾƼ�(Csharp�� Refletion�����Ѵ�)
        string name = System.Enum.GetName(typeof(Define.Scene), type); // �̷����ϸ� Define.Scene enum�� value���� ����
        return name;
    }
}
