using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RayCast : MonoBehaviour
{
    private const string CardTag = "Card";

    private List<GameObject> cardList;
    private Camera mainCamera;
    private GameObject cardA;
    private GameObject cardB;

    private bool isClick = true;

    private WaitForSeconds waitTime = new WaitForSeconds(1f);

    private void Start()
    {
        mainCamera = Camera.main;
        cardList = GetComponent<MemoryTable>().cardList;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isClick)
            RayCheck();
    }

    private void RayCheck()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.CompareTag(CardTag))
        {
            GameObject cardObject = hit.collider.gameObject;
            cardObject.transform.rotation = Quaternion.Euler(Vector3.zero);

            if (cardA == null)
            {
                cardA = cardObject;
            }
            else
            {
                if (cardA == cardObject) return;

                isClick = false;
                cardB = cardObject;
                StartCoroutine(IsCardNumberEqual());
            }
        }
    }

    private IEnumerator IsCardNumberEqual()
    {
        yield return waitTime;

        if (cardA.name == cardB.name)
        {
            cardList.Remove(cardA);
            cardList.Remove(cardB);
            Destroy(cardA);
            Destroy(cardB);
        }
        else
        {
            cardA.transform.rotation = cardB.transform.rotation = Quaternion.Euler(Vector3.forward * 180);
            cardA = cardB = null;
        }

        if (cardList.Count == 0)
            SceneManager.LoadScene(0);
        else
            isClick = true;
    }
}
