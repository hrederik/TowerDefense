using UnityEngine;
using System.Collections.Generic;

namespace Monetization.Metrica
{
    public class MetricaFacade : MonoBehaviour
    {
        protected IYandexAppMetrica Metrica { get; private set; }
        protected Dictionary<string, object> Parameters { get; set; }

        private void Start()
        {
            Metrica = AppMetrica.Instance;
        }

        protected void Send(string eventName)
        {
            Metrica.ReportEvent(eventName, Parameters);
        }

        protected void SendHard(string eventName)
        {
            Send(eventName);
            Metrica.SendEventsBuffer();
        }
    }
}