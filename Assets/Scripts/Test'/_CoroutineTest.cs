using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CoroutineTest : MonoBehaviour
{
    private Coroutine _coroutine; // Даем коротину название

    // Start is called before the first frame update
    void Start()
    {
        _coroutine = StartCoroutine(SomeCoroutine()); // Назначаем переменной _coroutine метод, также запуск корутина на старте (StartCoroutine)
    }

    [ContextMenu("Kill coroutine")]
    public void KillCoroutine()
    {
        StopCoroutine(_coroutine); // Остановка
    }

    private IEnumerator SomeCoroutine()
    {
        Debug.Log("Start coroutine!");
        while(enabled) 
        {
            Debug.Log("Wait for 2 seconds");
            yield return new WaitForSeconds(2f); // Тайм-аут 2 секунды
            Debug.Log("it's done!");
            yield return null; // Пропуск одного кадра
            Debug.Log("Wait for another coroutine");
            yield return AnotherCoroutine(); // Запуск корутина из другого метода
            Debug.Log("it's done!");
            yield return null; // Пропуск одного кадра
        }
    }

    private IEnumerator AnotherCoroutine() // Другой корутин
    {
        yield return new WaitForSeconds(3f); // Тайм-аут 3 секунды
    }
}
