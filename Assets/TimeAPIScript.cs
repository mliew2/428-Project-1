using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class TimeAPIScript : MonoBehaviour
{
    public GameObject timeTextObject;
    string url = "http://worldtimeapi.org/api/timezone/Asia/Kuala_Lumpur";
    
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

                int hour = int.Parse(webRequest.downloadHandler.text.Substring(startTime+1, 2));
                string min = webRequest.downloadHandler.text.Substring(startTime+4, 2);

                string ending = hour >= 12 ? "pm" : "am";
                if(hour == 0) {
                    hour = 12;
                } else if (hour > 12) {
                    hour -= 12;
                }

                timeTextObject.GetComponent<TextMeshPro>().text = "" + hour.ToString() + ":" + min + " " + ending;
            }
        }
    }
}
