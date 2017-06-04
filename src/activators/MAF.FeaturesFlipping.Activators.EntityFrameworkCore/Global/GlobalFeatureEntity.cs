namespace MAF.FeaturesFlipping.Activators.EntityFrameworkCore.Global
{
    internal class GlobalFeatureEntity
    {
        public string Application { get; set; }
        public string Scope { get; set; }
        public string Feature { get; set; }
        public bool? IsActive { get; set; }
    }
}