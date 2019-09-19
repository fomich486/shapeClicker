using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

[RequireComponent(typeof(TrackableBehaviour))]
public class TrackingEventHandler : MonoBehaviour,ITrackableEventHandler
{
    public UnityEvent OnTrackingFound;
    public UnityEvent OnTrackingLost;

    private TrackableBehaviour mTrackableBehaviour = null;
    private readonly List<TrackableBehaviour.Status> mTrackingFound = new List<TrackableBehaviour.Status>()
    {
        TrackableBehaviour.Status.DETECTED,
        TrackableBehaviour.Status.TRACKED,

        TrackableBehaviour.Status.EXTENDED_TRACKED
    };

    private readonly List<TrackableBehaviour.Status> mTrackingLost = new List<TrackableBehaviour.Status>()
    {
        TrackableBehaviour.Status.TRACKED,
        TrackableBehaviour.Status.NO_POSE
    };

    private void Awake()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    private void OnDestroy()
    {
        mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        foreach (TrackableBehaviour.Status trackedStatus in mTrackingFound)
        {
            if (newStatus == trackedStatus)
            {
                print("Found!");
                OnTrackingFound.Invoke();
                return;
            }
        }

        foreach (TrackableBehaviour.Status trackedStatus in mTrackingLost)
        {
            if (newStatus == trackedStatus)
            {
                print("Lost!");
                OnTrackingLost.Invoke();
                return;
            }
        }
    }
}
