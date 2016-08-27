using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class moveScene : MonoBehaviour
{
    [SerializeField]
    Graphic m_Graphics;
    [SerializeField]
    float m_AngularFrequency = 1.0f;
    [SerializeField]
    float m_DeltaTime = 0.0333f;
    Coroutine m_Coroutine;
    public float moveSceneSec = 2.0f;

    void Reset()
    {
        m_Graphics = GetComponent<Graphic>();
    }

    void Awake()
    {
        StartFlash();
    }

    IEnumerator Flash()
    {
        float m_Time = 0.0f;

        while (true)
        {
            m_Time += m_AngularFrequency * m_DeltaTime;
            var color = m_Graphics.color;
            color.a = Mathf.Abs(Mathf.Sin(m_Time));
            m_Graphics.color = color;
            yield return new WaitForSeconds(m_DeltaTime);
            if (Input.GetMouseButton(0))
            {
                m_DeltaTime = 0.1f;
                m_AngularFrequency = 11f;
                Debug.Log("press");
                Invoke("DelayMethod", moveSceneSec);
            }
        }
    }

    private void DelayMethod()
    {
        SceneManager.LoadScene("Stage01");
    }

    public void StartFlash()
    {
        m_Coroutine = StartCoroutine(Flash());
    }

    public void StopFlash()
    {
        StopCoroutine(m_Coroutine);
    }
}