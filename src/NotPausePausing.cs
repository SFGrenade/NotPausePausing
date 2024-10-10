using JetBrains.Annotations;
using Modding;
using UnityEngine;

namespace NotPausePausing;

[UsedImplicitly]
public class PauseHandler : MonoBehaviour
{
    private bool paused = false;
    private bool frameAdvance = false;

    private void Update()
    {
        //Modding.Logger.LogDebug($"[PauseHandler] - Update()");
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Modding.Logger.LogDebug($"[PauseHandler] - Update() - P pressed - {!paused} - {Time.timeScale}");
            paused = !paused;
            if (paused)
            {
                Time.timeScale = 0f;
            } else
            {
                Time.timeScale = 1f;
                //lastTimescale = 1f;
            }
        }
        if (paused && Input.GetKeyDown(KeyCode.O))
        {
            //Modding.Logger.LogDebug($"[PauseHandler] - Update() - O pressed");
            frameAdvance = true;
            Time.timeScale = 1f;
        }
        if (paused && !frameAdvance)
        {
            Time.timeScale = 0f;
        }
    }

    private void FixedUpdate()
    {
        //Modding.Logger.LogDebug($"[PauseHandler] - FixedUpdate()");
        if (frameAdvance)
        {
            //Modding.Logger.LogDebug($"[PauseHandler] - FixedUpdate() - Frame advanced");
            frameAdvance = false;
            Time.timeScale = 0f;
        }
    }
}

public class NotPausePausing : Mod
{
    public NotPausePausing() : base("Not Pause Pausing")
    {
        On.GameManager.Start += OnGameManagerStart;
    }

    private void OnGameManagerStart(On.GameManager.orig_Start orig, GameManager self)
    {
        orig(self);
        self.gameObject.AddComponent<PauseHandler>();
    }

    public override void Initialize()
    {
        //if (GameManager.instance != null)
        //{
        //    if (GameManager.instance.gameObject.GetComponent<PauseHandler>() == null)
        //    {
        //        GameManager.instance.gameObject.AddComponent<PauseHandler>();
        //    }
        //}
    }
}