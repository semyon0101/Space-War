using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;    
using UnityEngine;

public class PlayerNetworkSync : NetworkBehaviour
{
    public NetworkVariable<Vector3> position = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> rotation = new NetworkVariable<Quaternion>();

    public override void OnNetworkSpawn()
    {

        

        if (IsServer)
        {

            if (!GlobalVaribles.IsPLayerTeam1)
            {
                SpawnSpaceshipTeam1();
                GlobalVaribles.IsPLayerTeam1 = true;
            }
            else if (!GlobalVaribles.IsPLayerTeam2)
            {
                SpawnSpaceshipTeam2();
                GlobalVaribles.IsPLayerTeam2 = true;
            }
            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { OwnerClientId }
                }
            };
            SpawnSpaceshipForOwnerClientRpc(clientRpcParams);
        }

        //if (IsClient)
        //{
        //    Camera.main.gameObject.SetActive(false);    
        //}
        //if(IsServer)
        //{
        //    if (GlobalVaribles.PLayerTeam1 == null)
        //    {
        //        var go=Instantiate(GlobalVaribles.PrefabTeam1);
        //        go.transform.parent = transform;
        //        go.transform.position=GlobalVaribles.SpawnPointTeam1.transform.position;
        //        go.transform.rotation=GlobalVaribles.SpawnPointTeam1.transform.rotation;
        //    }
        //    else if(GlobalVaribles.PLayerTeam2 == null)
        //    {
        //        var go = Instantiate(GlobalVaribles.PrefabTeam2);
        //        go.transform.parent = transform;
        //        go.transform.position = GlobalVaribles.SpawnPointTeam2.transform.position;
        //        go.transform.rotation = GlobalVaribles.SpawnPointTeam2.transform.rotation;
        //    }
        //}

        //if (IsOwner)
        //{
        //    transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
        //    foreach (var item in Resources.FindObjectsOfTypeAll<GameObject>())
        //    {
        //        if (item.name == "UI")
        //            item.SetActive(true);
        //    }
        //}

    }
    [ClientRpc]
    public void SpawnSpaceshipForOwnerClientRpc(ClientRpcParams clientRpcParams = default)
    {
        //Camera.main.gameObject.SetActive(false);\
        //if(name== "Player(Clone)")
        //{
        //UI.SetActive(true);

        //}
        //transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);\
        
    }

    [ClientRpc]
    public void SpawnSpaceshipTeam1ClientRpc()
    {
        var go = Instantiate(GlobalVaribles.PrefabTeam1);
        go.transform.parent = transform;
        go.transform.position = new Vector3(100,100,0);
        go.transform.rotation = GlobalVaribles.SpawnPointTeam1.transform.rotation;
        UI.SetActive(true);
    }
    [ClientRpc]
    public void SpawnSpaceshipTeam2ClientRpc()
    {
        var go = Instantiate(GlobalVaribles.PrefabTeam2);
        go.transform.parent = transform;
        go.transform.position = GlobalVaribles.SpawnPointTeam2.transform.position;
        go.transform.rotation = GlobalVaribles.SpawnPointTeam2.transform.rotation;
    }

    public void SpawnSpaceshipTeam1()
    {
        //if (!IsHost)
        //{
        //    var go = Instantiate(GlobalVaribles.PrefabTeam1);
        //    go.transform.parent = transform;
        //    go.transform.position = GlobalVaribles.SpawnPointTeam1.transform.position;
        //    go.transform.rotation = GlobalVaribles.SpawnPointTeam1.transform.rotation;
        //}
        SpawnSpaceshipTeam1ClientRpc();

    }
    public void SpawnSpaceshipTeam2()
    {
        //if (!IsHost)
        //{
        //    var go = Instantiate(GlobalVaribles.PrefabTeam2);
        //    go.transform.parent = transform;
        //    go.transform.position = GlobalVaribles.SpawnPointTeam2.transform.position;
        //    go.transform.rotation = GlobalVaribles.SpawnPointTeam2.transform.rotation;
        //}
        SpawnSpaceshipTeam2ClientRpc();

    }

    //[ServerRpc]
    //public void SetServerRpc(Vector3 p, Quaternion r)
    //{
    //    position.Value = p;
    //    rotation.Value = r;

    //}

    //void Update()
    //{
    //    if (IsOwner)
    //    {
    //        SetServerRpc(transform.position, transform.rotation);
    //        transform.position = position.Value;
    //        transform.rotation = rotation.Value;
    //    }
    //    else
    //    {
    //        transform.position = position.Value;
    //        transform.rotation = rotation.Value;
    //    }

    //}


    //public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();


    //public override void OnNetworkSpawn()
    //{
    //    transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
    //    if (IsServer)
    //    {
    //        Position.Value = transform.position;

    //    }
    //    if (IsOwner)
    //    {
    //        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    //        gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = true;
    //    }
    //    GameObject.Find("Main Camera").GetComponent<Camera>().enabled = false;
    //    GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;

    //}

    //[ServerRpc]
    //public void SetServerRpc(Vector3 v)
    //{
    //    if (Vector3.Distance(Position.Value, v) > 1)
    //    {
    //        Position.Value = transform.position;
    //        TpClientRpc(Position.Value);

    //    }
    //    else
    //    {
    //        Position.Value = v;
    //    }

    //}
    //[ClientRpc]
    //public void TpClientRpc(Vector3 v)
    //{
    //    transform.position = v;
    //}
    //void Update()
    //{
    //    if (IsOwner)
    //    {
    //        SetServerRpc(transform.position);
    //    }
    //    else
    //    {
    //        transform.position = Position.Value;
    //    }
    //    if (IsServer)
    //    {
    //        if (Vector3.Distance(Position.Value, transform.position) > 1)
    //            Position.Value = transform.position;
    //    }
    //}
}