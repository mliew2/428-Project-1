using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class TimeAPIScript2 : MonoBehaviour
{
    public GameObject timeTextObject;
    string url = "http://worldtimeapi.org/api/timezone/Europe/London";
    
    void Start()
    {
        InvokeRepeating("UpdateTime", 2f, 10f);   
    }

   
    void UpdateTime()
    {
        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();


            if (webRequest.result ==  UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);

            	int startDateTime = webRequest.downloadHandler.text.IndexOf("datetime", 0);
                int startTime = webRequest.downloadHandler.text.IndexOf("T", startDateTime);
                int endTime = webRequest.downloadHandler.text.IndexOf(".", startTime);

                string time = webRequest.downloadHandler.text.Substring(startTime+1, 5);

                timeTextObject.GetComponent<TextMeshPro>().text = time;
            }
        }
    }
}
