using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Reflection;
using System;

public class UI_Button : UI_Popup
{
    // public 으로 해주거나 [SerializeField] 해주면 툴에서도 뜬다
    //[SerializeField]
    //Text _text;


    // 우리가 Bind<T> T에 Text도 넣어주고 Button도 넣어주고 타입을 여러가지를 넣어주었기 때문에 Dic형태로 저장,관리하자
    //Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    // Dic의 키타입에 Reflection, UnityEngine.Object[] 타입이렇게 넘겨준 _object를 생성함
    // 이렇게하면은 Button이라는 타입은 Button의 리스트로 들고있을것이고 Text를 넘겨주면 Text목록의 타입을 배열(리스트)형태로 다 들고 있을것이다.


    enum Buttons
    {
        PointButton,

    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,

    }

    enum Images
    {
        ItemIcon,
        Weapon,

    }

    private void Start()
    {
        init();

        //Bind(Buttons); 이렇게 넘겨주고 싶지만 변수가 아니라서 넘겨줄 수 없다리
        // Part1에서 c# 기초에서 reflection이라는 기능을 배웠제?
        // reflection은 여기 페이지에 존재하는 모든 오브젝트들에 대해서 그냥 X를 찍어가지고 X에대한 정보를 다 추출할 수 있다고 했었다.
        // enum Buttons 도 예외는 아니다 Buttons에 X를 딱 찍어가지고 정보를 다 추출할 수 있다. 뭐 예를들어 이름은 enum타입이고 이름은 Buttons라는애고 산하에는 PointButton이라는 애가 있다. 이런식으로 추출가능.

        //Bind<Button> (typeof(Buttons)); // 지금은 Buttons라는 enum타입을 넘기겠다라는 말이다., 그리고 <Button>이라는 컴포넌트를 찾아서 매핑해주세요 이다.
        //Bind<Text>(typeof(Texts));

        //Bind<GameObject>(typeof(GameObjects));

        //Bind<Image>(typeof(Images));


        ////GameObject go = GetImage((int)Images.ItemIcon).AddUIEvent(); // 이런식으로 하면 좋을거같은데 우리가 지금배운 문법으로는 이것이 구현이 안된다. => 그래서 Extension 문법 사용함
        //GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);
        //// action으로 OnButtonClicked 넘겨준거임 

        //GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        //GameObject go2 = GetImage((int)Images.Weapon).gameObject;

        //AddUIEvent(go, ((PointerEventData data) => { go.gameObject.transform.position = data.position; }), Define.UIEvent.Drag);
        //AddUIEvent(go2, ((PointerEventData data) => { go2.gameObject.transform.position = data.position; }), Define.UIEvent.Drag);


        //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        //evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
        // 람다식으로 함수를 선언함 

    }

    public override void init()
    {
        base.init();

        Bind<Button>(typeof(Buttons)); 
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);
        // action으로 OnButtonClicked 넘겨준거임 

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        GameObject go2 = GetImage((int)Images.Weapon).gameObject;

        AddUIEvent(go, ((PointerEventData data) => { go.gameObject.transform.position = data.position; }), Define.UIEvent.Drag);
        AddUIEvent(go2, ((PointerEventData data) => { go2.gameObject.transform.position = data.position; }), Define.UIEvent.Drag);
    }

    //void Bind<T> (Type type) where T : UnityEngine.Object // enum을 넘겨주면은 (Buttons같은애들을 넘겨주면) 안에있는애들을 이름을 모두 찾아서 이름이 곂치는 애들이 있으 찾아서 알아서 자장하게끔 만들어 줄 것이다.
    //{
    //    string[] names = Enum.GetNames(type);

    //    UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
    //    // int[] numbers = new int[5]; 이것이 배열 선언방법(기초임)


    //    // Key는 typeof(T)로 넘겨주고 Value는 배열로 만들어서 넘겨주어야한다.
    //    _objects.Add(typeof(T), objects);
    //    // 이렇게 Dictionary에 저장을 했으니까 하나하나씩 돌면서 매핑을 시켜주어야한다. ( 툴로 하나하나씩 연결해주었던 작업을 이제 시작한다.)

    //    for(int i = 0; i < names.Length; i++)
    //    {
    //        if(typeof(T) == typeof(GameObject)) // GameObject 전용버젼을 만들것이다.
    //            objects[i] = Util.FindChild(gameObject, names[i], true);

    //        else
    //            objects[i] = Util.FindChild<T>(gameObject, names[i], true);
    //        // 이 부분에다가 찾은 부분을 넣어 주어야한다. 지금은 objects는 배열로 names.Length크기만큼 선언만 되어있으니까
    //        // 근데 어떻게 찾을까?? 우리가 gameObject는 접근할 수 있었다. => Util을 만들어 관리한다(다른데서도 사용하기때문에 )


    //        if (objects[i] == null)
    //            Debug.Log($"Failed to Bind!{names[i]}");
    //    }


    //}

    //// 우리가 매핑을 해주었으니까 이제는 GET하는 부분을 만들어보자
    //T Get<T>(int idx) where T : UnityEngine.Object // 이거는 그냥 꺼내서 쓰기만 하는 부분
    //{
    //    UnityEngine.Object[] objects = null;
    //    if (_objects.TryGetValue(typeof(T), out objects) == false) // 여기서 키값을 추출해낼것이다.
    //        return null;

    //    return objects[idx] as T; // 왜 T로 캐스팅(형변환)을 하냐면은 objects가 가지고 있는 타입이 UnityEngine.Objects이니까 이것을 캐스팅하여 T로 뱉어준다.

    //}

    //// 자주사용하는 UI들 Get미리 만들기 

    //Text GetText(int idx){ return Get<Text>(idx); }

    //Button GetButton(int idx) { return Get<Button>(idx); }

    //Image GetImage(int idx) { return Get<Image>(idx); }




    int _score = 0;

   public void OnButtonClicked(PointerEventData data) // public 으로 꼭 해줘야한다.
    {
        Get<Text>((int)Texts.ScoreText).text = $"점수 : {_score}";
        _score++;
        //_text.text = $"점수 : {_score}";
        Debug.Log("클릭됨!");

    }
}
