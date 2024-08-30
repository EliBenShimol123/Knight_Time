using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{

    public List<GameObject> targets = new List<GameObject>();

    public void stopTarget()
    {
        targets.Clear();
        this.gameObject.SetActive(false);
    }

    public void changeTarget(List<GameObject> newTargets)
    {
        targets = newTargets;
    }
    // Update is called once per frame
    void Update()
    {
        GameObject mainTarget = null;
        float distance = -1;
        foreach (GameObject target in targets)
        {
            if (target.activeSelf)
            {
                float tmpDistance = Vector2.Distance(MainHero.instance.transform.position, target.transform.position);
                if (distance > tmpDistance || distance == -1)
                {
                    distance = tmpDistance;
                    mainTarget = target;
                }
            }
        }
        if (mainTarget != null)
        {
            var dir = mainTarget.transform.position - this.gameObject.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
