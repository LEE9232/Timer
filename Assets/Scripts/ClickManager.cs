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
            // 남은 시간 감소
            remainingTime -= Time.deltaTime;
            // 시간을 분과 초로 나누기
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            // UI에 타이머 표시
            timerText.text = $"{minutes:D2}  :  {seconds:D2}";
            // 시간이 끝나면 타이머 종료
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
    // 시작
    public void StartClickBtn()
    {
        // 분과 초 입력 값 읽기
        int minutes = 0;
        int seconds = 0;
        if (!int.TryParse(inputOur.text, out minutes) && !int.TryParse(inputMin.text, out seconds))
        {
            return;
        }
        if (!int.TryParse(inputOur.text, out minutes))
        {
            minutes = 0; // 입력이 잘못된 경우 0으로 설정
        }
        if (!int.TryParse(inputMin.text, out seconds))
        {
            seconds = 0; // 입력이 잘못된 경우 0으로 설정
        }
        // 전체 시간을 초로 변환
        remainingTime = (minutes * 60) + seconds;
        // 타이머 시작
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
        Debug.Log($"타이머 설정: {minutes}분 {seconds}초");
    }


    // 종료
    public void XClickBtn()
    {
#if UNITY_EDITOR
        Application.Quit();
        //EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서는 이 코드를 실행하여 애플리케이션을 종료
        Application.Quit();
#endif
    }
}
