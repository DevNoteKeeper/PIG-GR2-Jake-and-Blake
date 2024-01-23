using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class StoryManageable : MonoBehaviour
{
    [SerializeField]
    public string m_SceneToLoad;
    private PlayableDirector m_PlayeableDirector;

    public void Start()
    {
        m_PlayeableDirector = GetComponent<PlayableDirector>();
        Assert.IsNotNull(m_PlayeableDirector);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SkipToNextMarker();
        }
    }

    private void SkipToNextMarker()
    {
        // Retrieve the timeline from the playable director.
        var timeline = m_PlayeableDirector.playableAsset as TimelineAsset;
        var markers = timeline.GetOutputTracks().ToArray();

        double time = m_PlayeableDirector.time;
        int markerIdx = 0;

        foreach (TrackAsset marker in markers)
        {
            // If this is the last marker, skip to its end; there is no next marker to fetch.
            if (++markerIdx == markers.Length - 1)
            {
                m_PlayeableDirector.Play();
                // Not skipping to the immediate end, otherwise, nothing will be shown.
                m_PlayeableDirector.time = marker.end - 0.05;

                break;
            }

            // If the time is within this marker, skip to the next one.
            if (time >= marker.start && time <= marker.end)
            {
                m_PlayeableDirector.Play();
                m_PlayeableDirector.time = markers[markerIdx].start;

                break;
            }
        }
    }

    public void OnTimelineEnd()
    {
        Assert.IsTrue(m_SceneToLoad.Length > 0);
        SceneManager.LoadScene(m_SceneToLoad);
    }
}
