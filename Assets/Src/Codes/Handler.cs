using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler
{
    public static void InitialHadnler(InitialResponse res)
    {
        try
        {
            GameManager.instance.GameStart();
            GameManager.instance.player.UpdatePositionFromServer(res.x, res.y);
        }
        catch (Exception e)
        {
            Debug.LogError($"InitialHandler Error : {e}");
        }
    }
}
