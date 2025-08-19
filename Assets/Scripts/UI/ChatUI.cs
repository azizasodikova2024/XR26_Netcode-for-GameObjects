using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ChatUI : MonoBehaviour
{
    public static ChatUI Instance { get; private set; }

     public TMP_InputField chatInput;
     public GameObject chatMsgPrefab;
     public Transform content;

    public UnityEvent OnMessageSubmit;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        chatInput.onSubmit.AddListener(t => OnMessageSubmit.Invoke());
    }

    public void CreateChatMessage(string name, string msg)
    {
        var chatMsgObject = Instantiate(chatMsgPrefab, content,false);
        chatMsgObject.GetComponent<TMP_Text>().text = 
            $"[{name}] : {msg}";
    }
}