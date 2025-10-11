using UnityEngine;
using UnityEngine.Animations;

public class AudioListenerController
{
    [SerializeField]
    IConstraint[] constraints;

    GameObject listener, target;

    public AudioListenerController(GameObject listener)
    {
        this.listener = listener;

        Setting();
        SetAudioListener();
    }

    public void Setting()
    {
        constraints = new IConstraint[2];
        constraints[0] = listener.GetComponent<PositionConstraint>();
        constraints[1] = listener.GetComponent<RotationConstraint>();
    }

    public void SetAudioListener()
    {
        target = GameObject.Find("AudioListenerTarget");

        if (target == null)
            return;

        Debug.Log("Setting AudioListener");

        ConstraintSource source = new ConstraintSource
        {
            sourceTransform = target.transform,
            weight = 1f
        };

        var sourcesList = new System.Collections.Generic.List<ConstraintSource>();
        constraints[0].GetSources(sourcesList);

        sourcesList.Clear();
        sourcesList.Add(source);


        foreach (var temp in constraints)
        {
            temp.SetSources(sourcesList);
        }
        

    }
}
