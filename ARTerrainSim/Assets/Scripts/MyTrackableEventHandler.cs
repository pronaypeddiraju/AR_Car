using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class MyTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    //Meshes that we don't want active in AR view
    [SerializeField]
    private Renderer[] AlwaysOffMeshes;

    #region PUBLIC_CONSTANTS

    public const float ZeroTimeScale = 0f;

    #endregion // PUBLIC_CONSTANTS

    #region PUBLIC_MEMBER_VARIABLES

    /// <summary>
    /// Pause the TimeScale when a ImageTarget's state is changed.
    /// </summary>
    public bool pauseTimeScale = true;

    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES

    private TrackableBehaviour mTrackableBehaviour;
    private float originalTimeScale = 1f;

    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNTIY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        // Setting the timeScale has to be the first statement to be excecuted.
        // Otherwise it may be set to 0.
        originalTimeScale = Time.timeScale;

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS


    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        if (pauseTimeScale)
        {
            // Resume Timescale
            Time.timeScale = originalTimeScale;
            //Debug.Log("TimeScale: " + Time.timeScale);
        }

        //Disabling all the alwaysOffMEshes
        foreach (Renderer component in AlwaysOffMeshes)
        {
            component.enabled = false;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }

    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        if (pauseTimeScale)
        {
            // Pause Timescale
            Time.timeScale = ZeroTimeScale;
            //Debug.Log("TimeScale: " + Time.timeScale);
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

    #endregion // PRIVATE_METHODS
}