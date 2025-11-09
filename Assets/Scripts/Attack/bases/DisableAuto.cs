using System.Collections;
using UnityEngine;

public class DisableAuto : MonoBehaviour
{
    [SerializeField]
    float disableTime = .5f;
    private void OnEnable()
    {
        StartCoroutine(DelayDisable());
    }
    
    IEnumerator DelayDisable()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }
}
