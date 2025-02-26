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
                // ù ��° ��ü ���� ��, ���� �߰�
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);  // ���� ����Ǿ ��ü�� �ı����� �ʵ��� ����
                }
                return instance;
            }
        }
    }

    // Awake()�� �������̵��Ͽ� �ʱ�ȭ �۾��� ó��
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �� ��ü�� ����
        }
        else
        {
            Destroy(gameObject);  // �̹� �ν��Ͻ��� ������ �ߺ� ��ü ����
            //DontDestroyOnLoad(gameObject);  // �� ��ȯ �� ��ü�� ����
        }
        //else
        //{
        //    Destroy(gameObject);  // �̹� �ν��Ͻ��� ������ �ߺ� ��ü ����
        //}
    }
}