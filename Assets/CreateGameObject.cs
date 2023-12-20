using System.Collections;
using UnityEngine;

namespace Assets
{
    public class CreateGameObject : ObjectPool<GameObject>
    {
        [SerializeField] private GameObject displayedObject;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                GameObject gameObject = SpwanGameObject();

                StartCoroutine(ReturnGameObjectToPool(gameObject));
            }
        }

        protected override GameObject CreateItem()
        {
            GameObject gameObject = Instantiate(displayedObject);
            return gameObject;
        }

        public GameObject SpwanGameObject()
        {
            GameObject gameObject = GetItemFromPool();
            gameObject.SetActive(true);
            return gameObject;
        } 

        private IEnumerator ReturnGameObjectToPool(GameObject gameObject)
        {
            yield return new WaitForSeconds(1f);
            ReturnItemToPool(gameObject);
            gameObject.SetActive(false);
        }
    }
}