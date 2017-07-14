using System;

namespace MAF.FeaturesFlipping
{
    public sealed class FeaturesFlippingOptions
    {
        public FeaturesFlippingOptions()
        {
            ReadFeatureLockTimeout = TimeSpan.FromMinutes(1);
            WriteFeatureLockTimeout = TimeSpan.FromMinutes(1);
        }

        public TimeSpan ReadFeatureLockTimeout { get; set; }
        public TimeSpan WriteFeatureLockTimeout { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                // ReSharper disable NonReadonlyMemberInGetHashCode
                hash = hash * 23 + ReadFeatureLockTimeout.GetHashCode();
                hash = hash * 23 + WriteFeatureLockTimeout.GetHashCode();
                // ReSharper restore NonReadonlyMemberInGetHashCode
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is FeaturesFlippingOptions options &&
                ReadFeatureLockTimeout.Equals(options.ReadFeatureLockTimeout) &&
                WriteFeatureLockTimeout.Equals(options.WriteFeatureLockTimeout);
        }
    }
}