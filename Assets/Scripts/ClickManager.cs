using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ClickManager : MonoBehaviour
{
    public GameObject InputPanal;
    public GameObject soundObj;
    public TMP_InputField inputOur;
    public TMP_InputField inputMin;
    public Button xBtn;
    public Button startBtn;
    public Button StopBtn;
    public Button OkBtn;
    public TextMeshProUGUI timerText;
    private float remainingTime;
    private bool isTimerRunning = false;
    private void Awake()
    {
        xBtn.onClick.AddListener(XClickBtn);
        startBtn.onClick.AddListener(StartClickBtn);
        StopBtn.onClick.AddListener(StopBtnClick);
        OkBtn.onClick.AddListener(OkBtnClick);
    }
    private void OnEnable()
    {
        StopBtn.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        OkBtn.gameObject.SetActive(false);
        soundObj.SetActive(false);
    }
    private void Update()
    {
        if (isTimerRunning && remainingTime > 0)
        {
            // ���� �ð� ����
            remainingTime -= Time.deltaTime;
            // �ð��� �а� �ʷ� ������
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            // UI�� Ÿ�̸� ǥ��
            timerText.text = $"{minutes:D2}  :  {seconds:D2}";
            // �ð��� ������ Ÿ�̸� ����
            if (remainingTime <= 0)
            {
                isTimerRunning = false;
                timerText.text = "00 : 00";
                OkBtn.gameObject.SetActive(true);
                timerText.gameObject.SetActive(false);
                StopBtn.gameObject.SetActive(false);
                soundObj.SetActive(true);

            }
        }
    }
    // ����
    public void StartClickBtn()
    {
        // �а� �� �Է� �� �б�
        int minutes = 0;
        int seconds = 0;
        if (!int.TryParse(inputOur.text, out minutes) && !int.TryParse(inputMin.text, out seconds))
        {
            return;
        }
        if (!int.TryParse(inputOur.text, out minutes))
        {
            minutes = 0; // �Է��� �߸��� ��� 0���� ����
        }
        if (!int.TryParse(inputMin.text, out seconds))
        {
            seconds = 0; // �Է��� �߸��� ��� 0���� ����
        }
        // ��ü �ð��� �ʷ� ��ȯ
        remainingTime = (minutes * 60) + seconds;
        // Ÿ�̸� ����
        inputOur.text = "";
        inputMin.text = "";
        InputPanal.SetActive(false);
        timerText.gameObject.SetActive(true);
        startBtn.gameObject.SetActive(false);
        StopBtn.gameObject.SetActive(true);
        if (remainingTime > 0)
        {
            isTimerRunning = true;
        }
    }
    public void StopBtnClick()
    {
        isTimerRunning = false;
        timerText.text = "";
        timerText.gameObject.SetActive(false);
        StopBtn.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
        InputPanal.SetActive(true);

    }
    public void OkBtnClick()
    { 
        soundObj.SetActive(false);
        startBtn.gameObject.SetActive(true);
        OkBtn.gameObject.SetActive(false);
        InputPanal.SetActive(true);
    }
    public void SetTimer(int minutes, int seconds)
    {
        remainingTime = (minutes * 60) + seconds;
        inputOur.text = minutes.ToString();
        inputMin.text = seconds.ToString();
        Debug.Log($"Ÿ�̸� ����: {minutes}�� {seconds}��");
    }


    // ����
    public void XClickBtn()
    {
#if UNITY_EDITOR
        Application.Quit();
        //EditorApplication.isPlaying = false;
#else
        // ����� ���ӿ����� �� �ڵ带 �����Ͽ� ���ø����̼��� ����
        Application.Quit();
#endif
    }
}
