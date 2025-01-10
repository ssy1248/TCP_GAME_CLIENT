using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    //살아있는 지 체크하는 불값
    public bool isLive;
    //게임 진행 시간
    public float gameTime;
    //fps?
    public int targetFrameRate;
    //클라이언트 버전
    public string version = "1.0.0";
    //
    public int latency = 2;

    [Header("# Player Info")]
    //playerId => 서버에서 uuid를 전송해주면 그것을 세팅
    public uint playerId;
    //deviceId => 클라에서 처음에 세팅하고 서버에 전달
    public string deviceId;

    [Header("# Game Object")]
    //PoolManager?
    public PoolManager pool;
    //player관련 함수
    public Player player;
    public GameObject hud;
    public GameObject GameStartUI;

    void Awake() {
        instance = this;
        Application.targetFrameRate = targetFrameRate;
        playerId = (uint)Random.Range(0, 4);
        Debug.Log("세팅된 playerID : " + playerId);
    }

    public void GameStart() {
        player.deviceId = deviceId;
        player.gameObject.SetActive(true);
        hud.SetActive(true);
        GameStartUI.SetActive(false);
        isLive = true;

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver() {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameRetry() {
        SceneManager.LoadScene(0);
    }

    public void GameQuit() {
        Application.Quit();
    }

    void Update()
    {
        if (!isLive) {
            return;
        }
        gameTime += Time.deltaTime;
    }
}
