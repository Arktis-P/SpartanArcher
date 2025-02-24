using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                // 첫 번째 객체 생성 시, 씬에 추가
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);  // 씬이 변경되어도 객체가 파괴되지 않도록 설정
                }
                return instance;
            }
        }
    }

    // Awake()를 오버라이드하여 초기화 작업을 처리
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);  // 씬 전환 시 객체를 유지
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 있으면 중복 객체 삭제
        }
    }
}