using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject indicator;
    public Transform spawnTrm;
    public GameObject target;
    public WarningMark warningMark;

    private Renderer rd;

    private void Start()
    {
        rd = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (EnemySpawner.Instance.isIndicateMark)
        {
            Debug.Log(indicator.activeSelf);
            if (indicator.activeSelf == false)
            {
                indicator.SetActive(true);
                //warningMark.Init();
            }

            Vector2 direction = target.transform.position - spawnTrm.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction);

            if (ray.collider != null)
            {
                indicator.transform.position = ray.point;
            }
        }
        else
        {
            if(indicator.activeSelf == true)
            {
                Debug.Log("²°À½");
                indicator.SetActive(false);
            }
        }
    }

    public void SetTrm(Transform trm)
    {
        spawnTrm = trm;
    }
}