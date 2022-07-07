using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CoroutineTest : MonoBehaviour
{
    private Coroutine _coroutine; // ���� �������� ��������

    // Start is called before the first frame update
    void Start()
    {
        _coroutine = StartCoroutine(SomeCoroutine()); // ��������� ���������� _coroutine �����, ����� ������ �������� �� ������ (StartCoroutine)
    }

    [ContextMenu("Kill coroutine")]
    public void KillCoroutine()
    {
        StopCoroutine(_coroutine); // ���������
    }

    private IEnumerator SomeCoroutine()
    {
        Debug.Log("Start coroutine!");
        while(enabled) 
        {
            Debug.Log("Wait for 2 seconds");
            yield return new WaitForSeconds(2f); // ����-��� 2 �������
            Debug.Log("it's done!");
            yield return null; // ������� ������ �����
            Debug.Log("Wait for another coroutine");
            yield return AnotherCoroutine(); // ������ �������� �� ������� ������
            Debug.Log("it's done!");
            yield return null; // ������� ������ �����
        }
    }

    private IEnumerator AnotherCoroutine() // ������ �������
    {
        yield return new WaitForSeconds(3f); // ����-��� 3 �������
    }
}
