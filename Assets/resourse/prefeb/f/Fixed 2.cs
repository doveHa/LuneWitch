using UnityEngine;

public class Fixed2 : MonoBehaviour
{
    private void Start()
    {
        SetResolution();
    }

    /// <summary>
    /// �ػ� ���� �Լ�
    /// </summary>
    public void SetResolution()
    {
        int setWidth = 1920; // ȭ�� �ʺ�
        int setHeight = 1080; // ȭ�� ����

        // �ػ󵵸� �������� ���� ���� (��üȭ�� ���)
        Screen.SetResolution(setWidth, setHeight, true);
    }
}
