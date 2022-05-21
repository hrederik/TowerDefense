using UnityEngine;

namespace Monetization.Metrica
{
    public class MetricaFacadesHub : MonoBehaviour
    {
        [SerializeField] private IngameFacade _ingameFacade;

        public static MetricaFacadesHub Instance { get; private set; }
        public IngameFacade Ingame => _ingameFacade;

        private void Start()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}