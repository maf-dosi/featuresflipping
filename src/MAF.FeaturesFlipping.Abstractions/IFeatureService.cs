﻿using System.Threading.Tasks;

namespace MAF.FeaturesFlipping
{
    public interface IFeatureService
    {
        Task<bool> IsFeatureActiveAsync(IFeatureName featureName);
    }
}