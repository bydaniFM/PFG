using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeScript : MonoBehaviour
{
    public Image FadeImg;
    public float fadeSpeed = 1.5f;
    public bool sceneStarting = true;

    void Awake()
    {
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        //if the scene is starting, we call the StartScene,
        //which contains a FadeToClear to show the scene
        if (sceneStarting)
            StartScene();
    }

    /// <summary>
    /// Lerp the colour of the image between itself and black.
    /// </summary>
    void FadeToBlack()
    {
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Lerp the colour of the image between itself and transparent.
    /// </summary>
    void FadeToClear()
    {
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();

        // If the texture is almost clear
        if (FadeImg.color.a <= 0.05f)
        {
            //set the colour to clear and disable the RawImage.
            FadeImg.color = Color.clear;
            FadeImg.enabled = false;

            //The scene is no longer starting.
            sceneStarting = false;
        }
    }

    public void EndScene(int SceneNumber)
    {
        sceneStarting = false;
        StartCoroutine("EndSceneRoutine", SceneNumber);
    }

    public IEnumerator EndSceneRoutine(int SceneNumber)
    {
        FadeImg.enabled = true;
        do
        {
            // Start fading towards black.
            FadeToBlack();

            // If the screen is almost black
            if (FadeImg.color.a >= 0.95f)
            {
                //reload the level
                SceneManager.LoadScene(SceneNumber);
                yield break;
            }
            else
            {
                yield return null;
            }

        } while (true);
    }
}
