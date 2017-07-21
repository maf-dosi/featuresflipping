namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Specific
{
    public class SpecificFeatureEntity<TOtherColumn> : IFeatureEntity
    {
        public int FeatureId { get; set; }
        public string Application { get; set; }
        public string Scope { get; set; }
        public string Feature { get; set; }
        public bool? IsActive { get; set; }
        public TOtherColumn OtherColumn { get; set; }
    }
}