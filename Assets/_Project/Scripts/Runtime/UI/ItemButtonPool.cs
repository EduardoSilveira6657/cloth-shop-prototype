using UnityEngine;
using UnityEngine.Pool;

namespace _Project.Scripts.Runtime.UI
{
    public class ItemButtonPool : MonoBehaviour
    {
        [SerializeField] ItemButtonUI itemButtonPrefab;
    
        // Collection checks will throw errors if we try to release an item that is already in the pool.
        [SerializeField] bool collectionChecks = true;
        [SerializeField] int maxPoolSize = 20;

        IObjectPool<ItemButtonUI> m_Pool;

        public IObjectPool<ItemButtonUI> Pool
        {
            get
            {
                if (m_Pool == null)
                {
                    m_Pool = new ObjectPool<ItemButtonUI>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
                }
                return m_Pool;
            }
        }

        ItemButtonUI CreatePooledItem()
        {
            var itemButton = Instantiate(itemButtonPrefab,transform);
            itemButton.Dispose();
            return itemButton;
        }

        void OnReturnedToPool(ItemButtonUI itemButton)
        {
            itemButton.Dispose();
            itemButton.transform.SetParent(transform);
        }

        void OnTakeFromPool(ItemButtonUI itemButton)
        {
            itemButton.Dispose();
        }

        void OnDestroyPoolObject(ItemButtonUI itemButton)
        {
            itemButton.Dispose();
            Destroy(itemButton.gameObject);
        }
    }
}
