using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Bootstrap : MonoBehaviour
{
   [SerializeField] private SaveLoad          saveLoad = null;
   
   [SerializeField] private PlayerRaycast     playerRaycast = null;
   
   [SerializeField] private EnemyFactory[]    enemyFactories = null;
   
   [SerializeField] private TowerFactory      towerFactory = null;
   
   [SerializeField] private EnemyDetection    enemyDetection = null;
   
   [SerializeField] private TargetingSystem   targetingSystem = null;
   
   private void Awake() => Init();

   private void Start() => StartWork();

   private void Init()
   {
      saveLoad.GlobalInit();
      playerRaycast.GlobalInit();

      enemyDetection.GlobalInit();
      
      targetingSystem.GlobalInit();
      
      for (int i = 0; i < enemyFactories.Length; i++)
      {
         enemyFactories[i].GlobalInit();
      }
      
      towerFactory.GlobalInit();

   }

   private void StartWork()
   {
      for (int i = 0; i < enemyFactories.Length; i++)
      {
         enemyFactories[i].StartCreating();
      }

      towerFactory.StartCreating();
   }
}