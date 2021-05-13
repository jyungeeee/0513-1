using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverText;     //text of gameover
    public GameObject playerGameObject; //gameover of player
    public Text hpText;     //shows hp of player
    public Text scoreText; //shows score
    int score;  //score
    public bool isGameOver; // whether gameover or not

    public GameObject enemyPrefab; // enemy(bulletSpawner) prefab

    private MovementProvider movementProvider;

    private AudioSource midiSource;
    //public GameObject assets; // gameobject of assets
    // Start is called before the first frame update
    void Start()
    {
        //var cc = playerGameObject.GetComponent<CharacterController>();

        score = 0;
        isGameOver = false;
        movementProvider = GetComponent<MovementProvider>();

        midiSource = GetComponent<AudioSource>();
        midiSource.Play();
        //SpawnEnemy();
        // assets = GameObject.Find("[ASSETS]");
    }
    public void StartGame()
    {
        movementProvider.StartMove();
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other != null)
    //    {
    //        Debug.Log("GameManger Triggerrrrrrrr");

    //    }
    //}

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if (hit != null)
    //    {
    //        Debug.Log($"CC Hit");

    //    }
    //}
    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            hpText.text = "HP :" + (int)playerGameObject.GetComponent<PlayerController>().GetHp();
            scoreText.text = "Score :" + (int)score;
        }
    }

    public void SpawnEnemy()
    {
        StartCoroutine(SpawnEnemyImpl());
    }
    IEnumerator SpawnEnemyImpl()
    {
        while (!isGameOver)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-50f, 50f), 1, Random.Range(-50f, 50f));

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(5f);

        }
    }


    public void GetScored(int value)
    {
        score += value;
    }
    public void EndGame()
    {
        ShowHp();
        isGameOver = true;
        gameOverText.SetActive(true);

    }
    public void RestartGame()
    {
        SceneManager.LoadScene("VR Test Scene");

    }
    void ShowHp()
    {
        hpText.text = "HP :" + (int)playerGameObject.GetComponent<PlayerController>().GetHp();
    }
}
