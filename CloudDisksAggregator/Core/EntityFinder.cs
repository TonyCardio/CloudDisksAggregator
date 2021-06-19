using System.Collections.Generic;
using System.Linq;

namespace CloudDisksAggregator.Core
{
    public static class EntityFinder
    {
        public static List<DriveEntityInfo> Search(string searchLine, IEnumerable<DriveEntityInfo> allEntity) 
            => allEntity.Where(entityInfo => entityInfo.Name.Contains(searchLine)).ToList();
    }
}