namespace CloudDisksAggregator.Core
{
    public class UserAccount
    {
        public string Name { get; }
        public ICloudDriveEngine DriveEngine { get; }

        public UserAccount(string name, ICloudDriveEngine driveEngine)
        {
            Name = name;
            DriveEngine = driveEngine;
        }
    }
}