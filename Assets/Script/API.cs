using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using TMPro;

public class API : MonoBehaviour
{
    private string apiKey = "sk-dd08059d6f4b4df7a3b78c49aef3744c";
    private string apiUrl = "https://api.deepseek.com/chat/completions";
    public string aiContent;
    public string text;
    public TextMeshProUGUI userText;
    public TextMeshProUGUI aiText;
    public DetectGua detectGua;
    public CoinController coinController;

    public void Ask()
    {
        if (userText.text == "") return; // 如果用户问题为空则不发送请求
        string dongYaoStr = "，没有动爻";
        if (coinController.dongYao.Count > 0)
        {
            dongYaoStr = "，动爻是" + string.Join("、", coinController.dongYao) + "爻";
        }
        if (detectGua.benGua == "") return; // 如果本卦为空则不发送请求
        text = "我现在摇出来的六爻卦象是: 本卦:" + detectGua.benGua + ",变卦:" + detectGua.bianGua + dongYaoStr + "，我的问题是" + userText.text + "字数控制在50字以内, 用英文回答，不要特殊字符";
        SendMessageToAPI(text, OnAIResponse);
    }

    void Update()
    {
        aiText.text = aiContent;
    }

    public void SendMessageToAPI(string message, UnityAction<string> callback)
    {
        StartCoroutine(PostRequest(message, callback));
    }

    IEnumerator PostRequest(string message, UnityAction<string> callback)
    {
        var requestBody = new
        {
            model = "deepseek-chat",
            messages = new[]
            {
                new { role = "user", content = message}
            }
        };

        string jsonBody = JsonConvert.SerializeObject(requestBody);        
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;
            JObject jsonObj = JObject.Parse(responseJson);
            aiContent = jsonObj["choices"][0]["message"]["content"].ToString();
            callback?.Invoke(aiContent);
        }
        else
        {
            Debug.LogError("请求失败: " + request.error);
            callback?.Invoke(null);
        }
    }
    void OnAIResponse(string content)
    {
        Debug.Log("回调获取的AI回复: " + content);
    }
}
