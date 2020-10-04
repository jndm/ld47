using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;

public class WormholeManager : MonoBehaviour {
    [SerializeField]
    protected GameObject wormholePrefab;

    [SerializeField]
    protected GameObject player;

    [SerializeField]
    protected float minDistanceOffsetFromPlayer;

    [SerializeField]
    protected float wormholeWidth;

    [SerializeField]
    protected TMPro.TMP_Text usedWormholesText;

    [SerializeField]
    protected ScoreCardManager scoreCardManager;

    public Wormhole ActiveWormhole { get; private set; }
    private List<Wormhole> wormholePool = new List<Wormhole>();

    [SerializeField]
    private List<string> levels = new List<string>();
    private int currentLevelIndex = 0;

    private GameObject startGameObject;
    private GameObject finishGameObject;

    private List<int> scores = new List<int>();

    private bool IsPlayerActive { get; set; }

    private int _wormholesUsed = 0;
    public int WormholesUsed
    {
        get
        {
            return _wormholesUsed;
        }
        set
        {
            _wormholesUsed = value;
            this.usedWormholesText.SetText($"Wormholes used: {_wormholesUsed}");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        currentLevelIndex = 0;
        StartCoroutine(LoadScene(currentLevelIndex));

        player.GetComponent<PlayerCollision>().onDeath += OnPlayerDeath;
        player.GetComponent<PlayerCollision>().onFinish += OnPlayerFinish;

        levels.ForEach(x => scores.Add(0));

        scoreCardManager.InitScorecard(scores, OnNextLevelClick, OnMainMenuClick);
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        player.SetActive(false);

        var sceneLoad = SceneManager.LoadSceneAsync(levels[sceneIndex], LoadSceneMode.Additive);
        while (!sceneLoad.isDone)
        {
            yield return null;
        }

        if (sceneIndex > 0)
        {
            SceneManager.UnloadSceneAsync(levels[sceneIndex - 1]);
        }

        var scene = SceneManager.GetSceneByName(levels[currentLevelIndex]);
        finishGameObject = scene.GetRootGameObjects().FirstOrDefault(x => x.CompareTag("Finish"));
        startGameObject = scene.GetRootGameObjects().FirstOrDefault(x => x.CompareTag("Respawn"));

        CreateWormHole(startGameObject.transform.position);
        WormholesUsed = 0;

        player.SetActive(true);
        IsPlayerActive = true;
        player.transform.position = startGameObject.transform.position + startGameObject.transform.up * -7f;
        player.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.right);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleWormhole(InputAction.CallbackContext context)
    {
        if (!IsPlayerActive)
        {
            return;
        }

        if (context.phase != InputActionPhase.Canceled)
        {
            return;
        }

        if (ActiveWormhole != null && ActiveWormhole.IsActive)
        {
            ActiveWormhole.Disable();
            ActiveWormhole = null;
        }
        else
        {
            var mousePos = Mouse.current.position.ReadValue();
            var currentPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y));

            var distanceFromPlayer = Mathf.Abs(Vector2.Distance(currentPosition, player.transform.position));
            if (distanceFromPlayer <= wormholeWidth/2 + minDistanceOffsetFromPlayer)
            {
                return;
            }

            CreateWormHole(currentPosition);

            WormholesUsed++;
        }
    }

    private void CreateWormHole(Vector3 position)
    {
        if (ActiveWormhole != null && ActiveWormhole.IsActive)
        {
            ActiveWormhole.Disable();
            ActiveWormhole = null;
        }

        var newWormhole = wormholePool?.FirstOrDefault(x => !x.isActiveAndEnabled);
        if (newWormhole == null)
        {
            newWormhole = Instantiate(wormholePrefab).GetComponent<Wormhole>();
            newWormhole.transform.localScale = Vector3.one * wormholeWidth;
            wormholePool.Add(newWormhole);
        }

        newWormhole.Activate(position);
        ActiveWormhole = newWormhole;
    }

    private void OnPlayerDeath()
    {
        if (!IsPlayerActive)
        {
            return;
        }

        IsPlayerActive = false;
        player.SetActive(false);

        StartCoroutine(ResetRoutine());
    }

    IEnumerator ResetRoutine() {

        yield return new WaitForSeconds(1.5f);

        CreateWormHole(startGameObject.transform.position);
        WormholesUsed += 1;

        player.SetActive(true);
        IsPlayerActive = true;
        player.transform.position = startGameObject.transform.position + startGameObject.transform.up * -7f;
    }

    private void OnPlayerFinish()
    {
        if (!IsPlayerActive)
        {
            return;
        }

        if (ActiveWormhole != null && ActiveWormhole.IsActive)
        {
            ActiveWormhole.Disable();
            ActiveWormhole = null;
        }

        CreateWormHole(finishGameObject.transform.position);
        IsPlayerActive = false;

        scores[currentLevelIndex] = WormholesUsed;
        scoreCardManager.UpdateScores(scores, currentLevelIndex + 1 >= scores.Count);

    }

    private void OnNextLevelClick()
    {
        scoreCardManager.HideScoreCard();
        currentLevelIndex++;

        if (currentLevelIndex >= levels.Count)
        {
            Debug.Log("Completely finished");
        }
        else
        {
            StartCoroutine(LoadScene(currentLevelIndex));
        }
    }

    private void OnMainMenuClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
