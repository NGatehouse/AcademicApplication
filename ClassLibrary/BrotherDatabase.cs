using System.Data;
using System.Collections.Generic;
using System;
using ClassLibrary;

namespace DataLoggerWeb.Database.ServiceModel
{
    public class BrotherDatabase
    {
        public BrotherDatabase(string ConnectionString = null)
        {
            string dbConnectionString = ConnectionString;
            if (dbConnectionString == null)
                //dbConnectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
           // db = new OrmLiteConnectionFactory(dbConnectionString, SqlServer2012Dialect.Provider).Open();
        }

        /*********************************************
         * insert a brother into the database
         * returns true for successfull insertion
         *********************************************/
        public bool InsertBrother(Brother brother)
        {            
            return db.Save(brother, references: true);
        }
        public int DeleteBrother(Brother brother)
        {
            return db.Delete(brother);
        }
        public bool InsertQuarter(Quarter quarter)
        {
            return db.Save(quarter, references: true);
        }

        //Add dependency checks
        public int DeleteQuarter(Quarter quarter)
        {
            return db.Delete(quarter);
        }
        public bool InsertClassSchedule(ClassSchedule schedule)
        {
            return db.Save(schedule, references: true);
        }
        public int DeleteClassSchedule(ClassSchedule schedule)
        {
            return db.Delete(schedule);
        }

        /******************************************
         * Convenience method
         * 
         * returns true if the name is unavailable
         ******************************************/
        public bool IsBrotherNameTaken(string FirstName, string LastName)
        {
            return false;
        }

        /******************************************
        * returns the brother associated to the ID
        ******************************************/
        public Brother RetrieveBrother(int brotherId)
        {
            List<Brother> q = db.Select<Brother>(x => x.BadgeNumber == brotherId);

            if (q.Count != 1)
            {
                return null;
            }

            Brother brother = q[0];
            return brother;
        }

        /******************************************
        * returns the Quarter associated to the ID
        ******************************************/
        public Quarter RetrieveSpecificQuarter(int QuarterID)
        {
            List<Quarter> q = db.Select<Quarter>(x => x.QuarterID == QuarterID);

            if (q.Count != 1)
            {
                return null;
            }

            return q[0];
        }

        /*********************************************
        * returns all the Quarters associated to the ID
        *********************************************/
        public List<Quarter> RetrieveAllGauges(int brotherId)
        {
            List<Quarter> q = db.Select<Quarter>(x => x.UserId == userId);

            if (q.Count < 1)
            {
                Console.Error.WriteLine($"Unable to find any Quarters associated with the Badge Number: {brotherId}");
                return null;
            }

            return q;
        }

        /******************************************
        * returns the Class Schedule associated to the ID
        ******************************************/
        public ClassSchedule RetrieveSpecificSchedule(int scheduleId)
        {
            List<ClassSchedule> p = db.Select<ClassSchedule>(x => x.ScheduleId == scheduleId);

            if (p.Count != 1)
            {
                Console.Error.WriteLine($"Found {p.Count.ToString()} Schedules with the ID {scheduleId}");
                return null;
            }

            return p[0];
        }

        /*********************************************
        * returns all the Schedules associated to the ID
        *********************************************/
        public List<ClassSchedule> RetrieveAllSchedules(int quarterId)
        {
            List<ClassSchedule> q = db.Select<ClassSchedule>(x => x.QuarterID == quarterId);

            if (q.Count < 1)
            {
                Console.Error.WriteLine($"Unable to find any Class Schedule associated with the QuarterID: {quarterId}");
                return null;
            }

            return q;
        }
    }
}
