namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    public class GlobalFeatureEntity: IFeatureEntity
    {
        public int FeatureId { get; set; }
        public string Application { get; set; }
        public string Scope { get; set; }
        public string Feature { get; set; }
        public bool? IsActive { get; set; }
    }
}