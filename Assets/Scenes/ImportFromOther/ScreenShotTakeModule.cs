using UnityEngine;

/// <summary>
/// �X�N�V����F12���͂Ŏw��̃f�B���N�g���ɕۑ��B���ʂ�UnityEditor�̃f�B���N�g������Ȃ��Ă��C�P����B
/// </summary>
public class ScreenShotTakeModule : MonoBehaviour
{
    public string screenshotPath = "Screenshots";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            string fileName = string.Format("{0}/screenshot_{1}.png", screenshotPath, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
            ScreenCapture.CaptureScreenshot(fileName, 1);
            Debug.Log("Screenshot saved: " + fileName);
        }
    }
}
