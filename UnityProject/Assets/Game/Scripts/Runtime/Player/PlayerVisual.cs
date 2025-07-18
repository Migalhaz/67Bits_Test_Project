using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] List<Material> m_materials;
        [SerializeField] List<GameObject> m_hairs;
        [SerializeField] List<GameObject> m_accessories;

        [SerializeField] List<Renderer> m_renderers;

        private void OnEnable()
        {
            PlayerScoreObserverManager.m_OnBodyPriceLevelUp += RandomizeVisual;
            PlayerScoreObserverManager.m_OnStackLevelUp += RandomizeVisual;
        }

        private void OnDisable()
        {
            PlayerScoreObserverManager.m_OnBodyPriceLevelUp -= RandomizeVisual;
            PlayerScoreObserverManager.m_OnStackLevelUp -= RandomizeVisual;
        }
        void RandomizeVisual(int value) => RandomizeVisual();
        void RandomizeVisual()
        {
            RandomizeMaterial();
            RandomizeHair();
            RandomizeAcessories();
        }

        void RandomizeMaterial()
        {
            if (m_materials == null || m_materials.Count <= 0) return;

            foreach (Renderer obj in m_renderers)
            {
                Material currentMaterial = m_materials.GetRandom();
                if (!obj || currentMaterial == null) continue;
                obj.material = currentMaterial;
            }
        }

        void RandomizeHair()
        {
            if (m_hairs == null || m_hairs.Count <= 0) return;
            GameObject currentHair = m_hairs.GetRandom();
            foreach (GameObject hair in m_hairs)
            {
                if (!hair) continue;
                hair.SetActive(currentHair == hair);
            }
        }

        void RandomizeAcessories()
        {
            if (m_accessories == null || m_accessories.Count <= 0) return;
            foreach (GameObject accessory in m_accessories)
            {
                if (!accessory) continue;
                accessory.SetActive(Random.value < 0.5);
            }
        }
    }
}
