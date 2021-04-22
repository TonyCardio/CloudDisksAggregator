using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudDisksAggregator;
using CloudDisksAggregator.CloudDrives;

namespace CloudDisksAggregatorUI.UI.ViewEntity
{
    public class DriveViewInfo : IEquatable<DriveViewInfo>
    {
        public CloudDriveType DriveType { get; }
        public string Name { get; }

        public DriveViewInfo(CloudDriveType driveType, string name)
        {
            DriveType = driveType;
            Name = name;
        }

        public bool Equals(DriveViewInfo other) =>
             Name == other.Name && DriveType == other.DriveType;

        public override int GetHashCode() =>
             HashCode.Combine(Name.GetHashCode(0), DriveType.GetHashCode());
    }
}
