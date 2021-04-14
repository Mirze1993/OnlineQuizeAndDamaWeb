using MicroORM;
using Model.Models;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Repostory
{
    public class FollowRepository:CRUD<StudentTeacher>
    {
        public int Follow(int subjectId,int studentId,int teacherId)
        {
            using (var commander = DBContext.CreateCommander())
            {
                var outParam = commander.SetOutputParametr();
                var sqlparams = new List<System.Data.Common.DbParameter> {
                     commander.SetParametr("SubjectId",subjectId),
                    commander.SetParametr("StudentId",studentId),
                    commander.SetParametr("TeacherId",teacherId),
                    outParam
                };

                var b = commander.NonQuery("Follow", commandType: System.Data.CommandType.StoredProcedure,
                    parameters: sqlparams);
                if (outParam.Value == null) return 0;
                return (int)outParam.Value;
            }
        }

        public List<UIStudentTeacher> GetStudentsByTecherId(int teacherId)
        {
            using (var commander = DBContext.CreateCommander())
            {               
                var sqlparams = new List<System.Data.Common.DbParameter> {
                     commander.SetParametr("@TecherId",teacherId),                    
                };
                return commander.Reader<UIStudentTeacher>("Follow", commandType: System.Data.CommandType.StoredProcedure,
                    parameters: sqlparams).Item1;
            }
        }
    }
}
