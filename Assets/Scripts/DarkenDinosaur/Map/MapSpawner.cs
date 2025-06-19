using System;
using System.Collections.Generic;
using System.Linq;
using DarkenDinosaur.ResourcesManagementSystem;
using UnityEngine;

namespace DarkenDinosaur.Map
{
    [RequireComponent(typeof(TemplatesLoader))]
    public class MapSpawner : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TemplatesLoader templatesLoader;
        
        [Header("Settings")] 
        [Tooltip("Map parent transform.")]
        [SerializeField] private Transform templatesParentTransform;
        
        [Tooltip("Templates count on scene in one moment.")]
        [SerializeField] private int templatesPoolSize;

        [Tooltip("Template size.")]
        [SerializeField] private Vector3 templateSize;

        [Tooltip("Templates on scene.")]
        [SerializeField] private List<GameObject> spawnedTemplates;

        [Tooltip("Items on scene")]
        [SerializeField] private GameObject itemPrefab;

        [Tooltip("BackGroound")]
        [SerializeField] private GameObject BackGroundPrefab;
        [SerializeField] private Vector3 backGroundSize;
        [SerializeField] private List<GameObject> spawnedBackground;
        


        private void Awake()
        {
            if (this.templatesLoader == null) this.templatesLoader = GetComponent<TemplatesLoader>();
        }

        private void Update()
        {
            if (this.spawnedTemplates.Count < this.templatesPoolSize)
            {
                SpawnTemplate();
                SpawnItem();
            }
            if (this.spawnedBackground.Count < this.templatesPoolSize)
            {
                //SpawnBackGround();
            }
        }

        /// <summary>
        /// Spawn new template on scene.
        /// </summary>
        private void SpawnTemplate()
        {
            GameObject template = this.templatesLoader.GetRandomTemplate();
            GameObject spawnedTemplate = Instantiate(template, this.templatesParentTransform);
            
            GameObject lastSpawnedTemplate = this.spawnedTemplates.Last();
            Vector3 lastSpawnedTemplatePosition = lastSpawnedTemplate.transform.localPosition;
            Vector3 templatePosition = lastSpawnedTemplatePosition + this.templateSize;

            spawnedTemplate.transform.localPosition = templatePosition;
            this.spawnedTemplates.Add(spawnedTemplate);
        }

        private void SpawnItem()
        {
            GameObject lastSpawnedTemplate = this.spawnedTemplates.Last();
            Vector3 lastSpawnedTemplatePosition = lastSpawnedTemplate.transform.localPosition;
            Vector3 templatePosition = lastSpawnedTemplatePosition + this.templateSize;
            templatePosition.y += 0.8f;

            GameObject Item = Instantiate(itemPrefab, this.templatesParentTransform);
            Item.transform.localPosition = templatePosition;
        }

        private void SpawnBackGround()
        {
            GameObject spawnedBackground = Instantiate(BackGroundPrefab, this.templatesParentTransform);

            GameObject lastSpawnedBackground = this.spawnedBackground.Last();

            Vector3 lastSpawnedBackgroundPosition = lastSpawnedBackground.transform.localPosition;
            Vector3 templatePosition = lastSpawnedBackgroundPosition + this.backGroundSize;

            spawnedBackground.transform.localPosition = templatePosition;
            this.spawnedBackground.Add(spawnedBackground);
        }

        public void DeleteObject(GameObject destroingObject)
        {
            this.spawnedBackground.Remove(destroingObject);
            Destroy(destroingObject);
        }

        /// <summary>
        /// Destroy template from scene.
        /// </summary>
        /// <param name="template">Template to destroying.</param>
        public void DeleteTemplate(GameObject template)
        {
            this.spawnedTemplates.Remove(template);
            Destroy(template);
        }
    }
}