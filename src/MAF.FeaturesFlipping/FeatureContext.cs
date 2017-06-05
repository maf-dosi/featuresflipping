using System.Collections.Generic;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping
{
    internal class FeatureContext : IFeatureContext
    {
        private readonly Dictionary<string, object> _contextParts = new Dictionary<string, object>();

        public T GetPart<T>(string partName)
        {
            if (_contextParts.ContainsKey(partName))
            {
                var contextPart = _contextParts[partName];
                if (contextPart is T typedContextPart)
                {
                    return typedContextPart;
                }
            }
            return default(T);
        }

        public void SetPart<T>(T featureContextPart, string partName)
        {
            if (_contextParts.ContainsKey(partName))
            {
                _contextParts[partName] = featureContextPart;
            }
            else
            {
                _contextParts.Add(partName, featureContextPart);
            }
        }
    }
}