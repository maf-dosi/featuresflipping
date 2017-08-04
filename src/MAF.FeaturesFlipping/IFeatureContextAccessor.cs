using System;
using System.Threading.Tasks;
using MAF.FeaturesFlipping.Extensibility.Activators;

namespace MAF.FeaturesFlipping
{
    public interface IFeatureContextAccessor : IDisposable
    {
        Task<IFeatureContext> GetCurrentFeatureContextAsync();
    }
}