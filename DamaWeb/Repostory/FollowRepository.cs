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
                     commander.SetParametr("TecherId",teacherId),                    
                };
                return commander.Reader<UIStudentTeacher>("GetStudentsByTecherId", commandType: System.Data.CommandType.StoredProcedure,
                    parameters: sqlparams).Item1;
            }
        }

        public List<UIStudentTeacher> WaitApproveFollow (int teacherId)
        {
            using (var commander = DBContext.CreateCommander())
            {
                var sqlparams = new List<System.Data.Common.DbParameter> {
                     commander.SetParametr("TeacherId",teacherId),
                };
                return commander.Reader<UIStudentTeacher>("WaitApproveFollow", 
                    commandType: System.Data.CommandType.StoredProcedure,
                    parameters: sqlparams).Item1;
            }
        }

        public StudentTeacher GetStudentTeacher(int teacherId,int studentId)
        {
            using (var commander = DBContext.CreateCommander())
            {
                var sqlparams = new List<System.Data.Common.DbParameter> {
                     commander.SetParametr("TecherId",teacherId),
                     commander.SetParametr("StudentId",studentId),
                };
                return commander.ReaderFist<StudentTeacher>("GetStudentTeacher", commandType: System.Data.CommandType.StoredProcedure,
                    parameters: sqlparams).Item1;
            }
        }

        public List<UICategoryINGroupe> SelectTecherGroupeCategory(int teacherId, int groupId)
        {
            using (var commander = DBContext.CreateCommander())
            {
                var sqlparams = new List<System.Data.Common.DbParameter> {
                     commander.SetParametr("teacherId",teacherId),
                     commander.SetParametr("groupId",groupId),
                };
                return commander.Reader<UICategoryINGroupe>("SelectTecherGroupeCategory", commandType: System.Data.CommandType.StoredProcedure,
                    parameters: sqlparams).Item1;
            }
        }

        public bool DeleteCategoryGroup(int categoryId, int groupId)
        {
            using (var commander = DBContext.CreateCommander())
            {
                var sqlparams = new List<System.Data.Common.DbParameter> {
                     commander.SetParametr("CategoryId",categoryId),
                     commander.SetParametr("GroupId",groupId),
                };
                return commander.NonQuery("DeleteCategoryGroup", commandType: System.Data.CommandType.StoredProcedure,
                    parameters: sqlparams);
            }
        }
    }
}
