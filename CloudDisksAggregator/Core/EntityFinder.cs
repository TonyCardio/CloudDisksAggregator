using System.Collections.Generic;
using System.Linq;

namespace CloudDisksAggregator.Core
{
    public static class EntityFinder
    {
        // It is assumed that an efficient file search method will be implemented here,
        // or a library that does this will be added.
        //      Best wishes from the CloudDiskAggregator team.
        public static List<DriveEntityInfo> Search(string searchLine, IEnumerable<DriveEntityInfo> allEntity) 
            => allEntity.Where(entityInfo => entityInfo.Name.Contains(searchLine)).ToList();
    }
}