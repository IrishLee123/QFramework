using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkDesign.Example;
using FrameworkDesign;

public class Enemy : MonoBehaviour, IController
{
    IArchitecture IBelongToArchitecture.GetArchitecture()
    {
        return PointGame.Interface;
    }

    private void OnMouseDown()
    {
        // destroy the enemy
        gameObject.SetActive(false);

        // do kill enemy command
        this.SendCommand<KillEnemyCommand>();
    }
}
