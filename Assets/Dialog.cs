using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] Lines;
    public float textSpeed = 1;

    public VideoPlayer vp;
    public GameObject img;
    private bool VideoStarted = false;

    public Sprite TearChar;
    public Sprite DeterChar;
    public Sprite McChar;
    public Image Character;

    public GameObject BG;
    public Material McMat;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDiologue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == Lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = Lines[index];
            }
        }

        if (VideoStarted && vp.isPaused)
        {
            StartCoroutine(LoadScene("GameScene"));
            VideoStarted = false;
        }
    }

    void StartDiologue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach(char c in Lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < Lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            vp.Play();
            VideoStarted = true;
            img.SetActive(false);
            Destroy(textComponent);
        }

        if (index > 3)
        {
            Character.sprite = McChar;
            Renderer renderer = BG.GetComponent<Renderer>();
            renderer.material = McMat;
        }
        else if (index%2 == 0)
        {
            Character.sprite = TearChar;
        }
        else
        {
            Character.sprite = DeterChar;
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
