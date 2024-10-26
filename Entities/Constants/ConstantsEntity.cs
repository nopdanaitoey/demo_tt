using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_tt.Entities.Constants
{
    public static class ConstantsEntity
    {
        public static bool ACTIVE = true;
        public static bool INACTIVE = false;

        public static bool DELETED = true;
        public static bool NOT_DELETED = false;

        public static string REDIS_MASTER_RESOURCE = "MasterResource";
        public static string REDIS_ASSIGNMENTS = "Assignment";
        public static string REDIS_TRUCKS = "Trucks";
        public static string REDIS_AFFECTED_AREAS = "AffectedAreas";

        public static int MAX_URGENCY_LEVEL = 5;
        public static int MAX_TIME_REDIS_MIN = 30;

    }


}