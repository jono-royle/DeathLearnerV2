using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestScripts
{

    //A simple example unit test, in a real production build would have full coverage of tests
    [UnityTest]
    public IEnumerator TestPlayerCollisionWithEnemy()
    {
        GameObject playerObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Player/SwordBoy"));
        GameObject enemyObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Enemies/Goblin1_0"));
        var player = playerObject.GetComponent<SwordBoy>();
        var enemy = enemyObject.GetComponent<LeftRightGoblin>();
        var maxPlayerHealth = player.PlayerHealth;
        player.transform.position = enemy.transform.position;
        yield return new WaitForSeconds(0.1f);
        Assert.IsTrue(player.PlayerHealth == maxPlayerHealth - 2);
    }
}
