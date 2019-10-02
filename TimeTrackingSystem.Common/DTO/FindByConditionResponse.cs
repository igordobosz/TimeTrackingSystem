using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackingSystem.Common.DTO
{
    public class FindByConditionResponse<VM>
    {
        public List<VM> ItemList;
        public int CollectionSize;
    }
}
