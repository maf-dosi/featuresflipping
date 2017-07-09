namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore
{
    interface IFeatureEntity
    {
        string Application { get; set; }
        string Scope { get; set; }
        string Feature { get; set; }
        bool? IsActive { get; set; }
    }
}