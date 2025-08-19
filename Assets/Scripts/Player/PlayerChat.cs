using System.Collections;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerChat : NetworkBehaviour
{
    [SerializeField] Player player;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsOwner) return;

        StartCoroutine(OnStart());
    }

    private IEnumerator OnStart()
    {
        yield return new WaitForSeconds(2);
        ChatUI.Instance.OnMessageSubmit.AddListener(SendChatMessage);
        yield return null;
    }

    public void SendChatMessage()
    {
        SendMessageServerRpc(ChatUI.Instance.chatInput.text);
    }
    [ServerRpc]

    public void SendMessageServerRpc(string text)
    {

        RecieveMessageClientRpc(player.PlayerName.Value.ToString(), text);
    }

    [ClientRpc]

    public void RecieveMessageClientRpc(string name, string text)
    {
        ChatUI.Instance.CreateChatMessage(name,text);
    }
}