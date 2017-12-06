namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore
{
    interface IFeatureEntity
    {
        int FeatureId { get; set; }
        string Application { get; set; }
        string Scope { get; set; }
        string FeatureName { get; set; }
        bool? IsActive { get; set; }
    }
}