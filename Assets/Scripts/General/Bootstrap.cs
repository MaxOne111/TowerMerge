using System;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
   [SerializeField] private SaveLoad _Save_Load;
   [SerializeField] private PlayerRaycast _Player_Raycast;
   [SerializeField] private EnemyFactory[] _Enemy_Factories;
   [SerializeField] private TowerFactory _Tower_Factory;
   [SerializeField] private EnemyDetection _Enemy_Detection;
   [SerializeField] private TargetingSystem _Targeting_System;
   
   private void Awake() => Init();

   private void Start() => StartWork();

   private void Init()
   {
      _Save_Load.GlobalInit();
      _Player_Raycast.GlobalInit();
      for (int i = 0; i < _Enemy_Factories.Length; i++)
         _Enemy_Factories[i].GlobalInit();
      
      _Tower_Factory.GlobalInit();
      _Enemy_Detection.GlobalInit();
      _Targeting_System.GlobalInit();
   }

   private void StartWork()
   {
      for (int i = 0; i < _Enemy_Factories.Length; i++)
         _Enemy_Factories[i].StartCreating();
      
      _Tower_Factory.StartCreating();
   }
}