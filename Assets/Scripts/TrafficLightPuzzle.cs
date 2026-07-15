using UnityEngine;
using System.Collections;

public class TrafficLightPuzzle : MonoBehaviour
{
    public enum LightState { Red, Yellow, Green }
    public LightState CurrentState { get; private set; }

    public Renderer[] redLights;
    public Renderer[] yellowLights;
    public Renderer[] greenLights;

    public float minGreen = 7f;
    public float maxGreen = 10f;
    public float minRed = 3f;
    public float maxRed = 5f;
    public float yellowDuration = 2f;

    private PlayerController1 player;
    private Coroutine cycleRoutine;

    public void SetPlayer(PlayerController1 p)
    {
        player = p;

        if (cycleRoutine != null)
            StopCoroutine(cycleRoutine);

        cycleRoutine = StartCoroutine(LightCycle());
    }

    IEnumerator LightCycle()
    {
        while (true)
        {
            // Green
            SwitchToGreen();
            yield return new WaitForSeconds(Random.Range(minGreen, maxGreen));

            // Yellow (Before Red)
            SwitchToYellow();
            yield return new WaitForSeconds(yellowDuration);

            // Red
            SwitchToRed();
            yield return new WaitForSeconds(Random.Range(minRed, maxRed));

            // Yellow (Before Green)
            SwitchToYellow();
            yield return new WaitForSeconds(yellowDuration);
        }
    }

    void SwitchToGreen()
    {
        CurrentState = LightState.Green;

        foreach (Renderer r in greenLights)
            r.material.color = Color.green;

        foreach (Renderer r in redLights)
            r.material.color = Color.black;

        foreach (Renderer r in yellowLights)
            r.material.color = Color.black;

        player.SetSpeedMultiplier(1f);
        player.UnfreezePlayer();
    }

    void SwitchToYellow()
    {
        CurrentState = LightState.Yellow;

        foreach (Renderer r in yellowLights)
            r.material.color = Color.yellow;

        foreach (Renderer r in redLights)
            r.material.color = Color.black;

        foreach (Renderer r in greenLights)
            r.material.color = Color.black;

        // Limit Speed by half
        player.SetSpeedMultiplier(0.5f);
        player.UnfreezePlayer();
    }

    void SwitchToRed()
    {
        CurrentState = LightState.Red;

        foreach (Renderer r in redLights)
            r.material.color = Color.red;

        foreach (Renderer r in greenLights)
            r.material.color = Color.black;

        foreach (Renderer r in yellowLights)
            r.material.color = Color.black;

        // If moving reset
        if (player.IsMoving())
        {
            player.ResetToSpawn();
        }

        // Freeze after reset so they stay at spawn
        player.FreezePlayer();
    }
}
