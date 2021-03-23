using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AppController : MonoBehaviour
{
    #region VARIABLES

    public static AppController Instance;

    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject uxCanvas;
    [SerializeField] private List<VideoClip> videos;

    private VideoPlayer videoPlayer;
    private int videoPlayerIndex;

    #endregion

    #region MONOBEHAVIOUR_METHODS

    private void Start()
    {
        Instance = this;
        PlaceContent.OnPlacedContent += PlaceContent_OnPlacedContent;
    }

   

    #endregion

    #region PUBLIC_METHODS

    public void FindVideoPlayer(bool paused = true)
    {
        StartCoroutine(FindVideoPlayer_Coroutine(paused));
    }

    // UI Button
    public void OnButtonPlayClicked()
    {
        FindVideoPlayer(false);
        videoPlayer.Play();
    }

    // UI Button
    public void OnButtonChangeVideoClicked()
    {
        if (videoPlayerIndex >= videos.Count - 1)
            videoPlayerIndex = 0;
        else
            videoPlayerIndex++;

        FindVideoPlayer();
    }

    #endregion

    #region PRIVATE_METHODS

    private void PlaceContent_OnPlacedContent()
    {
        mainCanvas.SetActive(true);
        uxCanvas.SetActive(false);
        FindVideoPlayer();
    }

    #endregion

    #region COROUTINES

    private IEnumerator FindVideoPlayer_Coroutine(bool paused)
    {
        videoPlayer = FindObjectOfType<VideoPlayer>();
        videoPlayer.clip = videos[videoPlayerIndex];
        videoPlayer.Play();
        yield return new WaitForSeconds(.1f);
        if (paused) videoPlayer.Pause();
    }

    #endregion
}